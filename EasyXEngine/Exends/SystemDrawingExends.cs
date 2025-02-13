using Cheng.EasyX.DataStructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Cheng.EasyXEngine.Extends
{

    /// <summary>
    /// 框架扩展
    /// </summary>
    public unsafe static class SystemDrawingExends
    {

        #region 结构转化

        /// <summary>
        /// 将<see cref="System.Drawing.Color"/>颜色结构转化为RGB通用结构
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>转化后的结构</returns>
        public static RGBColor ToRGB(this System.Drawing.Color color)
        {
            return new RGBColor(color.R, color.G, color.B, color.A);
        }

        /// <summary>
        /// 将rgb通用结构转化为<see cref="System.Drawing.Color"/>颜色
        /// </summary>
        /// <param name="rgb"></param>
        /// <returns></returns>
        public static System.Drawing.Color ToSColor(this RGBColor rgb)
        {
            rgb.GetRGBA(out var r, out var g, out var b, out var a);
            return System.Drawing.Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        /// 将<see cref="EPoint"/>等价转化为<see cref="System.Drawing.Point"/>
        /// </summary>
        /// <param name="epoint"></param>
        /// <returns>转化后的数据</returns>
        public static System.Drawing.Point ToSPoint(this EPoint epoint)
        {
            return new Point(epoint.x, epoint.y);
        }

        /// <summary>
        /// 将<see cref="System.Drawing.Point"/>等价转化为<see cref="EPoint"/>
        /// </summary>
        /// <param name="point"></param>
        /// <returns>转化后的数据</returns>
        public static EPoint ToEPoint(this System.Drawing.Point point)
        {
            return new EPoint(point.X, point.Y);
        }

        /// <summary>
        /// 将<see cref="ERect"/>等价转化为<see cref="System.Drawing.Rectangle"/>
        /// </summary>
        /// <param name="rect"></param>
        /// <returns>转化后的数据</returns>
        public static System.Drawing.Rectangle ToRectangle(this ERect rect)
        {
            return new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
        }

        /// <summary>
        /// 将<see cref="System.Drawing.Rectangle"/>等价转化为<see cref="ERect"/>
        /// </summary>
        /// <param name="rect"></param>
        /// <returns>转化后的数据</returns>
        public static ERect ToERect(this Rectangle rect)
        {
            var x = rect.X;
            var y = rect.Y;
            return new ERect(x, y, x + rect.Width - 1, y + rect.Height - 1);
        }

        /// <summary>
        /// 将<see cref="EPointS"/>等价转化为<see cref="System.Drawing.PointF"/>
        /// </summary>
        /// <param name="epoint"></param>
        /// <returns>转化后的数据</returns>
        public static System.Drawing.PointF ToSPoint(this EPointS epoint)
        {
            return new PointF(epoint.x, epoint.y);
        }

        /// <summary>
        /// 将<see cref="System.Drawing.PointF"/>等价转化为<see cref="EPointS"/>
        /// </summary>
        /// <param name="point"></param>
        /// <returns>转化后的数据</returns>
        public static EPointS ToEPoint(this System.Drawing.PointF point)
        {
            return new EPointS(point.X, point.Y);
        }

        /// <summary>
        /// 将该实例转化为<see cref="RectangleF"/>
        /// </summary>
        /// <param name="rect"></param>
        /// <returns>转化后的实例</returns>
        public static RectangleF ToRectangleF(this ERectF rect)
        {
            return new RectangleF((float)rect.x, (float)rect.y, (float)rect.width, (float)rect.height);
        }

        /// <summary>
        /// 将给定的<see cref="RectangleF"/>转化为<see cref="ERectF"/>实例
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <returns>转化后的值</returns>
        public static ERectF ToRectF(this RectangleF rect)
        {
            return new ERectF(rect.X, rect.Y, rect.Width, rect.Height);
        }

        /// <summary>
        /// 将该实例转化为<see cref="RectangleF"/>
        /// </summary>
        /// <param name="rect"></param>
        /// <returns>转化后的实例</returns>
        public static RectangleF ToRectangleF(this ERectD rect)
        {
            return new RectangleF((float)rect.x, (float)rect.y, (float)rect.width, (float)rect.height);
        }

        /// <summary>
        /// 将给定的<see cref="RectangleF"/>转化为<see cref="ERectD"/>实例
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <returns>转化后的值</returns>
        public static ERectD ToRectD(this RectangleF rect)
        {
            return new ERectD(rect.X, rect.Y, rect.Width, rect.Height);
        }

        /// <summary>
        /// 转化为<see cref="ERectD"/>
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static ERectD ToERectD(this ERectF rect)
        {
            return new ERectD(rect.x, rect.y, rect.width, rect.height);
        }

        /// <summary>
        /// 转化为<see cref="ERectF"/>
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static ERectF ToERectF(this ERectD rect)
        {
            return new ERectF((float)rect.x, (float)rect.y, (float)rect.width, (float)rect.height);
        }

        #endregion

        #region 图像实例

        /// <summary>
        /// 将系统位图图像拷贝到easyx图像实例中
        /// </summary>
        /// <param name="bitmap">原图像</param>
        /// <param name="graphics">要拷贝到的图像</param>
        /// <exception cref="ArgumentNullException">参数为null</exception>
        /// <exception cref="NotSupportedException">没有权限</exception>
        /// <exception cref="Exception">其它错误</exception>
        public static void CopyTo(this Bitmap bitmap, BaseGraphics graphics)
        {
            if (bitmap is null || graphics is null) throw new ArgumentNullException();
            if (!graphics.CanSetPixedColor) throw new NotSupportedException();

            int oriWidth, oriHeight, toWidth, toHeight;

            oriWidth = bitmap.Width;
            oriHeight = bitmap.Height;

            toHeight = graphics.Height;
            toWidth = graphics.Width;

            int width, height;

            width = System.Math.Min(oriWidth, toWidth);
            height = System.Math.Min(oriHeight, toHeight);

            int x, y;

            //RGBColor rgb;
            //System.Drawing.Color color;

            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++)
                {
                    graphics.SetPixelColor(x, y, bitmap.GetPixel(x, y).ToRGB());
                }
            }

        }

        /// <summary>
        /// 将图像拷贝到位图
        /// </summary>
        /// <param name="grapics">原图</param>
        /// <param name="map">拷贝到的图</param>
        /// <exception cref="Exception">错误</exception>
        public static void CopyTo(this BaseGraphics grapics, Bitmap map)
        {
            if (grapics is null || map is null) throw new ArgumentNullException();
            if (grapics.IsDispose) throw new ObjectDisposedException(nameof(grapics));
            int width, height;
            width = System.Math.Min(grapics.Width, map.Width);
            height = System.Math.Min(grapics.Height, map.Height);

            int x, y;

            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++)
                {
                    map.SetPixel(x, y, grapics.GetPixelColor(x, y).ToSColor());
                }
            }

        }

        /// <summary>
        /// 在指定流创建<see cref="EasyImage"/>对象
        /// </summary>
        /// <remarks>
        /// <para>图像可按参数缩放大小；</para>
        /// <para>当<paramref name="width"/>等于0，且<paramref name="height"/>不等于0时，长度按给定高度和原高度等比缩放；</para>
        /// <para>当<paramref name="height"/>等于0，且<paramref name="width"/>不等于0时，高度按给定长度和原长度等比缩放；</para>
        /// <para>当长宽都不等于0时，按照给定的大小缩放图片；</para>
        /// <para>当长宽都是0时，使用图像原尺寸；</para>
        /// <para>该函数会消耗大量内存和非托管资源，不易频繁调用</para>
        /// </remarks>
        /// <param name="stream">包含图像资源的流</param>
        /// <param name="width">要缩放到的长度，0表示默认缩放</param>
        /// <param name="height">要缩放到的宽度，0表示默认缩放</param>
        /// <returns>对象实例</returns>
        /// <exception cref="Exception">错误</exception>
        public static EasyImage CreateEasyImage(this Stream stream, int width, int height)
        {
            if (stream is null) throw new ArgumentNullException();
            if (width < 0 || height < 0) throw new ArgumentOutOfRangeException();
            EasyImage image;
            using (Bitmap maps = new Bitmap(stream))
            {
                bool wb, rb;
                wb = width == 0;
                rb = height == 0;

                if ((width == maps.Width && height == maps.Height) || (wb && rb))
                {
                    //原大小

                    image = new EasyImage((ushort)maps.Width, (ushort)maps.Height);
                    try
                    {
                        maps.CopyTo(image);
                    }
                    catch (Exception)
                    {
                        image?.Close();
                        throw;
                    }

                    return image;
                }


                if (wb != rb)
                {
                    //只有一个等于0

                    if (wb)
                    {
                        //长度为0
                        //按高度缩放长度

                        width = (int)(maps.Width * ((double)height / (double)maps.Height));
                    }
                    else
                    {
                        //宽度为0
                        //按长度缩放高度
                        height = (int)(maps.Height * ((double)width / (double)maps.Width));

                    }
                }

                using (Bitmap map = new Bitmap(maps, width, height))
                {

                    image = new EasyImage((ushort)width, (ushort)height);
                    try
                    {
                        map.CopyTo(image);
                    }
                    catch (Exception)
                    {
                        image?.Close();
                        throw;
                    }

                }
            }
            return image;
        }

        /// <summary>
        /// 在指定文件创建对象
        /// </summary>
        /// <remarks>参数格式和<see cref="CreateEasyImage(Stream, int, int)"/>一致</remarks>
        /// <param name="path">图像文件路径</param>
        /// <param name="width">要缩放到的长度，0表示默认缩放</param>
        /// <param name="height">要缩放到的宽度，0表示默认缩放</param>
        /// <returns>对象实例</returns>
        /// <exception cref="Exception">错误</exception>
        public static EasyImage CreateEasyImage(this string path, int width, int height)
        {
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return CreateEasyImage(file, width, height);
            }
        }

        /// <summary>
        /// 将图像按另一个图像的大小缩放到另一个图像
        /// </summary>
        /// <remarks>
        /// <para>该函数会消耗大量内存和非托管资源，不易频繁调用</para>
        /// </remarks>
        /// <param name="image">原图</param>
        /// <param name="toImage">目标缓冲区</param>
        /// <exception cref="Exception">错误</exception>
        public static void ToResize(this BaseGraphics image, BaseGraphics toImage)
        {
            if (image is null || toImage is null) throw new ArgumentNullException();
            if (image.IsDispose || toImage.IsDispose) throw new ObjectDisposedException(typeof(EasyImage).Name);

            int width = image.Width;
            int height = image.Height;

            int tow, toh;

            tow = toImage.Width;
            toh = toImage.Height;

            bool widthFlag = tow == width;
            bool heightFlag = toh == height;

            if (widthFlag && heightFlag)
            {
                image.CopyTo(toImage);
                return;
            }


            using (Bitmap map = new Bitmap(width, height))
            {

                image.CopyTo(map);

                using (Bitmap tomap = new Bitmap(map, tow, toh))
                {
                    tomap.CopyTo(toImage);
                }

            }

        }

        #endregion

    }

}
