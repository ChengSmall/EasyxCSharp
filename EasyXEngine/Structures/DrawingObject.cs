using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheng.EasyXEngine.Structures
{

    /// <summary>
    /// 可绘制对象公共接口
    /// </summary>
    public interface IDrawingObject
    {

        /// <summary>
        /// 绘制层级
        /// </summary>
        /// <returns>该参数表示图像的绘制顺序，值从小到大排序，先绘制较小值，后绘制较大值，后绘制的会覆盖先绘制的图像</returns>
        int Lay { get; }

        /// <summary>
        /// 在派生类中重写此方法，以实现该对象的图像绘制
        /// </summary>
        /// <remarks>
        /// <para>
        /// 在派生类实现中使用<see cref="EasyX.Drawing"/>，<see cref="EasyX.TextInputOut"/>类或其它绘制函数绘制图像
        /// </para>
        /// </remarks>
        void Drawing();

        /// <summary>
        /// 在开始正式进入绘制循环前调用的方法，用于初始化
        /// </summary>
        void Start();

        /// <summary>
        /// 是否启用绘制功能
        /// </summary>
        /// <remarks>
        /// 在<see cref="GameForm"/>的绘图循环中，每次绘图会检查该参数，只有为true才会调用<see cref="Drawing"/>
        /// </remarks>
        /// <returns>该参数返回true时可进行绘制，false则不做绘制</returns>
        bool Active { get; set; }

    }

}
