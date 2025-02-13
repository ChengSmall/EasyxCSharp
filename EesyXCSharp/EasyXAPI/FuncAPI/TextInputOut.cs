using Cheng.EasyX.CPP;
using Cheng.EasyX.DataStructure;
using System;
using System.Text;

namespace Cheng.EasyX
{

    [Flags]
    public enum TextDrawFormat : uint
    {
        /// <summary>
        /// 左对齐
        /// </summary>
        DT_LEFT = 0x0,
        /// <summary>
        /// 文字水平居中
        /// </summary>
        DT_CENTER = 0x1,
        /// <summary>
        /// 文字右对齐
        /// </summary>
        DT_RIGHT = 0x2,
        /// <summary>
        /// 调整文字位置到矩形底部
        /// </summary>
        /// <remarks>仅当和 <see cref="DT_SINGLELINE"/> 一起使用时有效</remarks>
        DT_BOTTOM = 0x8,
        /// <summary>
        /// 使文字显示在一行
        /// </summary>
        /// <remarks>回车和换行符都无效</remarks>
        DT_SINGLELINE = 0x20,
        /// <summary>
        /// 检测矩形的宽高
        /// </summary>
        /// <remarks>
        /// 如果有多行文字，drawtext 使用 pRect 指定的宽度，并且扩展矩形的底部以容纳每一行文字<br/>
        /// 如果只有一行文字，drawtext 修改 pRect 的右边以容纳最后一个文字<br/>
        /// 无论哪种情况，drawtext 都返回格式化后的文字高度，并且不输出文字
        /// </remarks>
        DT_CALCRECT = 0x400,
        /// <summary>
        /// 文字垂直居中
        /// </summary>
        /// <remarks>仅当和 <see cref="DT_SINGLELINE"/> 一起使用时有效</remarks>
        DT_VCENTER = 0x4,
        /// <summary>
        /// 以单行编辑的方式复制可见文本
        /// </summary>
        /// <remarks>具体的说，就是以字符的平均宽度为计算依据，同时用这个方式应用于编辑控制，并且这种方式不显示可见部分的最后一行</remarks>
        DT_EDITCONTROL = 0x2000,
        /// <summary>
        /// 自动换行
        /// </summary>
        /// <remarks>当文字超过右边界时会自动换行(不拆开单词)。回车符同样可以换行</remarks>
        DT_WORDBREAK = 0x10,
        /// <summary>
        /// 对于文本显示，如果字符串的末字符不在矩形内，它会被截断并以省略号标识
        /// </summary>
        /// <remarks>如果是一个单词而不是一个字符，其末尾超出了矩形范围，它不会被截断；<br/>
        /// 字符串不会被修改，除非指定了 <see cref="DT_MODIFYSTRING"/> 标志。
        /// </remarks>
        DT_END_ELLIPSIS = 0x8000,
        /// <summary>
        /// 在行高里包含字体的行间距
        /// </summary>
        /// <remarks>通常情况下，行间距不被包含在正文的行高里</remarks>
        DT_EXTERNALLEADING = 0x200,
        /// <summary>
        /// 使输出文字不受rect裁剪限制
        /// </summary>
        /// <remarks>使用该值会使<see cref="TextInputOut.DrawText(string, ERect, TextDrawFormat)"/>执行稍快一些</remarks>
        DT_NOCLIP = 0x100,     
        /// <summary>
        /// 对于显示的文字，用省略号替换字符串中间的字符以便容纳于矩形内
        /// </summary>
        /// <remarks>
        /// 如果字符串包含反斜杠(\)，DT_PATH_ELLIPSIS 尽可能的保留最后一个反斜杠后面的文字<br/>
        /// 字符串不会被修改，除非指定了<see cref="DT_MODIFYSTRING"/>标志
        /// </remarks>
        DT_PATH_ELLIPSIS = 0x4000,
        /// <summary>
        /// 修改指定字符串为显示出的正文
        /// </summary>
        /// <remarks>仅当和 <see cref="DT_END_ELLIPSIS"/> 或<see cref="DT_PATH_ELLIPSIS"/> 标志同时使用时有效</remarks>
        DT_MODIFYSTRING = 0x10000,
        /// <summary>
        /// 使文字处于矩形正中心
        /// </summary>
        RectCenter = DT_CENTER | DT_VCENTER | DT_SINGLELINE,

    }

