using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Cheng.EasyX.CPP;
using System.Collections;
using Cheng.EasyX.Exceptions;
using Cheng.EasyX.DataStructure;

namespace Cheng.EasyX
{

    /// <summary>
    /// 样式设置
    /// </summary>
    public unsafe static class Styles
    {

        #region
        /// <summary>
        /// 访问或设置当前设备的二元光栅操作码
        /// </summary>
        public static Setrop2 Rop2
        {
            get
            {
                return (Setrop2)EasyX_API.getrop2_();
            }
            set
            {
                EasyX_API.setrop2_((int)value);
            }
        }

        /// <summary>
        /// 访问或设置当前设备图案填充和文字输出时是否透明
        /// </summary>
        /// <returns>
        /// 若是表示透明返回true，使用背景填充表示false
        /// </returns>
        /// <value>
        /// 值为true时，背景是透明的；值为false时，背景用当前背景色填充
        /// </value>
        public static bool BackMode
        {
            get
            {
                int r = EasyX_API.getbkmode_();
                return r == 1;
            }
            set
            {
                if (value)
                    EasyX_API.setbkmode_(1);
                else
                    EasyX_API.setbkmode_(2);
            }
        }

        /// <summary>
        /// 设置画线样式
        /// </summary>
        /// <param name="style">画线样式枚举</param>
        /// <param name="thickness">线的宽度，以像素为单位</param>
        /// <param name="puserstyle">
        /// 自定义画线样式数组，仅当线型为 <see cref="LineStyleType.USERSTYLE"/> 时该参数有效
        /// <para>数组第一个元素指定画线的长度，第二个元素指定空白的长度，第三个元素指定画线的长度，第四个元素指定空白的长度，以此类推</para>
        /// </param>
        /// <exception cref="ArgumentNullException">数组是null</exception>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        public static void SetLineStyle(LineStyleType style, int thickness, uint[] puserstyle)
        {        
            Device.f_testNotInitGraph(Device.exc_winNotInit);

            if (style == LineStyleType.USERSTYLE)
            {
                if (puserstyle is null) throw new ArgumentNullException();
                
                fixed(uint* ppu = puserstyle)
                {
                    EasyX_API.setlinestyle_2((int)style, thickness, ppu, (uint)puserstyle.Length);
                }
                return;
            }

            EasyX_API.setlinestyle_2((int)style, thickness, null, 0);
        }

        /// <summary>
        /// 获取画线样式
        /// </summary>
        /// <param name="style">画线样式枚举</param>
        /// <param name="thickness">线的宽度，以像素为单位</param>
        /// <param name="puserstyle">使用<see cref="List{T}"/>集合获取自定义画线样式数组，数组元素会以向后添加的方式获取</param>
        /// <exception cref="ArgumentNullException">数组是null</exception>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        public static void GetLineStyle(out LineStyleType style, out int thickness, List<uint> puserstyle)
        {
            if (puserstyle is null) throw new ArgumentNullException(nameof(puserstyle));
            Device.f_testNotInitGraph(Device.exc_winNotInit);

            void* lsp;
            byte* temptr = stackalloc byte[(int)EasyX_API.LineStyle_Size()];
            lsp = temptr;

            EasyX_API.LineStyle_ctor_1(lsp);

            EasyX_API.getlinestyle_(lsp);

            style = (LineStyleType)(*EasyX_API.LineStyle_style(lsp));
            thickness = (int)(*EasyX_API.LineStyle_thickness(lsp));

            if(style == LineStyleType.USERSTYLE)
            {
                int length = (int)(*EasyX_API.LineStyle_userstylecount(lsp));
                uint* arr = (*EasyX_API.LineStyle_puserstyle(lsp));

                if (arr != null)
                {
                    if (puserstyle.Capacity < length + puserstyle.Count) puserstyle.Capacity = length;

                    for (int i = 0; i < length; i++)
                    {
                        puserstyle.Add(arr[i]);
                    }
                }
            }

            EasyX_API.LineStyle_dtor(lsp);
        }
        /// <summary>
        /// 获取画线样式
        /// </summary>
        /// <param name="style">画线样式枚举</param>
        /// <param name="thickness">线的宽度，以像素为单位</param>
        /// <param name="puserstyle">使用<see cref="List{T}"/>集合获取自定义画线样式数组，数组元素会以向后添加的方式获取</param>
        /// <exception cref="ArgumentNullException">数组是null</exception>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        public static void GetLineStyle(out LineStyleType style, out int thickness, out uint[] puserstyle)
        {
            GetLineStyle(out style, out thickness, out puserstyle);
        }

