using Cheng.EasyX.CPP;
using Cheng.EasyX.Exceptions;
using System;

namespace Cheng.EasyX.DataStructure
{

    /// <summary>
    /// 表示一个EasyX图片结构的基类
    /// </summary>
    public abstract class BaseGraphics : SecureUnmanagedObjects
    {

        #region 回收

        /// <summary>
        /// 重写此方法用于图像的非托管资源回收
        /// </summary>
        protected override void ReleaseUnmanaged() { }

        #endregion

        #region 功能

        #region 封装
        protected void ThrowNotSupportedException()
        {
            throw new NotSupportedException("不支持此操作");
        }
        protected T ThrowNotSupportedException<T>()
        {
            throw new NotSupportedException("不支持此操作");
        }
        #endregion

        #region 参数
        /// <summary>
        /// 是否允许获取指定位置像素
        /// </summary>
        public virtual bool CanGetPixedColor => false;
        /// <summary>
        /// 此实例是否允许设置指定位置像素
        /// </summary>
        /// <returns>若此实例支持设置缓冲区图像返回true，不支持返回false</returns>
        public virtual bool CanSetPixedColor => false;
        /// <summary>
        /// 是否支持透明色
        /// </summary>
        /// <returns>判断此图像是否支持透明色绘制，若支持透明色返回true，在使用<see cref="PutDraw(EPoint)"/>绘图时会根据<see cref="RGBColor.A"/>绘制透明色；若不支持透明色返回false，绘图时会忽略<see cref="RGBColor.A"/>参数</returns>
        public virtual bool CanTransparency => false;
        /// <summary>
        /// 是否可调整图片大小
        /// </summary>
        public virtual bool CanResize => false;
        /// <summary>
        /// 返回图片长度
        /// </summary>
        /// <exception cref="ObjectDisposedException">对象已释放</exception>
        public abstract int Width { get; }
        /// <summary>
        /// 返回图片宽度
        /// </summary>
        /// <exception cref="ObjectDisposedException">对象已释放</exception>
        public abstract int Height { get; }
        /// <summary>
        /// 此函数用于快速获取图片长宽
        /// </summary>
        /// <remarks>此方法不宜过多嵌套函数或做安全检查，应尽量精简代码提高效率，供内部显示实现调用</remarks>
        /// <param name="width">图片长度</param>
        /// <param name="height">图片宽度</param>
        protected virtual void onlyGetSize(out int width, out int height)
        {
            width = Width;
            height = Height;
        }
        /// <summary>
        /// 此方法用于内部获取指定位置的像素色
        /// </summary>
        /// <remarks>此方法不宜过多嵌套函数或做安全检查，应尽量精简代码提高效率，供内部显示实现调用</remarks>
        /// <param name="x">从左向右的横坐标</param>
        /// <param name="y">从上到下的纵坐标</param>
        /// <returns>颜色值</returns>
        protected virtual RGBColor getPixelColor(int x, int y)
        {
            return ThrowNotSupportedException<RGBColor>();
        }
        /// <summary>
        /// 根据指定的长度，按比例缩放宽度并返回表示缩放后的矩形
        /// </summary>
        /// <param name="width">指定的长度</param>
        /// <returns>缩放后的图片大小，x表示长，y表示高</returns>
        public virtual EPoint GetWidthScaleRect(int width)
        {
            if (IsDispose) throw new ObjectDisposedException(string.Empty);
            int height = (int)(Height * (width / (double)Width));
            return new EPoint(width, height);
        }
        /// <summary>
        /// 根据指定的高度，按比例缩放长度并返回表示缩放后的矩形
        /// </summary>
        /// <param name="height">指定的高度</param>
        /// <returns>缩放后的图片大小，x表示长，y表示高</returns>
        public virtual EPoint GetHeightScaleRect(int height)
        {
            if (IsDispose) throw new ObjectDisposedException(string.Empty);
            int width = (int)(Width * (height / (double)Height));
            return new EPoint(width, height);
        }
        /// <summary>
        /// 是否支持缩放绘制
        /// </summary>
        public virtual bool CanScaleDraw => false;
        #endregion

        #region 参数调整

        /// <summary>
        /// 设置指定位置像素的颜色
        /// </summary>
        /// <remarks>此方法不宜过多嵌套函数或做安全检查，应尽量精简代码提高效率，供内部显示实现调用</remarks>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <exception cref="NotSupportedException">不支持此功能</exception>
        protected virtual void setPixedColor(int x, int y, RGBColor color)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 获取指定坐标的颜色像素
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>颜色像素</returns>
        /// <exception cref="ArgumentOutOfRangeException">参数超出范围</exception>
        /// <exception cref="NotSupportedException">没有此功能</exception>
        public virtual RGBColor GetPixelColor(int x, int y)
        {
            if (x < 0 || x > Width || y < 0 || y > Height) throw new ArgumentOutOfRangeException();
            return getPixelColor(x, y);
        }

