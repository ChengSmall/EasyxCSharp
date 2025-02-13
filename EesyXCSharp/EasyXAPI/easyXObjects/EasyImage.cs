using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Cheng.EasyX.CPP;
using Cheng.EasyX.Exceptions;
using System.IO;

namespace Cheng.EasyX.DataStructure
{


    /// <summary>
    /// EasyX图形库的图片结构
    /// </summary>
    public unsafe sealed class EasyImage : BaseGraphics
    {

        #region 构造
        /// <summary>
        /// 实例化一个EasyX图片缓冲区
        /// </summary>
        /// <param name="width">长度</param>
        /// <param name="height">高度</param>
        public EasyImage(ushort width, ushort height)
        {
            //if (width < 0 || height < 0) throw new ArgumentOutOfRangeException();

            //if ((long)width * height >= (1024L * 1024)) throw new InsufficientMemoryException();

            f_init(width, height);
        }
        /// <summary>
        /// 实例化一个EasyX图片缓冲区
        /// </summary>
        public EasyImage() : this(0, 0)
        {
        }
        /// <summary>
        /// 实例化一个EasyX图片缓冲区
        /// </summary>
        /// <param name="image">初始化时的拷贝对象</param>
        /// <exception cref="ArgumentNullException">参数为null</exception>
        /// <exception cref="ObjectDisposedException">拷贝对象已被释放</exception>
        public EasyImage(EasyImage image)
        {
            if (image is null) throw new ArgumentNullException();
            if (image.IsDispose) throw new ObjectDisposedException("image");
            f_init(image);
        }

        private void f_init(int width, int height)
        {

            p_imagePtr = EasyX_API.IMAGE_New_1(width, height);
        }
        private void f_init(EasyImage copy)
        {
            if (copy.IsDispose) throw new ObjectDisposedException(nameof(EasyImage));

            p_imagePtr = EasyX_API.IMAGE_New_2(copy.p_imagePtr);
        }

        #endregion

        #region 释放

        /// <summary>
        /// 使用对象终结器让对象在释放对象之前回收非托管内存
        /// </summary>
        ~EasyImage()
        {
            Dispose(false);
        }

        protected override void ReleaseUnmanaged()
        {
            p_disposeEvent?.Invoke(this);
            EasyX_API.IMAGE_Free(p_imagePtr);
            p_imagePtr = null;
        }

        /// <summary>
        /// 在资源被释放前发生的事件；参数表示引发的实例
        /// </summary>
        public event Action<EasyImage> DisposeEvent
        {
            add
            {
                if (p_disposeEvent is null) p_disposeEvent += value;
                else
                {
                    lock (p_disposeEvent) p_disposeEvent += value;
                }

            }
            remove
            {
                if (p_disposeEvent is null) return;
                else
                {
                    lock (p_disposeEvent) p_disposeEvent -= value;
                }
            }
        }

        #endregion

        #region 参数
        internal void* p_imagePtr;
        private Action<EasyImage> p_disposeEvent;
        #endregion

        #region 成员

        /// <summary>
        /// 获取图片高度
        /// </summary>
        public override int Height
        {
            get
            {
                if (IsDispose) throw new ObjectDisposedException(nameof(EasyImage));
                return EasyX_API.IMAGE_getheight(p_imagePtr);
            }
        }

        /// <summary>
        /// 获取图片长度
        /// </summary>
        public override int Width
        {
            get
            {
                if (IsDispose) throw new ObjectDisposedException(nameof(EasyImage));
                return EasyX_API.IMAGE_getwidth(p_imagePtr);
            }
        }

        /// <summary>
        /// 重新设置设置图片大小
        /// </summary>
        /// <param name="width">长</param>
        /// <param name="height">高</param>
        /// <exception cref="ObjectDisposedException">对象已释放</exception>
        public override void Resize(int width, int height)
        {
            if (IsDispose) throw new ObjectDisposedException(nameof(EasyImage));
            EasyX_API.IMAGE_Resize(p_imagePtr, width, height);
        }

