using Cheng.EasyX.CPP;
using System;
using System.Runtime.InteropServices;
using Cheng.EasyX.Exceptions;

namespace Cheng.EasyX.DataStructure
{

    #region 点

    /// <summary>
    /// 坐标结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct EPoint : IEquatable<EPoint>
    {

        #region 构造
        /// <summary>
        /// 实例化一个坐标结构
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public EPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion

        #region 参数
        /// <summary>
        /// x坐标
        /// </summary>
        public int x;
        /// <summary>
        /// y坐标
        /// </summary>
        public int y;
        #endregion

        #region 功能

        #region 转换
        /// <summary>
        /// 将坐标转化为64位整形数据
        /// </summary>
        /// <returns></returns>
        public long ToInt64()
        {
            return ((long)x) | (((long)y) << 32);
        }
        /// <summary>
        /// 将64位整形数据转化为坐标结构
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>坐标结构</returns>
        public static EPoint ToPoint(long value)
        {
            return new EPoint((int)((value) & 0xFFFFFFFF), (int)((value >> 32) & 0xFFFFFFFF));
        }
        /// <summary>
        /// 根据给定x坐标缩放
        /// </summary>
        /// <param name="x">要缩放到的x坐标</param>
        /// <returns>根据新的x坐标与原坐标的比值决定y值，返回新的实例</returns>
        public EPoint XScale(int x)
        {
            double scale = ((double)x / this.x);
            return new EPoint(x, (int)(y * scale));
        }
        /// <summary>
        /// 根据给定y坐标缩放
        /// </summary>
        /// <param name="y">要缩放到的y坐标</param>
        /// <returns>根据新的y坐标与原坐标的比值决定x值，返回新的实例</returns>
        public EPoint YScale(int y)
        {
            double scale = ((double)y / this.y);

            return new EPoint((int)(x * scale), y);
        }

        #endregion

        #region 运算
        /// <summary>
        /// 将两坐标分别相加
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static EPoint operator +(EPoint p1, EPoint p2)
        {
            return new EPoint(p1.x + p2.x, p1.y + p2.y);
        }
        /// <summary>
        /// 将左侧两坐标分别减去右侧坐标的值
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static EPoint operator -(EPoint p1, EPoint p2)
        {
            return new EPoint(p1.x - p2.x, p1.y - p2.y);
        }
        
        public static EPoint operator *(EPoint point, float value)
        {
            return new EPoint((int)(point.x * value), (int)(point.y * value));
        }

        public static EPoint operator /(EPoint point, float value)
        {
            return new EPoint((int)(point.x / value), (int)(point.y / value));
        }

        public static EPoint operator *(EPoint point, int value)
        {
            return new EPoint((int)(point.x * value), (int)(point.y * value));
        }

        public static EPoint operator /(EPoint point, int value)
        {
            return new EPoint((int)(point.x / value), (int)(point.y / value));
        }
        #endregion

        #region 派生
        /// <summary>
        /// 判断是否相等
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool operator ==(EPoint p1, EPoint p2)
        {
            return p1.x == p2.x && p1.y == p2.y;
        }
        /// <summary>
        /// 判断是否不相等
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool operator !=(EPoint p1, EPoint p2)
        {
            return p1.x != p2.x || p1.y != p2.y;
        }

        public bool Equals(EPoint other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj)
        {
            if(obj is EPoint other)
            {
                return x == other.x && y == other.y;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return x ^ y;
        }
        /// <summary>
        /// 以字符串的格式返回坐标值
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "(" + x.ToString() + "," + y.ToString() + ")";
        }
        /// <summary>
        /// 以字符串的格式返回坐标值
        /// </summary>
        /// <param name="format">坐标值的格式</param>
        /// <returns></returns>
        public string ToString(string format)
        {
            return "(" + x.ToString(format) + "," + y.ToString(format) + ")";
        }
        #endregion

        #endregion

    }

