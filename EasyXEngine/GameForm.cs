using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

using Cheng.EasyXEngine.Structures;
using Cheng.EasyX.DataStructure;
using Cheng.EasyX;
using Cheng.EasyX.Messages;
using Cheng.EasyX.Exceptions;
using Cheng.LoopThreads;
using Cheng.Algorithm.Collections;

using Cheng.EasyXEngine.Extends;
using System.Diagnostics;

namespace Cheng.EasyXEngine
{

    /// <summary>
    /// 鼠标状态参数
    /// </summary>
    public struct MouseState
    {

        #region
        /// <summary>
        /// 鼠标当前位置
        /// </summary>
        internal EPoint mousePos;
        /// <summary>
        /// 鼠标上一帧所在位置
        /// </summary>
        internal EPoint lateMousePos;
        /// <summary>
        /// 鼠标当前帧移动增量
        /// </summary>
        internal EPoint mouseFrameMove;
        /// <summary>
        /// 鼠标滚轮增量源数据
        /// </summary>
        internal int mouseWheel;
        /// <summary>
        /// 鼠标滚轮增量 / 120
        /// </summary>
        internal int mouseModWheel;
        /// <summary>
        /// 鼠标左键-状态
        /// </summary>
        internal bool mouseLeftState;
        /// <summary>
        /// 鼠标左键-帧按下
        /// </summary>
        internal bool mouseLeftDown;
        /// <summary>
        /// 鼠标左键-帧抬起
        /// </summary>
        internal bool mouseLeftUp;
        /// <summary>
        /// 鼠标中键-状态
        /// </summary>
        internal bool mouseMidState;
        /// <summary>
        /// 鼠标中键-帧按下
        /// </summary>
        internal bool mouseMidDown;
        /// <summary>
        /// 鼠标中键-帧抬起
        /// </summary>
        internal bool mouseMidUp;
        /// <summary>
        /// 鼠标右键-状态
        /// </summary>
        internal bool mouseRightState;
        /// <summary>
        /// 鼠标右键-帧按下
        /// </summary>
        internal bool mouseRightDown;
        /// <summary>
        /// 鼠标右键-帧抬起
        /// </summary>
        internal bool mouseRightUp;
        #endregion

        #region 访问
        /// <summary>
        /// 当前帧鼠标位置，单位像素
        /// </summary>
        public EPoint Position
        {
            get => mousePos;
        }
        /// <summary>
        /// 当前帧鼠标相对上一帧位置的移动增量，单位像素
        /// </summary>
        public EPoint Move
        {
            get => mouseFrameMove;
        }
        /// <summary>
        /// 上一帧鼠标的位置，单位像素
        /// </summary>
        public EPoint PreviousPosition
        {
            get => lateMousePos;
        }
        /// <summary>
        /// 鼠标滚轮增量，值是120的整数倍
        /// </summary>
        /// <returns>0表示没有增量，大于0表示向上滚动，小于0表示向下滚动</returns>
        public int Wheel => mouseWheel;
        /// <summary>
        /// 鼠标滚轮增量，该值是 <see cref="Wheel"/> / 120 的结果
        /// </summary>
        /// <returns>0表示没有增量，大于0表示向上滚动，小于0表示向下滚动</returns>
        public int WheelMod => mouseModWheel;

        /// <summary>
        /// 鼠标左键-状态
        /// </summary>
        public bool LeftState => mouseLeftState;
        /// <summary>
        /// 鼠标左键-帧按下
        /// </summary>
        public bool LeftDown => mouseLeftDown;
        /// <summary>
        /// 鼠标左键-帧抬起
        /// </summary>
        public bool LeftUp => mouseLeftUp;
        /// <summary>
        /// 鼠标中键-状态
        /// </summary>
        public bool MidState => mouseMidState;
        /// <summary>
        /// 鼠标中键-帧按下
        /// </summary>
        public bool MidDown => mouseMidDown;
        /// <summary>
        /// 鼠标中键-帧抬起
        /// </summary>
        public bool MidUp => mouseMidUp;
        /// <summary>
        /// 鼠标右键-状态
        /// </summary>
        public bool RightState => mouseRightState;
        /// <summary>
        /// 鼠标右键-帧按下
        /// </summary>
        public bool RightDown => mouseRightDown;
        /// <summary>
        /// 鼠标右键-帧抬起
        /// </summary>
        public bool RightUp => mouseRightUp;

        #endregion

    }

    /// <summary>
    /// 窗体消息参数
    /// </summary>
    public readonly struct WindowEventArg
    {
        public WindowEventArg(MessageValue meg, IntPtr l, IntPtr w)
        {
            Message = meg;
            LParam = l;
            WParam = w;
        }

        /// <summary>
        /// windows窗口消息参数
        /// </summary>
        public readonly IntPtr LParam;
        /// <summary>
        /// windows窗口消息参数
        /// </summary>
        public readonly IntPtr WParam;
        /// <summary>
        /// 消息ID
        /// </summary>
        public readonly MessageValue Message;
    }

    /// <summary>
    /// 游戏主循环
    /// </summary>
    /// <remarks>
    /// <para>类Unity的简单循环线程，自动管理绘图缓冲区</para>
    /// <para>
    /// 使用单线程帧循环运行程序，使用<see cref="LoopFunction.UpdateEvent"/>和<see cref="LoopFunction.FixedUpdateEvent"/>执行逻辑代码，每循环一次刷新一次绘图缓冲区
    /// </para>
    /// <para>
    /// 可使用<see cref="IDrawingObject"/>派生自定义绘制对象，添加到循环后会根据绘制层级调用绘制接口
    /// </para>
    /// <para>使用消息事件时，可在事件委托中绘制图像，绘制的图像在每次循环后绘制，但是层级始终在<see cref="IDrawingObject"/>之上</para>
    /// </remarks>
    public unsafe class GameForm : LoopFunction, IWin32Window
    {