        /// <summary>
        /// 将该图像结构和缓存拷贝指定的实例
        /// </summary>
        /// <remarks>此方法仅拷贝源图像的内容，不拷贝源图像的绘图环境；等价于c++ IMAGE对象的赋值运算符重载</remarks>
        /// <param name="toImage">要拷贝到的实例</param>
        /// <exception cref="ObjectDisposedException">对象已释放</exception>
        public void CopyToImage(EasyImage toImage)
        {
            if (toImage is null) throw new ArgumentNullException();
            if(toImage.IsDispose || IsDispose) throw new ObjectDisposedException(nameof(EasyImage));

            EasyX_API.IMAGE_operator_assignment(toImage.p_imagePtr, p_imagePtr);
        }

        /// <summary>
        /// 将指定图像非托管指针内存拷贝到此实例
        /// </summary>
        /// <param name="imagePtr"></param>
        internal void f_copyToPtr(void* imagePtr)
        {
            EasyX_API.IMAGE_operator_assignment(p_imagePtr, imagePtr);
        }

        protected override RGBColor getPixelColor(int x, int y)
        {
            byte* ptr;
            //获取首地址
            ptr = (byte*)EasyX_API.GetImageBuffer_(p_imagePtr);
            int width = EasyX_API.IMAGE_getwidth(p_imagePtr);
            //int height = EasyX_API.IMAGE_getheight(p_imagePtr);


            ptr += (((y * width) + x) * 4);

            //byte* bptr = (byte*)ptr;
            return new RGBColor(ptr[2], ptr[1], ptr[0]);
        }

        public override bool CanSetPixedColor => true;
        protected override void setPixedColor(int x, int y, RGBColor color)
        {
            byte* ptr;

            //获取首地址
            ptr = (byte*)EasyX_API.GetImageBuffer_(p_imagePtr);
            int width = EasyX_API.IMAGE_getwidth(p_imagePtr);
            //int height = EasyX_API.IMAGE_getheight(p_imagePtr);

            ptr += (((y * width) + x) * 4);

            byte r, g, b;
            color.GetRGB(out r, out g, out b);
            ptr[0] = b;
            ptr[1] = g;
            ptr[2] = r;
            ptr[3] = 0;
        }

        /// <summary>
        /// 获取图像上指定点的像素
        /// </summary>
        /// <param name="point">图像的坐标</param>
        /// <returns>指定点的像素颜色</returns>
        /// <exception cref="ObjectDisposedException">资源已释放</exception>
        /// <exception cref="ArgumentOutOfRangeException">坐标索引超出图像缓冲区范围</exception>
        public RGBColor GetPixelColor(EPoint point)
        {
            if (IsDispose) throw new ObjectDisposedException(nameof(EasyImage));

            int height = EasyX_API.IMAGE_getheight(p_imagePtr);
            int width = EasyX_API.IMAGE_getwidth(p_imagePtr);
            if (point.x < 0 || point.x >= width || point.y < 0 || point.y > height) throw new ArgumentOutOfRangeException();

            //获取首地址
            byte* ptr = (byte*)EasyX_API.GetImageBuffer_(p_imagePtr);
            ptr += (((point.y * width) + point.x) * 4);

            //byte* bptr = (byte*)ptr;
            return new RGBColor(ptr[2], ptr[1], ptr[0]);
        }
        /// <summary>
        /// 设置图像上指定点的像素
        /// </summary>
        /// <param name="point"></param>
        /// <param name="color"></param>
        public void SetPixelColor(EPoint point, RGBColor color)
        {
            if (IsDispose) throw new ObjectDisposedException(nameof(EasyImage));

            int height = EasyX_API.IMAGE_getheight(p_imagePtr);
            int width = EasyX_API.IMAGE_getwidth(p_imagePtr);
            if (point.x < 0 || point.x >= width || point.y < 0 || point.y > height) throw new ArgumentOutOfRangeException();

            //获取首地址
            byte* ptr = (byte*)EasyX_API.GetImageBuffer_(p_imagePtr);
            ptr += (((point.y * width) + point.x) * 4);

            //byte* bptr = (byte*)ptr;
            //return new RGBColor(ptr[2], ptr[1], ptr[0]);

            ptr[2] = color.R;
            ptr[1] = color.G;
            ptr[0] = color.B;
        }

        public override bool CanTransparency => false;

        public override bool CanResize => true;

        public override bool CanGetPixedColor => true;
        public override bool CanScaleDraw => false;
        
        protected override void onlyGetSize(out int width, out int height)
        {
            height = EasyX_API.IMAGE_getheight(p_imagePtr);
            width = EasyX_API.IMAGE_getwidth(p_imagePtr);
        }

