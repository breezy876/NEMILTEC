namespace NEMILTEC.Interfaces.Service.Automation
{
    public interface IIntelliFlowItem
    {
        SharedIntelliFlowProperties Properties { get; set; }

        IIntelliFlow IntelliFlow { get; set; }

        IIntelliFlowItem Parent { get; set; }
        IIntelliFlowIterator<IIntelliFlowItem> Children { get; set; }

        IntelliFlowItemInput Input { get; set; }
        IntelliFlowItemOutput Output { get; set; }

        IntelliFlowItemOutput Execute();
    }
}
