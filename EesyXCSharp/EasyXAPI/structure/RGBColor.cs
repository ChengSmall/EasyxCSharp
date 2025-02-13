using Cheng.EasyX.CPP;
using Cheng.EasyX.Exceptions;
using System;
using System.Runtime.InteropServices;

namespace Cheng.EasyX.DataStructure
{

    #region 颜色

    /// <summary>
    /// 一个RGB颜色结构
    /// </summary>
    /// <remarks>你可以使用<see cref="ColorPreset"/>类获取颜色预设</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RGBColor : IEquatable<RGBColor>
    {
        //0xAABBGGRR结构

        #region 构造
        /// <summary>
        /// 使用4字节颜色值初始化rgb颜色结构
        /// </summary>
        /// <param name="value">颜色值，位顺序是ABGR</param>
        public RGBColor(uint value)
        {
            colorValue = value;
        }
        /// <summary>
        /// 初始化rgb颜色结构
        /// </summary>
        /// <remarks>默认alpha值是255</remarks>
        /// <param name="r">r色值</param>
        /// <param name="g">g色值</param>
        /// <param name="b">b色值</param>
        public RGBColor(byte r, byte g, byte b)
        {
            colorValue = (0xFF000000) | ((uint)b << 16) | ((uint)g << 8) | r;
        }
        /// <summary>
        /// 初始化rgb颜色结构
        /// </summary>
        /// <param name="r">r色值</param>
        /// <param name="g">g色值</param>
        /// <param name="b">b色值</param>
        /// <param name="a">a色值</param>
        public RGBColor(byte r, byte g, byte b, byte a)
        {
            colorValue = ((uint)a << 24) | ((uint)b << 16) | ((uint)g << 8) | r;
        }
        #endregion

        #region 参数
        /// <summary>
        /// easyx颜色值
        /// </summary>
        /// <remarks>
        /// 此值是easyx库和windows操作系统所使用的4字节颜色值，可和原api的RGB宏返回的值等价交换；<br/>
        /// 此值的颜色分量在16进制数中表示为0xAABBGGRR
        /// </remarks>
        public readonly uint colorValue;
        #endregion

        #region 功能

        #region 参数访问

        /// <summary>
        /// 访问alpha值
        /// </summary>
        /// <remarks>
        /// 这是一个rgb颜色的额外参数，处于4字节颜色值的最后25 ~ 32位，用来表示透明度，0则完全透明，255为完全不透明
        /// <para>在easyx库中，颜色参数会忽略alpha值</para>
        /// </remarks>
        public byte A
        {
            get => (byte)((colorValue >> 24));
        }

        /// <summary>
        /// 访问r值
        /// </summary>
        public byte R
        {
            get => (byte)(colorValue & 0xFF);
        }

        /// <summary>
        /// 访问g值
        /// </summary>
        public byte G
        {
            get => (byte)((colorValue >> 8) & 0xFF);
        }

        /// <summary>
        /// 访问b值
        /// </summary>
        public byte B
        {
            get => (byte)((colorValue >> 16) & 0xFF);
        }

        /// <summary>
        /// 获取该实例的rgb颜色值
        /// </summary>
        /// <param name="r">r颜色值</param>
        /// <param name="g">g颜色值</param>
        /// <param name="b">b颜色值</param>
        public void GetRGB(out byte r, out byte g, out byte b)
        {
            r = (byte)(colorValue & 0xFF);
            g = (byte)((colorValue >> 8) & 0xFF);
            b = (byte)((colorValue >> 16) & 0xFF);
        }

        /// <summary>
        /// 获取该实例的rgba颜色值
        /// </summary>
        /// <param name="r">r颜色值</param>
        /// <param name="g">g颜色值</param>
        /// <param name="b">b颜色值</param>
        /// <param name="a">alpha颜色值</param>
        public void GetRGBA(out byte r, out byte g, out byte b, out byte a)
        {
            r = (byte)(colorValue & 0xFF);
            g = (byte)((colorValue >> 8) & 0xFF);
            b = (byte)((colorValue >> 16) & 0xFF);
            a = (byte)((colorValue >> 24) & 0xFF);
        }

        /// <summary>
        /// 返回一个重新设置R分量的新值
        /// </summary>
        /// <param name="value">要设置的新颜色值</param>
        /// <returns>新值</returns>
        public RGBColor SetR(byte value)
        {
            return new RGBColor((colorValue & 0xFFFFFF00) | (byte)(value));
        }
        /// <summary>
        /// 返回一个重新设置G分量的新值
        /// </summary>
        /// <param name="value">要设置的新颜色值</param>
        /// <returns>新值</returns>
        public RGBColor SetG(byte value)
        {
            return new RGBColor((colorValue & 0xFFFF00FF) | (byte)(value << 8));
        }
        /// <summary>
        /// 返回一个重新设置B分量的新值
        /// </summary>
        /// <param name="value">要设置的新颜色值</param>
        /// <returns>新值</returns>
        public RGBColor SetB(byte value)
        {
            return new RGBColor((colorValue & 0xFF00FFFF) | (byte)(value << 16));
        }
        /// <summary>
        /// 返回一个重新设置A分量的新值
        /// </summary>
        /// <param name="value">要设置的新颜色值</param>
        /// <returns>新值</returns>
        public RGBColor SetA(byte value)
        {
            return new RGBColor((colorValue & 0x00FFFFFF) | (byte)(value << 24));
        }
        /// <summary>
        /// 返回一个保持alpha并重新设置rgb颜色的新值
        /// </summary>
        /// <param name="r">要设置的r色值</param>
        /// <param name="g">要设置的g色值</param>
        /// <param name="b">要设置的b色值</param>
        /// <returns>新值</returns>
        public RGBColor SetRGB(byte r, byte g, byte b)
        {
            return new RGBColor(r, g, b, ((byte)(colorValue >> 24)));
        }

        #endregion

        #region 颜色转化

        /// <summary>
        /// 返回一个将蓝色值和红色值交换的新实例
        /// </summary>
        public RGBColor ToBGR
        {
            get
            {
                uint value = colorValue;
                return new RGBColor(((((value) & 0xFF) << 16) | ((value) & 0xFF00FF00) | (((value) & 0xFF0000) >> 16)));
            }
        }

        /// <summary>
        /// 将HSL颜色转化为rgb颜色
        /// </summary>
        /// <param name="H">
        /// H分量，色相；区间在[0,360)
        /// </param>
        /// <param name="S">
        /// S分量，饱和度；区间在[0,1]
        /// </param>
        /// <param name="L">
        /// L分量，亮度；区间在[0,1]
        /// </param>
        /// <returns>转化后的RGB颜色结构</returns>
        /// <exception cref="ArgumentOutOfRangeException">参数范围超出HSL颜色模型</exception>
        public static RGBColor HSLtoRGB(float H, float S, float L)
        {
            if (H < 0f || H > 360f || S < 0f || S > 1f || L < 0f || L > 1f) throw new ArgumentOutOfRangeException();

            return EasyX_API.HSLtoRGB_(H, S, L);
        }

        /// <summary>
        /// 将HSV颜色转化为rgb颜色
        /// </summary>
        /// <param name="H">
        /// H分量，色相；区间在[0,360)
        /// </param>
        /// <param name="S">
        /// S分量，饱和度；区间在[0,1]
        /// </param>
        /// <param name="V">
        /// V分量，明度；区间在[0,1]
        /// </param>
        /// <returns>转化后的RGB颜色结构</returns>
        /// <exception cref="ArgumentOutOfRangeException">参数范围超出HSV颜色模型</exception>
        public static RGBColor HSVtoRGB(float H, float S, float V)
        {
            if (H < 0f || H > 360f || S < 0f || S > 1f || V < 0f || V > 1f) throw new ArgumentOutOfRangeException();

            return EasyX_API.HSVtoRGB_(H, S, V);
        }

        /// <summary>
        /// 将rgb颜色转化到HSL颜色结构
        /// </summary>
        /// <param name="H">H分量，色相；区间在[0,360)</param>
        /// <param name="S">S分量，饱和度；区间在[0,1]</param>
        /// <param name="L">L分量，亮度；区间在[0,1]</param>
        public void ToHSL(out float H, out float S, out float L)
        {
            float h, s, l;
            EasyX_API.RGBtoHSL_(this, &h, &s, &l);
            H = h;
            S = s;
            L = l;
        }

        /// <summary>
        /// 将rgb颜色转化到HSV颜色结构
        /// </summary>
        /// <param name="H">
        /// H分量，色相；区间在[0,360)
        /// </param>
        /// <param name="S">
        /// S分量，饱和度；区间在[0,1]
        /// </param>
        /// <param name="V">
        /// V分量，明度；区间在[0,1]
        /// </param>
        public void ToHSV(out float H, out float S, out float V)
        {
            float h, s, v;
            EasyX_API.RGBtoHSV_(this, &h, &s, &v);
            H = h;
            S = s;
            V = v;
        }

        /// <summary>
        /// 返回与之对应的灰度值颜色
        /// </summary>
        /// <returns>对应的灰度颜色</returns>
        public RGBColor ToGRAY()
        {
            return EasyX_API.RGBtoGRAY_(this);
        }

        /// <summary>
        /// 返回一个舍去透明度分量<see cref="A"/>的颜色结构
        /// </summary>
        public RGBColor OnlyRGB
        {
            get
            {
                return new RGBColor(colorValue & 0x00FFFFFF);
            }
        }

        #region 文本颜色值转化

        /// <summary>
        /// 将字符串颜色编码转化为颜色结构
        /// </summary>
        /// <param name="rgbText">
        /// rgb颜色文本，可以使用#为开头，使用16进制整形表示为色值，格式为#AARRGGBB
        /// </param>
        /// <returns>转化后的颜色值</returns>
        /// <exception cref="ArgumentNullException">参数是null</exception>
        /// <exception cref="ArgumentException">参数错误</exception>
        /// <exception cref="FormatException">参数格式错误</exception>
        public static RGBColor TextToColor(string rgbText)
        {
            if (rgbText is null) throw new ArgumentNullException();

            if (rgbText.Length == 0) throw new ArgumentException();

            if((rgbText.Length > 1) && rgbText[0] == '#')
            {
                rgbText = rgbText.Substring(1);
            }

            return (new RGBColor(Convert.ToUInt32(rgbText, 16))).ToBGR;
        }

        /// <summary>
        /// 返回RGB颜色编码 #AARRGGBB
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return '#'.ToString() + ToBGR.colorValue.ToString("X8").ToUpper();
        }
        #endregion

