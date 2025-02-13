using Cheng.EasyX.CPP;
using Cheng.EasyX.DataStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cheng.EasyX.Exceptions;

namespace Cheng.EasyX.DataStructure
{

    //VK_APPS

    /// <summary>
    /// 虚拟键码
    /// </summary>
    public enum VkCode : byte
    {
        /// <summary>
        /// 无按键码
        /// </summary>
        None = 0,

        #region 鼠标

        /// <summary>
        /// 鼠标左键
        /// </summary>
        LeftButton = 0x01,
        /// <summary>
        /// 鼠标右键
        /// </summary>
        RightButton = 0x02,
        /// <summary>
        /// 控制中断处理
        /// </summary>
        CANCEL = 0x03,
        /// <summary>
        /// 鼠标中键
        /// </summary>
        MidButton = 0x04,
        /// <summary>
        /// X1 鼠标按钮
        /// </summary>
        XButton1 = 0x05,
        /// <summary>
        /// X2 鼠标按钮
        /// </summary>
        XButton2 = 0x06,

        #endregion

        #region 特殊

        /// <summary>
        /// 退格键
        /// </summary>
        BackSpace = 0x08,

        Tab = 0x09,
        /// <summary>
        /// CLEAR 键
        /// </summary>
        clear = 0x0C,
        /// <summary>
        /// 回车键
        /// </summary>
        Enter = 0x0D,
        /// <summary>
        /// Shift按键
        /// </summary>
        Shift = 0x10,
        /// <summary>
        /// Ctrl按键
        /// </summary>
        Ctrl = 0x11,
        /// <summary>
        /// Alt按键
        /// </summary>
        Alt = 0x12,	
        /// <summary>
        /// pause
        /// </summary>
        Pause = 0x13,
        /// <summary>
        /// 大小写锁定键
        /// </summary>
        CapsLock = 0x14,
        /// <summary>
        /// Esc键
        /// </summary>
        Esc = 0x1B,
        /// <summary>
        /// 空格键
        /// </summary>
        Space = 0x20,

        LeftWin = 0x5B,

        RightWin = 0x5C,

        NumLock = 0x90,

        Scroll_Lock = 0x91,

        /// <summary>
        /// 左Shift
        /// </summary>
        LeftShift = 0xA0,
        /// <summary>
        /// 右Shift
        /// </summary>
        RightShift = 0xA1,
        /// <summary>
        /// 左Ctrl
        /// </summary>
        LeftCtrl,
        /// <summary>
        /// 右Ctrl
        /// </summary>
        RightCtrl,
        /// <summary>
        /// 左Alt
        /// </summary>
        LeftAlt,
        /// <summary>
        /// 右Alt
        /// </summary>
        RightAlt = 0xA5,

        /// <summary>
        /// 应用程序密钥
        /// </summary>
        APPS = 0x5D,

        #endregion

        #region 键盘字符

        #region 数字
        /// <summary>
        /// 数字键0
        /// </summary>
        K_0 = 0x30,
        /// <summary>
        /// 数字键1
        /// </summary>
        K_1,
        /// <summary>
        /// 数字键2
        /// </summary>
        K_2,
        /// <summary>
        /// 数字键3
        /// </summary>
        K_3,
        /// <summary>
        /// 数字键4
        /// </summary>
        K_4,
        /// <summary>
        /// 数字键5
        /// </summary>
        K_5,
        /// <summary>
        /// 数字键6
        /// </summary>
        K_6,
        /// <summary>
        /// 数字键7
        /// </summary>
        K_7,
        /// <summary>
        /// 数字键8
        /// </summary>
        K_8,
        /// <summary>
        /// 数字键9
        /// </summary>
        K_9 = 0x39,
        #endregion

        #region 字母
        /// <summary>
        /// 表示键盘上的字母按键A，一直到<see cref="Z"/>
        /// </summary>
        A = 0x41,

