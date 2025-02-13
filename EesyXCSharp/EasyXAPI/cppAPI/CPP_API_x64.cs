using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.InteropServices;
using Cheng.EasyX.DataStructure;

using HWND = System.IntPtr;
using HRGN = System.IntPtr;
using HDC = System.IntPtr;
using DWORD = System.UInt32;
using TCHAR = System.Char;
using System.Text;

namespace Cheng.EasyX.CPP
{

    internal unsafe static class CPP_API_x64
    {

        #region 常量
        //const string path = @"D:\BianCheng\Project\EasyXcpp\EasyXcpp_dll\EasyXcpp_dll\output\win64\Release\";
        public const string dllName = "EasyXcpp64.dll";
        #endregion

        #region api

        #region 绘图设备

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getheight_();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getwidth_();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void clearcliprgn_();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void cleardevice_();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void closegraph_();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void getaspectratio_(float* pxasp, float* pyasp);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void graphdefaults_();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern HWND initgraph_(int width, int height,[MarshalAs(UnmanagedType.Bool)] bool flag = false);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void setaspectratio_(float xasp, float yasp);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void setcliprgn_(HRGN hrgn);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void setorigin_(int x, int y);


        #endregion

        #region 颜色模型
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static RGBColor BGR_(RGBColor color);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RGBColor HSLtoRGB_(float H, float S, float L);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RGBColor HSVtoRGB_(float H, float S, float V);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RGBColor RGBtoGRAY_(RGBColor rgb);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RGBtoHSL_(RGBColor rgb, float* H, float* S, float* L);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RGBtoHSV_(RGBColor rgb, float* H, float* S, float* V);

        #endregion