    /// <summary>
    /// 文本操作
    /// </summary>
    public unsafe static class TextInputOut
    {

        #region api

        #region 文字样式
        /// <summary>
        /// 获取或设置当前文字样式
        /// </summary>
        /// <exception cref="ArgumentNullException">设置的文字样式是null</exception>
        public static HeapLogFont TextStyle
        {
            get
            {
                Device.f_testNotInitGraph(Device.exc_winNotInit);
                var lf = new HeapLogFont();
                EasyX_API.gettextstyle_(ref lf.font);

                //fixed (LogFont* pf = &lf.font)
                //{
                //}
                return lf;
            }
            set
            {
                Device.f_testNotInitGraph(Device.exc_winNotInit);
                if (value is null) throw new ArgumentNullException();

                EasyX_API.settextstyle_4(ref value.font);

                //fixed (LogFont* pf = &value.font)
                //{
                //    EasyX_API.settextstyle_4(pf);
                //}
            }
        }

        /// <summary>
        /// 设置当前文字样式
        /// </summary>
        /// <param name="nHeight">文字高度</param>
        /// <param name="nWidth">指定字符的平均宽度。如果为 0，则比例自适应</param>
        /// <param name="lpszFace">字体名称，名称不得超过 31 个字符。如果是空字符串，系统将使用第一个满足其它属性的字体</param>
        /// <exception cref="ArgumentException">字符样式名称字符串超过31个字符</exception>
        public static void SetTextStyle(int nHeight, int nWidth, string lpszFace)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            if (lpszFace != null && lpszFace.Length > 31) throw new ArgumentException();
            EasyX_API.settextstyle_1(nHeight, nWidth, lpszFace);
        }
        /// <summary>
        /// 设置当前文字样式
        /// </summary>
        /// <param name="nHeight">文字高度</param>
        /// <param name="nWidth">指定字符的平均宽度。如果为 0，则比例自适应</param>
        /// <param name="lpszFace">字体名称，名称不得超过 31 个字符。如果是空字符串，系统将使用第一个满足其它属性的字体</param>
        /// <param name="nEscapement">字符串的书写角度，单位 0.1 度</param>
        /// <param name="nOrientation">每个字符的书写角度，单位 0.1 度，默认为 0</param>
        /// <param name="nWeight">字符的笔画粗细，范围 0~1000。0 表示默认粗细</param>
        /// <param name="bItalic">是否斜体</param>
        /// <param name="bUnderline">是否有下划线</param>
        /// <param name="bStrikeOut">是否有删除线</param>
        /// <exception cref="ArgumentException">字符样式名称字符串超过31个字符</exception>
        public static void SetTextStyle(int nHeight, int nWidth, string lpszFace, int nEscapement,
            int nOrientation, int nWeight, bool bItalic, bool bUnderline, bool bStrikeOut)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            if (lpszFace != null && lpszFace.Length > 31) throw new ArgumentException();

