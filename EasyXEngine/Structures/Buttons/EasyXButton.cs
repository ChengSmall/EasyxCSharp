using Cheng.ButtonTemplates;
using Cheng.EasyX.DataStructure;
using Cheng.LoopThreads;
using System;

namespace Cheng.EasyXEngine.Structures.Buttons
{

    /// <summary>
    /// EasyXEngine的按钮基类
    /// </summary>
    public abstract class EasyXButton : ResourceButton
    {

        protected EasyXButton()
        {
        }

        #region 参数

        #endregion

        #region 功能

        //public override bool CanGetFrameValue => true;

        public override ButtonAvailablePermissions AvailablePermissions
        {
            get
            {
                return ButtonAvailablePermissions.CanGetFrameValue;
            }
        }

        public override long NowFrame => (long)GameForm.Game.Frame;

        /// <summary>
        /// 是否在当前帧按下按钮
        /// </summary>
        /// <exception cref="NotSupportedException">没有权限</exception>
        public override bool ButtonDown
        {
            get => ThrowSupportedException<bool>();
        }

        /// <summary>
        /// 是否在当前帧松开按钮
        /// </summary>
        /// <exception cref="NotSupportedException">没有权限</exception>
        public override bool ButtonUp
        {
            get => ThrowSupportedException<bool>();
        }

        #endregion

    }

    /// <summary>
    /// 一个自动注册游戏事件的按钮基类
    /// </summary>
    public abstract class RegistrationEventButton : EasyXButton
    {

        protected RegistrationEventButton()
        {
            gameForm = GameForm.Game;
            gameForm.GetMessageEvent += fe_GetMessageEventInvoke;
            gameForm.UpdateEvent += fe_Update;
        }

        /// <summary>
        /// 在派生类重写以释放资源，需要调用基实现
        /// </summary>
        /// <param name="suppressFinalize"></param>
        /// <returns></returns>
        protected override bool Disposing(bool suppressFinalize)
        {
            if (suppressFinalize)
            {
                gameForm.GetMessageEvent -= fe_GetMessageEventInvoke;
                gameForm.UpdateEvent -= fe_Update;
            }

            return true;
        }

        /// <summary>
        /// 游戏实例
        /// </summary>
        protected GameForm gameForm;

        /// <summary>
        /// 游戏事件的回调函数
        /// </summary>
        /// <remarks>在派生类实现该实例的事件行为</remarks>
        /// <param name="loop"></param>
        /// <param name="obj">事件消息参数</param>
        protected virtual void fe_GetMessageEventInvoke(LoopFunction loop, CsMessage obj) { }

        /// <summary>
        /// 游戏帧循环
        /// </summary>
        /// <param name="loop"></param>
        protected virtual void fe_Update(LoopFunction loop) { }

        /// <summary>
        /// 注销事件系统并释放相关资源
        /// </summary>
        public override void Close()
        {
            Dispose(true);
        }

        public override long NowFrame => (long)gameForm.Frame;

    }

}
