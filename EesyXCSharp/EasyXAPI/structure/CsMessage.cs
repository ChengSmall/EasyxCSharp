using Cheng.EasyX.CPP;
using System;
using System.Runtime.InteropServices;

namespace Cheng.EasyX.DataStructure
{

    #region

    /// <summary>
    /// easyx消息标识
    /// </summary>
    [Flags]
    public enum MessageValue : ushort
    {
        /// <summary>
        /// 鼠标消息类型
        /// </summary>
        MouseType = 0x200,
        /// <summary>
        /// 按键消息类型
        /// </summary>
        KeyType = 0x100,
        /// <summary>
        /// 窗体消息类型
        /// </summary>
        WindowType = 0xF,
        /// <summary>
        /// 鼠标移动消息
        /// </summary>
        Mouse_Move = 0x200,
        /// <summary>
        /// 鼠标滚轮拨动消息
        /// </summary>
        Mouse_Wheel = 0x20A,
        /// <summary>
        /// 左键按下消息
        /// </summary>
        LeftButton_Down = 0x201,
        /// <summary>
        /// 左键弹起消息
        /// </summary>
        LeftButton_UP = 0x202,
        /// <summary>
        /// 左键双击消息
        /// </summary>
        LeftButton_DBlclk = 0x203,
        /// <summary>
        /// 中键按下消息
        /// </summary>
        MidButton_Down = 0x207,
        /// <summary>
        /// 中键弹起消息
        /// </summary>
        MidButton_UP = 0x208,
        /// <summary>
        /// 中键双击消息
        /// </summary>
        MidButton_DBlclk = 0x209,
        /// <summary>
        /// 右键按下消息
        /// </summary>
        RightButton_Down = 0x204,
        /// <summary>
        /// 右键弹起消息
        /// </summary>
        RightButton_UP = 0x205,
        /// <summary>
        /// 右键双击消息
        /// </summary>
        RightButton_DBlclk = 0x206,
        /// <summary>
        /// 按键按下消息
        /// </summary>
        Key_Down = 0x100,
        /// <summary>
        /// 按键抬起消息
        /// </summary>
        Key_Up = 0x101,
        /// <summary>
        /// 字符消息
        /// </summary>
        Char = 0x102,
        /// <summary>
        /// 窗口激活状态改变消息
        /// </summary>
        Activate = 0x6,
        /// <summary>
        /// 窗口移动消息
        /// </summary>
        Move = 0x3,
        /// <summary>
        /// 窗口大小改变消息
        /// </summary>
        Size = 0x5
    }

    /// <summary>
    /// 消息获取类型
    /// </summary>
    [Flags]
    public enum MessageType : byte
    {
        /// <summary>
        /// 鼠标消息
        /// </summary>
        Mouse = 0b1,
        /// <summary>
        /// 键盘消息
        /// </summary>
        Key = 0b10,
        /// <summary>
        /// 字符消息
        /// </summary>
        Char = 0b100,
        /// <summary>
        /// 窗口消息
        /// </summary>
        Window = 0b1000,
        /// <summary>
        /// 全部类型消息
        /// </summary>
        All = byte.MaxValue

    }

