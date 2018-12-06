using System.Collections.Generic;

namespace NEMILTEC.Interfaces.Service.Automation
{
    public interface IIntelliFlowIterator<T> : IEnumerable<T>, IEnumerator<T>
    {
        bool MovePrev();

    }
}
