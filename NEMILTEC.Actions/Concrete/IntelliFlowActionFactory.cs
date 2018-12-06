using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Interfaces.Service.Automation;
using NEMILTEC.Service.Automation.Concrete.Actions;
using NEMILTEC.Service.Automation.Enums;

namespace NEMILTEC.Service.Automation.Concrete
{
    public class IntelliFlowActionFactory : IIntelliFlowItemFactory
    {

        private static IDictionary<IntelliFlowActionType, IIntelliFlowItem> _actions = new Dictionary
            <IntelliFlowActionType, IIntelliFlowItem>()
        {
            {IntelliFlowActionType.DatabaseQueryAction, new DatabaseQueryIntelliFlowAction()}
        };

        public IIntelliFlowItem Create(long type)
        {
            return _actions[(IntelliFlowActionType)type];
        }
    }
}
