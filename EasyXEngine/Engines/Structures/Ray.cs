using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;

using Cheng.EasyX.DataStructure;
using Cheng.Algorithm.HashCodes;
using Cheng.DataStructure.Cherrsdinates;

namespace Cheng.EasyXEngine.Structures
{

    /// <summary>
    /// 平面射线
    /// </summary>
    public struct Ray : IEquatable<Ray>, IHashCode64
    {

        #region 构造

        /// <summary>
        /// 初始化射线实例
        /// </summary>
        /// <param name="origin">射线的发射点坐标</param>
        /// <param name="directionRadian">射线发射的方向，单位弧度制</param>
        public Ray(Point2 origin, double directionRadian)
        {
            this.origin = origin;
            this.directionRadian = directionRadian;
        }

        /// <summary>
        /// 初始化射线实例
        /// </summary>
        /// <param name="x">射线的发射点x坐标</param>
        /// <param name="y">射线的发射点y坐标</param>
        /// <param name="directionRadian">射线发射的方向，单位弧度制</param>
        public Ray(double x, double y, double directionRadian)
        {
            this.origin = new Point2(x, y);
            this.directionRadian = directionRadian;
        }

        #endregion

        #region 参数

        /// <summary>
        /// 一弧度角的值
        /// </summary>
        public const double OneRadian = System.Math.PI / 180;

        /// <summary>
        /// 发出射线的发射点坐标
        /// </summary>
        public readonly Point2 origin;

        /// <summary>
        /// 要发射的方向所在角度，单位为弧度制
        /// </summary>
        public readonly double directionRadian;

        #endregion

        #region 功能

        #region 参数访问

        /// <summary>
        /// 射线要发射的方向，单位为角度制
        /// </summary>
        public double DirectionAngle
        {
            get => directionRadian / OneRadian;
        }

        #endregion

        #region 运算符

        /// <summary>
        /// 比较相等
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        public static bool operator ==(Ray r1, Ray r2)
        {
            return r1.origin == r2.origin && r1.directionRadian == r2.directionRadian;
        }

        /// <summary>
        /// 比较不相等
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        public static bool operator !=(Ray r1, Ray r2)
        {
            return r1.origin != r2.origin || r1.directionRadian != r2.directionRadian;
        }

        #endregion

        #region 派生接口

        public bool Equals(Ray other)
        {
            return this.directionRadian == other.directionRadian && this.origin == other.origin;
        }

        public override bool Equals(object obj)
        {
            if(obj is Ray other)
            {
                return this.directionRadian == other.directionRadian && this.origin == other.origin;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return origin.GetHashCode() ^ directionRadian.GetHashCode();
        }

        public long GetHashCode64()
        {
            return origin.GetHashCode64() ^ directionRadian.GetHashCode64();
        }

        /// <summary>
        /// 返回射线的字符串格式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(27);

            sb.Append("origin");
            sb.Append(':');
            sb.Append(origin.ToString("G4"));
            sb.Append(' ');
            sb.Append("direction");
            sb.Append(':');
            sb.Append((directionRadian / OneRadian).ToString("G4"));
            return "";
        }

        #endregion

        #endregion

    }

}
