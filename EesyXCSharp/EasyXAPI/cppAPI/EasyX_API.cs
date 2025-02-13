using Cheng.EasyX.DataStructure;
using System;
using System.Runtime.InteropServices;
using System.Text;
using DWORD = System.UInt32;
using HDC = System.IntPtr;
using HRGN = System.IntPtr;
using HWND = System.IntPtr;
using TCHAR = System.Char;


namespace Cheng.EasyX.CPP
{

    /// <summary>
    /// easyx的原型接口
    /// </summary>
    /// <remarks>
    /// <para>
    /// 你可以根据easyx文档使用此接口内的静态方法以此来使用easyx的原版api，几乎在任何时候都没有必要用此api，除非你想要追求更加极致的效率和自由度；<br/>
    /// 内含有不安全代码，你必须使用unsafe编译你的代码，并且要时刻注意内存安全检查；<br/>
    /// </para>
    /// <para>此处的函数和已封装好的C#代码可能会有冲突，建议不要混用</para>
    /// </remarks>
    public unsafe static class EasyX_API
    {

        #region 常量

        /// <summary>
        /// 当前进程的运行环境是否为64位
        /// </summary>
        public static readonly bool x64 = sizeof(void*) == 8;

        #endregion

        #region api

        #region 绘图设备

        public static int getheight_()
        {
            if (x64)
                return CPP_API_x64.getheight_();
            else
                return CPP_API_x32.getheight_();
        }

        public static int getwidth_()
        {
            if (x64)
                return CPP_API_x64.getwidth_();
            else
                return CPP_API_x32.getwidth_();
        }

        public static void clearcliprgn_()
        {
            if (x64)
                CPP_API_x64.clearcliprgn_();
            else
                CPP_API_x32.clearcliprgn_();
        }

        public static void cleardevice_()
        {
            if (x64)
                CPP_API_x64.cleardevice_();
            else
                CPP_API_x32.cleardevice_();
        }

        public static void closegraph_()
        {
            if (x64)
                CPP_API_x64.closegraph_();
            else
                CPP_API_x32.closegraph_();
        }

        public static void getaspectratio_(float* pxasp, float* pyasp)
        {
            if (x64)
                CPP_API_x64.getaspectratio_(pxasp, pyasp);
            else
                CPP_API_x32.getaspectratio_(pxasp, pyasp);
        }

        public static void graphdefaults_()
        {
            if (x64)
                CPP_API_x64.graphdefaults_();
            else
                CPP_API_x32.graphdefaults_();
        }

        public static HWND initgraph_(int width, int height, bool flag = false)
        {
            if (x64)
                return CPP_API_x64.initgraph_(width, height, flag);
            else
                return CPP_API_x32.initgraph_(width, height, flag);
        }

        public static void setaspectratio_(float xasp, float yasp)
        {
            if (x64)
                CPP_API_x64.setaspectratio_(xasp, yasp);
            else
                CPP_API_x32.setaspectratio_(xasp, yasp);
        }

        public static void setcliprgn_(HRGN hrgn)
        {
            if (x64)
                CPP_API_x64.setcliprgn_(hrgn);
            else
                CPP_API_x32.setcliprgn_(hrgn);
        }

        public static void setorigin_(int x, int y)
        {
            if (x64)
                CPP_API_x64.setorigin_(x, y);
            else
                CPP_API_x32.setorigin_(x, y);
        }


        #endregion

        #region 颜色模型

        public static RGBColor BGR_(RGBColor color)
        {
            if (x64)
                return CPP_API_x64.BGR_(color);
            else
                return CPP_API_x32.BGR_(color);
        }

        public static RGBColor HSLtoRGB_(float H, float S, float L)
        {
            if (x64)
                return CPP_API_x64.HSLtoRGB_(H, S, L);
            else
                return CPP_API_x32.HSLtoRGB_(H, S, L);
        }

        public static RGBColor HSVtoRGB_(float H, float S, float V)
        {
            if (x64)
                return CPP_API_x64.HSVtoRGB_(H, S, V);
            else
                return CPP_API_x32.HSVtoRGB_(H, S, V);
        }

        public static RGBColor RGBtoGRAY_(RGBColor rgb)
        {
            if (x64)
                return CPP_API_x64.RGBtoGRAY_(rgb);
            else
                return CPP_API_x32.RGBtoGRAY_(rgb);
        }

        public static void RGBtoHSL_(RGBColor rgb, float* H, float* S, float* L)
        {
            if (x64)
                CPP_API_x64.RGBtoHSL_(rgb, H, S, L);
            else
                CPP_API_x32.RGBtoHSL_(rgb, H, S, L);
        }

        public static void RGBtoHSV_(RGBColor rgb, float* H, float* S, float* V)
        {
            if (x64)
                CPP_API_x64.RGBtoHSV_(rgb, H, S, V);
            else
                CPP_API_x32.RGBtoHSV_(rgb, H, S, V);
        }

        #endregion

        #region 颜色及样式设置

        public static RGBColor getbkcolor_()
        {
            if (x64)
                return CPP_API_x64.getbkcolor_();
            else
                return CPP_API_x32.getbkcolor_();
        }

        public static int getbkmode_()
        {
            if (x64)
                return CPP_API_x64.getbkmode_();
            else
                return CPP_API_x32.getbkmode_();
        }

        public static RGBColor getfillcolor_()
        {
            if (x64)
                return CPP_API_x64.getfillcolor_();
            else
                return CPP_API_x32.getfillcolor_();
        }

        public static void getfillstyle_(void* pstyle)
        {
            if (x64)
                CPP_API_x64.getfillstyle_(pstyle);
            else
                CPP_API_x32.getfillstyle_(pstyle);
        }

        public static RGBColor getlinecolor_()
        {
            if (x64)
                return CPP_API_x64.getlinecolor_();
            else
                return CPP_API_x32.getlinecolor_();
        }

        public static void getlinestyle_(void* pstyle)
        {
            if (x64)
                CPP_API_x64.getlinestyle_(pstyle);
            else
                CPP_API_x32.getlinestyle_(pstyle);
        }

        public static int getpolyfillmode_()
        {
            if (x64)
                return CPP_API_x64.getpolyfillmode_();
            else
                return CPP_API_x32.getpolyfillmode_();
        }

