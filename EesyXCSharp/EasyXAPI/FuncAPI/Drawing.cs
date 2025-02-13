using Cheng.EasyX.CPP;
using Cheng.EasyX.DataStructure;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using Cheng.EasyX.Exceptions;

namespace Cheng.EasyX
{
    /// <summary>
    /// 绘制类型
    /// </summary>
    public enum DrawingType : byte
    {

        /// <summary>
        /// 仅边框模式
        /// </summary>
        Frame = 1,
        /// <summary>
        /// 仅填充模式
        /// </summary>
        Fill = 2,
        /// <summary>
        /// 带有边框的填充模式
        /// </summary>
        FrameFill = 3,
        /// <summary>
        /// 使用背景色的清空模式
        /// </summary>
        Clear = 4

    }

    /// <summary>
    /// 绘图操作
    /// </summary>
    public unsafe static class Drawing
    {

        #region easy
        /// <summary>
        /// 获取当前绘图设备指定坐标的像素值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>指定坐标的像素值</returns>
        /// <exception cref="WindowEasyXException">未初始化窗口</exception>
        public static RGBColor GetPixed(int x, int y)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            return EasyX_API.getpixel_(x, y);
        }

        /// <summary>
        /// 获取当前绘图设备指定坐标的像素值
        /// </summary>
        /// <param name="point">像素坐标</param>
        /// <returns>指定坐标的像素值</returns>
        /// <exception cref="WindowEasyXException">未初始化窗口</exception>
        public static RGBColor GetPixed(EPoint point)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            return EasyX_API.getpixel_(point.x, point.y);
        }

        /// <summary>
        /// 画点
        /// </summary>
        /// <param name="x">坐标x</param>
        /// <param name="y">坐标y</param>
        /// <param name="color">绘制颜色</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        public static void Pixel(int x, int y, RGBColor color)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            EasyX_API.putpixel_(x, y, color.OnlyRGB);
        }

        /// <summary>
        /// 画点
        /// </summary>
        /// <param name="point">绘制的像素坐标</param>
        /// <param name="color">绘制颜色</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        public static void Pixel(EPoint point, RGBColor color)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            EasyX_API.putpixel_(point.x, point.y, color);
        }

        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="x1">点1x坐标</param>
        /// <param name="y1">点1y坐标</param>
        /// <param name="x2">点2x坐标</param>
        /// <param name="y2">点2y坐标</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        public static void Line(int x1, int y1, int x2, int y2)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            EasyX_API.line_(x1, y1, x2, y2);
        }

        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="p1">点1</param>
        /// <param name="p2">点2</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        public static void Line(EPoint p1, EPoint p2)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            EasyX_API.line_(p1.x, p1.y, p2.x, p2.y);
        }

        #endregion

        #region 三角形
        /// <summary>
        /// 绘制三角形
        /// </summary>
        /// <param name="p1">顶点1</param>
        /// <param name="p2">顶点2</param>
        /// <param name="p3">顶点3</param>
        /// <param name="type">绘制模式</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ArgumentException">绘制类型参数不是已存在的枚举值</exception>
        public static void Triangle(EPoint p1, EPoint p2, EPoint p3, DrawingType type)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            EPoint* pp = stackalloc EPoint[3];
            pp[0] = p1;
            pp[1] = p2;
            pp[2] = p3;

            switch (type)
            {
                case DrawingType.Frame:
                    EasyX_API.polygon_(pp, 3);
                    break;
                case DrawingType.Fill:
                    EasyX_API.solidpolygon_(pp, 3);
                    break;
                case DrawingType.FrameFill:
                    EasyX_API.fillpolygon_(pp, 3);
                    break;
                case DrawingType.Clear:
                    EasyX_API.clearpolygon_(pp, 3);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        /// <summary>
        /// 使用比例坐标绘制三角形
        /// </summary>
        /// <param name="p1">顶点1</param>
        /// <param name="p2">顶点2</param>
        /// <param name="p3">顶点3</param>
        /// <param name="type">绘制模式</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ArgumentException">绘制类型参数不是已存在的枚举值</exception>
        public static void TrianglePro(EPointS p1, EPointS p2, EPointS p3, DrawingType type)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);

            var wins = Device.WindowSize;

            Triangle(p1.ToPixel(wins.x, wins.y), p2.ToPixel(wins.x, wins.y), p3.ToPixel(wins.x, wins.y), type);
        }
        #endregion

        #region 画圆
        /// <summary>
        /// 画圆
        /// </summary>
        /// <param name="x">圆心的x坐标</param>
        /// <param name="y">圆心的y坐标</param>
        /// <param name="radius">半径</param>
        /// <param name="type">绘制类型</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ArgumentException">绘制类型参数不是已存在的枚举值</exception>
        public static void Circle(int x, int y, int radius, DrawingType type)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            switch (type)
            {
                case DrawingType.Frame:
                    EasyX_API.circle_(x, y, radius);
                    break;
                case DrawingType.Fill:
                    EasyX_API.solidcircle_(x, y, radius);
                    break;
                case DrawingType.FrameFill:
                    EasyX_API.fillcircle_(x, y, radius);
                    break;
                case DrawingType.Clear:
                    EasyX_API.clearcircle_(x, y, radius);
                    break;
                default:
                    throw new ArgumentException();
            }

        }
        /// <summary>
        /// 画圆
        /// </summary>
        /// <param name="point">圆心坐标</param>
        /// <param name="radius">圆的半径</param>
        /// <param name="type">绘制类型</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ArgumentException">绘制类型参数不是已存在的枚举值</exception>
        public static void Circle(EPoint point, int radius, DrawingType type)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            switch (type)
            {
                case DrawingType.Frame:
                    EasyX_API.circle_(point.x, point.y, radius);
                    break;
                case DrawingType.Fill:
                    EasyX_API.solidcircle_(point.x, point.y, radius);
                    break;
                case DrawingType.FrameFill:
                    EasyX_API.fillcircle_(point.x, point.y, radius);
                    break;
                case DrawingType.Clear:
                    EasyX_API.clearcircle_(point.x, point.y, radius);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        #endregion

        #region 椭圆
        /// <summary>
        /// 绘制椭圆
        /// </summary>
        /// <param name="left">椭圆的外切矩形左侧坐标</param>
        /// <param name="top">椭圆的外切矩形顶部坐标</param>
        /// <param name="right">椭圆的外切矩形右侧坐标</param>
        /// <param name="bottom">椭圆的外切矩形底部坐标</param>
        /// <param name="type">绘制类型</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ArgumentException">绘制类型参数不是已存在的枚举值</exception>
        public static void Ellipse(int left, int top, int right, int bottom, DrawingType type)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);

            switch (type)
            {
                case DrawingType.Frame:
                    EasyX_API.ellipse_(left, top, right, bottom);
                    break;
                case DrawingType.Fill:
                    EasyX_API.solidellipse_(left, top, right, bottom);
                    break;
                case DrawingType.FrameFill:
                    EasyX_API.fillellipse_(left, top, right, bottom);
                    break;
                case DrawingType.Clear:
                    EasyX_API.clearellipse_(left, top, right, bottom);
                    break;
                default:
                    throw new ArgumentException();
            }

        }
        /// <summary>
        /// 绘制椭圆
        /// </summary>
        /// <param name="rect">椭圆的外切矩形</param>
        /// <param name="type">绘制类型</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ArgumentException">绘制类型参数不是已存在的枚举值</exception>
        public static void Ellipse(ERect rect, DrawingType type)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);

            switch (type)
            {
                case DrawingType.Frame:
                    EasyX_API.ellipse_(rect.left, rect.top, rect.right, rect.bottom);
                    break;
                case DrawingType.Fill:
                    EasyX_API.solidellipse_(rect.left, rect.top, rect.right, rect.bottom);
                    break;
                case DrawingType.FrameFill:
                    EasyX_API.fillellipse_(rect.left, rect.top, rect.right, rect.bottom);
                    break;
                case DrawingType.Clear:
                    EasyX_API.clearellipse_(rect.left, rect.top, rect.right, rect.bottom);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        #endregion

        #region 扇形
        /// <summary>
        /// 绘制扇形
        /// </summary>
        /// <param name="rect">扇形所在椭圆的外切矩形</param>
        /// <param name="stangle">扇形的起始角的弧度</param>
        /// <param name="endangle">扇形的终止角的弧度</param>
        /// <param name="type">绘制类型</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ArgumentException">绘制类型参数不是已存在的枚举值</exception>
        public static void Pie(ERect rect, double stangle, double endangle, DrawingType type)
        {

            Device.f_testNotInitGraph(Device.exc_winNotInit);

            switch (type)
            {
                case DrawingType.Frame:
                    EasyX_API.pie_(rect.left, rect.top, rect.right, rect.bottom, stangle, endangle);
                    break;
                case DrawingType.Fill:
                    EasyX_API.solidpie_(rect.left, rect.top, rect.right, rect.bottom, stangle, endangle);
                    break;
                case DrawingType.FrameFill:
                    EasyX_API.fillpie_(rect.left, rect.top, rect.right, rect.bottom, stangle, endangle);
                    break;
                case DrawingType.Clear:
                    EasyX_API.clearpie_(rect.left, rect.top, rect.right, rect.bottom, stangle, endangle);
                    break;
                default:
                    throw new ArgumentException();
            }

        }
        /// <summary>
        /// 绘制扇形
        /// </summary>
        /// <param name="left">扇形所在椭圆的外切矩形的左侧坐标</param>
        /// <param name="top">扇形所在椭圆的外切矩形的顶部坐标</param>
        /// <param name="right">扇形所在椭圆的外切矩形的右侧坐标</param>
        /// <param name="bottom">扇形所在椭圆的外切矩形的底部坐标</param>
        /// <param name="stangle">扇形的起始角的弧度</param>
        /// <param name="endangle">扇形的终止角的弧度</param>
        /// <param name="type">绘制类型</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ArgumentException">绘制类型参数不是已存在的枚举值</exception>
        public static void Pie(int left, int top, int right, int bottom, double stangle, double endangle, DrawingType type)
        {
            Pie(new ERect(left, top, right, bottom), stangle, endangle, type);
        }

        #endregion

        #region 矩形

        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="rect">要绘制的矩形</param>
        /// <param name="type">绘制类型</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ArgumentException">参数不正确</exception>
        public static void Rectangle(ERect rect, DrawingType type)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);

            switch (type)
            {
                case DrawingType.Frame:
                    EasyX_API.rectangle_(rect.left, rect.top, rect.right, rect.bottom);
                    break;
                case DrawingType.Fill:
                    EasyX_API.solidrectangle_(rect.left, rect.top, rect.right, rect.bottom);
                    break;
                case DrawingType.FrameFill:
                    EasyX_API.fillrectangle_(rect.left, rect.top, rect.right, rect.bottom);
                    break;
                case DrawingType.Clear:
                    EasyX_API.clearrectangle_(rect.left, rect.top, rect.right, rect.bottom);

                    break;
                default:
                    throw new ArgumentException();
            }

        }
        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="left">要绘制的矩形左侧坐标</param>
        /// <param name="top">要绘制的矩形顶部坐标</param>
        /// <param name="right">要绘制的矩形右侧坐标</param>
        /// <param name="bottom">要绘制的矩形底部坐标</param>
        /// <param name="type">绘制类型</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ArgumentException">参数不正确</exception>
        public static void Rectangle(int left, int top, int right, int bottom, DrawingType type)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);

            switch (type)
            {
                case DrawingType.Frame:
                    EasyX_API.rectangle_(left, top, right, bottom);
                    break;
                case DrawingType.Fill:
                    EasyX_API.solidrectangle_(left, top, right, bottom);
                    break;
                case DrawingType.FrameFill:
                    EasyX_API.fillrectangle_(left, top, right, bottom);
                    break;
                case DrawingType.Clear:
                    EasyX_API.clearrectangle_(left, top, right, bottom);
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// 绘制圆角矩形
        /// </summary>
        /// <param name="rect">要绘制的矩形</param>
        /// <param name="ellipsewidth">构成圆角矩形的圆角的椭圆的宽度</param>
        /// <param name="ellipseheight">构成圆角矩形的圆角的椭圆的高度</param>
        /// <param name="type">绘制类型</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ArgumentException">参数不正确</exception>
        public static void RoundRect(ERect rect, int ellipsewidth, int ellipseheight, DrawingType type)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);

            switch (type)
            {
                case DrawingType.Frame:
                    EasyX_API.roundrect_(rect.left, rect.top, rect.right, rect.bottom, ellipsewidth, ellipseheight);
                    break;
                case DrawingType.Fill:
                    EasyX_API.solidroundrect_(rect.left, rect.top, rect.right, rect.bottom, ellipsewidth, ellipseheight);
                    break;
                case DrawingType.FrameFill:
                    EasyX_API.fillroundrect_(rect.left, rect.top, rect.right, rect.bottom, ellipsewidth, ellipseheight);
                    break;
                case DrawingType.Clear:
                    EasyX_API.clearroundrect_(rect.left, rect.top, rect.right, rect.bottom, ellipsewidth, ellipseheight);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        /// <summary>
        /// 绘制圆角矩形
        /// </summary>
        /// <param name="left">要绘制的矩形左侧坐标</param>
        /// <param name="top">要绘制的矩形顶部坐标</param>
        /// <param name="right">要绘制的矩形右侧坐标</param>
        /// <param name="bottom">要绘制的矩形底部坐标</param>
        /// <param name="ellipsewidth">构成圆角矩形的圆角的椭圆的宽度</param>
        /// <param name="ellipseheight">构成圆角矩形的圆角的椭圆的高度</param>
        /// <param name="type">绘制类型</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ArgumentException">参数不正确</exception>
        public static void RoundRect(int left, int top, int right, int bottom, int ellipsewidth, int ellipseheight, DrawingType type)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);

            switch (type)
            {
                case DrawingType.Frame:
                    EasyX_API.roundrect_(left, top, right, bottom, ellipsewidth, ellipseheight);
                    break;
                case DrawingType.Fill:
                    EasyX_API.solidroundrect_(left, top, right, bottom, ellipsewidth, ellipseheight);
                    break;
                case DrawingType.FrameFill:
                    EasyX_API.fillroundrect_(left, top, right, bottom, ellipsewidth, ellipseheight);
                    break;
                case DrawingType.Clear:
                    EasyX_API.clearroundrect_(left, top, right, bottom, ellipsewidth, ellipseheight);
                    break;
                default:
                    throw new ArgumentException();
            }

        }

        #endregion

        #region 多边形
        /// <summary>
        /// 绘制多条线
        /// </summary>
        /// <param name="points">点数组，绘制时从第一个元素依次开始连接线段</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ArgumentException">参数不正确</exception>
        public static void PolyLine(EPoint[] points)
        {
            if (points is null) throw new ArgumentNullException("point");
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            EasyX_API.polyline_(points, points.Length);
        }

        /// <summary>
        /// 绘制多边形
        /// </summary>
        /// <param name="points">点数组，绘制时从第一个元素依次开始连接，最后一个顶点自动连接到第一个点</param>
        /// <param name="type">绘制类型</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ArgumentException">参数不正确</exception>
        public static void Polygon(EPoint[] points, DrawingType type)
        {
            if (points is null) throw new ArgumentNullException("point");
            Device.f_testNotInitGraph(Device.exc_winNotInit);

            switch (type)
            {
                case DrawingType.Frame:
                    EasyX_API.polygon_(points, points.Length);
                    break;
                case DrawingType.Fill:
                    EasyX_API.solidpolygon_(points, points.Length);
                    break;
                case DrawingType.FrameFill:
                    EasyX_API.fillpolygon_(points, points.Length);
                    break;
                case DrawingType.Clear:
                    EasyX_API.clearpolygon_(points, points.Length);
                    break;
                default:
                    throw new ArgumentException();
            }

        }

        /// <summary>
        /// 绘制多边形
        /// </summary>
        /// <param name="points">指向多边形顶点数组的首地址</param>
        /// <param name="size">多边形顶点的个数</param>
        /// <param name="type">绘制类型</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        /// <exception cref="ArgumentNullException">指针是null</exception>
        /// <exception cref="ArgumentException">参数不正确</exception>
        public static void PolygonByPtr(EPoint* points, int size, DrawingType type)
        {
            if (points == null) throw new ArgumentNullException("point");
            if (size < 0) throw new ArgumentOutOfRangeException();
            Device.f_testNotInitGraph(Device.exc_winNotInit);

            switch (type)
            {
                case DrawingType.Frame:
                    EasyX_API.polygon_(points, size);
                    break;
                case DrawingType.Fill:
                    EasyX_API.solidpolygon_(points, size);
                    break;
                case DrawingType.FrameFill:
                    EasyX_API.fillpolygon_(points, size);
                    break;
                case DrawingType.Clear:
                    EasyX_API.clearpolygon_(points, size);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        #endregion

    }

}
