using System;
using System.Collections.Generic;
using System.Linq;
using NEMILTEC.Interfaces.Service.Automation;
using NEMILTEC.Service.Automation.Abstract;

namespace NEMILTEC.Service.Automation.Concrete
{
    public class IntelliFlowIterator<T> : IIntelliFlowIterator<T>
    {
        private T[] _items;
        private T _current;
        private int _currentIndex;

        public IntelliFlowIterator(T[] items)
        {
            _items = items;
        }

        public void Reset()
        {
            _currentIndex = 0;
        }

        public bool MoveNext()
        {
            _currentIndex++;
            return _currentIndex < _items.Length;
        }

        public bool MovePrev()
        {
            _currentIndex--;
            return _currentIndex >= 0;
        }

        public object Current
        {
            get
            {
                return _items[_currentIndex];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.AsEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.AsEnumerable().GetEnumerator();
        }

        T IEnumerator<T>.Current
        {
            get
            {
                return _items[_currentIndex];
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