        #region 结构
        /// <summary>
        /// 按钮类别
        /// </summary>
        protected enum S_KeyStateType : byte
        {
            /// <summary>
            /// 是否被按下的状态
            /// </summary>
            State = 0,
            /// <summary>
            /// 当前帧按下
            /// </summary>
            Down = 1,
            /// <summary>
            /// 当前帧抬起
            /// </summary>
            Up = 2
        }

        private class comparerDrawobj : IComparer<IDrawingObject>
        {
            public int Compare(IDrawingObject x, IDrawingObject y)
            {
                var xl = x.Lay;
                var yl = y.Lay;
                return (xl == yl) ? 0 : (xl < yl ? -1 : 1);
                //return x.Lay.CompareTo(y.Lay);
            }
        }

        #endregion

        #region 构造

        /// <summary>
        /// 实例化游戏主循环
        /// </summary>
        protected GameForm() : base()
        {
            f_init();
            isX64 = sizeof(void*) == 8;
        }

        private void f_init()
        {
            //p_loopStart = LoopFunctionMethod;
            p_gdiThreadSafeObj = new object();
            p_msg_ButtonKeyCodeStates = new bool[256 * 3];

            p_draws = new List<IDrawingObject>();
            p_addrawBuffer = new Stack<IDrawingObject>();
            p_comparerDrawing = new comparerDrawobj();

            p_nowFrameChar = -1;
            p_nowFrameDown = VkCode.None;
            p_nowFrameUp = VkCode.None;
            p_nowIsKeyDown = false;

            p_isRendering = true;
            p_isMessageGetting = true;
            p_graphics = null;

            ModifierKeyTellApartLR = false;

            //p_waitTimer = new Stopwatch();
        }       

        #endregion

        #region 释放

        /// <summary>
        /// 注销事件系统并释放非托管资源对象
        /// </summary>
        /// <param name="disposeing">是否释放托管对象</param>
        /// <returns>返回true</returns>
        protected override bool Disposeing(bool disposeing)
        {
            if (disposeing)
            {
                p_graphics?.Dispose();
            }
            p_graphics = null;

            return base.Disposeing(disposeing);
        }

        /// <summary>
        /// 释放此实例和关联的所有非托管资源
        /// </summary>
        public override void Close()
        {
            Dispose(true);
        }

        #endregion

        #region 参数

        #region 唯一实例

        /// <summary>
        /// 获取EasyX游戏主循环实例
        /// </summary>
        /// <remarks>在此之前请调用<see cref="GameForm.InitEasyXGame()"/>方法初始化游戏</remarks>
        /// <exception cref="EasyXEngineExcption">未执行初始化方法</exception>
        public static GameForm Game
        {
            get
            {
                if (sp_game is null) throw new EasyXEngineExcption("为执行初始化方法");
                return sp_game;
            }
        }

        private static GameForm sp_game;

        /// <summary>
        /// 初始化EasyX引擎
        /// </summary>
        /// <exception cref="EasyXEngineExcption">已执行初始化方法</exception>
        public static void InitEasyXGame()
        {
            if (sp_game != null) throw new EasyXEngineExcption("已执行初始化方法");
            sp_game = new GameForm();
        }