        public static int getrop2_()
        {
            if (x64)
                return CPP_API_x64.getrop2_();
            else
                return CPP_API_x32.getrop2_();
        }

        public static void setrop2_(int rop)
        {
            if (x64)
                CPP_API_x64.setrop2_(rop);
            else
                CPP_API_x32.setrop2_(rop);
        }

        public static void setbkcolor_(RGBColor color)
        {
            if (x64)
                CPP_API_x64.setbkcolor_(color);
            else
                CPP_API_x32.setbkcolor_(color);
        }

        public static void setbkmode_(int mode)
        {
            if (x64)
                CPP_API_x64.setbkmode_(mode);
            else
                CPP_API_x32.setbkmode_(mode);
        }

        public static void setfillcolor_(RGBColor color)
        {
            if (x64)
                CPP_API_x64.setfillcolor_(color);
            else
                CPP_API_x32.setfillcolor_(color);
        }

        public static void setfillstyle_1(void* pstyle)
        {
            if (x64)
                CPP_API_x64.setfillstyle_1(pstyle);
            else
                CPP_API_x32.setfillstyle_1(pstyle);
        }

        public static void setfillstyle_2(int style, int hatch = 0, void* ppattern = null)
        {
            if (x64)
                CPP_API_x64.setfillstyle_2(style, hatch, ppattern);
            else
                CPP_API_x32.setfillstyle_2(style, hatch, ppattern);
        }

        public static void setfillstyle_3(byte[,] ppattern8x8)
        {
            if (x64)
                CPP_API_x64.setfillstyle_3(ppattern8x8);
            else
                CPP_API_x32.setfillstyle_3(ppattern8x8);
        }

        public static void setlinecolor_(RGBColor color)
        {
            if (x64)
                CPP_API_x64.setlinecolor_(color);
            else
                CPP_API_x32.setlinecolor_(color);
        }

        public static void setlinestyle_1(void* pstyle)
        {
            if (x64)
                CPP_API_x64.setlinestyle_1(pstyle);
            else
                CPP_API_x32.setlinestyle_1(pstyle);
        }

        public static void setlinestyle_2(int style, int thickness = 1, uint* puserstyle = null, uint userstylecount = 0)
        {
            if (x64)
                CPP_API_x64.setlinestyle_2(style, thickness, puserstyle, userstylecount);
            else
                CPP_API_x32.setlinestyle_2(style, thickness, puserstyle, userstylecount);
        }


        #endregion

        #region 绘图

        public static RGBColor getpixel_(int x, int y)
        {
            if (x64)
                return CPP_API_x64.getpixel_(x, y);
            else
                return CPP_API_x32.getpixel_(x, y);
        }

        public static void putpixel_(int x, int y, RGBColor color)
        {
            if (x64)
                CPP_API_x64.putpixel_(x, y, color);
            else
                CPP_API_x32.putpixel_(x, y, color);
        }

        public static void line_(int x1, int y1, int x2, int y2)
        {
            if (x64)
                CPP_API_x64.line_(x1, y1, x2, y2);
            else
                CPP_API_x32.line_(x1, y1, x2, y2);
        }

        public static void rectangle_(int left, int top, int right, int bottom)
        {
            if (x64)
                CPP_API_x64.rectangle_(left, top, right, bottom);
            else
                CPP_API_x32.rectangle_(left, top, right, bottom);
        }

        public static void fillrectangle_(int left, int top, int right, int bottom)
        {
            if (x64)
                CPP_API_x64.fillrectangle_(left, top, right, bottom);
            else
                CPP_API_x32.fillrectangle_(left, top, right, bottom);
        }

        public static void solidrectangle_(int left, int top, int right, int bottom)
        {
            if (x64)
                CPP_API_x64.solidrectangle_(left, top, right, bottom);
            else
                CPP_API_x32.solidrectangle_(left, top, right, bottom);
        }

        public static void clearrectangle_(int left, int top, int right, int bottom)
        {
            if (x64)
                CPP_API_x64.clearrectangle_(left, top, right, bottom);
            else
                CPP_API_x32.clearrectangle_(left, top, right, bottom);
        }

        public static void circle_(int x, int y, int radius)
        {
            if (x64)
                CPP_API_x64.circle_(x, y, radius);
            else
                CPP_API_x32.circle_(x, y, radius);
        }

        public static void fillcircle_(int x, int y, int radius)
        {
            if (x64)
                CPP_API_x64.fillcircle_(x, y, radius);
            else
                CPP_API_x32.fillcircle_(x, y, radius);
        }

        public static void solidcircle_(int x, int y, int radius)
        {
            if (x64)
                CPP_API_x64.solidcircle_(x, y, radius);
            else
                CPP_API_x32.solidcircle_(x, y, radius);
        }

        public static void clearcircle_(int x, int y, int radius)
        {
            if (x64)
                CPP_API_x64.solidcircle_(x, y, radius);
            else
                CPP_API_x32.solidcircle_(x, y, radius);
        }

        public static void ellipse_(int left, int top, int right, int bottom)
        {
            if (x64)
                CPP_API_x64.ellipse_(left, top, right, bottom);
            else
                CPP_API_x32.ellipse_(left, top, right, bottom);
        }

        public static void fillellipse_(int left, int top, int right, int bottom)
        {
            if (x64)
                CPP_API_x64.fillellipse_(left, top, right, bottom);
            else
                CPP_API_x32.fillellipse_(left, top, right, bottom);
        }

        public static void solidellipse_(int left, int top, int right, int bottom)
        {
            if (x64)
                CPP_API_x64.solidellipse_(left, top, right, bottom);
            else
                CPP_API_x32.solidellipse_(left, top, right, bottom);
        }

        public static void clearellipse_(int left, int top, int right, int bottom)
        {
            if (x64)
                CPP_API_x64.clearellipse_(left, top, right, bottom);
            else
                CPP_API_x32.clearellipse_(left, top, right, bottom);
        }

        public static void roundrect_(int left, int top, int right, int bottom, int ellipsewidth, int ellipseheight)
        {
            if (x64)
                CPP_API_x64.roundrect_(left, top, right, bottom, ellipsewidth, ellipseheight);
            else
                CPP_API_x32.roundrect_(left, top, right, bottom, ellipsewidth, ellipseheight);
        }