            EasyX_API.settextstyle_2(nHeight, nWidth, lpszFace, nEscapement, nOrientation, nWeight, bItalic, bUnderline, bStrikeOut);
        }
        /// <summary>
        /// 设置当前文字样式
        /// </summary>
        /// <param name="font"></param>
        public static void SetTextStyle(HeapLogFont font)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            if (font is null) throw new ArgumentNullException();

            EasyX_API.settextstyle_4(ref font.font);
            //fixed (LogFont* lp = &font.font)
            //{
            //    EasyX_API.settextstyle_4(lp);
            //}
        }
        #endregion

        #region 文字输出      
        /// <summary>
        /// 在指定区域内以指定格式输出字符串
        /// </summary>
        /// <param name="str">待输出字符串</param>
        /// <param name="rect">输出区域</param>
        /// <param name="format">格式</param>
        public static void DrawText(string str, ERect rect, TextDrawFormat format)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            if (str is null) throw new ArgumentNullException();
            EasyX_API.drawtext_(str, &rect, (uint)format);
        }
        /// <summary>
        /// 在指定区域内以指定格式输出字符
        /// </summary>
        /// <param name="c">待输出字符</param>
        /// <param name="rect">输出区域</param>
        /// <param name="format">格式</param>
        public static void DrawText(char c, ERect rect, TextDrawFormat format)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            EasyX_API.drawtext_(c, &rect, (uint)format);
        }
        /// <summary>
        /// 在指定位置输出字符串
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <param name="str">要输出的字符串</param>
        /// <exception cref="ArgumentNullException">字符串为null</exception>
        public static void OutText(int x, int y, string str)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            if (str is null) throw new ArgumentNullException();
            EasyX_API.outtextxy_1(x, y, str);
        }
        /// <summary>
        /// 在指定位置输出的坐标字符
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <param name="c">要输出的字符</param>
        public static void OutText(int x, int y, char c)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            EasyX_API.outtextxy_2(x, y, c);
        }
        /// <summary>
        /// 在指定位置输出字符串
        /// </summary>
        /// <param name="point">坐标</param>
        /// <param name="str">要输出的字符串</param>
        /// <exception cref="ArgumentNullException">字符串为null</exception>
        public static void OutText(EPoint point, string str)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            if (str is null) throw new ArgumentNullException();
            EasyX_API.outtextxy_1(point.x, point.y, str);
        }
        /// <summary>
        /// 在指定位置输出字符串
        /// </summary>
        /// <param name="point">坐标</param>
        /// <param name="c">要输出的字符</param>
        public static void OutText(EPoint point, char c)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            EasyX_API.outtextxy_2(point.x, point.y, c);
        }
        #endregion

        #region 文字输入

        /// <summary>
        /// 以暂停线程的对话框窗口形式获取用户输入
        /// </summary>
        /// <param name="output">指定接收用户输入的字符串的引用</param>
        /// <param name="maxCount">
        /// 指定要获取的用户输入最大字符数，该值会限制用户输入内容的长度
        /// <para>
        /// 当允许多行输入时，用户键入的回车占两个字符位置
        /// </para></param>
        /// <param name="Prompt">
        /// <para>指定显示在对话框中的提示信息。提示信息中可以用“\n”分行</para>
        /// <para>InputBox 的高度会随着提示信息内容的多少自动扩充。如果该值为 null，则不显示提示信息</para>
        /// </param>
        /// <param name="title">指定 InputBox 的标题栏。如果为 null，将显示应用程序的名称</param>
        /// <param name="def">指定显示在用户输入区的默认值</param>
        /// <param name="width">指定 InputBox 的宽度（不包括边框），最小为 200 像素。如果为 0，则使用默认宽度</param>
        /// <param name="height">
        /// 指定 InputBox 的高度（不包括边框）
        /// <para>如果为 0，表示自动计算高度，用户输入框只允许输入一行内容，按“回车”确认输入信息；<br/>
        /// 如果大于 0，用户输入框的高度会自动拓展，同时允许输入多行内容，按“Ctrl+回车”确认输入信息
        /// </para>
        /// </param>
        /// <param name="hideCancelBtn">指定是否隐藏取消按钮禁止用户取消输入</param>
        /// <returns>返回用户是否输入信息。如果用户按“确定”，返回 true；如果用户按“取消”，返回 false，且字符串不会接收输入</returns>
        public static bool InputBox(out string output, int maxCount, string Prompt, string title, string def, int width, int height, bool hideCancelBtn)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            StringBuilder sb = new StringBuilder();
            bool flag = EasyX_API.InputBox_(sb, maxCount, Prompt, title, def, width, height, hideCancelBtn);
            if (flag) output = sb.ToString();
            else output = null;
            return flag;
        }

        /// <summary>
        /// 以暂停线程的对话框窗口形式获取用户输入
        /// </summary>
        /// <param name="output">接收用户输入的字符串的引用</param>
        /// <param name="maxCount">最大接收字符数，该值也会充当缓冲区大小</param>
        /// <param name="title">对话框标题</param>
        /// <param name="Prompt">对话框中的信息，该值为null则不显示信息</param>
        /// <param name="hideCancelBtn">指定是否隐藏取消按钮禁止用户取消输入</param>
        /// <returns>返回用户是否输入信息。如果用户按“确定”，返回 true；如果用户按“取消”，返回 false</returns>
        public static bool InputBox(out string output, int maxCount, string title, string Prompt, bool hideCancelBtn)
        {
            return InputBox(out output, maxCount, Prompt, title, string.Empty, 0, 0, hideCancelBtn);
        }

        /// <summary>
        /// 以暂停线程的对话框窗口形式获取用户输入
        /// </summary>
        /// <param name="output">接收用户输入的字符串的引用</param>
        /// <param name="maxCount">最大接收字符数，该值也会充当临时缓冲区大小</param>
        /// <param name="title">对话框标题</param>
        /// <param name="hideCancelBtn">指定是否隐藏取消按钮禁止用户取消输入</param>
        /// <returns>返回用户是否输入信息。如果用户按“确定”，返回 true；如果用户按“取消”，返回 false</returns>
        public static bool InputBox(out string output, int maxCount, string title, bool hideCancelBtn)
        {
            return InputBox(out output, maxCount, null, title, string.Empty, 0, 0, hideCancelBtn);
        }

        /// <summary>
        /// 以暂停线程的对话框窗口形式获取用户输入
        /// </summary>
        /// <param name="maxCount">最大接收字符数，该值也会充当临时缓冲区大小</param>
        /// <param name="title">对话框标题</param>
        /// <returns>返回用户输入的字符串</returns>
        public static string InputBox(int maxCount, string title)
        {
            string str;
            InputBox(out str, maxCount, null, title, string.Empty, 0, 0, true);
            return str;
        }

        /// <summary>
        /// 以暂停线程的对话框窗口形式获取用户输入
        /// </summary>
        /// <param name="buffer">接收用户输入的缓冲区</param>
        /// <param name="maxCount">最大获取字符数</param>
        /// <param name="title">对话框标题</param>
        /// <param name="Prompt">对话框提示信息</param>
        /// <param name="hideCancel">指定是否隐藏取消按钮禁止用户取消输入</param>
        /// <returns>返回用户是否输入信息。如果用户按“确定”，返回 true；如果用户按“取消”，返回 false</returns>
        /// <exception cref="ArgumentNullException">参数为null</exception>
        public static bool InputBox(StringBuilder buffer, int maxCount, string title, string Prompt, bool hideCancel)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            if (buffer is null) throw new ArgumentNullException();
            if (maxCount < 0) throw new ArgumentOutOfRangeException();

            int cap = buffer.Capacity;
            if (cap < maxCount)
            {
                cap *= 2;
                buffer.Capacity = (cap > maxCount) ? cap : maxCount;
            }

            return EasyX_API.InputBox_(buffer, maxCount, Prompt, title, null, 0, 0, hideCancel);
        }

        /// <summary>
        /// 以暂停线程的对话框窗口形式获取用户输入
        /// </summary>
        /// <param name="buffer">接收用户输入的字符缓冲区</param>
        /// <param name="maxCount">用户可在文本框中输入的最大字符数</param>
        /// <param name="title">对话框标题</param>
        /// <param name="Prompt">对话框提示信息，可用<![CDATA[\n]]>换行</param>
        /// <param name="defText">对话框的默认输入信息</param>
        /// <param name="width">指定 InputBox 的宽度（不包括边框），最小为 200 像素。如果为 0，则使用默认宽度</param>
        /// <param name="height">
        /// 指定 InputBox 的高度（不包括边框）
        /// <para>如果为 0，表示自动计算高度，用户输入框只允许输入一行内容，按“回车”确认输入信息；<br/>
        /// 如果大于 0，用户输入框的高度会自动拓展，同时允许输入多行内容，按“Ctrl+回车”确认输入信息
        /// </para>
        /// </param>
        /// <param name="hideCancelBtn">指定是否隐藏取消按钮禁止用户取消输入</param>
        /// <returns>返回用户是否输入信息。如果用户按“确定”，返回 true；如果用户按“取消”，返回 false</returns>
        /// <exception cref="ArgumentNullException">有既定参数为null</exception>
        /// <exception cref="ArgumentException">有参数格式不正确</exception>
        /// <exception cref="ArgumentOutOfRangeException">指定的数值小于0</exception>
        public static bool InputBox(StringBuilder buffer, int maxCount, string title, string Prompt, string defText, int width, int height, bool hideCancelBtn)
        {

            Device.f_testNotInitGraph(Device.exc_winNotInit);
            if (buffer is null) throw new ArgumentNullException();
            if (maxCount <= 0 || width < 0 || height < 0) throw new ArgumentOutOfRangeException();

            if (defText != null && (defText.Length > maxCount)) throw new ArgumentException("文本框默认信息超出最大范围");
            int cap = buffer.Capacity;
            if (cap < maxCount)
            {
                cap *= 2;
                buffer.Capacity = (cap > maxCount) ? cap : maxCount;
            }

            return EasyX_API.InputBox_(buffer, maxCount, Prompt, title, defText, width, height, hideCancelBtn);
        }

        /// <summary>
        /// 以暂停线程的对话框窗口形式获取用户输入
        /// </summary>
        /// <param name="buffer">接收用户输入的字符缓冲区</param>
        /// <param name="maxCount">用户可在文本框中输入的最大字符数，该值不可超过缓冲区</param>
        /// <param name="title">对话框标题</param>
        /// <param name="Prompt">对话框提示信息，可用<![CDATA[\n]]>换行</param>
        /// <param name="defText">对话框的默认输入信息</param>
        /// <param name="width">指定 InputBox 的宽度（不包括边框），最小为 200 像素。如果为 0，则使用默认宽度</param>
        /// <param name="height">
        /// 指定 InputBox 的高度（不包括边框）
        /// <para>如果为 0，表示自动计算高度，用户输入框只允许输入一行内容，按“回车”确认输入信息；<br/>
        /// 如果大于 0，用户输入框的高度会自动拓展，同时允许输入多行内容，按“Ctrl+回车”确认输入信息
        /// </para>
        /// </param>
        /// <param name="hideCancelBtn">指定是否隐藏取消按钮禁止用户取消输入</param>
        /// <returns>返回用户是否输入信息。如果用户按“确定”，返回 true；如果用户按“取消”，返回 false</returns>
        /// <exception cref="ArgumentNullException">有既定参数为null</exception>
        /// <exception cref="ArgumentException">有参数格式不正确</exception>
        /// <exception cref="ArgumentOutOfRangeException">指定的数值小于0</exception>
        public static bool InputBox(char[] buffer, int maxCount, string title, string Prompt, string defText, int width, int height, bool hideCancelBtn)
        {

            Device.f_testNotInitGraph(Device.exc_winNotInit);
            if (buffer is null) throw new ArgumentNullException();
            if (buffer.Length == 0 || (maxCount > buffer.Length) || maxCount < 0 || width < 0 || height < 0) throw new ArgumentOutOfRangeException();

            if (defText != null && (defText.Length > maxCount)) throw new ArgumentException("文本框默认信息超出最大范围");

            fixed (char* buf = buffer)
            {
                return EasyX_API.InputBox_(buf, maxCount, Prompt, title, defText, width, height, hideCancelBtn);
            }
        }

        #endregion

        #endregion

    }


}
