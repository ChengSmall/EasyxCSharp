
namespace Cheng.EasyX.DataStructure
{
    #region

    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtend
    {
        //各个枚举扩展方法

        /// <summary>
        /// 该消息是否是鼠标消息类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsMouseType(this MessageValue value)
        {
            return (value & MessageValue.MouseType) != 0;
        }

        /// <summary>
        /// 该消息是否是键盘消息类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsKeyType(this MessageValue value)
        {
            return (value & MessageValue.KeyType) != 0;
        }

    }

    #endregion

}