    /// <summary>
    /// 浮点值坐标结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct EPointS : IEquatable<EPointS>
    {

        #region 构造
        /// <summary>
        /// 实例化一个坐标结构
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public EPointS(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion

        #region 参数
        /// <summary>
        /// x浮点坐标
        /// </summary>
        public float x;
        /// <summary>
        /// y浮点坐标
        /// </summary>
        public float y;
        #endregion

        #region 功能

        #region 转换

        /// <summary>
        /// 使用指定长宽缩放到像素坐标
        /// </summary>
        /// <param name="width">指定绘制区域的长</param>
        /// <param name="height">指定绘制区域的高</param>
        /// <returns>
        /// 一个像素坐标
        /// <para>将该实例的浮点值参数的<see cref="x"/>,<see cref="y"/>与参数指定的区域[0~width,0~height]进行比例计算，并返回指定区域的像素坐标</para>
        /// <para>实例的<see cref="x"/>和<see cref="y"/>如果在超出0~1的范围，则返回的坐标将处于给定区域之外</para>
        /// </returns>
        public EPoint ToPixel(int width, int height)
        {
            float nx, ny;
            nx = width * x;
            ny = height * y;
            int ix, iy;
            ix = (int)nx;
            iy = (int)ny;
            if (nx - ix >= 0.5f) ix++;
            if (ny - iy >= 0.5f) iy++;

            return new EPoint(ix, iy);
        }
        /// <summary>
        /// 使用当前绘图大小缩放到像素坐标
        /// </summary>
        /// <returns>
        /// 一个像素坐标
        /// <para>将该实例的浮点值参数的<see cref="x"/>,<see cref="y"/>与绘图区域进行比例计算，并返回指定区域的像素坐标</para>
        /// <para>实例的<see cref="x"/>和<see cref="y"/>如果在超出0~1的范围，则返回的坐标将处于给定区域之外</para>
        /// </returns>
        /// <exception cref="WindowEasyXException">窗口未初始化</exception>
        public EPoint ToPixel()
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);

            float nx, ny;
            nx = EasyX_API.getwidth_() * x;
            ny = EasyX_API.getheight_() * y;
            int ix, iy;
            ix = (int)nx;
            iy = (int)ny;
            if (nx - ix >= 0.5f) ix++;
            if (ny - iy >= 0.5f) iy++;

            return new EPoint(ix, iy);
        }
        /// <summary>
        /// 使用指定位置大小的矩形缩放到像素坐标
        /// </summary>
        /// <param name="rect">缩放所在范围</param>
        /// <returns>
        /// 像素坐标
        /// <para>根据给定的矩形作为限制范围，<see cref="x"/>表示横坐标在矩形内的相对坐标比，<see cref="y"/>表示纵坐标在矩形内的相对坐标比</para>
        /// </returns>
        public EPoint ToPixel(ERect rect)
        {
            //Device.f_testNotInitGraph(Device.exc_winNotInit);
            float width = rect.right - rect.left;
            float height = rect.bottom - rect.top;

            float nx, ny;
            nx = width * x;
            ny = height * y;
            int ix, iy;
            ix = (int)nx;
            iy = (int)ny;
            if (nx - ix >= 0.5f) ix++;
            if (ny - iy >= 0.5f) iy++;

            return new EPoint(ix + rect.left, iy + rect.top);
        }

        #endregion

        #region 运算
        /// <summary>
        /// 将两坐标分别相加
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static EPointS operator +(EPointS p1, EPointS p2)
        {
            return new EPointS(p1.x + p2.x, p1.y + p2.y);
        }
        /// <summary>
        /// 将左侧两坐标分别减去右侧坐标的值
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static EPointS operator -(EPointS p1, EPointS p2)
        {
            return new EPointS(p1.x - p2.x, p1.y - p2.y);
        }
        /// <summary>
        /// 将坐标进行乘法运算
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static EPointS operator *(EPointS p1, float num)
        {
            return new EPointS(p1.x * num, p1.y * num);
        }
        /// <summary>
        /// 将坐标进行除法运算
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static EPointS operator /(EPointS p1, float num)
        {
            return new EPointS(p1.x / num, p1.y / num);
        }
        /// <summary>
        /// 隐式转换为浮点坐标
        /// </summary>
        /// <param name="p"></param>
        public static implicit operator EPointS(EPoint p)
        {
            return new EPointS(p.x, p.y);
        }
        /// <summary>
        /// 显示转换为整形坐标
        /// </summary>
        /// <param name="p"></param>
        public static explicit operator EPoint(EPointS p)
        {
            return new EPoint((int)p.x, (int)p.y);
        }
        #endregion

        #region 派生
        /// <summary>
        /// 判断是否相等
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool operator ==(EPointS p1, EPointS p2)
        {
            return p1.x == p2.x && p1.y == p2.y;
        }
        /// <summary>
        /// 判断是否不相等
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool operator !=(EPointS p1, EPointS p2)
        {
            return p1.x != p2.x || p1.y != p2.y;
        }

        public bool Equals(EPointS other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj)
        {
            if (obj is EPointS other)
            {
                return x == other.x && y == other.y;
            }
            return false;
        }
        public override int GetHashCode()
        {
            float x = this.x, y = this.y;

            return (*(int*)&x) ^ (*(int*)&y);
        }
        /// <summary>
        /// 以字符串的格式返回坐标值
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "(" + x.ToString() + "," + y.ToString() + ")";
        }
        /// <summary>
        /// 以字符串的格式返回坐标值
        /// </summary>
        /// <param name="format">坐标值的格式</param>
        /// <returns></returns>
        public string ToString(string format)
        {
            return "(" + x.ToString(format) + "," + y.ToString(format) + ")";
        }
        #endregion

        #endregion

    }

    #endregion

}
