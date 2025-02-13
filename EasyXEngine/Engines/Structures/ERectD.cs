using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Cheng.Algorithm.HashCodes;

using CT = System.Double;
using POINT = Cheng.DataStructure.Cherrsdinates.Point2;
using RECT = Cheng.EasyX.DataStructure.ERectD;

namespace Cheng.EasyX.DataStructure
{

    /// <summary>
    /// 一个双精度浮点矩形
    /// </summary>
    public struct ERectD : IEquatable<ERectD>, IHashCode64
    {

        #region 构造

        /// <summary>
        /// 初始化矩形
        /// </summary>
        /// <param name="x">左上角x坐标位置</param>
        /// <param name="y">左上角y坐标位置</param>
        /// <param name="width">x坐标方向长度</param>
        /// <param name="height">y坐标方向长度</param>
        public ERectD(CT x, CT y, CT width, CT height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// 初始化矩形
        /// </summary>
        /// <param name="leftUp">左上角坐标位置</param>
        /// <param name="width">x坐标方向长度</param>
        /// <param name="height">y坐标方向长度</param>
        public ERectD(POINT leftUp, CT width, CT height)
        {
            this.x = leftUp.x;
            this.y = leftUp.y;
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// 初始化矩形
        /// </summary>
        /// <param name="leftUp">左上角坐标位置</param>
        /// <param name="rightDown">右下角坐标位置</param>
        public ERectD(POINT leftUp, POINT rightDown)
        {
            this.x = leftUp.x;
            this.y = leftUp.y;

            this.width = rightDown.x - this.x;
            this.height = rightDown.y - this.y;
        }

        #endregion

        #region 参数

        /// <summary>
        /// 左上角x位置
        /// </summary>
        public readonly CT x;

        /// <summary>
        /// 左上角y位置
        /// </summary>
        public readonly CT y;

        /// <summary>
        /// 矩形长度
        /// </summary>
        public readonly CT width;

        /// <summary>
        /// 矩形高度
        /// </summary>
        public readonly CT height;

        #endregion

        #region 功能

        #region 参数访问

        /// <summary>
        /// 获取矩形的中间点
        /// </summary>
        public POINT Mid
        {
            get
            {
                return new POINT(x + (width / 2), y + (height / 2));
            }
        }

        /// <summary>
        /// 获取矩形的中间点的x坐标
        /// </summary>
        public CT MidX
        {
            get => x + (width / 2);
        }

        /// <summary>
        /// 获取矩形的中间点的y坐标
        /// </summary>
        public CT MidY
        {
            get => y + (height / 2);
        }

        /// <summary>
        /// 获取矩形的左上角坐标
        /// </summary>
        public POINT LeftUp
        {
            get => new POINT(x, y);
        }

        /// <summary>
        /// 获取矩形的左下角坐标
        /// </summary>
        public POINT LeftDown
        {
            get => new POINT(x, y + height);
        }

        /// <summary>
        /// 获取矩形的右上角坐标
        /// </summary>
        public POINT RightUp
        {
            get => new POINT(x + width, y);
        }

        /// <summary>
        /// 获取矩形的右下角坐标
        /// </summary>
        public POINT RightDown
        {
            get => new POINT(x + width, y + height);
        }

        #endregion

        #region 实例化

        /// <summary>
        /// 使用矩形中点初始化矩形
        /// </summary>
        /// <param name="mid">矩形的中点坐标</param>
        /// <param name="width">矩形的x坐标长度</param>
        /// <param name="height">矩形的y坐标高度</param>
        /// <returns>新矩形实例</returns>
        public static RECT MidToRect(POINT mid, CT width, CT height)
        {
            return new RECT(mid.x - (width / 2), mid.y - (height / 2), width, height);
        }

        /// <summary>
        /// 使用反向角落坐标初始化矩形
        /// </summary>
        /// <param name="rightUp">矩形的右上角坐标</param>
        /// <param name="leftDown">矩形的左下角坐标</param>
        /// <returns></returns>
        public static RECT ReversePointToRect(POINT rightUp, POINT leftDown)
        {
            return new RECT(new POINT(leftDown.x, rightUp.y), new POINT(rightUp.x, leftDown.y));
        }

        #endregion

        #region 参数转化

        /// <summary>
        /// 将当前矩形进行垂直镜像翻转并返回翻转后的矩形
        /// </summary>
        /// <remarks>
        /// 以矩形中心点的x轴作为对称轴，沿其进行180度反转，总体位置和大小保持不变
        /// </remarks>
        /// <returns>转化后的矩形</returns>
        public RECT VerticalMirrorFlip()
        {
            return new RECT(LeftDown, RightUp);
        }

        /// <summary>
        /// 将当前矩形进行水平镜像翻转并返回翻转后的矩形
        /// </summary>
        /// <remarks>
        /// 以矩形中心点的y轴作为对称轴，沿其进行180度翻转，总体位置和大小保持不变
        /// </remarks>
        /// <returns>转化后的矩形</returns>
        public RECT HorizontalMirrorFlip()
        {
            return new RECT(RightUp, LeftDown);
        }

        /// <summary>
        /// 将矩形进行中心对称旋转180度并返回
        /// </summary>
        public RECT ToReverse()
        {
            return new RECT(RightDown, LeftUp);
        }

        /// <summary>
        /// 重新设置左上角坐标后的矩形
        /// </summary>
        /// <remarks>
        /// 固定当前矩形的右下角坐标，并指定左上角坐标实例化的新矩形
        /// </remarks>
        /// <param name="leftUp">左上角坐标</param>
        /// <returns>新的矩形</returns>
        public RECT SetLeftUp(POINT leftUp)
        {
            return new RECT(leftUp, RightDown);
        }

        /// <summary>
        /// 重新设置左下角坐标后的矩形
        /// </summary>
        /// <remarks>
        /// 固定当前矩形的右上角坐标，并指定左下角坐标实例化的新矩形
        /// </remarks>
        /// <param name="leftDown">左下角坐标</param>
        /// <returns>新的矩形</returns>
        public RECT SetLeftDown(POINT leftDown)
        {
            var rightUp = RightUp;
            return new RECT(new POINT(leftDown.x, rightUp.y), new POINT(rightUp.x, leftDown.y));
        }

        /// <summary>
        /// 重新设置右上角坐标后的矩形
        /// </summary>
        /// <remarks>
        /// 固定当前矩形的左下角坐标，并指定右上角坐标实例化的新矩形
        /// </remarks>
        /// <param name="rightUp">右上角坐标</param>
        /// <returns>新的矩形</returns>
        public RECT SetRightUp(POINT rightUp)
        {
            POINT leftDown = LeftDown;
            return new RECT(new POINT(leftDown.x, rightUp.y), new POINT(rightUp.x, leftDown.y));
        }

        /// <summary>
        /// 重新设置右上角坐标后的矩形
        /// </summary>
        /// <remarks>
        /// 固定当前矩形的左下角坐标，并指定右上角坐标实例化的新矩形
        /// </remarks>
        /// <param name="rightDown">右上角坐标</param>
        /// <returns>新的矩形</returns>
        public RECT SetRightDown(POINT rightDown)
        {
            return new RECT(x, y, rightDown.x - x, rightDown.y - y);
        }

        /// <summary>
        /// 获取左侧边缘的x坐标
        /// </summary>
        public CT Left
        {
            get => x;
        }

        /// <summary>
        /// 获取顶部边缘的y坐标
        /// </summary>
        public CT Top
        {
            get => y;
        }

        /// <summary>
        /// 获取右侧边缘的x坐标
        /// </summary>
        public CT Right
        {
            get => x + width;
        }

        /// <summary>
        /// 获取底部边缘的y坐标
        /// </summary>
        public CT Bottom
        {
            get => y + height;
        }

        #endregion

        #region 运算符

        /// <summary>
        /// 比较相等
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        public static bool operator ==(RECT r1, RECT r2)
        {
            return r1.x == r2.x && r1.y == r2.y && r1.width == r2.width && r1.height == r2.height;
        }

        /// <summary>
        /// 比较不相等
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        public static bool operator !=(RECT r1, RECT r2)
        {
            return r1.x != r2.x || r1.y != r2.y || r1.width != r2.width || r1.height != r2.height;
        }

        #endregion

        #region 派生接口

        public override bool Equals(object obj)
        {
            if (obj is RECT other)
            {
                return this.x == other.x && this.y == other.y && this.width == other.width && this.height == other.height;
            }
            return false;
        }

        public bool Equals(RECT other)
        {
            return this.x == other.x && this.y == other.y && this.width == other.width && this.height == other.height;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ width.GetHashCode() ^ height.GetHashCode();
        }

        public long GetHashCode64()
        {
            return x.GetHashCode64() ^ y.GetHashCode64() ^ width.GetHashCode64() ^ height.GetHashCode64();
        }

        /// <summary>
        /// 返回当前实例的字符串格式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(36);
            sb.Append('x');
            sb.Append(':');
            sb.Append(x.ToString("G4"));
            sb.Append(' ');
            sb.Append('y');
            sb.Append(':');
            sb.Append(y.ToString("G4"));
            sb.Append(nameof(width));
            sb.Append(':');
            sb.Append(width.ToString("G4"));
            sb.Append(' ');
            sb.Append(nameof(height));
            sb.Append(':');
            sb.Append(height.ToString("G4"));

            return sb.ToString();
        }

        #endregion

        #endregion

    }


}
