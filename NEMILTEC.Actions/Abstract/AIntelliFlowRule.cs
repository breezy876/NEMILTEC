using System.Collections.Generic;
using NEMILTEC.Interfaces.Service.Data.Expressions;

namespace NEMILTEC.Service.Automation.Abstract
{
    public abstract class AIntelliFlowRule : AIntelliFlowItem
    {

        public IEnumerable<IDataExpression> Expressions { get; set; }

    }
}