        /// <summary>
        /// 设置指定坐标的颜色像素
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color">要设置的像素颜色</param>
        /// <exception cref="ArgumentOutOfRangeException">参数超出范围</exception>
        /// <exception cref="NotSupportedException">没有此功能</exception>
        public virtual void SetPixelColor(int x, int y, RGBColor color)
        {
            if (x < 0 || x > Width || y < 0 || y > Height) throw new ArgumentOutOfRangeException();
            setPixedColor(x, y, color);
        }   

        /// <summary>
        /// 调整图片大小
        /// </summary>
        /// <param name="width">图片长度</param>
        /// <param name="height">图片宽度</param>
        public virtual void Resize(int width, int height)
        {
            ThrowNotSupportedException();
        }

        /// <summary>
        /// 将该图片的内容拷贝到另一个图片实例中
        /// </summary>
        /// <remarks>若图片容量不同，则拷贝的范围是最小长宽</remarks>
        /// <param name="copy">要拷贝到的实例</param>
        /// <exception cref="NotSupportedException">没有指定权限</exception>
        /// <exception cref="ArgumentNullException">参数为null</exception>
        public virtual void CopyTo(BaseGraphics copy)
        {
            if (IsDispose) throw new ObjectDisposedException(GetType().Name);
            if (copy is null) throw new ArgumentNullException();

            int height, width;
            onlyGetSize(out width, out height);

            int x, y;

            int th, tw;
            copy.onlyGetSize(out tw, out th);

            width = System.Math.Min(width, tw);
            height = System.Math.Min(height, th);

            for (y = 0; y < height; y++)
            {

                for(x = 0; x < width; x++)
                {

                    copy.setPixedColor(x, y, getPixelColor(x, y));
                }

            }

        }

        /// <summary>
        /// 设置整个图像的透明度
        /// </summary>
        /// <param name="a">新的透明度rgb分量</param>
        /// <exception cref="NotSupportedException">没有指定权限</exception>
        /// <exception cref="ObjectDisposedException">已释放</exception>
        public virtual void Setransparency(byte a)
        {
            //if (!CanTransparency) throw new NotSupportedException();
            if (IsDispose) throw new ObjectDisposedException(GetType().Name);
            
            int height, width;
            onlyGetSize(out width, out height);

            int x, y;
            for(y = 0; y < height; y++)
            {
                for(x = 0; x < width; x++)
                {
                    setPixedColor(x, y, getPixelColor(x, y).Transparency(a));
                }
            }
        }

