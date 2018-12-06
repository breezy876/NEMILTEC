using NEMILTEC.Interfaces.Service.Shared.Data;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.MVC.Models;
using NEMILTEC.MVC.Models.Container;
using NEMILTEC.Service.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Interfaces.Service.Domain;
using NEMILTEC.MVC.Models.Container.Query;
using NEMILTEC.MVC.Models.Container.Report;
using NEMILTEC.MVC.Models.Query;
using NEMILTEC.MVC.Models.Report;

namespace NEMILTEC.MVC.Code
{
    public static class ViewModelBuilderMappings
    {
        static ViewModelBuilderMappings()
        {
            ContainerMappings = new Dictionary<ModelType, IContainerModel>()
            {
                {ModelType.Query, new QueryContainerModel()},
                {ModelType.QueryProjection, new QueryProjectionContainerModel()},
                {ModelType.QueryJoin, new QueryJoinContainerModel()},

                {ModelType.Report, new ReportContainerModel()},
                {ModelType.ReportElement, new ReportElementContainerModel()},
                {ModelType.ReportElementType, new ReportElementTypeContainerModel()},
                {ModelType.ReportElementParameter, new ReportElementParameterContainerModel()},
                {ModelType.ReportOutputType, new ReportOutputTypeContainerModel()},

            };

            ModelMappings = new Dictionary<ModelType, IModel>()
            {
                {ModelType.Query, new QueryModel()},
                {ModelType.QueryProjection, new QueryProjectionModel()},
                {ModelType.QueryJoin, new QueryJoinModel()},

                {ModelType.Report, new ReportModel()},
                {ModelType.ReportElement, new ReportElementModel()},
                {ModelType.ReportElementType, new ReportElementTypeModel()},
                {ModelType.ReportElementParameter, new ReportElementParameterModel()},
                {ModelType.ReportOutputType, new ReportOutputTypeModel()},
            };


        }


        public static Dictionary<ModelType, IContainerModel> ContainerMappings { get; set; }

        public static Dictionary<ModelType, Dictionary<string, IDataRepository<IDataEntity>>> PropertyDataSourceMappings { get; set; }

        public static Dictionary<ModelType, Dictionary<string, string>> PropertyNameMappings { get; set; }

        public static Dictionary<ModelType, Dictionary<string, List<NameValueModel>>> PropertyValueMappings { get; set; }

        public static Dictionary<ModelType, IModel> ModelMappings { get; set; }
    }
}