        public static void fillroundrect_(int left, int top, int right, int bottom, int ellipsewidth, int ellipseheight)
        {
            if (x64)
                CPP_API_x64.fillroundrect_(left, top, right, bottom, ellipsewidth, ellipseheight);
            else
                CPP_API_x32.fillroundrect_(left, top, right, bottom, ellipsewidth, ellipseheight);
        }

        public static void solidroundrect_(int left, int top, int right, int bottom, int ellipsewidth, int ellipseheight)
        {
            if (x64)
                CPP_API_x64.solidroundrect_(left, top, right, bottom, ellipsewidth, ellipseheight);
            else
                CPP_API_x32.solidroundrect_(left, top, right, bottom, ellipsewidth, ellipseheight);
        }

        public static void clearroundrect_(int left, int top, int right, int bottom, int ellipsewidth, int ellipseheight)
        {
            if (x64)
                CPP_API_x64.clearroundrect_(left, top, right, bottom, ellipsewidth, ellipseheight);
            else
                CPP_API_x32.clearroundrect_(left, top, right, bottom, ellipsewidth, ellipseheight);
        }

        public static void arc_(int left, int top, int right, int bottom, double stangle, double endangle)
        {
            if (x64)
                CPP_API_x64.arc_(left, top, right, bottom, stangle, endangle);
            else
                CPP_API_x32.arc_(left, top, right, bottom, stangle, endangle);
        }

        public static void pie_(int left, int top, int right, int bottom, double stangle, double endangle)
        {
            if (x64)
                CPP_API_x64.pie_(left, top, right, bottom, stangle, endangle);
            else
                CPP_API_x32.pie_(left, top, right, bottom, stangle, endangle);
        }

        public static void fillpie_(int left, int top, int right, int bottom, double stangle, double endangle)
        {
            if (x64)
                CPP_API_x64.fillpie_(left, top, right, bottom, stangle, endangle);
            else
                CPP_API_x32.fillpie_(left, top, right, bottom, stangle, endangle);
        }

        public static void solidpie_(int left, int top, int right, int bottom, double stangle, double endangle)
        {
            if (x64)
                CPP_API_x64.solidpie_(left, top, right, bottom, stangle, endangle);
            else
                CPP_API_x32.solidpie_(left, top, right, bottom, stangle, endangle);
        }

        public static void clearpie_(int left, int top, int right, int bottom, double stangle, double endangle)
        {
            if (x64)
                CPP_API_x64.clearpie_(left, top, right, bottom, stangle, endangle);
            else
                CPP_API_x32.clearpie_(left, top, right, bottom, stangle, endangle);
        }

        public static void polyline_(EPoint[] points, int num)
        {
            if (x64)
                CPP_API_x64.polyline_(points, num);
            else
                CPP_API_x32.polyline_(points, num);
        }

        public static void polygon_(EPoint[] points, int num)
        {
            if (x64)
                CPP_API_x64.polygon_(points, num);
            else
                CPP_API_x32.polygon_(points, num);
        }

        public static void fillpolygon_(EPoint[] points, int num)
        {
            if (x64)
                CPP_API_x64.fillpolygon_(points, num);
            else
                CPP_API_x32.fillpolygon_(points, num);
        }

        public static void solidpolygon_(EPoint[] points, int num)
        {
            if (x64)
                CPP_API_x64.solidpolygon_(points, num);
            else
                CPP_API_x32.solidpolygon_(points, num);
        }

        public static void clearpolygon_(EPoint[] points, int num)
        {
            if (x64)
                CPP_API_x64.clearpolygon_(points, num);
            else
                CPP_API_x32.clearpolygon_(points, num);
        }

        public static void polybezier_(EPoint[] points, int num)
        {
            if (x64)
                CPP_API_x64.polybezier_(points, num);
            else
                CPP_API_x32.polybezier_(points, num);
        }

        public static void floodfill_(int x, int y, RGBColor color, int filltype = 0)
        {
            if (x64)
                CPP_API_x64.floodfill_(x, y, color, filltype);
            else
                CPP_API_x32.floodfill_(x, y, color, filltype);
        }

        #region 多边形指针绘制

        public static void polygon_(EPoint* points, int num)
        {
            if (x64)
                CPP_API_x64.polygon_(points, num);
            else
                CPP_API_x32.polygon_(points, num);
        }

        public static void fillpolygon_(EPoint* points, int num)
        {
            if (x64)
                CPP_API_x64.fillpolygon_(points, num);
            else
                CPP_API_x32.fillpolygon_(points, num);
        }

        public static void solidpolygon_(EPoint* points, int num)
        {
            if (x64)
                CPP_API_x64.solidpolygon_(points, num);
            else
                CPP_API_x32.solidpolygon_(points, num);
        }

        public static void clearpolygon_(EPoint* points, int num)
        {
            if (x64)
                CPP_API_x64.solidpolygon_(points, num);
            else
                CPP_API_x32.solidpolygon_(points, num);
        }

        #endregion

        #endregion

        #region 文字输出

        public static int drawtext_(string str, ERect* pRect, uint uFormat)
        {
            if (x64)
                return CPP_API_x64.drawtext_(str, pRect, uFormat);
            else
                return CPP_API_x32.drawtext_(str, pRect, uFormat);
        }

        public static int drawtext_(char c, ERect* pRect, uint uFormat)
        {
            if (x64)
                return CPP_API_x64.drawtext_2(c, pRect, uFormat);
            else
                return CPP_API_x32.drawtext_2(c, pRect, uFormat);
        }

        public static RGBColor gettextcolor_()
        {
            if (x64)
                return CPP_API_x64.gettextcolor_();
            else
                return CPP_API_x32.gettextcolor_();
        }

        public static void gettextstyle_(ref LogFont font)
        {
            if (x64)
                CPP_API_x64.gettextstyle_(ref font);
            else
                CPP_API_x32.gettextstyle_(ref font);
        }

        public static void outtextxy_1(int x, int y, string str)
        {
            if (x64)
                CPP_API_x64.outtextxy_1(x, y, str);
            else
                CPP_API_x32.outtextxy_1(x, y, str);
        }

