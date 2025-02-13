namespace Cheng.EasyX.DataStructure
{

    #region 结构
    /// <summary>
    /// 填充类型
    /// </summary>
    public enum FillStyleType
    {
        /// <summary>
        /// 固实填充
        /// </summary>
        SOLID = 0,
        /// <summary>
        /// 不填充
        /// </summary>
        NULL = 1,
        /// <summary>
        /// 图案填充
        /// </summary>
        HATCHED = 2,
        /// <summary>
        /// 自定义图案填充
        /// </summary>
        PATTERN = 3,
        /// <summary>
        /// 自定义图像填充
        /// </summary>
        DIBPATTERN = 5
    }

    /// <summary>
    /// 填充图案类型，具体含义请参考EasyX文档
    /// </summary>
    /// <remarks>
    /// https://docs.easyx.cn/setfillstyle
    /// </remarks>
    public enum HatchType
    {
        HORIZONTAL = 0,
        VERTICAL = 1,
        FDIAGONAL = 2,
        BDIAGONAL = 3,
        CROSS = 4,
        DIAGCROSS = 5
    }

    #endregion

    
}
