using Cheng.ButtonTemplates;
using Cheng.EasyX.DataStructure;
using Cheng.LoopThreads;

namespace Cheng.EasyXEngine.Structures.Buttons
{

    /// <summary>
    /// 可映射虚拟键码的EasyX按钮
    /// </summary>
    public sealed class VKCodeButton : EasyXButton
    {

        #region 构造

        /// <summary>
        /// 实例化一个虚拟键按钮
        /// </summary>
        /// <param name="keyCode">要映射的虚拟键码</param>
        /// <exception cref="EasyXEngineExcption">游戏引擎未初始化</exception>
        public VKCodeButton(VkCode keyCode)
        {
            p_keyCode = keyCode;
            p_game = GameForm.Game;
        }

        /// <summary>
        /// 实例化一个虚拟键按钮
        /// </summary>
        /// <exception cref="EasyXEngineExcption">游戏引擎未初始化</exception>
        public VKCodeButton()
        {
            p_keyCode = VkCode.None;
            p_game = GameForm.Game;
        }
        #endregion

        #region 参数

        private GameForm p_game;
        private VkCode p_keyCode;

        /// <summary>
        /// 访问或设置映射的虚拟键码
        /// </summary>
        public VkCode KeyCode
        {
            get => p_keyCode;
            set => p_keyCode = value;
        }

        #endregion

        #region 派生

        public override ButtonAvailablePermissions AvailablePermissions
        {
            get
            {
                const ButtonAvailablePermissions my =
                 ButtonAvailablePermissions.CanGetAllStatePower |
                 ButtonAvailablePermissions.CanGetFrameValue |
                 ButtonAvailablePermissions.AllFrameGetPermissions;

                return my;
            }
        }

        /// <summary>
        /// 当前帧按钮是否被按下
        /// </summary>
        public override bool ButtonDown => p_game.GetKeyDown(p_keyCode);

        /// <summary>
        /// 当前帧按钮是否抬起
        /// </summary>
        public override bool ButtonUp => p_game.GetKeyUp(p_keyCode);

        /// <summary>
        /// 当前按钮是否处于按下状态
        /// </summary>
        public override bool ButtonState
        {
            get => p_game.GetKey(p_keyCode);
            set => ThrowSupportedException();
        }

        public override float Power
        {
            get => p_game.GetKey(p_keyCode) ? 1 : 0;
            set => ThrowSupportedException();
        }

        public override float MaxPower => 1;

        public override float MinPower => 0;

        #region 事件

        #endregion

        /// <summary>
        /// 返回当前映射的虚拟键码枚举名
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return p_keyCode.ToString();
        }

        #endregion

    }

}