    /// <summary>
    /// EasyX消息结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct CsMessage
    {

        #region 结构

        /// <summary>
        /// 鼠标消息的按键枚举
        /// </summary>
        [Flags]
        public enum MouseType : byte
        {
            /// <summary>
            /// 鼠标左键
            /// </summary>
            LeftButton =    0b1,
            /// <summary>
            /// 鼠标中键
            /// </summary>
            MidButton =     0b10,
            /// <summary>
            /// 鼠标右键
            /// </summary>
            RightButton =   0b100,
            /// <summary>
            /// 辅助按键 Shift
            /// </summary>
            Shift =         0b1000,
            /// <summary>
            /// 辅助按键 Ctrl
            /// </summary>
            Ctrl =          0b10000
        }

        #endregion

        #region 参数

        #region 占位成员
        private void* buffer1;
        private void* buffer2;
        private void* buffer3;
        #endregion

        #region 标识符

        /// <summary>
        /// 消息标识符
        /// </summary>
        public MessageValue Message
        {
            get
            {
                fixed(CsMessage* cp = &this)
                {
                    return (MessageValue)(*EasyX_API.CsMessage_message(cp));
                }
            }
        }

        /// <summary>
        /// 该消息是否是鼠标消息
        /// </summary>
        public bool IsMouseMessage
        {
            get
            {
                return (Message & MessageValue.MouseType) != 0;
            }
        }

        /// <summary>
        /// 该消息是否是键盘消息消息
        /// </summary>
        public bool IsKeyMessage
        {
            get
            {
                return (Message & MessageValue.KeyType) != 0;
            }
        }

        #endregion

        #region 鼠标

        /// <summary>
        /// 鼠标消息的按键枚举组合
        /// </summary>
        public MouseType MouseButton
        {
            get
            {
                MouseType m = 0;
                fixed (CsMessage* cp = &this)
                {

                    if (EasyX_API.CsMessage_M_lbutton_get(cp)) m |= MouseType.LeftButton;

                    if (EasyX_API.CsMessage_M_mbutton_get(cp)) m |= MouseType.MidButton;

                    if (EasyX_API.CsMessage_M_rbutton_get(cp)) m |= MouseType.RightButton;

                    if (EasyX_API.CsMessage_M_shift_get(cp)) m |= MouseType.Shift;

                    if (EasyX_API.CsMessage_M_ctrl_get(cp)) m |= MouseType.Ctrl;

                }
                return m;
            }
        }

        /// <summary>
        /// 鼠标事件->是否按下Shift
        /// </summary>
        public bool ShiftDown
        {
            get
            {
                fixed (CsMessage* cp = &this)
                {
                    return EasyX_API.CsMessage_M_shift_get(cp);
                }
            }
        }

        /// <summary>
        /// 鼠标事件->是否按下Ctrl
        /// </summary>
        public bool CtrlDown
        {
            get
            {
                fixed (CsMessage* cp = &this)
                {
                    return EasyX_API.CsMessage_M_ctrl_get(cp);
                }
            }
        }

        /// <summary>
        /// 鼠标事件->是否按下鼠标左键
        /// </summary>
        public bool LeftButtonDown
        {
            get
            {
                fixed (CsMessage* cp = &this)
                {
                    return EasyX_API.CsMessage_M_lbutton_get(cp);
                }
            }
        }

        /// <summary>
        /// 鼠标事件->是否按下鼠标右键
        /// </summary>
        public bool RightButtonDown
        {
            get
            {
                fixed (CsMessage* cp = &this)
                {
                    return EasyX_API.CsMessage_M_rbutton_get(cp);
                }
            }
        }

        /// <summary>
        /// 鼠标事件->是否按下鼠标中键
        /// </summary>
        public bool MidButtonDown
        {
            get
            {
                fixed (CsMessage* cp = &this)
                {
                    return EasyX_API.CsMessage_M_mbutton_get(cp);
                }
            }
        }

        /// <summary>
        /// 鼠标的 x 坐标
        /// </summary>
        public short X
        {
            get
            {
                fixed (CsMessage* cp = &this)
                {
                    return *EasyX_API.CsMessage_M_x(cp);
                }
            }
        }

        /// <summary>
        /// 鼠标的 y 坐标
        /// </summary>
        public short Y
        {
            get
            {
                fixed (CsMessage* cp = &this)
                {
                    return *EasyX_API.CsMessage_M_y(cp);
                }
            }
        }

        /// <summary>
        /// 鼠标滚轮滚动值，为 120 的倍数
        /// </summary>
        public short Wheel
        {
            get
            {
                fixed (CsMessage* cp = &this)
                {
                    return *EasyX_API.CsMessage_M_wheel(cp);
                }
            }
        }

        /// <summary>
        /// 鼠标所在位置
        /// </summary>
        public EPoint MousePosition
        {
            get
            {
                fixed (CsMessage* cp = &this)
                {
                    return new EPoint(*EasyX_API.CsMessage_M_x(cp), *EasyX_API.CsMessage_M_y(cp));
                }
            }
        }

        /// <summary>
        /// 鼠标滚轮滚动值，表示为 <see cref="Wheel"/> / 120
        /// </summary>
        public int WheelMod
        {
            get
            {
                fixed (CsMessage* cp = &this)
                {
                    return *EasyX_API.CsMessage_M_wheel(cp) / 120;
                }
            }
        }

        #endregion

        #region 键盘

        /// <summary>
        /// 按键的虚拟键码
        /// </summary>
        public VkCode KeyCode
        {
            get
            {
                fixed (CsMessage* cp = &this)
                {
                    return (VkCode)(*EasyX_API.CsMessage_K_vkcode(cp));
                }
            }
        }

        /// <summary>
        /// 按键的扫描码
        /// </summary>
        public byte ScanCode
        {
            get
            {
                fixed (CsMessage* cp = &this)
                {
                    return (*EasyX_API.CsMessage_K_scancode(cp));
                }
            }
        }

        /// <summary>
        /// 按键是否是扩展键
        /// </summary>
        public bool Extended
        {
            get
            {
                fixed (CsMessage* cp = &this)
                {
                    return (EasyX_API.CsMessage_K_extended_get(cp));
                }
            }
        }

        /// <summary>
        /// 按键的前一个状态是否按下
        /// </summary>
        public bool PrevDown
        {
            get
            {
                fixed (CsMessage* cp = &this)
                {
                    return (EasyX_API.CsMessage_K_prevdown_get(cp));
                }
            }
        }

        /// <summary>
        /// 按键的虚拟键码字节值
        /// </summary>
        public byte vkCode
        {
            get
            {
                fixed (CsMessage* cp = &this)
                {
                    return (*EasyX_API.CsMessage_K_vkcode(cp));
                }
            }
        }

        #endregion

        #region 字符

        /// <summary>
        /// 字符消息
        /// </summary>
        /// <remarks>仅在<see cref="Message"/>是<see cref="MessageValue.Char"/>时有用</remarks>
        public char Ch
        {
            get
            {
                fixed (CsMessage* cp = &this)
                {
                    return (*EasyX_API.CsMessage_ch(cp));
                }
            }
        }

        #endregion

        #region 窗口消息

        /// <summary>
        /// windows窗口消息参数
        /// </summary>
        public IntPtr LParam
        {
            get
            {
                fixed (CsMessage* cp = &this)
                {
                    return *EasyX_API.CsMessage_lParam(cp);
                }
            }
        }

        /// <summary>
        /// windows窗口消息参数
        /// </summary>
        public IntPtr WParam
        {
            get
            {
                fixed (CsMessage* cp = &this)
                {
                    return *EasyX_API.CsMessage_wParam(cp);
                }
            }
        }

        #endregion

        #endregion

    }

    #endregion


}
