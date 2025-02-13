using Cheng.EasyX.Exceptions;
using System;

namespace Cheng.EasyXEngine
{

    /// <summary>
    /// EasyX游戏引擎引发的异常
    /// </summary>
    public class EasyXEngineExcption : EasyXException
    {
        /// <summary>
        /// EasyX游戏引擎引发的异常
        /// </summary>
        public EasyXEngineExcption() : base("easyX引擎引发的异常")
        {
        }

        /// <summary>
        /// EasyX游戏引擎引发的异常
        /// </summary>
        /// <param name="message"></param>
        public EasyXEngineExcption(string message) : base(message)
        {
        }

        /// <summary>
        /// EasyX游戏引擎引发的异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public EasyXEngineExcption(string message, Exception exception) : base(message, exception)
        {
        }

    }
}
