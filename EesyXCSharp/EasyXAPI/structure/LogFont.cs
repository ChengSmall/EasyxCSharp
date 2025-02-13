using System;
using System.Runtime.InteropServices;


namespace Cheng.EasyX.DataStructure
{


    /// <summary>
    /// 字符集宏
    /// </summary>
    public enum LogFontCharSet : byte
    {
        ANSI = 0,

        BALTIC = 186,
        CHINESEBIG5 = 136,
        DEFAULT = 1,
        EASTEUROPE = 238,

        GB2312 = 134,
        GREEK = 161,
        HANGUL = 129,
        MAC = 77,
        OEM = 255,
        RUSSIAN = 204,
        SHIFTJIS = 128,
        SYMBOL = 2,

        TURKISH_CHARSET = 162
    }


    /// <summary>
    /// 文字样式
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct LogFont
    {

        #region 参数
        /// <summary>
        /// 高度
        /// </summary>
        public int lfHeight;
        /// <summary>
        /// 指定字符的平均宽度。如果为 0，则比例自适应
        /// </summary>
        public int lfWidth;
        /// <summary>
        /// 字符串的书写角度，单位 0.1 度，默认为 0
        /// </summary>
        public int lfEscapement;
        /// <summary>
        /// 每个字符的书写角度，单位 0.1 度，默认为 0
        /// </summary>
        public int lfOrientation;
        /// <summary>
        /// 字符的笔画粗细，范围 [0,1000]，0 表示默认粗细
        /// </summary>
        public int lfWeight;
        /// <summary>
        /// 字体是否是斜体
        /// </summary>
        public bool lfItalic;
        /// <summary>
        /// 字体是否有下划线
        /// </summary>
        public bool lfUnderline;
        /// <summary>
        /// 字体是否有删除线
        /// </summary>
        public bool lfStrikeOut;
        /// <summary>
        /// 指定字符集
        /// </summary>
        public LogFontCharSet lfCharSet;
        /// <summary>
        /// 指定文字的输出精度
        /// </summary>
        /// <remarks>
        /// 输出精度定义输出与所请求的字体高度、宽度、字符方向、行距、间距和字体类型相匹配必须达到的匹配程度
        /// </remarks>
        public byte lfOutPrecision;
        /// <summary>
        /// 指定文字的剪辑精度
        /// </summary>
        /// <remarks>剪辑精度定义如何剪辑字符的一部分位于剪辑区域之外的字符</remarks>
        public byte lfClipPrecision;
        /// <summary>
        /// 指定文字的输出质量
        /// </summary>
        /// <remarks>输出质量定义图形设备界面 (GDI) 必须尝试将逻辑字体属性与实际物理字体的字体属性进行匹配的仔细程度</remarks>
        public byte lfQuality;
        /// <summary>
        /// 指定以常规方式描述字体的字体系列
        /// </summary>
        public byte lfPitchAndFamily;
        /// <summary>
        /// 文字样式名称，此为非托管成员，第32位是'\0'，有效字符数是31位
        /// </summary>
        public fixed char lfFaceName[32];

        #endregion

        #region 访问
        /// <summary>
        /// 访问或修改文字样式名称
        /// </summary>
        /// <returns>将以'\0'为终止符的非托管字符串转化为托管数据返回</returns>
        /// <value>将字符串封送为以'\0'为终止符的非托管字符串，字符数不得大于31位</value>
        /// <exception cref="ArgumentNullException">设置的参数为null</exception>
        /// <exception cref="ArgumentException">字符串长度超出31个字符</exception>
        public string LfFaceName
        {
            get
            {
                fixed (char* cp = lfFaceName)
                {
                    return new string(cp);
                }
            }
            set
            {
                if (value is null) throw new ArgumentNullException();

                fixed (char* cp = lfFaceName)
                {
                    int length = value.Length;
                    int i;
                    if (length > 31) throw new ArgumentException("字符串长度超出31个字符");

                    for(i = 0; i < length; i++)
                    {
                        cp[i] = value[i];
                    }
                    for ( ; i < 32; i++)
                    {
                        cp[i] = '\0';
                    }

                }
                
            }
        }

        #endregion

    }

    /// <summary>
    /// 文字样式
    /// </summary>
    public class HeapLogFont
    {

        /// <summary>
        /// 实例化一个文字样式
        /// </summary>
        public HeapLogFont()
        {
            font = default;
        }

        /// <summary>
        /// 实例化一个文字样式
        /// </summary>
        /// <param name="width">字体长度</param>
        /// <param name="height">字体高度</param>
        /// <param name="lfaceName">字体名称</param>
        public HeapLogFont(int width, int height, string lfaceName)
        {
            font.lfWidth = width;
            font.lfHeight = height;
            font.LfFaceName = lfaceName;
        }

        /// <summary>
        /// 实例化一个文字样式
        /// </summary>
        public HeapLogFont(LogFont font)
        {
            this.font = font;
        }

        /// <summary>
        /// 文字样式结构
        /// </summary>
        public LogFont font;
    }


}
