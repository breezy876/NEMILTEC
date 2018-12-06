namespace NEMILTEC.Service.Data.Objects.Queries
{
    public class DataQueryOrderingParameter : DataQueryFieldParameter
    {
        DataQueryOrderType Type { get; set; }

        public override string ToString()
        {
            switch (Type)
           {
               case DataQueryOrderType.Asc:
                   return "ASC";
               case DataQueryOrderType.Desc:
                   return "DESC";
               default:
                   return "ASC";
           }
        }
    }
}
