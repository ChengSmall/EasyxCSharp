using System.Text;
using System;

namespace Cheng.EasyX
{

    /// <summary>
    /// 可将用户的对话框输入存入缓冲区的结构
    /// </summary>
    public sealed class EasyXTextInput
    {

        #region 构造
        /// <summary>
        /// 实例化用户对话框输入缓冲区
        /// </summary>
        public EasyXTextInput()
        {
            f_init();
        }

        private void f_init()
        {
            p_buffer = new StringBuilder(32);
            p_maxInput = 32;
        }
        #endregion

        #region 参数

        private StringBuilder p_buffer;

        private string p_title;
        private string p_prompt;
        private string p_defText;

        private int p_width;
        private int p_height;
        private int p_maxInput;

        #endregion

        #region 参数访问
        /// <summary>
        /// 获取用户输入缓存
        /// </summary>
        public StringBuilder TextBuffer
        {
            get => p_buffer;
        }
        /// <summary>
        /// 访问或设置打开对话框时的标题，默认为null
        /// </summary>
        public string Title
        {
            get => p_title;
            set => p_title = value;
        }
        /// <summary>
        /// 访问或设置打开对话框时的对话框提示信息，默认为null
        /// </summary>
        public string Prompt
        {
            get => p_prompt;
            set => p_prompt = value;
        }
        /// <summary>
        /// 访问或设置打开对话框时的默认输入文本，默认为null
        /// </summary>
        public string DefaulText
        {
            get => p_defText;
            set
            {
                p_defText = value;
                if(value != null && value.Length > p_maxInput)
                {
                    p_maxInput = value.Length;
                }
            }
        }
        /// <summary>
        /// 访问或设置打开对话框时的输入框宽度，默认为0
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">值小于0</exception>
        public int Width
        {
            get => p_width;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException();
                p_width = value;
            }
        }
        /// <summary>
        /// 访问或设置打开对话框时的输入框高度，默认为0
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">值小于0</exception>
        public int Height
        {
            get => p_height;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException();
                p_height = value;
            }
        }
        /// <summary>
        /// 访问或设置打开对话框时用户可输入的最大字符数，默认为32
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">值小于0</exception>
        public int MaxInputCount
        {
            get => p_maxInput;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException();               
                p_maxInput = value;
            }
        }
        #endregion

        #region 功能
        /// <summary>
        /// 暂停线程开启用户输入对话框，返回用户输入；隐藏取消按钮
        /// </summary>
        /// <returns>用户输入</returns>
        public string Input()
        {
            if (p_buffer.Capacity < p_maxInput) p_buffer.Capacity = p_maxInput;
            TextInputOut.InputBox(p_buffer, p_maxInput, p_title, p_prompt, p_defText, p_width, p_height, true);
            string str = p_buffer.ToString();
            p_buffer.Clear();
            return str;
        }
        /// <summary>
        /// 暂停线程开启用户输入对话框，返回用户输入
        /// </summary>
        /// <param name="text">返回的输入</param>
        /// <returns>用户点击的按钮，点击确定返回true，点击取消返回false</returns>
        public bool Input(out string text)
        {
            bool flag = TextInputOut.InputBox(p_buffer, p_maxInput, p_title, p_prompt, p_defText, p_width, p_height, false);
            if (flag) text = p_buffer.ToString();
            else text = null;

            p_buffer.Clear();
            return flag;
        }
        /// <summary>
        /// 暂停线程开启用户输入对话框，将用户输入存入缓冲区
        /// </summary>
        /// <remarks>该方法调用后需要手动清理缓冲区缓存，否则下一次调用时会将这次的缓存一并返回</remarks>
        /// <param name="hideCancel">是否隐藏取消按钮禁止让用户必须输入；true隐藏取消按钮，false不隐藏</param>
        public void InputToBuffer(bool hideCancel)
        {
            bool flag = TextInputOut.InputBox(p_buffer, p_maxInput, p_title, p_prompt, p_defText, p_width, p_height, hideCancel);
            if ((!flag) && (!hideCancel)) p_buffer.Clear();
        }
        /// <summary>
        /// 暂停线程开启用户输入对话框，将用户输入存入缓冲区
        /// </summary>
        /// <remarks>该方法调用后需要手动清理缓冲区缓存，否则下一次调用时会将这次的缓存一并返回</remarks>
        public void InputToBuffer()
        {
            InputToBuffer(false);
        }
        /// <summary>
        /// 清理用户输入的缓存
        /// </summary>
        public void ClearBuffer()
        {
            p_buffer.Clear();
        }
        #endregion

    }

}
