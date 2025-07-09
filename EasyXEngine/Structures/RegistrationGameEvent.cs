using Cheng.EasyX.DataStructure;
using Cheng.LoopThreads;
using Cheng.Memorys;

namespace Cheng.EasyXEngine.Structures
{

    /// <summary>
    /// 可自动注册游戏事件的类
    /// </summary>
    public abstract class RegistrationGameEvent : SafreleaseUnmanagedResources
    {

        /// <summary>
        /// 注册游戏事件系统
        /// </summary>
        /// <exception cref="EasyXEngineExcption">没有调用初始化方法</exception>
        protected RegistrationGameEvent()
        {
            gameForm = GameForm.Game;
            gameForm.GetMessageEvent += fe_GetMessageEventInvoke;
            gameForm.UpdateEvent += fe_UpdateEventInvoke;
        }

        /// <summary>
        /// 游戏事件的回调函数
        /// </summary>
        /// <remarks>在派生类实现该实例的事件行为</remarks>
        /// <param name="loop"></param>
        /// <param name="obj"></param>
        protected virtual void fe_GetMessageEventInvoke(LoopFunction loop, CsMessage obj) { }

        /// <summary>
        /// 帧循环事件回调函数
        /// </summary>
        /// <param name="loop"></param>
        protected virtual void fe_UpdateEventInvoke(LoopFunction loop) { }

        /// <summary>
        /// 取消注册事件，在派生类重写需要调用基实现
        /// </summary>
        /// <param name="disposeing"></param>
        /// <returns></returns>
        protected override bool Disposeing(bool disposeing)
        {
            if (disposeing)
            {
                gameForm.GetMessageEvent -= fe_GetMessageEventInvoke;
                gameForm.UpdateEvent -= fe_UpdateEventInvoke;
            }

            return true;
        }

        /// <summary>
        /// 游戏实例
        /// </summary>
        protected readonly GameForm gameForm;

        /// <summary>
        /// 注销游戏事件系统和释放资源
        /// </summary>
        public override void Close()
        {
            Dispose(true);
        }

    }

    /// <summary>
    /// 可绘制和注册事件对象基实现
    /// </summary>
    public abstract class DrawObject : RegistrationGameEvent, IDrawingObject
    {

        protected DrawObject()
        {
            p_active = true;
        }

        protected DrawObject(bool active)
        {
            p_active = active;
        }

        /// <summary>
        /// 层级
        /// </summary>
        protected int p_lay;

        /// <summary>
        /// 是否启用绘制
        /// </summary>
        protected bool p_active;

        public int Lay
        {
            get => p_lay;
            set => p_lay = value;
        }

        public virtual bool Active
        {
            get => p_active;
            set => p_active = value;
        }

        public abstract void Drawing();
        public virtual void Start() { }
    }

}
