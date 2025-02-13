using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Cheng.EasyX.DataStructure;
using Cheng.EasyX.CPP;
using Cheng.EasyX.Exceptions;
using System.ComponentModel;

namespace Cheng.EasyX
{

    /// <summary>
    /// 绘图设备管理
    /// </summary>
    /// <remarks>
    /// <para>这是管理绘图设备相关api的静态类</para>
    /// </remarks>
    public unsafe static class Device
    {

        private class SafeObject
        {
            public SafeObject()
            {
            }

            public EPoint winSize = default;
            public EPoint origin;
            public IntPtr winHandle;
            public bool isOpenWin = false;
            public bool isBeginBatchDraw = false;
        }

        #region 参数
        static SafeObject p_par = new SafeObject();
        #endregion

        #region 封装

        #region winapi

        #endregion

        /// <summary>
        /// 测试是否未初始化并引发异常
        /// </summary>
        internal static void f_testNotInitGraph(string message)
        {
            if (!p_par.isOpenWin) throw new WindowEasyXException(message);
        }

        #endregion

        #region 公开参数

        /// <summary>
        /// 绘图设备管理的线程对象锁
        /// </summary>
        public static object SafeThreadLockObject
        {
            get => p_par;
        }

        /// <summary>
        /// 窗口是否已打开
        /// </summary>
        public static bool IsOpenWindow => p_par.isOpenWin;

        /// <summary>
        /// 获取当前窗口的大小
        /// </summary>
        /// <returns>使用坐标结构储存当前的窗体大小，x表示长度，y表示高度</returns>
        /// <exception cref="WindowEasyXException">窗口未初始化</exception>
        public static EPoint WindowSize
        {
            get
            {
                if (!p_par.isOpenWin) throw new WindowEasyXException(exc_winNotInit);
                return p_par.winSize;
            }
        }

        /// <summary>
        /// 当前窗体是否开启批量绘图
        /// </summary>
        /// <returns>窗口开启批量绘图返回true，未开启则返回false；若窗口未打开则返回false</returns>
        public static bool IsBeginBatchDraw => p_par.isBeginBatchDraw;

        /// <summary>
        /// 返回当前窗体的HWND句柄；若窗体未打开或已关闭，则返回空句柄
        /// </summary>
        public static IntPtr WindowHandle
        {
            get
            {
                return p_par.winHandle;
            }
        }

        /// <summary>
        /// 获取窗口绘图句柄，若窗体未打开则返回空句柄
        /// </summary>
        public static IntPtr WindowHDC
        {
            get
            {
                if (p_par.isOpenWin) return EasyX_API.GetImageHDC_(null);
                return IntPtr.Zero;
            }
        }
        #endregion

        #region api

        /// <summary>
        /// 获取绘图区的大小
        /// </summary>
        /// <returns>绘图区的大小；x表示长，y表示高</returns>
        /// <exception cref="WindowEasyXException">窗口未初始化</exception>
        public static EPoint GraphSize
        {
            get
            {
                f_testNotInitGraph(exc_winNotInit);
                return new EPoint(EasyX_API.getwidth_(), EasyX_API.getheight_());
            }
        }

        internal const string exc_winNotInit = "窗口未初始化";
        /// <summary>
        /// 初始化窗口
        /// </summary>
        /// <param name="width">窗口长度</param>
        /// <param name="height">窗口高度</param>
        /// <returns>新建绘图窗口的句柄</returns>
        public static IntPtr InitGraph(int width, int height)
        {
            lock (p_par)
            {
                p_par.winSize = new EPoint(width, height);
                IntPtr p = EasyX_API.initgraph_(width, height, false);
                p_par.isOpenWin = true;
                p_par.winHandle = p;
                return p;
            }
        }

