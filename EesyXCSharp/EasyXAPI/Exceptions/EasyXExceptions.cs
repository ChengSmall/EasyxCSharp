using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Cheng.EasyX.Exceptions
{

    /// <summary>
    /// easyX库的异常处理基类
    /// </summary>
    public class EasyXException : Exception
    {
        
        protected const string easyXMessageDefault = "EasyX库所引发的异常";

        public EasyXException() : base(easyXMessageDefault)
        {
        }

        public EasyXException(string message) : base(message)
        {
        }

        public EasyXException(string message, Exception exception) : base(message, exception)
        {
        }

        protected EasyXException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// easyX窗口引发的异常
    /// </summary>
    public class WindowEasyXException : EasyXException
    {
        public WindowEasyXException() : base(easyXMessageDefault)
        {
        }

        public WindowEasyXException(string message) : base(message)
        {
        }

        public WindowEasyXException(string message, Exception exception) : base(message, exception)
        {
        }

        protected WindowEasyXException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