        #region 颜色及样式设置

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static RGBColor getbkcolor_();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int getbkmode_();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static RGBColor getfillcolor_();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void getfillstyle_(void* pstyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static RGBColor getlinecolor_();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void getlinestyle_(void* pstyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int getpolyfillmode_();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int getrop2_();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void setrop2_(int rop);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void setbkcolor_(RGBColor color);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void setbkmode_(int mode);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void setfillcolor_(RGBColor color);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void setfillstyle_1(void* pstyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void setfillstyle_2(int style, int hatch = 0, void* ppattern = null);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void setfillstyle_3([MarshalAs(UnmanagedType.LPArray)] byte[,] ppattern8x8);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void setlinecolor_(RGBColor color);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void setlinestyle_1(void* pstyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void setlinestyle_2(int style, int thickness = 1, uint* puserstyle = null, uint userstylecount = 0);


        #endregion

        #region 绘图

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static RGBColor getpixel_(int x, int y);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void putpixel_(int x, int y, RGBColor color);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void line_(int x1, int y1, int x2, int y2);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void rectangle_(int left, int top, int right, int bottom);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void fillrectangle_(int left, int top, int right, int bottom);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void solidrectangle_(int left, int top, int right, int bottom);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void clearrectangle_(int left, int top, int right, int bottom);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void circle_(int x, int y, int radius);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void fillcircle_(int x, int y, int radius);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void solidcircle_(int x, int y, int radius);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void clearcircle_(int x, int y, int radius);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void ellipse_(int left, int top, int right, int bottom);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void fillellipse_(int left, int top, int right, int bottom);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void solidellipse_(int left, int top, int right, int bottom);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void clearellipse_(int left, int top, int right, int bottom);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void roundrect_(int left, int top, int right, int bottom, int ellipsewidth, int ellipseheight);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void fillroundrect_(int left, int top, int right, int bottom, int ellipsewidth, int ellipseheight);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void solidroundrect_(int left, int top, int right, int bottom, int ellipsewidth, int ellipseheight);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void clearroundrect_(int left, int top, int right, int bottom, int ellipsewidth, int ellipseheight);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void arc_(int left, int top, int right, int bottom, double stangle, double endangle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void pie_(int left, int top, int right, int bottom, double stangle, double endangle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void fillpie_(int left, int top, int right, int bottom, double stangle, double endangle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void solidpie_(int left, int top, int right, int bottom, double stangle, double endangle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void clearpie_(int left, int top, int right, int bottom, double stangle, double endangle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void polyline_([MarshalAs(UnmanagedType.LPArray)] EPoint[] points, int num);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void polygon_([MarshalAs(UnmanagedType.LPArray)] EPoint[] points, int num);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void fillpolygon_([MarshalAs(UnmanagedType.LPArray)] EPoint[] points, int num);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void solidpolygon_([MarshalAs(UnmanagedType.LPArray)] EPoint[] points, int num);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void clearpolygon_([MarshalAs(UnmanagedType.LPArray)] EPoint[] points, int num);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void polybezier_([MarshalAs(UnmanagedType.LPArray)] EPoint[] points, int num);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void floodfill_(int x, int y, RGBColor color, int filltype = 0);


        #region 多边形

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void polygon_(EPoint* points, int num);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void fillpolygon_(EPoint* points, int num);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void solidpolygon_(EPoint* points, int num);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void clearpolygon_(EPoint* points, int num);

        #endregion

        #endregion

        #region 文字输出

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int drawtext_([MarshalAs(UnmanagedType.LPWStr)] string str, ERect* pRect, uint uFormat);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int drawtext_2(char c, ERect* pRect, uint uFormat);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static RGBColor gettextcolor_();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void gettextstyle_(ref LogFont font);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void outtextxy_1(int x, int y, [MarshalAs(UnmanagedType.LPWStr)] string str);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void outtextxy_2(int x, int y, TCHAR c);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void settextcolor_(RGBColor color);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void settextstyle_1(int nHeight, int nWidth, [MarshalAs(UnmanagedType.LPWStr)] string lpszFace);


        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void settextstyle_2(int nHeight, int nWidth, [MarshalAs(UnmanagedType.LPWStr)] string lpszFace, int nEscapement, int nOrientation,
            int nWeight, bool bItalic, bool bUnderline, bool bStrikeOut);


        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void settextstyle_3(int nHeight, int nWidth, [MarshalAs(UnmanagedType.LPWStr)] string lpszFace,
            int nEscapement, int nOrientation, int nWeight, bool bItalic, bool bUnderline, bool bStrikeOut,
            byte fbCharSet, byte fbOutPrecision, byte fbClipPrecision, byte fbQuality, byte fbPitchAndFamily);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void settextstyle_4(ref LogFont font);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int textheight_1([MarshalAs(UnmanagedType.LPWStr)] string str);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int textheight_2(TCHAR c);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int textwidth_1([MarshalAs(UnmanagedType.LPWStr)] string str);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int textwidth_2(TCHAR c);



        #endregion

        #region 图像处理

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void getimage_(void* pDstImg, int srcX, int srcY, int srcWidth, int srcHeight);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static uint* GetImageBuffer_(void* pImg = null);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static HDC GetImageHDC_(void* pImg = null);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void* GetWorkingImage_();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void loadimage_1(void* pDstImg, [MarshalAs(UnmanagedType.LPWStr)] string pImgFile,
            int nWidth = 0, int nHeight = 0, bool bResize = false);


        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void loadimage_2(void* pDstImg, [MarshalAs(UnmanagedType.LPWStr)] string pResType, [MarshalAs(UnmanagedType.LPWStr)] string pResName,
            int nWidth = 0, int nHeight = 0, bool bResize = false);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void putimage_1(int dstX, int dstY, void* pSrcImg, DWORD dwRop = 0x00CC0020);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void putimage_2(int dstX, int dstY, int dstWidth, int dstHeight,
            void* pSrcImg, int srcX, int srcY, uint dwRop = 0x00CC0020);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void Resize_(void* pImg, int width, int height);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void rotateimage_(void* dstimg, void* srcimg, double radian,
            RGBColor bkcolor = default, bool autosize = false, bool highquality = true);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void saveimage_([MarshalAs(UnmanagedType.LPWStr)] string strFileName, void* pImg = null);

        #endregion

        #region 消息处理

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void flushmessage_(byte filter = byte.MaxValue);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static CsMessage getmessage_1(byte filter = byte.MaxValue);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void getmessage_2(CsMessage* msg, byte filter = byte.MaxValue);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool peekmessage_(CsMessage* msg, byte filter = byte.MaxValue, bool removemsg = true);

        #endregion

        #region 其它

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void BeginBatchDraw_();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void EndBatchDraw_1();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void EndBatchDraw_2(int left, int top, int right, int bottom);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void FlushBatchDraw_1();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void FlushBatchDraw_2(int left, int top, int right, int bottom);

        [return: MarshalAs(UnmanagedType.LPWStr)]
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static string GetEasyXVer_();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static HWND GetHWnd_();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool InputBox_([MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder pString, int nMaxCount, [MarshalAs(UnmanagedType.LPWStr)] string pPrompt = null,
            [MarshalAs(UnmanagedType.LPWStr)] string pTitle = null,
            [MarshalAs(UnmanagedType.LPWStr)] string pDefault = null,
            int width = 0, int height = 0, bool bHideCancelBtn = true);


        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool InputBox_(char* pString, int nMaxCount, [MarshalAs(UnmanagedType.LPWStr)] string pPrompt = null,
            [MarshalAs(UnmanagedType.LPWStr)] string pTitle = null,
            [MarshalAs(UnmanagedType.LPWStr)] string pDefault = null,
            int width = 0, int height = 0, bool bHideCancelBtn = true);

        #endregion

        #region 对象api

        #region 消息CsMessage

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static uint CsMessage_Size();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static ushort* CsMessage_message(CsMessage* meg);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool CsMessage_M_ctrl_get(CsMessage* meg);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void CsMessage_M_ctrl_set(CsMessage* meg, bool value);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool CsMessage_M_shift_get(CsMessage* meg);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void CsMessage_M_shift_set(CsMessage* meg, bool value);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool CsMessage_M_lbutton_get(CsMessage* meg);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void CsMessage_M_lbutton_set(CsMessage* meg, bool value);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool CsMessage_M_mbutton_get(CsMessage* meg);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void CsMessage_M_mbutton_set(CsMessage* meg, bool value);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool CsMessage_M_rbutton_get(CsMessage* meg);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void CsMessage_M_rbutton_set(CsMessage* meg, bool value);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static short* CsMessage_M_x(CsMessage* meg);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static short* CsMessage_M_y(CsMessage* meg);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static short* CsMessage_M_wheel(CsMessage* meg);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static byte* CsMessage_K_vkcode(CsMessage* meg);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static byte* CsMessage_K_scancode(CsMessage* meg);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool CsMessage_K_extended_get(CsMessage* meg);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void CsMessage_K_extended_set(CsMessage* meg, bool value);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool CsMessage_K_prevdown_get(CsMessage* meg);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void CsMessage_K_prevdown_set(CsMessage* meg, bool value);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static TCHAR* CsMessage_ch(CsMessage* meg);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr* CSMessage_wParam(CsMessage* meg);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static UIntPtr* CSMessage_lParam(CsMessage* meg);
        #endregion

        #region 画线样式

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static uint LineStyle_Size();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void LineStyle_ctor_1(void* linestyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void LineStyle_ctor_2(void* linestyle, void* copyLineStyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void LineStyle_operator_assignment(void* linestyle, void* setLineStyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void LineStyle_dtor(void* linestyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static DWORD* LineStyle_style(void* linestyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static DWORD* LineStyle_thickness(void* linestyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static DWORD** LineStyle_puserstyle(void* linestyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static DWORD* LineStyle_userstylecount(void* linestyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void* LineStyle_New_1();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void* LineStyle_New_2(void* copyLS);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void LineStyle_Free(void* linestyle);


        #endregion

        #region 填充样式

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static uint FillStyle_Size();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void FillStyle_ctor_1(void* fillstyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void FillStyle_ctor_2(void* fillstyle, void* copyFillStyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void FillStyle_operator_assignment(void* fillstyle, void* setFillStyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void FillStyle_dtor(void* fillstyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int* FillStyle_style(void* fillstyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int* FillStyle_hatch(void* fillstyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void** FillStyle_ppattern(void* fillstyle);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void* FillStyle_New_1();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void* FillStyle_New_2(void* copyFS);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void FillStyle_Free(void* fillstyle);


        #endregion

        #region 图片缓冲区


        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int IMAGE_getwidth(void* img);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int IMAGE_getheight(void* img);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void IMAGE_Resize(void* img, int _width, int _height);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static uint IMAGE_Size();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void IMAGE_ctor_1(void* img, int _width = 0, int _height = 0);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void IMAGE_ctor_2(void* img, void* copyImg);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void IMAGE_operator_assignment(void* img, void* setImg);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void IMAGE_dtor(void* img);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void* IMAGE_New_1(int _width = 0, int _height = 0);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void* IMAGE_New_2(void* copyImg);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void IMAGE_Free(void* img);


        #endregion

        #endregion

        #endregion


    }
}