        /// <summary>
        /// 初始化窗口
        /// </summary>
        /// <param name="width">窗口长度</param>
        /// <param name="height">窗口高度</param>
        /// <param name="flag">是否开启控制台，默认为false</param>
        /// <returns>窗口的Hwnd句柄</returns>
        public static IntPtr InitGraph(int width, int height, bool flag)
        {
            lock (p_par)
            {
                p_par.winSize = new EPoint(width, height);
                IntPtr p = EasyX_API.initgraph_(width, height, flag);
                p_par.isOpenWin = true;
                p_par.winHandle = p;
                return p;
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <exception cref="WindowEasyXException">窗口未初始化</exception>
        public static void CloseGraph()
        {
            lock (p_par)
            {
                if (!p_par.isOpenWin) throw new WindowEasyXException("窗口未初始化");
                p_par.winHandle = IntPtr.Zero;
                p_par.isOpenWin = false;
                p_par.isBeginBatchDraw = false;
                EasyX_API.closegraph_();
            }
        }

        /// <summary>
        /// 使用当前背景色清空绘图设备
        /// </summary>
        public static void Cleardevice()
        {
            f_testNotInitGraph(exc_winNotInit);
            EasyX_API.cleardevice_();
        }

        /// <summary>
        /// 重置视图、当前点、绘图色、背景色、线形、填充样式、字体为默认值
        /// </summary>
        public static void Graphdefaults()
        {
            f_testNotInitGraph(exc_winNotInit);

            EasyX_API.graphdefaults_();
        }

        /// <summary>
        /// 获取窗口缩放因子
        /// </summary>
        /// <param name="pxasp">x 方向上的缩放因子</param>
        /// <param name="pyasp">y 方向上的缩放因子</param>
        /// <exception cref="WindowEasyXException">窗口未初始化</exception>
        public static void Aspectratio(out float pxasp, out float pyasp)
        {
            f_testNotInitGraph(exc_winNotInit);
            pxasp = default;
            pyasp = default;
            fixed(float* xp = &pxasp, yp = &pyasp)
            {
                EasyX_API.getaspectratio_(xp, yp);
            }
        }

        /// <summary>
        /// 设置窗口缩放因子
        /// </summary>
        /// <param name="pxasp">x 方向上的缩放因子</param>
        /// <param name="pyasp">y 方向上的缩放因子</param>
        /// <exception cref="WindowEasyXException">窗口未初始化</exception>
        /// <exception cref="ArgumentException">参数是<see cref="float.NaN"/>或范围过大</exception>
        public static void Aspectratio(float pxasp, float pyasp)
        {
            f_testNotInitGraph(exc_winNotInit);
            if (float.IsNaN(pxasp) || float.IsNaN(pyasp) || float.IsInfinity(pxasp) || float.IsInfinity(pyasp)) throw new ArgumentException();

            EasyX_API.setaspectratio_(pxasp, pyasp);
        }

        /// <summary>
        /// 获取或设置坐标原点
        /// </summary>
        public static EPoint Origin
        {
            get
            {
                f_testNotInitGraph(exc_winNotInit);
                return p_par.origin;
            }
            set
            {
                f_testNotInitGraph(exc_winNotInit);
                EasyX_API.setorigin_(value.x, value.y);
                p_par.origin = value;
            }
        }

        /// <summary>
        /// 开启批量绘图
        /// </summary>
        /// <exception cref="WindowEasyXException">窗口未初始化</exception>
        public static void BeginBatchDraw()
        {
            f_testNotInitGraph(exc_winNotInit);
            lock (p_par)
            {
                if (p_par.isBeginBatchDraw) throw new WindowEasyXException("无法重复开启批量绘图");

                EasyX_API.BeginBatchDraw_();
                p_par.isBeginBatchDraw = true;
            }
        }

        /// <summary>
        /// 开启批量绘图
        /// </summary>
        /// <returns>是否开启批量绘图，如果返回true表示成功开启，返回false表示已开启</returns>
        /// <exception cref="WindowEasyXException">窗口未初始化</exception>
        public static bool TryBeginBatchDraw()
        {
            f_testNotInitGraph(exc_winNotInit);
            lock (p_par)
            {
                if (p_par.isBeginBatchDraw) return false;

                EasyX_API.BeginBatchDraw_();
                p_par.isBeginBatchDraw = true;
                return true;
            }
        }

        /// <summary>
        /// 结束批量绘制，并执行未完成的绘制任务
        /// </summary>
        /// <exception cref="WindowEasyXException">窗口未初始化</exception>
        public static void EndBatchDraw()
        {
            f_testNotInitGraph(exc_winNotInit);
            if (!p_par.isBeginBatchDraw) throw new WindowEasyXException("无法重复关闭批量绘图");
            lock (p_par)
            {
                EasyX_API.EndBatchDraw_1();
                p_par.isBeginBatchDraw = false;
            }
        }

        /// <summary>
        /// 结束批量绘制，并执行指定区域内未完成的绘制任务
        /// </summary>
        /// <param name="FlushRect">指定区域</param>
        /// <exception cref="WindowEasyXException">窗口未初始化</exception>
        public static void EndBatchDraw(ERect FlushRect)
        {
            f_testNotInitGraph(exc_winNotInit);
            if (!p_par.isBeginBatchDraw) throw new WindowEasyXException("无法重复关闭批量绘图");
            lock (p_par)
            {
                EasyX_API.EndBatchDraw_2(FlushRect.left, FlushRect.top, FlushRect.right, FlushRect.bottom);
                p_par.isBeginBatchDraw = false;
            }
        }

        /// <summary>
        /// 执行未完成的绘制任务
        /// </summary>
        /// <exception cref="WindowEasyXException">窗口未初始化</exception>
        public static void FlushBatchDraw()
        {
            f_testNotInitGraph(exc_winNotInit);
            EasyX_API.FlushBatchDraw_1();
        }

        /// <summary>
        /// 执行指定区域内未完成的绘制任务
        /// </summary>
        /// <param name="FlushRect">指定区域</param>
        public static void FlushBatchDraw(ERect FlushRect)
        {
            f_testNotInitGraph(exc_winNotInit);

            EasyX_API.FlushBatchDraw_2(FlushRect.left, FlushRect.top, FlushRect.right, FlushRect.bottom);
        }

        /// <summary>
        /// 获取或设置窗口名称
        /// </summary>
        /// <exception cref="ArgumentNullException">设置参数为null</exception>
        /// <exception cref="WindowEasyXException">无法获取窗口名称</exception>
        public static string WindowName
        {
            get
            {
                const int maxLength = 128;
                StringBuilder sb = new StringBuilder(maxLength);
                int re = EasyX_API.GetWindowText(p_par.winHandle, sb, maxLength);
                if (re == 0) throw new WindowEasyXException("无法获取窗口名称");
                return sb.ToString();
            }
            set
            {
                if (value is null) throw new ArgumentNullException();
                EasyX_API.SetWindowText(p_par.winHandle, value);
            }
        }

        /// <summary>
        /// 获取窗口名称
        /// </summary>
        /// <param name="nameBuffer">要将名称获取到的缓冲区，其中<see cref="System.Text.StringBuilder.Capacity"/>参数代表获取的最大字符数</param>
        /// <returns>是否获取成功</returns>
        /// <exception cref="ArgumentNullException">参数为null</exception>
        public static bool GetWindowName(System.Text.StringBuilder nameBuffer)
        {
            if (nameBuffer is null) throw new ArgumentNullException();
            int re = EasyX_API.GetWindowText(p_par.winHandle, nameBuffer, nameBuffer.Capacity);
            return re != 0;
        }

        /// <summary>
        /// 调整指定绘图设备的尺寸
        /// </summary>
        /// <param name="width">设置的长</param>
        /// <param name="height">设置的高</param>
        /// <param name="img">要设置的绘图面板</param>
        /// <exception cref="ArgumentException">参数不正确</exception>
        /// <exception cref="ArgumentNullException">参数为null</exception>
        /// <exception cref="ObjectDisposedException">设备已释放</exception>
        public static void ResizeDraw(EasyImage img, int width, int height)
        {
            if (img is null) throw new ArgumentNullException();
            if (img.p_isDispose) throw new ObjectDisposedException(nameof(img));
            if (width < 0 || height < 0) throw new ArgumentOutOfRangeException();

            EasyX_API.Resize_(img.p_imagePtr, width, height);
        }

        /// <summary>
        /// 重新指定窗口大小和位置
        /// </summary>
        /// <param name="rect">要指定的新区域，在屏幕大小范围内</param>
        /// <param name="repaint">
        /// <para>指示是否重新绘制窗口</para>
        /// <para>如果此参数为true，则窗口将收到消息；如果参数为false，则不会进行任何类型的重新绘制</para>
        /// <para>这适用于工作区、非工作区 (包括标题栏和滚动条) ，以及由于移动子窗口而发现父窗口的任何部分</para>
        /// </param>
        public static void MoveWindow(ERect rect,bool repaint)
        {
            f_testNotInitGraph(exc_winNotInit);
            lock (p_par)
            {
                if(!EasyX_API.MoveWindow(p_par.winHandle, rect.left, rect.top, rect.Width, rect.Height, repaint))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            
        }

        #endregion

    }

}
