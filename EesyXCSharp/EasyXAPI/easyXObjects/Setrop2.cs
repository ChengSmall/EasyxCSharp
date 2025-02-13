

namespace Cheng.EasyX.DataStructure
{
    /// <summary>
    /// 二元光栅操作码
    /// </summary>
    public enum Setrop2
    {
		#region
		/// <summary>
		/// 黑色
		/// </summary>
		BLACK,
		/// <summary>
		/// NOT(屏幕颜色 OR 当前颜色)
		/// </summary>
		NOTMERGEPEN,
		/// <summary>
		/// 屏幕颜色 AND(NOT 当前颜色)
		/// </summary>
		MASKNOTPEN,
		/// <summary>
		/// NOT 当前颜色
		/// </summary>
		NOTCOPYPEN,
		/// <summary>
		/// (NOT 屏幕颜色) AND 当前颜色
		/// </summary>
		MASKPENNOT,
		/// <summary>
		/// NOT 屏幕颜色
		/// </summary>
		NOT,
		/// <summary>
		/// 屏幕颜色 XOR 当前颜色
		/// </summary>
		XORPEN,
		/// <summary>
		/// NOT(屏幕颜色 AND 当前颜色)
		/// </summary>
		NOTMASKPEN,
		/// <summary>
		/// 屏幕颜色 AND 当前颜色
		/// </summary>
		R2_MASKPEN,
		/// <summary>
		/// NOT(屏幕颜色 XOR 当前颜色)
		/// </summary>
		NOTXORPEN,
		/// <summary>
		/// 屏幕颜色
		/// </summary>
		NOP,
		/// <summary>
		/// 屏幕颜色 OR(NOT 当前颜色)
		/// </summary>
		MERGENOTPEN,
		/// <summary>
		/// 当前颜色（默认）
		/// </summary>
		COPYPEN,
		/// <summary>
		/// (NOT 屏幕颜色) OR 当前颜色
		/// </summary>
		MERGEPENNOT,
		/// <summary>
		/// 屏幕颜色 OR 当前颜色
		/// </summary>
		MERGEPEN,
		/// <summary>
		/// 白色
		/// </summary>
		WHITE
		#endregion
	}
}
