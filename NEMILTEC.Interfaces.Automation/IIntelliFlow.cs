namespace NEMILTEC.Interfaces.Service.Automation
{
    public interface IIntelliFlow
    {
        SharedIntelliFlowProperties Properties { get; set; }

        IIntelliFlow Parent { get; set; }
        IIntelliFlowIterator<IIntelliFlow> Children { get; set; }

        IIntelliFlowIterator<IIntelliFlowItem> Items { get; set; }

        IntelliFlowOutput Execute();
    }
}