        /// <summary>
        /// 返回一个将透明度分量<see cref="A"/>设为最大值的颜色结构
        /// </summary>
        public RGBColor OpaqueRGB
        {
            get
            {
                return new RGBColor(colorValue | 0xFF000000);
            }
        }

        /// <summary>
        /// 返回一个新透明度的rgb颜色
        /// </summary>
        /// <param name="transparency">要设置的新透明度，范围在0~1</param>
        /// <returns>新透明度的颜色</returns>
        public RGBColor Transparency(float transparency)
        {
            byte r, g, b, a;
            GetRGB(out r, out g, out b);
            a = (byte)(transparency * 255f);
            return new RGBColor(r, g, b, a);
        }

        /// <summary>
        /// 返回一个新透明度的rgb颜色
        /// </summary>
        /// <param name="a">透明度分量</param>
        /// <returns>新透明度的颜色</returns>
        public RGBColor Transparency(byte a)
        {
            byte r, g, b;
            GetRGB(out r, out g, out b);
            return new RGBColor(r, g, b, a);
        }

        /// <summary>
        /// 根据给定的背景色融合成新颜色
        /// </summary>
        /// <remarks>
        /// 混色公式：
        /// <code>
        /// c = a * g + (1 - a) * b
        /// c 是混合后的颜色
        /// g 是前景色（你的原始颜色）
        /// b 是背景色（你希望混合进去的颜色）
        /// a是前景色的透明度（范围是0到1，其中0是完全透明，1是完全不透明）
        /// </code>
        /// </remarks>
        /// <param name="bkcolor">要融合的背景色</param>
        /// <returns>
        /// 通过该颜色的透明度分量 <see cref="A"/> ，根据背景色 <paramref name="bkcolor"/> 计算并融合的新颜色
        /// </returns>
        public RGBColor ColorSynthesis(RGBColor bkcolor)
        {

            byte a;
            byte r, g, b, br, bg, bb;

            GetRGBA(out r, out g, out b, out a);

            if (a == 0) return bkcolor;
            if (a == byte.MaxValue) return this;
            float als = a / 255f;

            bkcolor.GetRGB(out br, out bg, out bb);

            return new RGBColor((byte)(als * r + (1f - als) * br), (byte)(als * g + (1f - als) * bg), (byte)(als * b + (1f - als) * bb));
        }

