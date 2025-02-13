using System;

namespace Cheng.EasyX.DataStructure
{

    /// <summary>
    /// 能够安全释放非托管资源的对象基类
    /// </summary>
    public abstract class SecureUnmanagedObjects : IDisposable
    {

        /// <summary>
        /// 重写此方法，用于释放非托管资源
        /// </summary>
        protected virtual void ReleaseUnmanaged() { }

        /// <summary>
        /// 重写该方法以更自由的释放非托管资源
        /// </summary>
        /// <remarks>该方法在派生类释放时调用一次，参数为<see cref="Dispose(bool)"/>的参数，返回值表示是否执行GC取消对象析构函数；不受<see cref="isHaveDispose"/>影响</remarks>
        /// <param name="disposing">调用<see cref="Dispose(bool)"/>时传入的参数</param>
        /// <returns>是否执行GC取消对象析构函数；默认返回true</returns>
        protected virtual bool Disposing(bool disposing) => true;

        /// <summary>
        /// 是否已释放非托管资源
        /// </summary>
        public bool IsDispose => p_isDispose;

        /// <summary>
        /// 在进行释放时如果该对象进行调用<see cref="GC.SuppressFinalize(object)"/>则在此之前调用该方法
        /// </summary>
        protected virtual void OnSuppressFinalize() { }

        /// <summary>
        /// 如果已释放资源则抛出<see cref="ObjectDisposedException"/>
        /// </summary>
        /// <exception cref="ObjectDisposedException">已释放资源</exception>
        protected void ThrowObjectDisposedException()
        {
            if(p_isDispose) throw new ObjectDisposedException(this.GetType().FullName);
        }

        #region 封装
        /// <summary>
        /// 是否已释放
        /// </summary>
        internal bool p_isDispose = false;
        /// <summary>
        /// 调用此方法释放和关闭非托管资源
        /// </summary>
        public virtual void Close()
        {
            Dispose(true);
        }

        /// <summary>
        /// 调用此方法释放所有非托管资源
        /// </summary>
        /// <param name="disposing">是否释放托管资源，若要释放托管资源则为true，不释放托管资源使用false；通常在析构函数中调用时使用false</param>
        protected void Dispose(bool disposing)
        {
            if (p_isDispose) return;
            p_isDispose = true;

            ReleaseUnmanaged();

            var b = Disposing(disposing);
            if ((disposing) && b)
            {
                //非构造调用
                OnSuppressFinalize();
                GC.SuppressFinalize(this);
            }

        }
        void IDisposable.Dispose()
        {
            Dispose(true);
        }

        #endregion

    }

}