        public static void outtextxy_2(int x, int y, TCHAR c)
        {
            if (x64)
                CPP_API_x64.outtextxy_2(x, y, c);
            else
                CPP_API_x32.outtextxy_2(x, y, c);
        }

        public static void settextcolor_(RGBColor color)
        {
            if (x64)
                CPP_API_x64.settextcolor_(color);
            else
                CPP_API_x32.settextcolor_(color);
        }

        public static void settextstyle_1(int nHeight, int nWidth, string lpszFace)
        {
            if (x64)
                CPP_API_x64.settextstyle_1(nHeight, nWidth, lpszFace);
            else
                CPP_API_x32.settextstyle_1(nHeight, nWidth, lpszFace);
        }

        public static void settextstyle_2(int nHeight, int nWidth, string lpszFace, int nEscapement,
            int nOrientation, int nWeight, bool bItalic, bool bUnderline, bool bStrikeOut)
        {
            if (x64)
                CPP_API_x64.settextstyle_2(nHeight, nWidth, lpszFace, nEscapement, nOrientation, nWeight, bItalic, bUnderline, bStrikeOut);
            else
                CPP_API_x32.settextstyle_2(nHeight, nWidth, lpszFace, nEscapement, nOrientation, nWeight, bItalic, bUnderline, bStrikeOut);
        }


        public static void settextstyle_3(int nHeight, int nWidth, string lpszFace,
            int nEscapement, int nOrientation, int nWeight, bool bItalic, bool bUnderline,
            bool bStrikeOut, byte fbCharSet, byte fbOutPrecision, byte fbClipPrecision,
            byte fbQuality, byte fbPitchAndFamily)
        {
            if (x64)
                CPP_API_x64.settextstyle_3(nHeight, nWidth, lpszFace, nEscapement, nOrientation, nWeight, bItalic, bUnderline, bStrikeOut, fbCharSet, fbOutPrecision, fbClipPrecision, fbQuality, fbPitchAndFamily);
            else
                CPP_API_x32.settextstyle_3(nHeight, nWidth, lpszFace, nEscapement, nOrientation, nWeight, bItalic, bUnderline, bStrikeOut, fbCharSet, fbOutPrecision, fbClipPrecision, fbQuality, fbPitchAndFamily);
        }

        public static void settextstyle_4(ref LogFont logfont)
        {
            if (x64)
                CPP_API_x64.settextstyle_4(ref logfont);
            else
                CPP_API_x32.settextstyle_4(ref logfont);
        }

        public static int textheight_1(string str)
        {
            if (x64)
                return CPP_API_x64.textheight_1(str);
            else
                return CPP_API_x32.textheight_1(str);
        }

        public static int textheight_2(TCHAR c)
        {
            if (x64)
                return CPP_API_x64.textheight_2(c);
            else
                return CPP_API_x32.textheight_2(c);
        }

        public static int textwidth_1(string str)
        {
            if (x64)
                return CPP_API_x64.textwidth_1(str);
            else
                return CPP_API_x32.textwidth_1(str);
        }

        public static int textwidth_2(TCHAR c)
        {
            if (x64)
                return CPP_API_x64.textwidth_2(c);
            else
                return CPP_API_x32.textwidth_2(c);
        }


        #endregion

        #region 图像处理

        public static void getimage_(void* pDstImg, int srcX, int srcY, int srcWidth, int srcHeight)
        {
            if (x64)
                CPP_API_x64.getimage_(pDstImg, srcX, srcY, srcWidth, srcHeight);
            else
                CPP_API_x32.getimage_(pDstImg, srcX, srcY, srcWidth, srcHeight);
        }

        /// <summary>
        /// 获取缓冲区指针
        /// </summary>
        /// <param name="pImg"></param>
        /// <returns></returns>
        public static uint* GetImageBuffer_(void* pImg = null)
        {
            if (x64)
                return CPP_API_x64.GetImageBuffer_(pImg);
            else
                return CPP_API_x32.GetImageBuffer_(pImg);
        }

        public static HDC GetImageHDC_(void* pImg = null)
        {
            if (x64)
                return CPP_API_x64.GetImageHDC_(pImg);
            else
                return CPP_API_x32.GetImageHDC_(pImg);
        }

        public static void* GetWorkingImage_()
        {
            if (x64)
                return CPP_API_x64.GetWorkingImage_();
            else
                return CPP_API_x32.GetWorkingImage_();
        }

        public static void loadimage_1(void* pDstImg, string pImgFile,
            int nWidth = 0, int nHeight = 0, bool bResize = false)
        {
            if (x64)
                CPP_API_x64.loadimage_1(pDstImg, pImgFile, nWidth, nHeight, bResize);
            else
                CPP_API_x32.loadimage_1(pDstImg, pImgFile, nWidth, nHeight, bResize);
        }


        public static void loadimage_2(void* pDstImg, string pResType, string pResName,
            int nWidth = 0, int nHeight = 0, bool bResize = false)
        {
            if (x64)
                CPP_API_x64.loadimage_2(pDstImg, pResType, pResName, nWidth, nHeight, bResize);
            else
                CPP_API_x32.loadimage_2(pDstImg, pResType, pResName, nWidth, nHeight, bResize);
        }

        public static void putimage_1(int dstX, int dstY, void* pSrcImg, DWORD dwRop = 0x00CC0020)
        {
            if (x64)
                CPP_API_x64.putimage_1(dstX, dstY, pSrcImg, dwRop);
            else
                CPP_API_x32.putimage_1(dstX, dstY, pSrcImg, dwRop);
        }

        public static void putimage_2(int dstX, int dstY, int dstWidth, int dstHeight,
            void* pSrcImg, int srcX, int srcY, uint dwRop = 0x00CC0020)
        {
            if (x64)
                CPP_API_x64.putimage_2(dstX, dstY, dstWidth, dstHeight, pSrcImg, srcX, srcY, dwRop);
            else
                CPP_API_x32.putimage_2(dstX, dstY, dstWidth, dstHeight, pSrcImg, srcX, srcY, dwRop);
        }