        #endregion

        #region 系统

        /// <summary>
        /// 访问或设置当前设备背景色
        /// </summary>
        /// <exception cref="WindowEasyXException">未初始化窗口</exception>
        public static RGBColor BKColor
        {
            get
            {
                Device.f_testNotInitGraph(Device.exc_winNotInit);
                return EasyX_API.getbkcolor_().OpaqueRGB;
            }
            set
            {
                Device.f_testNotInitGraph(Device.exc_winNotInit);
                EasyX_API.setbkcolor_(value.OnlyRGB);
            }
        }

        /// <summary>
        /// 访问或设置当前设备填充色
        /// </summary>
        /// <exception cref="WindowEasyXException">未初始化窗口</exception>
        public static RGBColor FillColor
        {
            get
            {
                Device.f_testNotInitGraph(Device.exc_winNotInit);
                return EasyX_API.getfillcolor_().OpaqueRGB;
            }
            set
            {
                Device.f_testNotInitGraph(Device.exc_winNotInit);
                EasyX_API.setfillcolor_(value.OnlyRGB);
            }
        }

        /// <summary>
        /// 访问或设置当前设备画线色
        /// </summary>
        /// <exception cref="WindowEasyXException">窗口未初始化</exception>
        public static RGBColor LineColor
        {
            get
            {
                Device.f_testNotInitGraph(Device.exc_winNotInit);
                return EasyX_API.getlinecolor_().OpaqueRGB;
            }
            set
            {
                Device.f_testNotInitGraph(Device.exc_winNotInit);
                EasyX_API.setlinecolor_(value.OnlyRGB);
            }
        }