        B, C, D, E, F, G,
        H, I, J, K, L, M, N,
        O, P, Q, R, S, T,
        U, V, W, X, Y,

        /// <summary>
        /// 表示键盘上的字母按键Z，从<see cref="A"/>到<see cref="Z"/>分别代表A~Z的26个字母
        /// </summary>
        Z = 0x5A,
        #endregion

        #region F系列
        /// <summary>
        /// 表示键盘上的F1按键
        /// </summary>
        F1 = 0x70,
        F2, F3, F4, F5, F6, F7, F8, F9, F10, F11,
        F12 = 0x7C,
        F13, F14, F15, F16, F17, F18, F19, F20, F21, F22, F23,
        F24 = 0x87,
        #endregion

        #region 其它

        /// <summary>
        /// 波浪号 `~
        /// </summary>
        Tilde = 192,
        /// <summary>
        /// 下划线 -_
        /// </summary>
        Underline = 189,
        /// <summary>
        /// 等号 =+
        /// </summary>
        Equalsign = 187,
        /// <summary>
        /// 反斜杠
        /// </summary>
        Backslash = 220,
        /// <summary>
        /// 左中括号
        /// </summary>
        LeftSquareacket = 219,
        /// <summary>
        /// 右中括号
        /// </summary>
        ReftSquareacket = 221,
        /// <summary>
        /// 分号和冒号
        /// </summary>
        Semicolon = 186,
        /// <summary>
        /// 引号
        /// </summary>
        Quotation = 222,
        /// <summary>
        /// 左尖括号
        /// </summary>
        LeftAngleBracket = 188,
        /// <summary>
        /// 右尖括号
        /// </summary>
        RightAngleBracket = 190,
        /// <summary>
        /// 正斜杠和问号
        /// </summary>
        QuestionMark = 191,

        #endregion

        #endregion

        #region 小键盘

        #region 小键盘左侧

        /// <summary>
        /// 小键盘-向上翻页
        /// </summary>
        PageUP = 0x21,
        /// <summary>
        /// 小键盘-向下翻页
        /// </summary>
        PageDown = 0x22,
        /// <summary>
        /// 小键盘-End
        /// </summary>
        END = 0x23,
        /// <summary>
        /// 小键盘-Home
        /// </summary>
        HOME = 0x24,
        /// <summary>
        /// 左箭头
        /// </summary>
        LeftArrow = 0x25,
        /// <summary>
        /// 上箭头
        /// </summary>
        UpArrow = 0x26,
        /// <summary>
        /// 右箭头
        /// </summary>
        RightArrow = 0x27,
        /// <summary>
        /// 下箭头
        /// </summary>
        DownArrow = 0x28,
        /// <summary>
        /// 小键盘-Ins
        /// </summary>
        Insert = 0x2D,
        /// <summary>
        /// 小键盘-Del
        /// </summary>
        Delete = 0x2E,
        /// <summary>
        /// 小键盘锁定
        /// </summary>
        NumberLock = 144,

        #endregion

        #region 数字
        /// <summary>
        /// 小键盘数字
        /// </summary>
        Numpad_0 = 0x60,
        Numpad_1, Numpad_2, Numpad_3, Numpad_4, Numpad_5, Numpad_6, Numpad_7, Numpad_8,
        Numpad_9 = 0x69,
        #endregion

        #region 运算符
        /// <summary>
        /// 小键盘-乘号键
        /// </summary>
        Multiply = 0x6A,
        /// <summary>
        /// 小键盘-加号键
        /// </summary>
        Add = 0x6B,
        /// <summary>
        /// 小键盘-分隔符键
        /// </summary>
        Separator = 0x6C,
        /// <summary>
        /// 小键盘-减号键
        /// </summary>
        Subtract = 0x6D,
        /// <summary>
        /// 小键盘-句点键
        /// </summary>
        Period = 0x6E,
        /// <summary>
        /// 小键盘-除号键
        /// </summary>
        Divide = 0x6F,
        #endregion

        #endregion

    }

}