        public static void Resize_(void* pImg, int width, int height)
        {
            if (x64)
                CPP_API_x64.Resize_(pImg, width, height);
            else
                CPP_API_x32.Resize_(pImg, width, height);
        }

        public static void rotateimage_(void* dstimg, void* srcimg, double radian,
            RGBColor bkcolor = default, bool autosize = false, bool highquality = true)
        {
            if (x64)
                CPP_API_x64.rotateimage_(dstimg, srcimg, radian, bkcolor, autosize, highquality);
            else
                CPP_API_x32.rotateimage_(dstimg, srcimg, radian, bkcolor, autosize, highquality);
        }
        
        public static void saveimage_(string strFileName, void* pImg = null)
        {
            if (x64)
                CPP_API_x64.saveimage_(strFileName, pImg);
            else
                CPP_API_x32.saveimage_(strFileName, pImg);
        }

        #endregion

        #region 消息处理

        public static void flushmessage_(byte filter = byte.MaxValue)
        {
            if (x64)
                CPP_API_x64.flushmessage_(filter);
            else
                CPP_API_x32.flushmessage_(filter);
        }

        public static CsMessage getmessage_1(byte filter = byte.MaxValue)
        {
            if (x64)
                return CPP_API_x64.getmessage_1(filter);
            else
                return CPP_API_x32.getmessage_1(filter);
                
        }

        public static void getmessage_2(CsMessage* msg, byte filter = byte.MaxValue)
        {
            if (x64)
                CPP_API_x64.getmessage_2(msg, filter);
            else
                CPP_API_x32.getmessage_2(msg, filter);
        }

        public static bool peekmessage_(CsMessage* msg, byte filter = byte.MaxValue, bool removemsg = true)
        {
            if (x64)
                return CPP_API_x64.peekmessage_(msg, filter, removemsg);
            else
                return CPP_API_x32.peekmessage_(msg, filter, removemsg);
        }

        #endregion

        #region 其它

        public static void BeginBatchDraw_()
        {
            if (x64)
                CPP_API_x64.BeginBatchDraw_();
            else
                CPP_API_x32.BeginBatchDraw_();
        }

        public static void EndBatchDraw_1()
        {
            if (x64)
                CPP_API_x64.EndBatchDraw_1();
            else
                CPP_API_x32.EndBatchDraw_1();
        }

        public static void EndBatchDraw_2(int left, int top, int right, int bottom)
        {
            if (x64)
                CPP_API_x64.EndBatchDraw_2(left, top, right, bottom);
            else
                CPP_API_x32.EndBatchDraw_2(left, top, right, bottom);
        }

        public static void FlushBatchDraw_1()
        {
            if (x64)
                CPP_API_x64.FlushBatchDraw_1();
            else
                CPP_API_x32.FlushBatchDraw_1();
        }

        public static void FlushBatchDraw_2(int left, int top, int right, int bottom)
        {
            if (x64)
                CPP_API_x64.FlushBatchDraw_2(left, top, right, bottom);
            else
                CPP_API_x32.FlushBatchDraw_2(left, top, right, bottom);
        }

        public static string GetEasyXVer_()
        {
            if (x64)
                return CPP_API_x64.GetEasyXVer_();
            else
                return CPP_API_x32.GetEasyXVer_();
        }

        public static HWND GetHWnd_()
        {
            if (x64)
                return CPP_API_x64.GetHWnd_();
            else
                return CPP_API_x32.GetHWnd_();
        }
        /// <summary>
        /// 以对话框形式获取用户输入
        /// </summary>
        /// <param name="pString">指定接收用户输入字符串的指针，指针最大长度是缓冲区内存长度</param>
        /// <param name="nMaxCount">
        /// 指定 pString 指向的缓冲区的大小，该值会限制用户输入内容的长度；此值不要超过<see cref="System.Text.StringBuilder.Capacity"/>
        /// <para>
        /// 缓冲区的大小包括表示字符串结尾的 '\0' 字符。当允许多行输入时，用户键入的回车占两个字符位置
        /// </para></param>
        /// <param name="pPrompt">
        /// <para>指定显示在对话框中的提示信息。提示信息中可以用“\n”分行</para>
        /// <para>InputBox 的高度会随着提示信息内容的多少自动扩充。如果该值为 null，则不显示提示信息</para>
        /// </param>
        /// <param name="pTitle">指定 InputBox 的标题栏。如果为 null，将显示应用程序的名称</param>
        /// <param name="pDefault">指定显示在用户输入区的默认值</param>
        /// <param name="width">指定 InputBox 的宽度（不包括边框），最小为 200 像素。如果为 0，则使用默认宽度</param>
        /// <param name="height">
        /// 指定 InputBox 的高度（不包括边框）
        /// <para>如果为 0，表示自动计算高度，用户输入框只允许输入一行内容，按“回车”确认输入信息；<br/>
        /// 如果大于 0，用户输入框的高度会自动拓展，同时允许输入多行内容，按“Ctrl+回车”确认输入信息
        /// </para>
        /// </param>
        /// <param name="bHideCancelBtn">指定是否隐藏取消按钮禁止用户取消输入</param>
        /// <returns>返回用户是否输入信息。如果用户按“确定”，返回 true；如果用户按“取消”，返回 false</returns>
        public static bool InputBox_(System.Text.StringBuilder pString, int nMaxCount, 
            string pPrompt = null, string pTitle = null, string pDefault = null,
            int width = 0, int height = 0, bool bHideCancelBtn = true)
        {
            if (x64)
                return CPP_API_x64.InputBox_(pString, nMaxCount, pPrompt, pTitle, pDefault, width, height, bHideCancelBtn);
            else
                return CPP_API_x32.InputBox_(pString, nMaxCount, pPrompt, pTitle, pDefault, width, height, bHideCancelBtn);
        }