        /// <summary>
        /// 访问或设置当前设备字体颜色
        /// </summary>
        /// <exception cref="WindowEasyXException">窗口未初始化</exception>
        public static RGBColor TextColor
        {
            get
            {
                Device.f_testNotInitGraph(Device.exc_winNotInit);
                return EasyX_API.gettextcolor_().OpaqueRGB;
            }
            set
            {
                Device.f_testNotInitGraph(Device.exc_winNotInit);
                EasyX_API.settextcolor_(value.OnlyRGB);
            }
        }

        #endregion

        #region 派生

        /// <summary>
        /// 比较是否相等
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(RGBColor other)
        {
            return colorValue == other.colorValue;
        }
        public override bool Equals(object obj)
        {
            if(obj is RGBColor c)
            {
                return colorValue == c.colorValue;
            }
            return false;
        }
        /// <summary>
        /// RGB的哈希码
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (int)colorValue;
        }
        /// <summary>
        /// 比较是否相等
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static bool operator ==(RGBColor c1, RGBColor c2)
        {
            return c1.colorValue == c2.colorValue;
        }
        /// <summary>
        /// 比较是否不相等
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static bool operator !=(RGBColor c1, RGBColor c2)
        {
            return c1.colorValue != c2.colorValue;
        }

        #endregion

        #endregion

    }

    #endregion

}
