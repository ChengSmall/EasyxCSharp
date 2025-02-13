using System;
using System.Runtime.InteropServices;

namespace Cheng.EasyX.DataStructure
{

    #region

    /// <summary>
    /// 矩形结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ERect : IEquatable<ERect>
    {

        #region 构造
        /// <summary>
        /// 初始化一个矩形结构
        /// </summary>
        /// <param name="left">左侧</param>
        /// <param name="top">顶部</param>
        /// <param name="right">右侧</param>
        /// <param name="bottom">底部</param>
        public ERect(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }
        /// <summary>
        /// 初始化一个矩形结构
        /// </summary>
        /// <param name="leftTop">左上角坐标</param>
        /// <param name="rightDown">右下角坐标</param>
        public ERect(EPoint leftTop, EPoint rightDown)
        {
            left = leftTop.x;
            top = leftTop.y;
            right = rightDown.x;
            bottom = rightDown.y;
        }
        /// <summary>
        /// 初始化一个矩形结构
        /// </summary>
        /// <param name="leftTop">左上角坐标</param>
        /// <param name="width">向右延申的长度</param>
        /// <param name="height">向下延伸的宽度</param>
        public ERect(EPoint leftTop, int width, int height)
        {
            left = leftTop.x;
            top = leftTop.y;
            right = left + width;
            bottom = top + height;
        }
        #endregion

        #region 参数

        /// <summary>
        /// 左侧
        /// </summary>
        public int left;

        /// <summary>
        /// 顶部
        /// </summary>
        public int top;

        /// <summary>
        /// 右侧
        /// </summary>
        public int right;

        /// <summary>
        /// 底部
        /// </summary>
        public int bottom;

        #endregion

        #region 功能

        #region 参数访问
        /// <summary>
        /// 访问或设置左上角坐标
        /// </summary>
        public EPoint LeftUp
        {
            get
            {
                return new EPoint(left, top);
            }
        }
        /// <summary>
        /// 访问右下角坐标
        /// </summary>
        public EPoint RightDown
        {
            get => new EPoint(right, bottom);
        }
        /// <summary>
        /// 获取右上角的坐标
        /// </summary>
        public EPoint RightUp
        {
            get => new EPoint(right, top);
            
        }
        /// <summary>
        /// 获取左下角的坐标
        /// </summary>
        public EPoint LeftDown
        {
            get => new EPoint(left, bottom);
        }
        /// <summary>
        /// 矩形的长度，以像素为单位
        /// </summary>
        public int Width
        {
            get => (right + 1) - left;
        }
        /// <summary>
        /// 矩形的高度，以像素为单位
        /// </summary>
        public int Height
        {
            get => (bottom + 1) - top;
        }
        /// <summary>
        /// 返回矩形中心点，单位像素
        /// </summary>
        public EPoint Centre
        {
            get
            {
                return new EPoint((left + (right - left) / 2), (top + (bottom - top) / 2));
            }
        }
        /// <summary>
        /// 获取x轴中心点
        /// </summary>
        public int MidX
        {
            get
            {
                return left + (right - left) / 2;
            }
        }
        /// <summary>
        /// 获取y轴中心点
        /// </summary>
        public int MidY
        {
            get => (top + (bottom - top) / 2);
        }

        #endregion

        #region 参数判断
        /// <summary>
        /// 判断给定的点是否处于该矩形内部
        /// </summary>
        /// <param name="pos">给定点</param>
        /// <returns>处于矩形内部返回true，没有处于内部返回false</returns>
        public bool IsPositionIn(EPoint pos)
        {
            return (pos.x > left && pos.x < right && pos.y > top && pos.y < bottom);
        }

        #endregion

        #region 变换

        /// <summary>
        /// 返回该矩形移动指定坐标后的矩形
        /// </summary>
        /// <param name="move">要移动的坐标，x和y分别表示移动的坐标轴分量</param>
        /// <returns>移动后的矩形</returns>
        public ERect MoveTo(EPoint move)
        {
            return new ERect(left + move.x, top + move.y, right + move.x, bottom + move.y);
        }
        /// <summary>
        /// 返回该矩形移动到指定坐标后的矩形
        /// </summary>
        /// <param name="move">矩形要移动到的坐标位置，以左上角为原点</param>
        /// <returns></returns>
        public ERect MoveForm(EPoint move)
        {
            return new ERect(move, Width, Height);
        }

        /// <summary>
        /// 根据给定长度缩放高度并返回
        /// </summary>
        /// <param name="width">新的长度</param>
        /// <returns>缩放后的矩形，缩放原点在左上角坐标</returns>
        public ERect ScaleWidth(int width)
        {
            int height = (int)(Height * ((double)width / Width));
            return new ERect(LeftUp, width, height);
        }
        /// <summary>
        /// 根据给定高度缩放长度并返回
        /// </summary>
        /// <param name="height">新的高度</param>
        /// <returns>缩放后的矩形，缩放原点在左上角坐标</returns>
        public ERect ScaleHeight(int height)
        {
            int width = (int)(Width * ((double)height / Height));
            return new ERect(LeftUp, width, height);
        }
        #endregion

        #region 派生
        /// <summary>
        /// 判断相等
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        public static bool operator ==(ERect r1, ERect r2)
        {
            return r1.left == r2.left && r1.right == r2.right && r1.top == r2.top && r1.bottom == r2.bottom;
        }
        /// <summary>
        /// 判断不相等
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        public static bool operator !=(ERect r1, ERect r2)
        {
            return r1.left != r2.left || r1.right != r2.right || r1.top != r2.top || r1.bottom != r2.bottom;
        }

        public bool Equals(ERect other)
        {
            return this.left == other.left && this.right == other.right && this.top == other.top && this.bottom == other.bottom;
        }
        public override bool Equals(object obj)
        {
            if(obj is ERect other)
            {
                return this.left == other.left && this.right == other.right && this.top == other.top && this.bottom == other.bottom;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return left ^ top ^ right ^ bottom;
        }

        /// <summary>
        /// 以字符串的方式返回结构数据
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(32);
            sb.Append(nameof(left));
            sb.Append(':');
            sb.Append(left.ToString());
            sb.Append(',');
            sb.Append(nameof(top));
            sb.Append(':');
            sb.Append(top.ToString());
            sb.Append(',');
            sb.Append(nameof(right));
            sb.Append(':');
            sb.Append(right.ToString());
            sb.Append(',');
            sb.Append(nameof(bottom));
            sb.Append(':');
            sb.Append(bottom.ToString());
            return sb.ToString();
        }
        #endregion

        #endregion

    }

    #endregion

}