        #endregion

        #region 操作

        public override void PutDraw(EPoint point)
        {
            if (IsDispose) throw new ObjectDisposedException(nameof(EasyImage));
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            EasyX_API.putimage_1(point.x, point.y, p_imagePtr);
        }
        /// <summary>
        /// 加载图像到实例
        /// </summary>
        /// <param name="filePath">加载的图像</param>
        /// <param name="width">要加载的长度</param>
        /// <param name="height">要加载的高度</param>
        /// <param name="bResize">是否调整大小以拉伸图片</param>
        /// <exception cref="ObjectDisposedException">对象已释放</exception>
        /// <exception cref="ArgumentException">图像路径不正确</exception>
        public void Load(string filePath, int width, int height, bool bResize)
        {
            if (IsDispose) throw new ObjectDisposedException(nameof(EasyImage));
            if (File.Exists(filePath))
                EasyX_API.loadimage_1(p_imagePtr, filePath, width, height, bResize);
            else throw new ArgumentException("图像路径不正确");
        }
        /// <summary>
        /// 绘制图像
        /// </summary>
        /// <param name="dstX">绘制的x坐标</param>
        /// <param name="dstY">绘制的y坐标</param>
        /// <param name="dstWidth"></param>
        /// <param name="dstHeight"></param>
        /// <param name="srcX"></param>
        /// <param name="srcY"></param>
        /// <exception cref="ObjectDisposedException">对象已释放</exception>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        public void PutDraw(int dstX, int dstY, int dstWidth, int dstHeight, int srcX, int srcY)
        {
            if (IsDispose) throw new ObjectDisposedException(nameof(EasyImage));
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            EasyX_API.putimage_2(dstX, dstY, dstWidth, dstHeight, p_imagePtr, srcX, srcY);
        }
        /// <summary>
        /// 绘制图像
        /// </summary>
        /// <param name="dstX">绘制的x坐标</param>
        /// <param name="dstY">绘制的y坐标</param>
        /// <exception cref="ObjectDisposedException">对象已释放</exception>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        public void PutDraw(int dstX, int dstY)
        {
            if (IsDispose) throw new ObjectDisposedException(nameof(EasyImage));
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            EasyX_API.putimage_1(dstX, dstY, p_imagePtr);
        }
        /// <summary>
        /// 不支持透明绘制
        /// </summary>
        /// <param name="point"></param>
        /// <param name="backColor"></param>
        /// <exception cref="NotSupportedException"></exception>
        public override void PutDraw(EPoint point, RGBColor backColor)
        {
            ThrowNotSupportedException();
        }
        /// <summary>
        /// 不支持透明绘制
        /// </summary>
        /// <param name="position"></param>
        /// <param name="getBackcolorFunc"></param>
        public override void PutDraw(EPoint position, Func<EPoint, RGBColor> getBackcolorFunc)
        {
            ThrowNotSupportedException();
        }
        /// <summary>
        /// 不支持缩放绘制
        /// </summary>
        /// <param name="rect"></param>
        public override void PutDraw(ERect rect)
        {
            ThrowNotSupportedException();
        }
        /// <summary>
        /// 不支持缩放绘制
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="getBackcolorFunc"></param>
        public override void PutDraw(ERect rect, Func<EPoint, RGBColor> getBackcolorFunc)
        {
            ThrowNotSupportedException();
        }

        public override void CopyTo(BaseGraphics copy)
        {
            if (IsDispose) throw new ObjectDisposedException(nameof(EasyImage));
            if (copy is EasyImage ei) CopyToImage(ei);
            else base.CopyTo(copy);
        }

        public override void SetAllColor(RGBColor color)
        {
            if (IsDispose) throw new ObjectDisposedException(nameof(EasyImage));


            uint* bufferPtr = EasyX_API.GetImageBuffer_(p_imagePtr);

            int width, height;
            onlyGetSize(out width, out height);

            int length = width * height;

            //byte* bp;
            //byte r, g, b;
            //color.GetRGB(out r, out g, out b);

            RGBColor ec = color.OnlyRGB.ToBGR;

            for (int i = 0; i < length; i++)
            {
                //bp = (byte*)(bufferPtr + i);

                bufferPtr[i] = ec.colorValue;

                //bp[2] = r;
                //bp[1] = g;
                //bp[0] = b;
            }

        }
        #endregion

    }


}
