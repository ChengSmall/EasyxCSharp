using Cheng.EasyX;
using Cheng.EasyX.DataStructure;

namespace Cheng.EasyXEngine.Structures.Buttons
{

    /// <summary>
    /// 表示一个矩形形状的可点击按钮
    /// </summary>
    public class RectButton : EasyXClickButton
    {

        #region 构造
        /// <summary>
        /// 实例化一个矩形点击按钮
        /// </summary>
        public RectButton()
        {
            f_init();
        }

        /// <summary>
        /// 实例化一个矩形点击按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        public RectButton(string text)
        {
            f_init();
            ButtonText = text;
        }

        /// <summary>
        /// 实例化一个矩形点击按钮
        /// </summary>
        /// <param name="init">是否自动初始化基本参数</param>
        public RectButton(bool init)
        {
            if (init) f_init();
        }

        /// <summary>
        /// 实例化一个矩形点击按钮
        /// </summary>
        /// <param name="init">是否自动初始化基本参数</param>
        /// <param name="text">按钮文本</param>
        public RectButton(bool init, string text)
        {
            if (init) f_init();
            ButtonText = text;
        }

        private void f_init()
        {
            p_lay = 10;
            p_textformat = TextDrawFormat.RectCenter;
            p_lineColor = ColorPreset.Black;

            p_onMouseFillColor = ColorPreset.White;
            p_commonFillColor = ColorPreset.White;
            p_onClickFillColor = ColorPreset.Grey;
            p_commonTextColor = ColorPreset.Black;
        }
        #endregion

        #region 参数

        /// <summary>
        /// 按钮显示文字
        /// </summary>
        private string p_buttonText;
        /// <summary>
        /// 文本格式
        /// </summary>
        private TextDrawFormat p_textformat;

        /// <summary>
        /// 按钮边框颜色
        /// </summary>
        private RGBColor p_lineColor;

        /// <summary>
        /// 按钮常态填充色
        /// </summary>
        private RGBColor p_commonFillColor;

        /// <summary>
        /// 鼠标处于按钮上的填充色
        /// </summary>
        private RGBColor p_onMouseFillColor;

        /// <summary>
        /// 鼠标在按钮上按下的填充色
        /// </summary>
        private RGBColor p_onClickFillColor;

        /// <summary>
        /// 一般的按钮文字显示
        /// </summary>
        private RGBColor p_commonTextColor;

        /// <summary>
        /// 鼠标处于按下状态
        /// </summary>
        protected bool p_isDown;
        /// <summary>
        /// 鼠标处于按钮内
        /// </summary>
        protected bool p_mouseIn;

        #endregion

        #region 参数访问

        /// <summary>
        /// 按钮上的提示文本，null表示没有文本
        /// </summary>
        public string ButtonText
        {
            get => p_buttonText;
            set => p_buttonText = value;
        }

        /// <summary>
        /// 按钮边框颜色
        /// </summary>
        public RGBColor LineColor
        {
            get => p_lineColor;
            set
            {
                p_lineColor = value;
            }
        }

        /// <summary>
        /// 按钮填充颜色
        /// </summary>
        public RGBColor FillColor
        {
            get => p_commonFillColor;
            set
            {
                p_commonFillColor = value;
            }
        }

        /// <summary>
        /// 鼠标在按钮内时按钮的填充色
        /// </summary>
        public RGBColor OnMouseFillColor
        {
            get => p_onMouseFillColor;
            set
            {
                p_onMouseFillColor = value;
            }
        }

        /// <summary>
        /// 按钮上的文本颜色
        /// </summary>
        public RGBColor TextColor
        {
            get => p_commonTextColor;
            set
            {
                p_commonTextColor = value;
            }
        }

        /// <summary>
        /// 按钮文本在按钮范围内的显示方式
        /// </summary>
        public TextDrawFormat ButtonTextFormat
        {
            get => p_textformat;
            set => p_textformat = value;
        }

        /// <summary>
        /// 鼠标在按钮上点击时的填充色
        /// </summary>
        public RGBColor OnClickFillColor
        {
            get => p_onClickFillColor;
            set => p_onClickFillColor = value;
        }
        #endregion

        #region 派生

        protected override void ClickDown(MouseClick click)
        {
            if(p_mouseIn && click == MouseClick.LeftClick)
            {
                p_isDown = true;
                //按压按钮
            }
        }

        protected override void ClickUp(MouseClick click)
        {
            if (p_mouseIn && click == MouseClick.LeftClick && p_isDown)
            {
                p_isDown = false;
                if(p_active) ClickInvoke();
                //抬起按钮
            }
        }

        protected override void MouseIn()
        {
            p_mouseIn = true;
        }

        protected override void MouseOut()
        {
            p_mouseIn = false;
            if (p_isDown)
            {
                //按压时离开按钮
                p_isDown = false;
            }
        }

        public override void Drawing()
        {
            if (IsDispose || (!Device.IsOpenWindow)) return;

            RGBColor fill, line, text;
            line = p_lineColor;
            text = p_commonTextColor;

            if (p_mouseIn)
            {

                if (p_isDown)
                {
                    //在按钮上点击
                    fill = p_onClickFillColor;
                }
                else
                {
                    //鼠标处于按钮上
                    fill = p_onMouseFillColor;
                }

            }
            else
            {
                //鼠标未处于按钮上
                fill = p_commonFillColor;
            }

            RGBColor.FillColor = fill;
            RGBColor.LineColor = line;
            
            //绘制按钮
            Cheng.EasyX.Drawing.Rectangle(p_buttonRect, DrawingType.FrameFill);

            //绘制文字
            if (!string.IsNullOrEmpty(p_buttonText))
            {
                RGBColor.TextColor = text;
                Styles.BackMode = true;
                TextInputOut.DrawText(p_buttonText, p_buttonRect, p_textformat);
            }

        }

        #endregion

    }

}
