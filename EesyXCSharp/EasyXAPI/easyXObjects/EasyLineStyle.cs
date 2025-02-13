

namespace Cheng.EasyX.DataStructure
{

    #region 结构

    /// <summary>
    /// 画线样式
    /// </summary>
    public enum LineStyleType : uint
    {
        /// <summary>
        /// 线形为实线
        /// </summary>
        SOLID = 0,
        /// <summary>
        /// 线形为：------------ 
        /// </summary>
        DASH = 1,
        /// <summary>
        /// 线形为：············ 
        /// </summary>
        DOT = 2,
        /// <summary>
        /// 线形为：-·-·-·-·-·-· 
        /// </summary>
        DASHDOT = 3,
        /// <summary>
        /// 线形为：-··-··-··-·· 
        /// </summary>
        DASHDOTDOT = 4,
        /// <summary>
        /// 线形为不可见
        /// </summary>
        NULL = 5,
        /// <summary>
        /// 线形样式为用户自定义，由参数 puserstyle 和 userstylecount 指定 
        /// </summary>
        USERSTYLE = 7
    }

    #endregion


}
