using Cheng.EasyX.CPP;
using Cheng.EasyX.DataStructure;
using Cheng.EasyX.Exceptions;

namespace Cheng.EasyX
{

    /// <summary>
    /// easyx消息处理
    /// </summary>
    public unsafe static class Messaging
    {

        /// <summary>
        /// 获取一个消息，指定全部类型
        /// </summary>
        /// <remarks>若没有消息，则做线程等待直至有消息传入</remarks>
        /// <returns>获取的消息</returns>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        public static CsMessage GetMessage()
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            return EasyX_API.getmessage_1(byte.MaxValue);
        }

        /// <summary>
        /// 获取一个消息，指定全部类型
        /// </summary>
        /// <remarks>若没有消息，则做线程等待直至有消息传入</remarks>
        /// <param name="type">要获取的消息类型</param>
        /// <returns>获取的消息</returns>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        public static CsMessage GetMessage(MessageType type)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            return EasyX_API.getmessage_1((byte)type);
        }

        /// <summary>
        /// 从消息队列中判断并获取一个消息
        /// </summary>
        /// <remarks>此函数不做等待</remarks>
        /// <param name="message">要获取的消息</param>
        /// <param name="type">要获取的消息类型</param>
        /// <param name="isRemove">获取消息后是否将其从消息队列清除</param>
        /// <returns>是否成功获取</returns>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        public static bool GetMessage(out CsMessage message, MessageType type, bool isRemove)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            fixed (CsMessage* cp = &message)
            {
                *cp = default;
                return EasyX_API.peekmessage_(cp, (byte)type, isRemove);
            }
        }

        /// <summary>
        /// 从消息队列中判断并获取一个消息
        /// </summary>
        /// <remarks>此函数不做等待</remarks>
        /// <param name="message">要获取的消息，成功获取后将会从消息队列中清除</param>
        /// <param name="type">要获取的消息类型</param>
        /// <returns>是否成功获取</returns>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        public static bool GetMessage(out CsMessage message, MessageType type)
        {
            return GetMessage(out message, type, true);
        }

        /// <summary>
        /// 从消息队列中判断并获取一个消息
        /// </summary>
        /// <remarks>此函数不做等待</remarks>
        /// <param name="message">要获取的消息，成功获取后将会从消息队列中清除</param>
        /// <returns>是否成功获取</returns>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        public static bool GetMessage(out CsMessage message)
        {
            return GetMessage(out message, MessageType.All, true);
        }

        /// <summary>
        /// 清空指定类型的消息队列
        /// </summary>
        /// <param name="type">消息类型</param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        public static void FlushMessage(MessageType type)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            EasyX_API.flushmessage_((byte)type);
        }

        /// <summary>
        /// 清空消息队列中的所有消息
        /// </summary>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        public static void FlushMessage()
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            EasyX_API.flushmessage_();
        }

    }

}