        /// <summary>
        /// 设置整个图像的透明度
        /// </summary>
        /// <param name="transparency">新的透明度，范围在[0,1]，对应rgb色值A的[0,255]</param>
        /// <exception cref="NotSupportedException">没有指定权限</exception>
        /// <exception cref="ObjectDisposedException">已释放</exception>
        public virtual void Setransparency(float transparency)
        {
            //if (!CanTransparency) throw new NotSupportedException();
            if (IsDispose) throw new ObjectDisposedException(GetType().Name);

            int height, width;
            onlyGetSize(out width, out height);
            byte als = (byte)(transparency * 255);
            int x, y;
            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++)
                {
                    setPixedColor(x, y, getPixelColor(x, y).Transparency(als));
                }
            }
        }

        /// <summary>
        /// 将图像设置为纯色
        /// </summary>
        /// <param name="color">要设置的纯色</param>
        public virtual void SetAllColor(RGBColor color)
        {
            int width, height;
            //width = Width;
            //height = Height;

            onlyGetSize(out width, out height);

            int x, y;

            for (x = 0; x < width; x++)
            {
                for (y = 0; y < height; y++)
                {
                    setPixedColor(x, y, color);
                }
            }

        }

        #endregion

        #endregion

        #region 绘图
        /// <summary>
        /// 在绘图设备中显示图像
        /// </summary>
        /// <param name="point">图像左上角所在的坐标</param>
        /// <param name="backColor">指定像素透明时的背景色</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ObjectDisposedException">对象已释放</exception>
        /// <exception cref="NotSupportedException">此实例没有图像透明功能</exception>
        public virtual void PutDraw(EPoint point, RGBColor backColor)
        {
            if (!CanTransparency) throw new NotSupportedException();
            if (IsDispose) throw new ObjectDisposedException(GetType().Name);
            Device.f_testNotInitGraph(Device.exc_winNotInit);

            int x;
            int y;
            int width;
            int height;
            onlyGetSize(out width, out height);
            int tx, ty;
            RGBColor c;
            //RGBColor c;
            for (y = 0, ty = point.y; y < height; y++, ty++)
            {

                for (x = 0, tx = point.x; x < width; x++, tx++)
                {
                    //c = getPixelColor(x, y);
                    c = getPixelColor(x, y);
                    c = c.ColorSynthesis(backColor).OnlyRGB;
                    Drawing.Pixel(tx, ty, c);
                }
            }

        }

        /// <summary>
        /// 在绘图设备中显示图像
        /// </summary>
        /// <remarks>若支持透明色绘制，则会根据绘图区当前所显示的像素值做背景色</remarks>
        /// <param name="point">图像左上角所在的坐标</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ObjectDisposedException">对象已释放</exception>
        public virtual void PutDraw(EPoint point)
        {
            if (IsDispose) throw new ObjectDisposedException(GetType().Name);
            Device.f_testNotInitGraph(Device.exc_winNotInit);

            int x;
            int y;
            int width;
            int height;
            onlyGetSize(out width, out height);
            int tx, ty;
            RGBColor c;
            //RGBColor c;
            for (y = 0, ty = point.y; y < height; y++, ty++)
            {

                for (x = 0, tx = point.x; x < width; x++, tx++)
                {

                    //c = getPixelColor(x, y);
                    c = getPixelColor(x, y);                    
                    c = c.ColorSynthesis(EasyX_API.getpixel_(tx, ty)).OnlyRGB;
                    Drawing.Pixel(tx, ty, c);
                }
            }

        }

        /// <summary>
        /// 在绘图设备中显示图像
        /// </summary>
        /// <remarks>若支持透明色绘制，则会根据指定的背景色获取委托获取的颜色做背景色</remarks>
        /// <param name="position">图像左上角所在的坐标</param>
        /// <param name="getBackcolorFunc">用于获取背景色的委托，参数为坐标，返回该坐标的像素色</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ObjectDisposedException">对象已释放</exception>
        /// <exception cref="ArgumentNullException">参数为null</exception>
        public virtual void PutDraw(EPoint position, Func<EPoint, RGBColor> getBackcolorFunc)
        {
            if (getBackcolorFunc is null) throw new ArgumentNullException();
            
            if (IsDispose) throw new ObjectDisposedException(GetType().Name);
            Device.f_testNotInitGraph(Device.exc_winNotInit);

            int x;
            int y;
            int width;
            int height;
            onlyGetSize(out width, out height);
            //int tx, ty;
            EPoint tp = default;
            RGBColor c;
            //RGBColor c;
            for (y = 0, tp.y = position.y; y < height; y++, tp.y++)
            {

                for (x = 0, tp.x = position.x; x < width; x++, tp.x++)
                {

                    //c = getPixelColor(x, y);
                    c = getPixelColor(x, y);
                    c = c.ColorSynthesis(getBackcolorFunc.Invoke(tp)).OnlyRGB;
                    Drawing.Pixel(tp.x, tp.y, c);
                }
            }
        }

        /// <summary>
        /// 在绘图设备的指定区域显示图像
        /// </summary>
        /// <remarks>
        /// 采用最近邻插值法（Nearest Neighbor Interpolation）来缩放图像
        /// </remarks>
        /// <param name="rect">要显示的矩形区域，图像会根据区域缩放</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ObjectDisposedException">对象已释放</exception>
        /// <exception cref="NotSupportedException">不支持缩放绘制</exception>
        public virtual void PutDraw(ERect rect)
        {
            PutDraw(rect, Drawing.GetPixed);
        }

        /// <summary>
        /// 在绘图设备的指定区域显示图像
        /// </summary>
        /// <param name="rect">要显示的矩形区域，图像会根据区域缩放</param>
        /// <param name="getBackcolorFunc">获取背景色的方法</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ObjectDisposedException">对象已释放</exception>
        /// <exception cref="ArgumentNullException">参数为null</exception>
        /// <exception cref="NotSupportedException">不支持缩放绘制</exception>
        public virtual void PutDraw(ERect rect, Func<EPoint, RGBColor> getBackcolorFunc)
        {
            if (getBackcolorFunc is null) throw new ArgumentNullException();
            if (IsDispose) throw new ObjectDisposedException(GetType().Name);

            int Width, Height;
            double srcXFloat;
            double srcYFloat;
            int srcX, srcY;

            onlyGetSize(out Width, out Height);

            // 计算源图像和目标矩形之间的缩放比例
            double scaleX = (double)rect.Width / Width;
            double scaleY = (double)rect.Height / Height;

            // 遍历目标矩形的每个像素点  
            for (int destX = rect.left; destX <= rect.right; destX++)
            {
                for (int destY = rect.top; destY <= rect.bottom; destY++)
                {
                    // 根据缩放比例计算源图像中对应的浮点坐标  
                    srcXFloat = (destX - rect.left) / scaleX;
                    srcYFloat = (destY - rect.top) / scaleY;

                    // 使用最近邻插值法找到源图像中的最近整数坐标  
                    srcX = (int)Math.Round(srcXFloat);
                    srcY = (int)Math.Round(srcYFloat);

                    // 确保坐标不会越界  
                    srcX = Math.Max(0, Math.Min(srcX, Width - 1));
                    srcY = Math.Max(0, Math.Min(srcY, Height - 1));

                    // 获取源图像中对应像素点的颜色  
                    RGBColor srcColor = getPixelColor(srcX, srcY);

                    srcColor = srcColor.ColorSynthesis(getBackcolorFunc.Invoke(new EPoint(destX, destY))).OnlyRGB;

                    // 在目标设备上绘制颜色  
                    EasyX_API.putpixel_(destX, destY, srcColor);
                    //Drawing.Pixel(destX, destY, srcColor);
                }
            }

        }


        #endregion

    }

}