        /// <summary>
        /// 初始化EasyX游戏
        /// </summary>
        /// <param name="form">派生于此类型的实例</param>
        /// <exception cref="EasyXEngineExcption">已执行初始化方法</exception>
        /// <exception cref="ArgumentNullException">实例参数为null</exception>
        public static void InitEasyXGame(GameForm form)
        {
            if (sp_game != null) throw new EasyXEngineExcption("已执行初始化方法");
            sp_game = form ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// 退出游戏窗口
        /// </summary>
        /// <returns>是否成功退出</returns>
        /// <exception cref="EasyXEngineExcption">未执行初始化方法</exception>
        public static bool ExitGame()
        {
            var g = Game;
            return g.Exit();         
        }

        #endregion

        #region 线程

        #endregion

        #region 游戏对象

        /// <summary>
        /// 可绘制对象集合
        /// </summary>
        protected List<IDrawingObject> p_draws;

        /// <summary>
        /// 添加可绘制对象的缓冲区
        /// </summary>
        protected Stack<IDrawingObject> p_addrawBuffer;

        /// <summary>
        /// 层级排序比较器
        /// </summary>
        private comparerDrawobj p_comparerDrawing;

        #endregion

        #region GDI

        /// <summary>
        /// 绘图HDC的GDI
        /// </summary>
        protected Graphics p_graphics;

        /// <summary>
        /// gdi绘图线程锁
        /// </summary>
        private object p_gdiThreadSafeObj;
        #endregion

        #region 消息捕获

        /// <summary>
        /// KeyCode按键状态捕获
        /// </summary>
        /// <remarks>前256表示按钮当前状态，中256表示按钮这一帧是否按下，后256表示按钮这一帧是否松开</remarks>
        private bool[] p_msg_ButtonKeyCodeStates;

        /// <summary>
        /// KeyCode按键状态捕获
        /// </summary>
        /// <remarks>前256表示按钮当前状态，中256表示按钮这一帧是否按下，后256表示按钮这一帧是否松开</remarks>
        protected bool[] p_Message_ButtonKeyCodeStates
        {
            get => p_msg_ButtonKeyCodeStates;
        }

        /// <summary>
        /// 鼠标状态捕获
        /// </summary>
        protected MouseState p_msg_mouse;

        /// <summary>
        /// 当前帧字符输入
        /// </summary>
        protected int p_nowFrameChar;

        /// <summary>
        /// 当前帧按下
        /// </summary>
        protected VkCode p_nowFrameDown;

        /// <summary>
        /// 当前帧抬起
        /// </summary>
        protected VkCode p_nowFrameUp;

        /// <summary>
        /// 当前帧有没有按下的按键
        /// </summary>
        protected bool p_nowIsKeyDown;

        /// <summary>
        /// 是否开启内部消息捕获
        /// </summary>
        protected bool p_isMessageGetting;

        /// <summary>
        /// 是否开启画面渲染
        /// </summary>
        protected bool p_isRendering;

        /// <summary>
        /// 辅助按键区分左右
        /// </summary>
        protected bool p_modifierKeyLR;

        /// <summary>
        /// 当前程序进程是否为64位环境
        /// </summary>
        public readonly bool isX64;
        #endregion

        #endregion

        #region 封装

        /// <summary>
        /// 设置按钮状态
        /// </summary>
        /// <param name="keyCode">按钮枚举</param>
        /// <param name="type">状态类型</param>
        /// <param name="value">设置的值</param>
        protected void f_setButtonState(VkCode keyCode, S_KeyStateType type, bool value)
        {
            p_msg_ButtonKeyCodeStates[(256 * (byte)type) + (byte)keyCode] = value;
        }
        /// <summary>
        /// 访问按钮状态
        /// </summary>
        /// <param name="keyCode">按钮枚举</param>
        /// <param name="type">状态类型</param>
        /// <returns>按钮状态</returns>
        protected bool f_getButtonState(VkCode keyCode, S_KeyStateType type)
        {
            return p_msg_ButtonKeyCodeStates[(256 * (byte)type) + (byte)keyCode];
        }
        /// <summary>
        /// 清除所有按钮状态
        /// </summary>
        protected void f_clearAllButtonState()
        {
            Array.Clear(p_msg_ButtonKeyCodeStates, 0, 256 * 3);
        }

        /// <summary>
        /// 清除指定类型按钮状态
        /// </summary>
        /// <param name="type"></param>
        protected void f_clearButtonState(S_KeyStateType type)
        {

            Array.Clear(p_msg_ButtonKeyCodeStates, (int)type * 256, 256);
        }

        private void f_clearMouseArg()
        {
            ref var m = ref p_msg_mouse;
            m.mouseLeftDown = false;
            m.mouseLeftUp = false;
            m.mouseMidDown = false;
            m.mouseMidUp = false;
            m.mouseRightDown = false;
            m.mouseRightUp = false;

            m.mouseFrameMove = new EPoint(0, 0);
            m.mouseWheel = 0;
            m.mouseModWheel = 0;
        }

        /// <summary>
        /// 释放（如果有）后重新初始化绘图gdi
        /// </summary>
        private void f_initGrapHDC()
        {
            lock (p_gdiThreadSafeObj)
            {
                if (p_graphics != null)
                {
                    p_graphics.Dispose();
                }
                p_graphics = Graphics.FromHdc(Device.WindowHDC, Device.WindowHandle);
            }       
        }

        #endregion

        #region 事件

        #region 消息事件
        /// <summary>
        /// 窗口消息事件
        /// </summary>
        public event LoopThreadAction<WindowEventArg> WindowEvent;

        /// <summary>
        /// 每当获取按键按下消息时引发的事件
        /// </summary>
        public event LoopThreadAction<KeyCode> KeyDownEvent;

        /// <summary>
        /// 每当获取按键抬起消息时引发的事件
        /// </summary>
        public event LoopThreadAction<KeyCode> KeyUpEvent;

        /// <summary>
        /// 每当获取到消息时引发的事件
        /// </summary>
        /// <remarks>该事件始终在每次循环消息获取的最后执行</remarks>
        public event LoopThreadAction<CsMessage> GetMessageEvent;

        /// <summary>
        /// 每当获取到键盘字符输入消息时引发的事件
        /// </summary>
        public event LoopThreadAction<char> KeyboardEvent;
        #endregion

        #endregion

        #region 消息循环

        /// <summary>
        /// 消息获取函数，在每帧开始时调用可更新消息参数
        /// </summary>
        protected void f_messageOnceLoop()
        {

            CsMessage meg;
            bool flag;
            p_nowIsKeyDown = false;
            p_nowFrameChar = -1;
            p_nowFrameDown = VkCode.None;
            p_nowFrameUp = VkCode.None;
            f_clearButtonState(S_KeyStateType.Down);
            f_clearButtonState(S_KeyStateType.Up);
            f_clearMouseArg();

            flag = Messaging.GetMessage(out meg, MessageType.All, true);

            var msid = meg.Message;

            if (flag)
            {
                if(((msid & MessageValue.KeyType) != 0)) f_meg_getKey(meg, p_modifierKeyLR);

                if ((msid & MessageValue.MouseType) != 0) f_meg_getMouse(meg);

                if(msid == MessageValue.Char) f_meg_getChar(meg);

                switch (msid)
                {
                    case MessageValue.Activate:
                    case MessageValue.Move:
                    case MessageValue.Size:
                        f_meg_getWin(meg);
                        break;
                }

                GetMessageEvent?.Invoke(this, meg);
            }

            //Messaging.FlushMessage();
        }

        private void f_meg_getMouse(CsMessage meg)
        {
            var megID = meg.Message;

            ref var arg = ref p_msg_mouse;


            if(megID == MessageValue.Mouse_Wheel)
            {
                arg.mouseWheel = meg.Wheel;
                arg.mouseModWheel = arg.mouseWheel / 120;
            }
            else
            {
                arg.mouseWheel = 0;
                arg.mouseModWheel = 0;
            }

            if (megID == MessageValue.Mouse_Move)
            {
                var latePos = arg.mousePos;
                arg.mousePos = meg.MousePosition;
                arg.lateMousePos = latePos;
                arg.mouseFrameMove = arg.mousePos - latePos;
            }
            
            if(megID == MessageValue.LeftButton_Down)
            {
                arg.mouseLeftDown = true;
                f_setButtonState(VkCode.LeftButton, S_KeyStateType.Down, true);
                if(p_nowFrameDown == VkCode.None) p_nowFrameDown = VkCode.LeftButton;
                p_nowIsKeyDown = true;
            }
            else
            {
                arg.mouseLeftDown = false;
                //f_setButtonState(VkCode.LeftButton, S_KeyStateType.Up, true);
            }
            //arg.mouseLeftDown = megID == MessageValue.LeftButton_Down;

            if (megID == MessageValue.MidButton_Down)
            {
                arg.mouseMidDown = true;
                f_setButtonState(VkCode.MidButton, S_KeyStateType.Down, true);
                if (p_nowFrameDown == VkCode.None) p_nowFrameDown = VkCode.MidButton;
            }
            else
            {
                arg.mouseMidDown = false;
                //f_setButtonState(VkCode.MidButton, S_KeyStateType.Up, true);
            }
            //arg.mouseMidDown = megID == MessageValue.MidButton_Down;

            if (megID == MessageValue.RightButton_Down)
            {
                arg.mouseRightDown = true;
                f_setButtonState(VkCode.RightButton, S_KeyStateType.Down, true);
                if (p_nowFrameDown == VkCode.None) p_nowFrameDown = VkCode.RightButton;
            }
            else
            {
                arg.mouseRightDown = false;
                //f_setButtonState(VkCode.RightButton, S_KeyStateType.Up, true);
            }

            if(megID == MessageValue.LeftButton_UP)
            {
                arg.mouseLeftUp = true;
                f_setButtonState(VkCode.LeftButton, S_KeyStateType.Up, true);
                if (p_nowFrameUp == VkCode.None) p_nowFrameUp = VkCode.LeftButton;
            }
            else
            {
                arg.mouseLeftUp = false;
            }
            //arg.mouseLeftUp = megID == MessageValue.LeftButton_UP;

            if (megID == MessageValue.MidButton_UP)
            {
                arg.mouseMidUp = true;
                f_setButtonState(VkCode.MidButton, S_KeyStateType.Up, true);
                if (p_nowFrameUp == VkCode.None) p_nowFrameUp = VkCode.MidButton;
            }
            else
            {
                arg.mouseMidUp = false;
            }
            //arg.mouseMidUp = megID == MessageValue.MidButton_UP;

            if (megID == MessageValue.RightButton_UP)
            {
                arg.mouseRightUp = true;
                f_setButtonState(VkCode.RightButton, S_KeyStateType.Up, true);
                if (p_nowFrameUp == VkCode.None) p_nowFrameUp = VkCode.RightButton;
            }
            else
            {
                arg.mouseRightUp = false;
            }
            //arg.mouseRightUp = megID == MessageValue.RightButton_UP;

            if (arg.mouseLeftDown && (!arg.mouseLeftState))
            {
                arg.mouseLeftState = true;
            }
            else if(arg.mouseLeftUp && (arg.mouseLeftState))
            {
                arg.mouseLeftState = false;
            }


            if (arg.mouseRightDown && (!arg.mouseRightState))
            {
                arg.mouseRightState = true;
            }
            else if (arg.mouseRightUp && (arg.mouseRightState))
            {
                arg.mouseRightState = false;
            }


            if (arg.mouseMidDown && (!arg.mouseMidState))
            {
                arg.mouseMidState = true;
            }
            else if (arg.mouseMidUp && (arg.mouseMidState))
            {
                arg.mouseMidState = false;
            }

            f_setButtonState(VkCode.LeftButton, S_KeyStateType.State, arg.mouseLeftState);
            f_setButtonState(VkCode.RightButton, S_KeyStateType.State, arg.mouseRightState);
            f_setButtonState(VkCode.MidButton, S_KeyStateType.State, arg.mouseMidState);

            if (arg.mouseLeftDown)
            {
                KeyDownEvent?.Invoke(this, new KeyCode(VkCode.LeftButton, meg.ScanCode));
            }
            else if (arg.mouseMidDown)
            {
                KeyDownEvent?.Invoke(this, new KeyCode(VkCode.MidButton, meg.ScanCode));
            }
            else if (arg.mouseRightDown)
            {
                KeyDownEvent?.Invoke(this, new KeyCode(VkCode.RightButton, meg.ScanCode));
            }

            if (arg.mouseLeftUp)
            {
                KeyUpEvent?.Invoke(this, new KeyCode(VkCode.LeftButton, meg.ScanCode));
            }
            else if (arg.mouseMidUp)
            {
                KeyUpEvent?.Invoke(this, new KeyCode(VkCode.MidButton, meg.ScanCode));
            }
            else if (arg.mouseRightUp)
            {
                KeyUpEvent?.Invoke(this, new KeyCode(VkCode.RightButton, meg.ScanCode));
            }

        }

        private void f_meg_getKey(CsMessage meg, bool mkb)
        {
            var megID = meg.Message;

            VkCode vk;
            vk = meg.KeyCode;
            bool flag;

            //IntPtr lp = default;
            IntPtr wp = default;

            if (mkb)
            {
                //lp = meg.LParam;
                wp = meg.WParam;
            }

            const int leftShift = 0x2A10;
            const int leftCtrl = 0x1D11;
            const int leftAlt = 0x3812;
            const int leftUpShift = 0x22A10;
            const int leftUpCtrl = 0x21D11;
            const int leftUpAlt = 0x23812;

            const int rShift = 0x3610;
            const int rCtrl = 0x11D11;
            const int rAlt = 0x13812;
            const int rUpShift = 0x23610;
            const int rUpCtrl = 0x31D11;
            const int rUpAlt = 0x33812;

            if (megID == MessageValue.Key_Down)
            {
                
                if (mkb)
                {
                    var wpv = wp.ToInt32();
                    switch (wpv)
                    {
                        case leftShift:
                            //f_setButtonState(VkCode.Shift, S_KeyStateType.State, false);
                            vk = VkCode.LeftShift;
                            break;
                        case leftCtrl:
                            //f_setButtonState(VkCode.Ctrl, S_KeyStateType.State, false);
                            vk = VkCode.LeftCtrl;
                            break;
                        case leftAlt:
                            //f_setButtonState(VkCode.Alt, S_KeyStateType.State, false);
                            vk = VkCode.LeftAlt;
                            break;
                        case rShift:
                            //f_setButtonState(VkCode.Shift, S_KeyStateType.State, false);
                            vk = VkCode.RightShift;
                            break;
                        case rCtrl:
                            //f_setButtonState(VkCode.Ctrl, S_KeyStateType.State, false);
                            vk = VkCode.RightCtrl;
                            break;
                        case rAlt:
                            //f_setButtonState(VkCode.Alt, S_KeyStateType.State, false);
                            vk = VkCode.RightAlt;
                            break;
                    }

                    switch (wpv)
                    {
                        case leftUpShift:
                        case leftUpCtrl:
                        case leftUpAlt:
                        case rUpShift:
                        case rUpCtrl:
                        case rUpAlt:
                            goto MoveDownOver;
                            //break;
                    }
                }

                //MoveDown:

                //访问上一帧按钮状态
                flag = f_getButtonState(vk, S_KeyStateType.State);

                if (!flag)
                {
                    //上一帧状态为抬起
                    //改变状态到按下 false => true
                    f_setButtonState(vk, S_KeyStateType.State, true);

                    f_setButtonState(vk, S_KeyStateType.Down, true);
                    p_nowFrameDown = vk;
                    p_nowIsKeyDown = true;
                }
                KeyDownEvent?.Invoke(this, new KeyCode(vk, meg.ScanCode));

                MoveDownOver:
                ;
            }

            if (megID == MessageValue.Key_Up)
            {
              
                if (mkb)
                {
                    var wpv = wp.ToInt32();

                    switch (wpv)
                    {
                        case leftUpShift:
                            //f_setButtonState(VkCode.Shift, S_KeyStateType.State, false);
                            vk = VkCode.LeftShift;
                            break;
                        case leftUpCtrl:
                            //f_setButtonState(VkCode.Ctrl, S_KeyStateType.State, false);
                            vk = VkCode.LeftCtrl;
                            break;
                        case leftUpAlt:
                            //f_setButtonState(VkCode.Alt, S_KeyStateType.State, false);
                            vk = VkCode.LeftAlt;
                            break;
                        case rUpShift:
                            //f_setButtonState(VkCode.Shift, S_KeyStateType.State, false);
                            vk = VkCode.RightShift;
                            break;
                        case rUpCtrl:
                            //f_setButtonState(VkCode.Ctrl, S_KeyStateType.State, false);
                            vk = VkCode.RightCtrl;
                            break;
                        case rUpAlt:
                            //f_setButtonState(VkCode.Alt, S_KeyStateType.State, false);
                            vk = VkCode.RightAlt;
                            break;
                    }
                }

                //访问上一帧按钮状态
                flag = f_getButtonState(vk, S_KeyStateType.State);

                if (flag)
                {
                    //上一帧状态为按下
                    //改变状态到抬起 true => false
                    f_setButtonState(vk, S_KeyStateType.State, false);

                    f_setButtonState(vk, S_KeyStateType.Up, true);
                    p_nowFrameUp = vk;
                }
                KeyUpEvent?.Invoke(this, new KeyCode(vk, meg.ScanCode));
            }

        }

        private void f_meg_getChar(CsMessage meg)
        {
            var megID = meg.Message;

            if(megID == MessageValue.Char)
            {
                char c = meg.Ch;
                p_nowFrameChar = c;
                KeyboardEvent?.Invoke(this, c);
            }
        }

        private void f_meg_getWin(CsMessage meg)
        {

            WindowEvent?.Invoke(this, new WindowEventArg(meg.Message, meg.LParam, meg.WParam));
        }

        /// <summary>
        /// 按键消息捕获到 Shift, Ctrl, Alt 这类辅助按键时，是否区分左右
        /// </summary>
        /// <value>
        /// 若该参数设为false，则按键消息返回的<see cref="VkCode"/>辅助按键为<see cref="VkCode.Shift"/>，<see cref="VkCode.Alt"/>和<see cref="VkCode.Ctrl"/>；若设为true，则消息返回的<see cref="VkCode"/>为<see cref="VkCode.LeftShift"/>，<see cref="VkCode.RightShift"/>等区分左右的枚举值；
        /// <para>该值默认为false</para>
        /// </value>
        public bool ModifierKeyTellApartLR
        {
            get => p_modifierKeyLR;
            set
            {
                if (value)
                {
                    f_setButtonState(VkCode.Shift, S_KeyStateType.State, false);
                    f_setButtonState(VkCode.Ctrl, S_KeyStateType.State, false);
                    f_setButtonState(VkCode.Alt, S_KeyStateType.State, false);
                }
                else
                {
                    f_setButtonState(VkCode.LeftShift, S_KeyStateType.State, false);
                    f_setButtonState(VkCode.LeftCtrl, S_KeyStateType.State, false);
                    f_setButtonState(VkCode.LeftAlt, S_KeyStateType.State, false);
                    f_setButtonState(VkCode.RightShift, S_KeyStateType.State, false);
                    f_setButtonState(VkCode.RightCtrl, S_KeyStateType.State, false);
                    f_setButtonState(VkCode.RightAlt, S_KeyStateType.State, false);
                }

                p_modifierKeyLR = value;
            }
        }

        #endregion

        #region 功能

        #region 消息
        /// <summary>
        /// 返回当前帧按下的按键
        /// </summary>
        /// <returns>若没有按下的按键则为<see cref="VkCode.None"/></returns>
        public VkCode GetKeyDown()
        {
            return p_nowFrameDown;
        }

        /// <summary>
        /// 返回当前帧抬起的按键
        /// </summary>
        /// <returns>若没有按下的按键则为<see cref="VkCode.None"/></returns>
        public VkCode GetKeyUp()
        {
            return p_nowFrameUp;
        }

        /// <summary>
        /// 当前帧是否有按键被按下
        /// </summary>
        public bool IsKeyDown
        {
            get => p_nowIsKeyDown;
        }

        /// <summary>
        /// 判断指定按键是否处于按下状态
        /// </summary>
        /// <param name="keyCode">判断的按键键码</param>
        /// <returns>true表示按下，false表示没有按下</returns>
        public bool GetKey(VkCode keyCode)
        {
            return f_getButtonState(keyCode, S_KeyStateType.State);
        }

        /// <summary>
        /// 判断指定按键是否在当前帧被按下
        /// </summary>
        /// <param name="keyCode">指定按键</param>
        /// <returns>若按键在该帧被按下，则这一帧返回true</returns>
        public bool GetKeyDown(VkCode keyCode)
        {
            return f_getButtonState(keyCode, S_KeyStateType.Down);
        }

        /// <summary>
        /// 判断指定按键是否在当前帧被松开
        /// </summary>
        /// <param name="keyCode">指定按键</param>
        /// <returns>若按键在该帧被按下，则这一帧返回true</returns>
        public bool GetKeyUp(VkCode keyCode)
        {
            return f_getButtonState(keyCode, S_KeyStateType.Up);
        }

        /// <summary>
        /// 获取当前的鼠标状态
        /// </summary>
        public MouseState MouseArg
        {
            get => p_msg_mouse;
        }

        /// <summary>
        /// 获取当前帧字符输入
        /// </summary>
        /// <returns>当前帧输入的字符，若没有字符输入则返回-1</returns>
        public int InputChar
        {
            get => p_nowFrameChar;
        }

        /// <summary>
        /// 获取当前帧字符输入
        /// </summary>
        /// <param name="c">接收输入的字符</param>
        /// <returns>若当前帧存在有效的字符输入，返回true；若不存在字符输入，返回false</returns>
        public bool GetInputChar(out char c)
        {
            c = (char)p_nowFrameChar;
            return p_nowFrameChar != -1;
        }

        /// <summary>
        /// 是否开启消息更新循环
        /// </summary>
        /// <value>
        /// <para>是否开启消息更新循环，当该参数设为true表示开启消息更新，false则不进行消息获取</para>
        /// <para>
        /// 当参数设为false时，该实例的<see cref="GetKey(VkCode)"/>、<see cref="GetKeyDown(VkCode)"/>，<see cref="GetKeyUp(VkCode)"/>等所有消息参数获取的值停止更新，<see cref="GameForm.WindowEvent"/>、<see cref="GameForm.KeyDownEvent"/>、<see cref="GameForm.KeyUpEvent"/>等所有消息事件停止触发；EasyX图形库消息类型的API在循环内停止调用；
        /// </para>
        /// <para>如果想要重写消息循环，将该参数设为false可防止消息获取冲突；</para>
        /// <para>在一个帧循环事件内设置该参数时，消息循环会在下一帧停止更新；</para>
        /// <para>该参数默认值为true；</para>
        /// <para>注意：当该参数设为false时，你将无法利用<see cref="GameForm"/>内的方法和事件获取设备输入，但是<see cref="LoopFunction"/>内的方法和事件依然有效</para>
        /// </value>
        public bool IsMessageGetting
        {
            get => p_isMessageGetting;
            set
            {
                p_isMessageGetting = value;
            }
        }

        #endregion

        #region 对象绘制功能

        /// <summary>
        /// 添加一个可绘制对象
        /// </summary>
        /// <param name="obj">可绘制对象</param>
        /// <exception cref="ArgumentNullException">参数为null</exception>
        public void AddDrawObject(IDrawingObject obj)
        {
            ThrowObjectDisposeException();
            if (obj is null) throw new ArgumentNullException();

            lock (p_addrawBuffer)
            {
                p_addrawBuffer.Push(obj);
            }
        }

        /// <summary>
        /// 添加一组可绘制对象
        /// </summary>
        /// <param name="objs">可绘制对象集合</param>
        /// <exception cref="ArgumentNullException">参数为null</exception>
        public void AddDrawObject(IEnumerable<IDrawingObject> objs)
        {
            ThrowObjectDisposeException();
            if (objs is null) throw new ArgumentNullException();
            lock (p_addrawBuffer)
            {
                foreach (var item in objs)
                {
                    if (item is null) continue;
                    p_addrawBuffer.Push(item);
                }
            }
        }

        /// <summary>
        /// 添加一组可绘制对象
        /// </summary>
        /// <param name="list">可绘制对象集合</param>
        /// <param name="index">要添加的对象起始索引</param>
        /// <param name="count">要添加的对象个数</param>
        /// <exception cref="ArgumentNullException">参数为null</exception>
        /// <exception cref="ArgumentOutOfRangeException">超出索引范围</exception>
        public void AddDrawObject(IList<IDrawingObject> list, int index, int count)
        {
            ThrowObjectDisposeException();
            if (list is null) throw new ArgumentNullException();
            if (index < 0 || count < 0 || index + count > list.Count) throw new ArgumentOutOfRangeException();
            if (count == 0) return;
            lock (p_addrawBuffer)
            {
                var length = index + count;
                for (; index < length; index++)
                {
                    var d = list[index];
                    if (d is null) continue;
                    p_addrawBuffer.Push(d);
                }
            }
        }

        /// <summary>
        /// 移除指定的可绘制对象
        /// </summary>
        /// <param name="obj">要移除的对象</param>
        /// <returns>是否成功移除</returns>
        /// <exception cref="ArgumentNullException">参数为null</exception>
        public bool RemoveDrawObject(IDrawingObject obj)
        {
            ThrowObjectDisposeException();
            if (obj is null) throw new ArgumentNullException();
            lock (p_draws) return p_draws.Remove(obj);
        }

        /// <summary>
        /// 根据谓词获取可绘制对象
        /// </summary>
        /// <param name="match">谓词</param>
        /// <returns>满足条件的可绘制对象，若没有则返回null</returns>
        public IDrawingObject FindDrawObject(Predicate<IDrawingObject> match)
        {
            ThrowObjectDisposeException();
            lock (p_draws) return p_draws.Find(match);
        }

        /// <summary>
        /// 移除所有可绘制对象
        /// </summary>
        public void ClearDrawObjects()
        {
            ThrowObjectDisposeException();
            lock (p_draws) p_draws.Clear();
        }

        /// <summary>
        /// 刷新对象绘制顺序
        /// </summary>
        /// <remarks>
        /// 在主循环内的绘图优先级按照<see cref="IDrawingObject.Lay"/>参数由低到高依次调用<see cref="IDrawingObject.Drawing"/>；<br/>
        /// 当一个对象被调用<see cref="IDrawingObject.Start"/>方法后，已处于绘图循环内时，再修改<see cref="IDrawingObject.Lay"/>参数将不会自动更改该对象绘图顺序，需要调用该方法以刷新绘图顺序；
        /// </remarks>
        public void RefreshDrawObjectByLay()
        {
            ThrowObjectDisposeException();
            lock (p_draws)
            {
                p_draws.Sort(p_comparerDrawing);
            }
        }

        /// <summary>
        /// 立即刷新绘图缓冲区
        /// </summary>
        public void FlushBatchDraw()
        {
            if(Device.IsOpenWindow) Device.FlushBatchDraw();
        }

        /// <summary>
        /// 是否开启画面渲染
        /// </summary>
        /// <value>
        /// <para>修改该参数以开启或关闭画面渲染；true表示开启画面渲染，false表示关闭画面渲染；该参数默认初始值为true</para>
        /// <para>当该参数为false时，将不会在每一帧调用对象的<see cref="IDrawingObject.Drawing"/>方法，也不会在每一帧重置画面缓冲区；</para>
        /// <para>在<see cref="IDrawingObject.Drawing"/>方法内设置该参数不会立即停止渲染，而是会在下一帧开始停止</para>
        /// <para>可以在<see cref="LoopFunction.UpdateEvent"/>或<see cref="LoopFunction.FixedUpdateEvent"/>等帧循环事件中修改该参数，再调用<see cref="FlushBatchDraw"/>，以达到停止渲染并重置画面为背景色的效果</para>
        /// </value>
        public bool IsRendering
        {
            get => p_isRendering;
            set
            {
                ThrowObjectDisposeException();
                p_isRendering = value;
            }
        }

        /// <summary>
        /// 访问或设置画面背景色
        /// </summary>
        public RGBColor BackgroundColor
        {
            get => RGBColor.BKColor;
            set
            {
                RGBColor.BKColor = value;
            }
        }

        /// <summary>
        /// 访问或设置画面背景色
        /// </summary>
        public System.Drawing.Color BackgroundSystemColor
        {
            get
            {
                return RGBColor.BKColor.ToSColor();
            }
            set
            {
                RGBColor.BKColor = new RGBColor(value.R, value.G, value.B);
            }
        }

        /// <summary>
        /// 获取该窗口HDC的绘图GDI
        /// </summary>
        /// <remarks>
        /// 请不要随意释放此实例，若要释放非托管资源，请调用<see cref="LoopFunction.Close"/>
        /// </remarks>
        public Graphics DrawHDCGraphics
        {
            get
            {
                ThrowObjectDisposeException();
                return p_graphics;
            }
        }

        /// <summary>
        /// 在整个绘图区域绘制图像
        /// </summary>
        /// <remarks>使用<see cref="Graphics"/>绘制图像到窗口</remarks>
        /// <param name="image">要绘制在绘图区域的图像</param>
        /// <exception cref="ArgumentNullException">参数为null</exception>
        /// <exception cref="EasyX.Exceptions.WindowEasyXException">窗口未初始化</exception>
        public void DrawImage(Image image)
        {
            ThrowObjectDisposeException();
            var size = Device.GraphSize;
            p_graphics?.DrawImage(image, new Rectangle(0, 0, size.x, size.y));
        }

        /// <summary>
        /// 在指定区域绘制图像
        /// </summary>
        /// <remarks>使用<see cref="Graphics"/>绘制图像到窗口指定区域</remarks>
        /// <param name="image">要绘制在绘图区域的图像</param>
        /// <param name="rect">要绘制到的窗口区域，按像素为单位</param>
        /// <exception cref="ArgumentNullException">参数为null</exception>
        /// <exception cref="EasyX.Exceptions.WindowEasyXException">窗口未初始化</exception>
        public void DrawImage(Image image, ERect rect)
        {
            ThrowObjectDisposeException();
            p_graphics?.DrawImage(image, rect.ToRectangle());
        }

        #endregion

        #region 运行

        /// <summary>
        /// 在当前线程开始执行easyx游戏主循环
        /// </summary>
        /// <param name="width">窗口长度</param>
        /// <param name="height">窗口高度</param>
        /// <param name="flag">是否打开控制台</param>
        public void Run(int width, int height, bool flag)
        {
            Device.InitGraph(width, height, flag);
            Device.BeginBatchDraw();

            LoopFunctionMethod();
        }

        /// <summary>
        /// 调整窗口大小
        /// </summary>
        /// <param name="width">窗口长度</param>
        /// <param name="height">窗口高度</param>
        public void ResizeForm(int width, int height)
        {
            if (!Device.IsOpenWindow) throw new EasyXEngineExcption("窗口未初始化");
            Device.InitGraph(width, height);
            f_initGrapHDC();
        }

        /// <summary>
        /// 在当前线程开始执行easyx游戏主循环
        /// </summary>
        /// <param name="width">窗口长度</param>
        /// <param name="height">窗口高度</param>
        public void Run(int width, int height)
        {
            Device.InitGraph(width, height);
            
            Device.BeginBatchDraw();

            LoopFunctionMethod();
        }

        #endregion

        #endregion

        #region 派生

        #region 循环

        /// <summary>
        /// 窗口退出前释放
        /// </summary>
        private void f_exitDispose()
        {
            lock (p_addrawBuffer)
            {
                while (p_addrawBuffer.Count != 0)
                {
                    var pop = p_addrawBuffer.Pop();
                    if(pop is IDisposable disp)
                    {
                        disp.Dispose();
                    }
                }
            }
            lock (p_draws)
            {
                int length = p_draws.Count;
                for (int i = 0; i < length; i++)
                {
                    if (p_draws[i] is IDisposable disp)
                    {
                        disp.Dispose();
                    }
                }
            }

            lock (p_gdiThreadSafeObj)
            {
                p_graphics.Dispose();
            }

        }

        private void f_toDrawingObj()
        {

            lock (p_addrawBuffer)
            {
                IDrawingObject dro;
                while (p_addrawBuffer.Count != 0)
                {
                    //弹出一个待加入对象
                    dro = p_addrawBuffer.Pop();
                    if (!p_draws.Contains(dro))
                    {
                        //调用初始化方法
                        dro.Start();
                        p_draws.Add(dro);
                    }
                }
            }

            //按绘制层级排序
            if(p_draws.Count != 0) p_draws.Sort(p_comparerDrawing);

        }

        protected override void LoopFirst()
        {
            if (Device.IsOpenWindow)
            {
                if (p_isRendering) Device.Cleardevice();

                if (p_isMessageGetting) f_messageOnceLoop();

                f_toDrawingObj();
            }
        }

        private void f_beginDrawObjs()
        {

            lock (p_draws)
            {
                int count = p_draws.Count;
                if (count == 0) return;

                //if (!p_draws.IsOrdered(p_comparerDrawing)) p_draws.Sort(p_comparerDrawing);

                IDrawingObject dobj;
                for (int i = 0; i < count; i++)
                {
                    dobj = p_draws[i];
                    if (dobj.Active) dobj.Drawing();
                }
            }

        }

        protected override void LoopEnd()
        {
            if (Device.IsOpenWindow)
            {
                if (p_isRendering)
                {
                    f_beginDrawObjs();
                    Device.FlushBatchDraw();
                }
            }
        }

        protected override void ExitLoop()
        {
            f_exitDispose();

            p_addrawBuffer.Clear();
            p_draws.Clear();
            if (Device.IsOpenWindow)
            {
                Device.CloseGraph();
            }
        }

        protected override void LoopStartInvoke()
        {
            f_initGrapHDC();
        }

        //protected override void EndLoopWaitFPS()
        //{
        //    if(p_fps > 0)
        //    {

        //        //计算等待时间
        //        var waitTime = TimeSpan.FromSeconds((1d / p_fps)) - this.p_nowFrameTime;

        //        //var sw = new System.Threading.SpinWait();

        //        if (waitTime > TimeSpan.Zero)
        //        {
        //            //waitTime = new TimeSpan(waitTime.Ticks / 2);
        //            //Thread.Sleep(waitTime);

        //            //p_waitTimer.Reset();
        //            p_waitTimer.Restart();

        //            SpinWait wait = new SpinWait();

        //            while (p_waitTimer.Elapsed < waitTime)
        //            {
        //                wait.SpinOnce();
        //            }

        //            //p_waitTimer.Reset();
        //        }
        //    }
        //}

        #endregion

        #region 接口

        /// <summary>
        /// 返回游戏窗体的HWND句柄；若窗体未打开则返回空句柄
        /// </summary>
        public IntPtr Handle
        {
            get => Device.WindowHandle;
        }

        /// <summary>
        /// 返回游戏窗体的HDC句柄；若窗体未打开则返回空句柄
        /// </summary>
        public IntPtr HandleHDC
        {
            get
            {
                if(Device.IsOpenWindow) return Device.WindowHDC;
                return IntPtr.Zero;
            }
        }

        #endregion

        #endregion

    }

}
