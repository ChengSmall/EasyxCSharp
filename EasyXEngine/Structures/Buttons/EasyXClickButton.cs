using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cheng.EasyX.DataStructure;
using Cheng.EasyX;
using Cheng.ButtonTemplates;
using Cheng.LoopThreads;

namespace Cheng.EasyXEngine.Structures.Buttons
{

    /// <summary>
    /// 可被鼠标点击的按钮
    /// </summary>
    public abstract class EasyXClickButton : RegistrationEventButton, IDrawingObject
    {

        #region 结构

        /// <summary>
        /// 鼠标点击类型
        /// </summary>
        public enum MouseClick
        {
            /// <summary>
            /// 左键点击
            /// </summary>
            LeftClick,
            /// <summary>
            /// 右键点击
            /// </summary>
            RightClick,
            /// <summary>
            ///  中键点击
            /// </summary>
            MidClick
        }

        #endregion

        #region 释放

        protected override bool Disposing(bool suppressFinalize)
        {
            if (suppressFinalize)
            {
                this.ButtonClickEvent = null;
                this.MouseInEvent = null;
                this.MouseOutEvent = null;
            }

            return base.Disposing(suppressFinalize);
        }

        #endregion

        #region 构造
      
        protected EasyXClickButton()
        {
            p_active = true;
        }
        protected EasyXClickButton(bool active)
        {
            p_active = active;
        }

        #endregion

        #region 参数

        /// <summary>
        /// 按钮所在位置
        /// </summary>
        protected ERect p_buttonRect;

        /// <summary>
        /// 渲染层级
        /// </summary>
        protected int p_lay;

        protected bool p_active;

        #endregion

        #region 功能

        #region 参数访问

        /// <summary>
        /// 按钮所在范围
        /// </summary>
        public ERect Position
        {
            get => p_buttonRect;
            set => p_buttonRect = value;
        }

        #endregion

        #region 事件封装

        /// <summary>
        /// 消息事件注册
        /// </summary>
        /// <remarks>派生类重写需要调用基实现</remarks>
        /// <param name="loop"></param>
        /// <param name="obj"></param>
        protected override void fe_GetMessageEventInvoke(LoopFunction loop, CsMessage obj)
        {

            var type = obj.Message;

            var mrg = gameForm.MouseArg;
            var pos = mrg.mousePos;

            bool latne = IsButtonIn(mrg.lateMousePos);

            bool ne = IsButtonIn(pos);

            if(latne != ne)
            {
                if (latne)
                {
                    //移出
                    MouseOut();
                }
                else
                {
                    //移入
                    MouseIn();
                }
            }

            
            if (type == MessageValue.LeftButton_Down)
            {
                ClickDown(MouseClick.LeftClick);
            }
            else if (type == MessageValue.LeftButton_UP)
            {
                ClickUp(MouseClick.LeftClick);
            }
            else if (type == MessageValue.MidButton_Down)
            {
                ClickDown(MouseClick.MidClick);
            }
            else if (type == MessageValue.MidButton_UP)
            {
                ClickUp(MouseClick.MidClick);
            }
            else if (type == MessageValue.RightButton_Down)
            {
                ClickDown(MouseClick.RightClick);
            }
            else if (type == MessageValue.RightButton_UP)
            {
                ClickUp(MouseClick.RightClick);
            }
        }

        ///// <summary>
        ///// 每当获取消息时执行的方法
        ///// </summary>
        ///// <param name="obj">获取的消息</param>
        //protected virtual void GetMessageEvent(CsMessage obj) { }

        /// <summary>
        /// 引发按钮点击事件的函数
        /// </summary>
        /// <remarks>在派生类重写使需要调用基实现</remarks>
        protected virtual void ClickInvoke()
        {
            ButtonClickEvent?.Invoke(this);
        }

        /// <summary>
        /// 当鼠标被按下时执行，重写此方法实现功能
        /// </summary>
        /// <param name="click">鼠标按钮类型</param>
        protected virtual void ClickDown(MouseClick click)
        {
        }

        /// <summary>
        /// 当鼠标被松开时执行，重写此方法实现功能
        /// </summary>
        /// <param name="click">鼠标按钮类型</param>
        protected virtual void ClickUp(MouseClick click)
        {
        }

        /// <summary>
        /// 当鼠标移出按钮范围内时调用此方法
        /// </summary>
        /// <remarks>在派生类重写时需要调用基实现</remarks>
        protected virtual void MouseOut()
        {
            if(p_active) MouseOutEvent?.Invoke(this);
        }

        /// <summary>
        /// 当鼠标移入按钮范围内时调用此方法
        /// </summary>
        /// <remarks>在派生类重写时需要调用基实现</remarks>
        protected virtual void MouseIn()
        {
            if (p_active) MouseInEvent?.Invoke(this);
        }

        #endregion

        #region 事件

        /// <summary>
        /// 当鼠标进入按钮范围内时引发的事件
        /// </summary>
        public event ButtonEvent<EasyXClickButton> MouseInEvent;

        /// <summary>
        /// 当鼠标从按钮范围内离开时引发的事件
        /// </summary>
        public event ButtonEvent<EasyXClickButton> MouseOutEvent;

        #endregion

        #region 功能

        /// <summary>
        /// 判断给定点是否处于按钮范围内
        /// </summary>
        /// <param name="pos">给定点</param>
        /// <returns>处于按钮范围内返回true，否则返回false</returns>
        public bool IsButtonIn(EPoint pos)
        {
            var rect = this.p_buttonRect;
            return ((pos.x >= rect.left && pos.x <= rect.right) && (pos.y >= rect.top && pos.y <= rect.bottom));
        }

        #endregion

        #region 派生

        /// <summary>
        /// 在派生类重写该函数以实现按钮在窗口的显示图像
        /// </summary>
        public virtual void Drawing() { }

        public virtual void Start() { }

        public bool Active
        {
            get => p_active;
            set => p_active = value;
        }

        public override bool CanButtonClick => true;

        public int Lay
        {
            get
            {
                return p_lay;
            }
            set
            {
                p_lay = value;
            }
        }

        public override event ButtonEvent<BaseButton> ButtonClickEvent;

        #endregion

        #endregion

    }

}
