using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NEMILTEC.Interfaces.Service.Data.File;
using NEMILTEC.MVC.Models;
using NEMILTEC.Service.Data.Expressions.Data;
using NEMILTEC.Service.Data.Expressions.Evaluators;
using NEMILTEC.Service.Data.Expressions.Query;
using NEMILTEC.Service.Data.File.Exporters;
using NEMILTEC.Service.Data.File.Importers;
using NEMILTEC.Service.Data.Objects.Queries;
using NEMILTEC.Shared.Classes.Data;
using NEMILTEC.Shared.Classes.Serializers;
using NEMILTEC.Shared.Enums.Data;

namespace NEMILTEC.Tests.NEMILTECT.Data
{

    [TestClass]
    public class DataQueryTests
    {



        //[TestInitialize]
        //public void Initialize()
        //{

        //}



                    [TestMethod]
        public void Test_Serialize()
                    {
            var json1 = @"{ ""Properties"":[{""IsNew"":true,""Name"":null,""Type"":1,""Value"":null,""Index"":0},{""IsNew"":false,""Name"":""Property1"",""Type"":2,""Value"":1,""Index"":1},{""IsNew"":false,""Name"":""Property2"",""Type"":2,""Value"":2,""Index"":2},{""IsNew"":false,""Name"":""Property3"",""Type"":1,""Value"":""Test"",""Index"":3}],""Name"":null}";
                        var json = @"{""Name"":""test"",""Properties"":[{IsNew:true,Name:null,Type:1,Value:1,Index:0}]}";
                        var obj = JSONSerializer.Deserialize(DataType.Entity, json);
            var x = (object)obj;
            var stream = new MemoryStream();
            var data = BinarySerializer.Serialize<DataEntity>((DataEntity)x);
            var entity = BinarySerializer.Deserialize(DataType.Entity, data);

        }

        [TestMethod]
        public void Test_Excel_FileExport()
        {

            IFileExporter<DataTable> exporter = new ExcelDataTableFileExporter();

            var dt = new DataTable()
            {
                Columns = new [] { "Column1", "Column2", "Column3"},
                Rows = new  DataRow[]
                {
                    new DataRow() { Values = new [] { "1", "2", "3"} },
                    new DataRow() { Values = new [] { "4", "5", "6"} }
                }

            };

            var outStream = exporter.Export(dt);
            outStream.Position = 0;

            using (FileStream outFileStream = new FileStream("C:\\Tests\\test.xlsx", FileMode.Create))
            {
                outStream.CopyTo(outFileStream);
            }
        }

        [TestMethod]
        public void Test_Excel_FileImport()
        {

            using (FileStream fileStream = new FileStream("C:\\Tests\\test.xlsx", FileMode.Open))
            {
                IFileImporter<DataTable> importer = new ExcelDataTableFileImporter();

                var dt = new DataTable();

                importer.Import(dt, fileStream);
            }


        }
    }
}

