using System.Collections.Generic;

namespace NEMILTEC.Interfaces.Service.Automation
{

    public class IntelliFlowItemInput
    {
        public virtual object Data { get; set; }
        public IEnumerable<IntelliFlowItemInputParameter> Parameters { get; set; }
        
        public IntelliFlowItemInput()
        {
            Parameters = new List<IntelliFlowItemInputParameter>();
        }
    }
}