        /// <summary>
        /// 以对话框形式获取用户输入
        /// </summary>
        /// <param name="pString">指定接收用户输入字符串的字符缓冲区首地址</param>
        /// <param name="nMaxCount">
        /// pString 指向的字符串缓冲区的大小，该值会限制用户输入内容的长度；
        /// <para>
        /// 缓冲区的大小包括表示字符串结尾的 '\0' 字符。当允许多行输入时，用户键入的回车占两个字符位置
        /// </para></param>
        /// <param name="pPrompt">
        /// <para>指定显示在对话框中的提示信息。提示信息中可以用“\n”分行</para>
        /// <para>InputBox 的高度会随着提示信息内容的多少自动扩充。如果该值为 null，则不显示提示信息</para>
        /// </param>
        /// <param name="pTitle">指定 InputBox 的标题栏。如果为 null，将显示应用程序的名称</param>
        /// <param name="pDefault">指定显示在用户输入区的默认值</param>
        /// <param name="width">指定 InputBox 的宽度（不包括边框），最小为 200 像素。如果为 0，则使用默认宽度</param>
        /// <param name="height">
        /// 指定 InputBox 的高度（不包括边框）
        /// <para>如果为 0，表示自动计算高度，用户输入框只允许输入一行内容，按“回车”确认输入信息；<br/>
        /// 如果大于 0，用户输入框的高度会自动拓展，同时允许输入多行内容，按“Ctrl+回车”确认输入信息
        /// </para>
        /// </param>
        /// <param name="bHideCancelBtn">指定是否隐藏取消按钮禁止用户取消输入</param>
        /// <returns>返回用户是否输入信息。如果用户按“确定”，返回 true；如果用户按“取消”，返回 false</returns>
        public static bool InputBox_(char* pString, int nMaxCount,
            string pPrompt = null, string pTitle = null, string pDefault = null,
            int width = 0, int height = 0, bool bHideCancelBtn = true)
        {
            if (x64)
                return CPP_API_x64.InputBox_(pString, nMaxCount, pPrompt, pTitle, pDefault, width, height, bHideCancelBtn);
            else
                return CPP_API_x32.InputBox_(pString, nMaxCount, pPrompt, pTitle, pDefault, width, height, bHideCancelBtn);
        }

        /// <summary>
        /// winapi，设置指定窗口名称
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="lpString">窗口名称</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetWindowText(IntPtr hwnd,[MarshalAs(UnmanagedType.LPWStr)] string lpString);

        /// <summary>
        /// winapi，获取指定窗口名称
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="lpString">要获取到的句柄</param>
        /// <param name="nMaxCount">要获取的名称字符串最大字符数</param>
        /// <returns>实际获取的名称字符数</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        /// <summary>
        /// 更改指定窗口的位置和尺寸
        /// </summary>
        /// <remarks>
        /// <para>对于顶级窗口，位置和尺寸是相对于屏幕左上角的；对于子窗口，它们相对于父窗口工作区的左上角</para>
        /// <para></para>
        /// </remarks>
        /// <param name="hwnd">窗口的句柄</param>
        /// <param name="X">窗口左侧的新位置</param>
        /// <param name="Y">窗口顶部的新位置</param>
        /// <param name="nWidth">窗口的新宽度</param>
        /// <param name="nHeight">窗口的新高度</param>
        /// <param name="bRepaint">
        /// <para>指示是否重新绘制窗口</para>
        /// <para>如果此参数为true，则窗口将收到消息。如果参数为 FALSE，则不会进行任何类型的重新绘制</para>
        /// <para>这适用于工作区、非工作区 (包括标题栏和滚动条) ，以及由于移动子窗口而发现父窗口的任何部分</para>
        /// </param>
        /// <returns>如果该函数成功，则返回true，失败返回false；要获得更多的错误信息，请调用 <see cref="Marshal.GetLastWin32Error"/></returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, [MarshalAs(UnmanagedType.Bool)] bool bRepaint);

        /// <summary>
        /// 更新窗口
        /// </summary>
        /// <remarks>
        /// <para>如果窗口的更新区域不为空，函数通过向窗口发送 WM_PAINT 消息来更新指定窗口的工作区</para>
        /// <para>函数绕过应用程序队列，将 WM_PAINT 消息直接发送到指定窗口的窗口过程</para>
        /// <para>如果更新区域为空，则不发送任何消息</para>
        /// </remarks>
        /// <param name="hwnd">要更新的窗口的句柄</param>
        /// <returns>如果该函数成功，则返回值为true；如果函数失败，则返回值为false</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public extern static bool UpdateWindow(IntPtr hwnd);

        #endregion

        #region 对象api

        #region 消息CsMessage

        public static uint CsMessage_Size()
        {
            if (x64)
                return CPP_API_x64.CsMessage_Size();
            else
                return CPP_API_x32.CsMessage_Size();
        }

        public static ushort* CsMessage_message(CsMessage* meg)
        {
            if (x64)
                return CPP_API_x64.CsMessage_message(meg);
            else
                return CPP_API_x32.CsMessage_message(meg);
        }

        public static bool CsMessage_M_ctrl_get(CsMessage* meg)
        {
            if (x64)
                return CPP_API_x64.CsMessage_M_ctrl_get(meg);
            else
                return CPP_API_x32.CsMessage_M_ctrl_get(meg);
        }

        public static void CsMessage_M_ctrl_set(CsMessage* meg, bool value)
        {
            if (x64)
                CPP_API_x64.CsMessage_M_ctrl_set(meg, value);
            else
                CPP_API_x32.CsMessage_M_ctrl_set(meg, value);
        }

        public static bool CsMessage_M_shift_get(CsMessage* meg)
        {
            if (x64)
                return CPP_API_x64.CsMessage_M_shift_get(meg);
            else
                return CPP_API_x32.CsMessage_M_shift_get(meg);
        }

        public static void CsMessage_M_shift_set(CsMessage* meg, bool value)
        {
            if (x64)
                CPP_API_x64.CsMessage_M_shift_set(meg, value);
            else
                CPP_API_x32.CsMessage_M_shift_set(meg, value);
        }

        public static bool CsMessage_M_lbutton_get(CsMessage* meg)
        {
            if (x64)
                return CPP_API_x64.CsMessage_M_lbutton_get(meg);
            else
                return CPP_API_x32.CsMessage_M_lbutton_get(meg);
        }

        public static void CsMessage_M_lbutton_set(CsMessage* meg, bool value)
        {
            if (x64)
                CPP_API_x64.CsMessage_M_lbutton_set(meg, value);
            else
                CPP_API_x32.CsMessage_M_lbutton_set(meg, value);
        }

