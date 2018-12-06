namespace NEMILTEC.Interfaces.Service.Automation
{

    public class IntelliFlowItemOutput
    {
        IntelliFlowItemLog Log { get; set; }
        public virtual object Data { get; set; }

        public IntelliFlowItemOutput()
        {
            Log = new IntelliFlowItemLog();
        }
    }
}
