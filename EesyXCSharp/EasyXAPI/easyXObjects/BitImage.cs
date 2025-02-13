using Cheng.EasyX.CPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheng.EasyX.DataStructure
{

    /// <summary>
    /// 一个后备为内存的图像
    /// </summary>
    public class BitImage : BaseGraphics
    {

        #region 释放

        #endregion

        #region 构造
        /// <summary>
        /// 实例化一个内存缓冲区的图片
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public BitImage(int width, int height)
        {
            if (width < 0 || height < 0) throw new ArgumentNullException();
            p_buffer = new RGBColor[width, height];
        }
        #endregion

        #region 参数

        private RGBColor[,] p_buffer;

        #endregion

        #region 功能

        #region 参数获取
        /// <summary>
        /// 图像缓冲区
        /// </summary>
        public RGBColor[,] ImageBuffer
        {
            get => p_buffer;
        }

        #endregion

        #region 派生

        public override int Width => p_buffer.GetLength(0);

        public override int Height => p_buffer.GetLength(1);

        public override bool CanTransparency => true;

        public override bool CanSetPixedColor => true;

        protected override RGBColor getPixelColor(int x, int y)
        {
            return p_buffer[x, y];
        }

        protected override void setPixedColor(int x, int y, RGBColor color)
        {
            p_buffer[x, y] = color;
        }

        public override bool CanGetPixedColor => true;

        protected override void onlyGetSize(out int width, out int height)
        {
            width = p_buffer.GetLength(0);
            height = p_buffer.GetLength(1);
        }

        public override bool CanScaleDraw => false;

        public unsafe override void SetAllColor(RGBColor color)
        {
            int length = p_buffer.Length;

            int i;

            fixed (RGBColor* ptr = p_buffer)
            {
                for (i = 0; i < length; i++)
                {
                    ptr[i] = color;
                }
            }

        }

        public override void CopyTo(BaseGraphics copy)
        {
            if(copy is BitImage bit) bit.p_buffer = (RGBColor[,])p_buffer.Clone();
            else base.CopyTo(copy);
        }

        public override void PutDraw(EPoint point)
        {
            if (IsDispose) throw new ObjectDisposedException(GetType().Name);
            Device.f_testNotInitGraph(Device.exc_winNotInit);

            int x;
            int y;
            int width;
            int height;
            //onlyGetSize(out width, out height);
            width = p_buffer.GetLength(0);
            height = p_buffer.GetLength(1);
            int tx, ty;
            RGBColor c;
            //RGBColor c;
            for (y = 0, ty = point.y; y < height; y++, ty++)
            {

                for (x = 0, tx = point.x; x < width; x++, tx++)
                {

                    //c = getPixelColor(x, y);
                    c = p_buffer[x, y];
                    c = c.ColorSynthesis(EasyX_API.getpixel_(tx, ty)).OnlyRGB;
                    Drawing.Pixel(tx, ty, c);
                }
            }
        }

        public override void PutDraw(EPoint point, RGBColor backColor)
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
                    c = p_buffer[x, y];
                    c = c.ColorSynthesis(backColor);
                    Drawing.Pixel(tx, ty, c);
                }
            }
        }

        public override void PutDraw(EPoint position, Func<EPoint, RGBColor> getBackcolorFunc)
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
                    c = p_buffer[x, y];
                    c = c.ColorSynthesis(getBackcolorFunc.Invoke(tp));
                    Drawing.Pixel(tp.x, tp.y, c);
                }
            }
        }



        #endregion

        #endregion

    }

}