        public static bool CsMessage_M_mbutton_get(CsMessage* meg)
        {
            if (x64)
                return CPP_API_x64.CsMessage_M_mbutton_get(meg);
            else
                return CPP_API_x32.CsMessage_M_mbutton_get(meg);
        }

        public static void CsMessage_M_mbutton_set(CsMessage* meg, bool value)
        {
            if (x64)
                CPP_API_x64.CsMessage_M_mbutton_set(meg, value);
            else
                CPP_API_x32.CsMessage_M_mbutton_set(meg, value);
        }

        public static bool CsMessage_M_rbutton_get(CsMessage* meg)
        {
            if (x64)
                return CPP_API_x64.CsMessage_M_rbutton_get(meg);
            else
                return CPP_API_x32.CsMessage_M_rbutton_get(meg);
        }

        public static void CsMessage_M_rbutton_set(CsMessage* meg, bool value)
        {
            if (x64)
                CPP_API_x64.CsMessage_M_rbutton_set(meg, value);
            else
                CPP_API_x32.CsMessage_M_rbutton_set(meg, value);
        }

        public static short* CsMessage_M_x(CsMessage* meg)
        {
            if (x64)
                return CPP_API_x64.CsMessage_M_x(meg);
            else
                return CPP_API_x32.CsMessage_M_x(meg);
        }

        public static short* CsMessage_M_y(CsMessage* meg)
        {
            if (x64)
                return CPP_API_x64.CsMessage_M_y(meg);
            else
                return CPP_API_x32.CsMessage_M_y(meg);
        }

        public static short* CsMessage_M_wheel(CsMessage* meg)
        {
            if (x64)
                return CPP_API_x64.CsMessage_M_wheel(meg);
            else
                return CPP_API_x32.CsMessage_M_wheel(meg);
        }

        public static byte* CsMessage_K_vkcode(CsMessage* meg)
        {
            if (x64)
                return CPP_API_x64.CsMessage_K_vkcode(meg);
            else
                return CPP_API_x32.CsMessage_K_vkcode(meg);
        }

        public static byte* CsMessage_K_scancode(CsMessage* meg)
        {
            if (x64)
                return CPP_API_x64.CsMessage_K_vkcode(meg);
            else
                return CPP_API_x32.CsMessage_K_vkcode(meg);
        }

        public static bool CsMessage_K_extended_get(CsMessage* meg)
        {
            if (x64)
                return CPP_API_x64.CsMessage_K_extended_get(meg);
            else
                return CPP_API_x32.CsMessage_K_extended_get(meg);
        }

        public static void CsMessage_K_extended_set(CsMessage* meg, bool value)
        {
            if (x64)
                CPP_API_x64.CsMessage_K_extended_set(meg, value);
            else
                CPP_API_x32.CsMessage_K_extended_set(meg, value);
        }

        public static bool CsMessage_K_prevdown_get(CsMessage* meg)
        {
            if (x64)
                return CPP_API_x64.CsMessage_K_prevdown_get(meg);
            else
                return CPP_API_x32.CsMessage_K_prevdown_get(meg);
        }

        public static void CsMessage_K_prevdown_set(CsMessage* meg, bool value)
        {
            if (x64)
                CPP_API_x64.CsMessage_K_prevdown_set(meg, value);
            else
                CPP_API_x32.CsMessage_K_prevdown_set(meg, value);
        }

        public static TCHAR* CsMessage_ch(CsMessage* meg)
        {
            if (x64)
                return CPP_API_x64.CsMessage_ch(meg);
            else
                return CPP_API_x32.CsMessage_ch(meg);
        }

        public static IntPtr* CsMessage_wParam(CsMessage* meg)
        {
            if (x64)
                return CPP_API_x64.CSMessage_wParam(meg);
            else
                return CPP_API_x32.CSMessage_wParam(meg);
        }

        public static IntPtr* CsMessage_lParam(CsMessage* meg)
        {
            if (x64)
                return (IntPtr*)CPP_API_x64.CSMessage_lParam(meg);
            else
                return (IntPtr*)CPP_API_x32.CSMessage_lParam(meg);
        }
        #endregion

        #region 画线样式

        public static uint LineStyle_Size()
        {
            if (x64)
                return CPP_API_x64.LineStyle_Size();
            else
                return CPP_API_x32.LineStyle_Size();
        }

        public static void LineStyle_ctor_1(void* linestyle)
        {
            if (x64)
                CPP_API_x64.LineStyle_ctor_1(linestyle);
            else
                CPP_API_x32.LineStyle_ctor_1(linestyle);
        }

        public static void LineStyle_ctor_2(void* linestyle, void* copyLineStyle)
        {
            if (x64)
                CPP_API_x64.LineStyle_ctor_2(linestyle, copyLineStyle);
            else
                CPP_API_x32.LineStyle_ctor_2(linestyle, copyLineStyle);
        }

        public static void LineStyle_operator_assignment(void* linestyle, void* setLineStyle)
        {
            if (x64)
                CPP_API_x64.LineStyle_operator_assignment(linestyle, setLineStyle);
            else
                CPP_API_x32.LineStyle_operator_assignment(linestyle, setLineStyle);
        }

        public static void LineStyle_dtor(void* linestyle)
        {
            if (x64)
                CPP_API_x64.LineStyle_dtor(linestyle);
            else
                CPP_API_x32.LineStyle_dtor(linestyle);
        }

        public static DWORD* LineStyle_style(void* linestyle)
        {
            if (x64)
                return CPP_API_x64.LineStyle_style(linestyle);
            else
                return CPP_API_x32.LineStyle_style(linestyle);
        }

        public static DWORD* LineStyle_thickness(void* linestyle)
        {
            if (x64)
                return CPP_API_x64.LineStyle_thickness(linestyle);
            else
                return CPP_API_x32.LineStyle_thickness(linestyle);
        }

        public static DWORD** LineStyle_puserstyle(void* linestyle)
        {
            if (x64)
                return CPP_API_x64.LineStyle_puserstyle(linestyle);
            else
                return CPP_API_x32.LineStyle_puserstyle(linestyle);
        }

