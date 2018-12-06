using NEMILTEC.Shared.Classes.Data;

namespace NEMILTEC.Service.Data.Objects.Queries
{
    public class DataQueryJoinParameter 
    {
        public JoinType Type { get; set; }

        public string ParentTableName { get; set; }
        public string ChildTableName { get; set; }
        public string ParentColumnName { get; set; }
        public string ChildColumnName { get; set; }

        public override string ToString()
        {
            switch (Type)
            {
                case JoinType.Inner:
                    return "INNER";
                case JoinType.LeftOuter:
                    return "LEFT OUTER";
                case JoinType.RightOuter:
                    return "RIGHT OUTER";
                case JoinType.FullOuter:
                    return "FULL OUTER";
                default:
                    return "INNER";
            }
        }
    }
}
