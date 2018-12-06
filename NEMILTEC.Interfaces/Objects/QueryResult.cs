using System;

namespace NEMILTEC.Interfaces.Service.Data.Objects
{
   

    public class QueryResult
    {
        public TimeSpan TimeTaken { get; set; }
        public long TotalRows { get; set; }
        public object Data { get; set; }
        public bool Success { get; set; }
    }
}