        /// <summary>
        /// 设置填充样式
        /// </summary>
        /// <param name="style">填充样式类型</param>
        /// <param name="hatch">
        /// 填充图案
        /// <para>仅当 style 为<see cref="FillStyleType.HATCHED"/>时有效；<br/>填充图案的颜色由<see cref="RGBColor.FillColor"/>属性设置，背景区域使用背景色还是保持透明由<see cref="BackMode"/>属性设置</para>
        /// </param>
        /// <param name="ppattern">
        /// 指定自定义填充图案或图像，仅当 style 为 <see cref="FillStyleType.PATTERN"/>或<see cref="FillStyleType.DIBPATTERN"/> 时有效；不需要时可为null
        /// <para>当<paramref name="style"/>为<see cref="FillStyleType.PATTERN"/> 时，<paramref name="ppattern"/>表示<see cref="EasyImage"/>对象自定义填充图案，<see cref="EasyImage"/>中的黑色默认颜色值对应背景区域，非默认颜色值对应图案区域。图案区域的颜色由函数<see cref="RGBColor.TextColor"/>设置</para>
        ///<para>当<paramref name="style"/>为<see cref="FillStyleType.DIBPATTERN"/>时，<paramref name="ppattern"/>指向的<see cref="EasyImage"/>对象表示自定义填充图像，以该图像为填充单元实施填充</para>
        ///<para>若该对象已被释放，则当作null来处理</para>
        /// </param>
        /// <exception cref="WindowEasyXException">窗体未初始化</exception>
        public static void SetFillStyle(FillStyleType style, HatchType hatch, EasyImage ppattern)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            if (ppattern is null || ppattern.IsDispose)
            {
                EasyX_API.setfillstyle_2((int)style, (int)hatch, null);
            }
            else
            {
                EasyX_API.setfillstyle_2((int)style, (int)hatch, ppattern.p_imagePtr);
            }
        }
        /// <summary>
        /// 获取填充样式
        /// </summary>
        /// <param name="style">填充样式类型</param>
        /// <param name="hatch">填充图案类型</param>
        /// <param name="ppattern">要将自定义图像拷贝到的实例</param>
        /// <returns>是否成功拷贝了自定义图案实例，若成功返回true，若获取的填充样式没有自定义图像或无法拷贝则返回false</returns>
        public static bool GetFillStyle(out FillStyleType style, out HatchType hatch, EasyImage ppattern)
        {
            Device.f_testNotInitGraph(Device.exc_winNotInit);
            if (ppattern is null) throw new ArgumentNullException();
            if (ppattern.IsDispose) throw new ObjectDisposedException(nameof(ppattern));

            byte* fillptr = stackalloc byte[(int)EasyX_API.FillStyle_Size()];

            EasyX_API.FillStyle_ctor_1(fillptr);


            EasyX_API.getfillstyle_(fillptr);

            style = (FillStyleType)(*EasyX_API.FillStyle_style(fillptr));

            hatch = (HatchType)(*EasyX_API.FillStyle_hatch(fillptr));

            void* imgPtr = (*EasyX_API.FillStyle_ppattern(fillptr));
            bool flag = imgPtr != null;

            if (flag)
            {
                ppattern.f_copyToPtr(imgPtr);

            }

            EasyX_API.FillStyle_dtor(fillptr);

            return flag;
        }

        #endregion


    }
}