        public static DWORD* LineStyle_userstylecount(void* linestyle)
        {
            if (x64)
                return CPP_API_x64.LineStyle_userstylecount(linestyle);
            else
                return CPP_API_x32.LineStyle_userstylecount(linestyle);
        }

        public static void* LineStyle_New_1()
        {
            if (x64)
                return CPP_API_x64.LineStyle_New_1();
            else
                return CPP_API_x32.LineStyle_New_1();
        }

        public static void* LineStyle_New_2(void* copyLS)
        {
            if (x64)
                return CPP_API_x64.LineStyle_New_2(copyLS);
            else
                return CPP_API_x32.LineStyle_New_2(copyLS);
        }

        public static void LineStyle_Free(void* linestyle)
        {
            if (x64)
                CPP_API_x64.LineStyle_Free(linestyle);
            else
                CPP_API_x32.LineStyle_Free(linestyle);
        }


        #endregion

        #region 填充样式

        public static uint FillStyle_Size()
        {
            if (x64)
                return CPP_API_x64.FillStyle_Size();
            else
                return CPP_API_x32.FillStyle_Size();
        }

        public static void FillStyle_ctor_1(void* fillstyle)
        {
            if (x64)
                CPP_API_x64.FillStyle_ctor_1(fillstyle);
            else
                CPP_API_x32.FillStyle_ctor_1(fillstyle);
        }

        public static void FillStyle_ctor_2(void* fillstyle, void* copyFillStyle)
        {
            if (x64)
                CPP_API_x64.FillStyle_ctor_2(fillstyle, copyFillStyle);
            else
                CPP_API_x32.FillStyle_ctor_2(fillstyle, copyFillStyle);
        }

        public static void FillStyle_operator_assignment(void* fillstyle, void* setFillStyle)
        {
            if (x64)
                CPP_API_x64.FillStyle_operator_assignment(fillstyle, setFillStyle);
            else
                CPP_API_x32.FillStyle_operator_assignment(fillstyle, setFillStyle);
        }

        public static void FillStyle_dtor(void* fillstyle)
        {
            if (x64)
                CPP_API_x64.FillStyle_dtor(fillstyle);
            else
                CPP_API_x32.FillStyle_dtor(fillstyle);
        }

        public static int* FillStyle_style(void* fillstyle)
        {
            if (x64)
                return CPP_API_x64.FillStyle_style(fillstyle);
            else
                return CPP_API_x32.FillStyle_style(fillstyle);
        }

        public static int* FillStyle_hatch(void* fillstyle)
        {
            if (x64)
                return CPP_API_x64.FillStyle_hatch(fillstyle);
            else
                return CPP_API_x32.FillStyle_hatch(fillstyle);
        }

        public static void** FillStyle_ppattern(void* fillstyle)
        {
            if (x64)
                return CPP_API_x64.FillStyle_ppattern(fillstyle);
            else
                return CPP_API_x32.FillStyle_ppattern(fillstyle);
        }

        public static void* FillStyle_New_1()
        {
            if (x64)
                return CPP_API_x64.FillStyle_New_1();
            else
                return CPP_API_x32.FillStyle_New_1();
        }

        public static void* FillStyle_New_2(void* copyFS)
        {
            if (x64)
                return CPP_API_x64.FillStyle_New_2(copyFS);
            else
                return CPP_API_x32.FillStyle_New_2(copyFS);
        }

        public static void FillStyle_Free(void* fillstyle)
        {
            if (x64)
                CPP_API_x64.FillStyle_Free(fillstyle);
            else
                CPP_API_x32.FillStyle_Free(fillstyle);
        }

        #endregion

        #region 图片缓冲区

        public static int IMAGE_getwidth(void* img)
        {
            if (x64)
                return CPP_API_x64.IMAGE_getwidth(img);
            else
                return CPP_API_x32.IMAGE_getwidth(img);
        }

        public static int IMAGE_getheight(void* img)
        {
            if (x64)
                return CPP_API_x64.IMAGE_getheight(img);
            else
                return CPP_API_x32.IMAGE_getheight(img);
        }

        public static void IMAGE_Resize(void* img, int _width, int _height)
        {
            if (x64)
                CPP_API_x64.IMAGE_Resize(img, _width, _height);
            else
                CPP_API_x32.IMAGE_Resize(img, _width, _height);
        }

        public static uint IMAGE_Size()
        {
            if (x64)
                return CPP_API_x64.IMAGE_Size();
            else
                return CPP_API_x32.IMAGE_Size();
        }

        public static void IMAGE_ctor_1(void* img, int _width = 0, int _height = 0)
        {
            if (x64)
                CPP_API_x64.IMAGE_ctor_1(img, _width, _height);
            else
                CPP_API_x32.IMAGE_ctor_1(img, _width, _height);
        }

        public static void IMAGE_ctor_2(void* img, void* copyImg)
        {
            if (x64)
                CPP_API_x64.IMAGE_ctor_2(img, copyImg);
            else
                CPP_API_x32.IMAGE_ctor_2(img, copyImg);
        }

        public static void IMAGE_operator_assignment(void* img, void* setImg)
        {
            if (x64)
                CPP_API_x64.IMAGE_operator_assignment(img, setImg);
            else
                CPP_API_x32.IMAGE_operator_assignment(img, setImg);
        }

        public static void IMAGE_dtor(void* img)
        {
            if (x64)
                CPP_API_x64.IMAGE_dtor(img);
            else
                CPP_API_x32.IMAGE_dtor(img);
        }

        public static void* IMAGE_New_1(int _width = 0, int _height = 0)
        {
            if (x64)
                return CPP_API_x64.IMAGE_New_1(_width, _height);
            else
                return CPP_API_x32.IMAGE_New_1(_width, _height);
        }

        public static void* IMAGE_New_2(void* copyImg)
        {
            if (x64)
                return CPP_API_x64.IMAGE_New_2(copyImg);
            else
                return CPP_API_x32.IMAGE_New_2(copyImg);
        }

        public static void IMAGE_Free(void* img)
        {
            if (x64)
                CPP_API_x64.IMAGE_Free(img);
            else
                CPP_API_x32.IMAGE_Free(img);
        }

        #endregion

        #endregion

        #endregion

    }

}
