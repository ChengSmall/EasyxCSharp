using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Cheng.EasyX.DataStructure
{

    /// <summary>
    /// 表示一个非托管资源数组
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public unsafe class UnmanagedArray<T> : SecureUnmanagedObjects, IList<T> where T : unmanaged
    {

        #region 释放
        protected override void ReleaseUnmanaged()
        {
            //p_releaseEvent?.Invoke(this);
            if (p_canDispose)
            {
                Marshal.FreeHGlobal(new IntPtr(p_ptr));
                p_ptr = null;
            }
        }

        ~UnmanagedArray()
        {
            Dispose(false);
        }

        #region 废弃

        #endregion

        #endregion

        #region 构造
        /// <summary>
        /// 实例化一个非托管数组
        /// </summary>
        /// <param name="count">元素数量</param>
        /// <exception cref="ArgumentOutOfRangeException">指定元素数为负数</exception>
        public UnmanagedArray(int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException();
            p_count = count;
            if (count == 0) p_ptr = null;
            else p_ptr = (T*)Marshal.AllocHGlobal(sizeof(T) * count);

            p_canDispose = true;
        }
        /// <summary>
        /// 实例化一个非托管数组
        /// </summary>
        /// <param name="copyArray">拷贝数组元素到非托管数组</param>
        /// <exception cref="ArgumentNullException">参数为null</exception>
        public UnmanagedArray(T[] copyArray)
        {
            if (copyArray is null) throw new ArgumentNullException();

            p_count = copyArray.Length;
            int bs = sizeof(T) * p_count;

            if (p_count == 0)
            {
                p_ptr = null;
                return;
            }
            p_ptr = (T*)Marshal.AllocHGlobal(bs);
            fixed (T* yp = copyArray)
            {
                Buffer.MemoryCopy(yp, p_ptr, bs, bs);
            }
            p_canDispose = true;
        }
        /// <summary>
        /// 实例化一个非托管数组
        /// </summary>
        /// <param name="list">拷贝集合元素到非托管数组</param>
        /// <exception cref="ArgumentNullException">参数为null</exception>
        public UnmanagedArray(IList<T> list)
        {
            if (list is null) throw new ArgumentNullException();

            p_count = list.Count;
            int bs = sizeof(T) * p_count;

            if (p_count == 0)
            {
                p_ptr = null;
                return;
            }
            p_ptr = (T*)Marshal.AllocHGlobal(bs);

            int i;
            for (i = 0; i < p_count; i++)
            {
                p_ptr[i] = list[i];
            }
            p_canDispose = true;
        }
        /// <summary>
        /// 实例化一个非托管数组
        /// </summary>
        /// <param name="uarr">拷贝构造</param>
        /// <exception cref="ArgumentNullException">参数为null</exception>
        /// <exception cref="ObjectDisposedException">参数非托管数组已释放</exception>
        public UnmanagedArray(UnmanagedArray<T> uarr)
        {
            if (uarr is null) throw new ArgumentNullException();
            if (uarr.IsDispose) throw new ObjectDisposedException(nameof(uarr));
            p_count = uarr.p_count;

            if (p_count == 0)
            {
                p_ptr = null;
                return;
            }
            int bs = sizeof(T) * p_count;

            p_ptr = (T*)Marshal.AllocHGlobal(bs);

            Buffer.MemoryCopy(uarr.p_ptr, p_ptr, bs, bs);
            p_canDispose = true;
        }

        private UnmanagedArray(bool isEmpty)
        {
            if (isEmpty)
            {
                p_ptr = null;
                p_count = 0;
                p_strobing = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handptr">数组指针</param>
        /// <param name="count">数组元素</param>
        /// <param name="canDispose">是否释放</param>
        internal UnmanagedArray(T* handptr, int count, bool canDispose)
        {
            p_count = count;
            p_ptr = handptr;
            p_canDispose = canDispose;
        }
        #endregion

        #region 参数

        private static UnmanagedArray<T> cp_Empty = new UnmanagedArray<T>(true);

        private int p_count;
        internal T* p_ptr;
        private uint p_strobing = 0;
        private bool p_canDispose;
        #endregion

        #region 功能

        #region 数组功能

        /// <summary>
        /// 访问或设置指定索引的元素
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>指定索引的元素</returns>
        /// <exception cref="ArgumentOutOfRangeException">索引超出范围</exception>
        /// <exception cref="ObjectDisposedException">资源已释放</exception>
        public T this[int index]
        {
            get
            {
                if (IsDispose) throw new ObjectDisposedException(nameof(UnmanagedArray<T>));
                if (index < 0 || index >= p_count) throw new ArgumentOutOfRangeException();
                return p_ptr[index];
            }
            set
            {
                if (IsDispose) throw new ObjectDisposedException(nameof(UnmanagedArray<T>));
                if (index < 0 || index >= p_count) throw new ArgumentOutOfRangeException();
                p_ptr[index] = value;
                p_strobing++;
            }
        }

        /// <summary>
        /// 获取集合元素数
        /// </summary>
        /// <exception cref="ObjectDisposedException">资源已释放</exception>
        public int Count
        {
            get
            {
                if (IsDispose) throw new ObjectDisposedException(nameof(UnmanagedArray<T>));
                return p_count;
            }
        }

        public int IndexOf(T item)
        {
            if (IsDispose) throw new ObjectDisposedException(nameof(UnmanagedArray<T>));
            if (p_count == 0) return -1;
            T* tp = p_ptr;
            var eq = EqualityComparer<T>.Default;

            for (int i = 0; i < p_count; i++)
            {
                if (eq.Equals(tp[i], item)) return i;
            }
            return -1;
        }

        public bool Contains(T item)
        {
            if (IsDispose) throw new ObjectDisposedException(nameof(UnmanagedArray<T>));
            if (p_count == 0) return false;
            T* tp = p_ptr;
            var eq = EqualityComparer<T>.Default;

            for (int i = 0; i < p_count; i++)
            {
                if (eq.Equals(tp[i], item)) return true;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (IsDispose) throw new ObjectDisposedException(nameof(UnmanagedArray<T>));
            if (array is null) throw new ArgumentNullException();
            if (array.Length <= arrayIndex) throw new ArgumentOutOfRangeException();
            if (p_count == 0) return;
            int cop = array.Length - arrayIndex;
            int sp = cop;
            if (p_count < cop) sp = p_count;
            fixed (T* top = array)
            {
                Buffer.MemoryCopy(p_ptr, top + arrayIndex, cop, sp);
            }
        }

        /// <summary>
        /// 将非托管数组元素拷贝到数组中并返回
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ObjectDisposedException">资源已释放</exception>
        public T[] ToArray()
        {
            if (IsDispose) throw new ObjectDisposedException(nameof(UnmanagedArray<T>));
            T[] arr = new T[p_count];
            if (p_count == 0) return arr;

            fixed (T* tp = arr)
            {
                int size = p_count * sizeof(T);
                Buffer.MemoryCopy(p_ptr, tp, size, size);
            }
            return arr;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (IsDispose) throw new ObjectDisposedException(nameof(UnmanagedArray<T>));
            return new Enumator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (IsDispose) throw new ObjectDisposedException(nameof(UnmanagedArray<T>));
            return new Enumator(this);
        }

        #region 结构

        private class Enumator : IEnumerator<T>
        {
            public Enumator(UnmanagedArray<T> arr)
            {
                dt = arr.p_strobing;
                p_arr = arr;
                index = -1;
                cur = default;
            }
            private T cur;
            private UnmanagedArray<T> p_arr;
            private int index;
            private uint dt;
            public T Current
            {
                get
                {
                    return cur;
                }
            }

            object IEnumerator.Current => this.Current;

            public bool MoveNext()
            {
                if (p_arr.p_strobing != dt) throw new InvalidOperationException();


                if (index <= p_arr.p_count) index++;

                if (index >= 0 && index < p_arr.p_count)
                {
                    cur = p_arr[index];
                    return true;
                }
                cur = default;
                return false;
            }

            public void Reset()
            {
                index = -1;
                cur = default;
            }

            void IDisposable.Dispose()
            {
            }
        }

        #endregion

        #endregion

        #region 派生

        void ICollection<T>.Add(T v)
        {
            throw new NotSupportedException();
        }
        void ICollection<T>.Clear()
        {
            throw new NotSupportedException();
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException();
        }

        void IList<T>.Insert(int index, T item)
        {
            throw new NotSupportedException();
        }

        void IList<T>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        bool ICollection<T>.IsReadOnly
        {
            get => true;
        }

        #endregion

        #endregion
        
    }


}
