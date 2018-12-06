using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace NEMILTEC.Shared.Classes.Data
{
    [Serializable]
    [ProtoContract()]
    public class DataTable 
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string[] Columns { get; set; }
        [ProtoMember(3)]
        public DataRow[] Rows { get; set; }

        public DataTable()
        {
            
        }

        ///// <summary>
        ///// deep clone
        ///// </summary>
        ///// <param name="data"></param>
        //public void Clone(object obj)
        //{
        //    var data = (DataTable) obj;

        //    var colList = new List<string>();
        //    foreach (var col in data.Columns)
        //    {
        //        colList.Add(col);
        //    }

        //    Columns = colList.ToArray();

        //    var rowList = new List<DataRow>();
        //    foreach (var row in data.Rows)
        //    {
        //        rowList.Add(row);
        //    }

        //    Rows = rowList.ToArray();
        //}


        
  

    }
}
