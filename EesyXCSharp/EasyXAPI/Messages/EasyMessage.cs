using Cheng.EasyX.DataStructure;
using System;
using System.Threading;

namespace Cheng.EasyX.Messages
{

    /// <summary>
    /// 表示一个按键键码
    /// </summary>
    public struct KeyCode : IEquatable<KeyCode>
    {

        /// <summary>
        /// 初始化按键键码
        /// </summary>
        /// <param name="code">虚拟键码</param>
        /// <param name="scanCode">按键扫描码</param>
        public KeyCode(VkCode code, byte scanCode)
        {
            this.code = code;
            this.scanCode = scanCode;
        }

        /// <summary>
        /// 虚拟键码
        /// </summary>
        public readonly VkCode code;
        /// <summary>
        /// 按键扫描码
        /// </summary>
        public readonly byte scanCode;

        /// <summary>
        /// 比较相等
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static bool operator ==(KeyCode c1, KeyCode c2)
        {
            return c1.code == c2.code && c1.scanCode == c2.scanCode;
        }

        /// <summary>
        /// 比较不相等
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static bool operator !=(KeyCode c1, KeyCode c2)
        {
            return c1.code != c2.code || c1.scanCode != c2.scanCode;
        }

        public override bool Equals(object obj)
        {
            if(obj is KeyCode other)
            {
                return code == other.code && scanCode == other.scanCode;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return (int)code | ((int)scanCode << 8);
        }

        public bool Equals(KeyCode other)
        {
            return code == other.code && scanCode == other.scanCode;
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(32);

            sb.Append("VkCode:");
            sb.Append(code.ToString());
            sb.Append(' ');
            sb.Append("scanCode:");
            sb.Append(scanCode.ToString("x").ToUpper());
            return sb.ToString();
        }

    }

}
