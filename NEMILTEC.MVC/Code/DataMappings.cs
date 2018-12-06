using System.Collections.Generic;
using NEMILTEC.Domain;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.Interfaces.Service.Shared.Data;
using System;
using NEMILTEC.Service.Shared.Data;
using NEMILTEC.MVC.Models.Query;
using NEMILTEC.MVC.Models;
using System.Linq.Expressions;
using System.Linq;
using System.Collections;
using NEMILTEC.Interfaces.Service.Domain;
using NEMILTEC.MVC.Models.Container;
using NEMILTEC.MVC.Models.Container.Query;
using NEMILTEC.MVC.Models.Container.Report;
using NEMILTEC.MVC.Models.Report;

namespace NEMILTEC.MVC.Code
{
    public static class DataMappings
    {
        static DataMappings()
        {

            DataSourceMappings = new Dictionary<ModelType, IDataRepository<IDataEntity>>()
            {
                {ModelType.Project, new EFDataRepository<Domain.Project>() },
                {ModelType.Category, new EFDataRepository<Domain.Category>() },

                {ModelType.Document, new EFDataRepository<Domain.Document>() },
                {ModelType.FileType, new EFDataRepository<Domain.FileType>() },

                {ModelType.Query, new EFDataRepository<Domain.Query>() },
                {ModelType.QueryProjection, new EFDataRepository<Domain.QueryProjection>() },
                {ModelType.QueryJoin, new EFDataRepository<Domain.QueryJoin>() },

                {ModelType.Report, new EFDataRepository<Domain.Report>() },
                {ModelType.ReportElement, new EFDataRepository<Domain.ReportElement>() },
                {ModelType.ReportElementType, new EFDataRepository<Domain.ReportElementType>() },
                {ModelType.ReportElementParameter, new EFDataRepository<Domain.ReportElementParameter>() },
                {ModelType.ReportOutputType,  new EFDataRepository<Domain.ReportOutputType>()},
            };

            TypeMappings = new Dictionary<ModelType, Tuple<Type, Type>>()
            {
                {ModelType.Project, new Tuple<Type, Type>(typeof(Project), typeof(ProjectModel))},
                {ModelType.Category, new Tuple<Type, Type>(typeof(Category), typeof(CategoryModel))},

                {ModelType.Document, new Tuple<Type, Type>(typeof(Document), typeof(DocumentModel)) },
                {ModelType.FileType, new Tuple<Type, Type>(typeof(FileType), typeof(FileTypeModel))  },

                {ModelType.Query, new Tuple<Type, Type>(typeof(Query), typeof(QueryModel))},
                {ModelType.QueryProjection, new Tuple<Type, Type>(typeof(QueryProjection), typeof(QueryProjectionModel))},
                {ModelType.QueryJoin, new Tuple<Type, Type>(typeof(QueryJoin), typeof(QueryJoinModel))},

                {ModelType.Report, new Tuple<Type, Type>(typeof(Report), typeof(ReportModel))},
                {ModelType.ReportElement, new Tuple<Type, Type>(typeof(ReportElement), typeof(ReportElementModel)) },
                {ModelType.ReportElementType, new Tuple<Type, Type>(typeof(ReportElementType), typeof(ReportElementTypeModel)) },
                {ModelType.ReportElementParameter, new Tuple<Type, Type>(typeof(ReportElementParameter), typeof(ReportElementParameterModel)) },
                {ModelType.ReportOutputType,  new Tuple<Type, Type>(typeof(ReportOutputType), typeof(ReportOutputTypeModel))},
            };

            ChildDataSourceMappings = new Dictionary<ModelType, Dictionary<ModelType, IDataRepository<IDataEntity>>>()
            {
                {ModelType.Query, new Dictionary<ModelType, IDataRepository<IDataEntity>>()
                    {
                        {ModelType.QueryProjection, new EFDataRepository<Domain.QueryProjection>()},
                        {ModelType.QueryJoin, new EFDataRepository<Domain.QueryJoin>()}
                    }
                },

                {ModelType.Report, new Dictionary<ModelType, IDataRepository<IDataEntity>>()
                    {
                        {ModelType.ReportElement, new EFDataRepository<Domain.ReportElement>()},
                    }
                },

                {ModelType.ReportElement, new Dictionary<ModelType, IDataRepository<IDataEntity>>()
                    {
                        {ModelType.ReportElementParameter, new EFDataRepository<Domain.ReportElementParameter>()},
                    }
                }
            };

            ChildSelectorMappings = new Dictionary<ModelType, Expression<Func<IDataEntity, object>>>()
            {
                { ModelType.Query, (child) => ((IQueryChild)child).QueryId },

                { ModelType.Report, (child) => ((IReportChild)child).ReportId },

                { ModelType.ReportElement, (child) => ((IReportElementChild)child).ReportElementId }
            };


            NewChildActionMappings = new Dictionary<ModelType, Action<IModel, long>>()
            {
                {ModelType.Query, (newChild, parentId) => {  } },
                { ModelType.QueryProjection, (newChild, parentId) => { ((IQueryChild)newChild).QueryId = parentId; } },
                { ModelType.QueryJoin, (newChild, parentId) => { ((IQueryChild) newChild).QueryId = parentId; } },

                { ModelType.Report, (newChild, parentId) => {  } },
                { ModelType.ReportElement, (newChild, parentId) => { ((IReportChild) newChild).ReportId = parentId; } },
                { ModelType.ReportElementParameter, (newChild, parentId) => { ((IReportElementChild) newChild).ReportElementId = parentId; } }
            };

            ParentKeyMappings = new Dictionary<ModelType, System.Linq.Expressions.Expression<Func<IDataEntity, long>>>()
            {
                {ModelType.QueryProjection, p => (p as IQueryChild).QueryId  },
                {ModelType.QueryJoin, p => (p as IQueryChild).QueryId  },

                {ModelType.ReportElement, p => (p as IReportChild).ReportId  },
                {ModelType.ReportElementParameter, p => (p as IReportElementChild).ReportElementId  },
            };


            ContainerMappings = new Dictionary<ModelType, Type>()
            {
                {ModelType.Query, typeof(QueryContainerModel)},

                {ModelType.Report, typeof(ReportContainerModel)},
                {ModelType.ReportElement, typeof(ReportElementContainerModel)},
                {ModelType.ReportElementParameter, typeof(ReportElementParameterContainerModel)},
            };

            ModelMappings = new Dictionary<ModelType, Type>()
            {
                {ModelType.Query,typeof(QueryModel)},
                {ModelType.QueryProjection, typeof(QueryProjectionModel)},
                {ModelType.QueryJoin, typeof(QueryJoinModel)},

                {ModelType.Report, typeof(ReportModel)},
                {ModelType.ReportElement, typeof(ReportElementModel)},
                {ModelType.ReportElementParameter, typeof(ReportElementParameterModel)},
            };

            UpdateModelDataActionMappings = new Dictionary<ModelType, Action<IDataEntity, byte[]>>()
            {
                       {ModelType.ReportElement, (model, data) => { ((Domain.ReportElement) model).TemplateInfo = data; } }
            };
        }

        public static Dictionary<ModelType, IDataRepository<IDataEntity>> DataSourceMappings = new Dictionary<ModelType, IDataRepository<IDataEntity>>();
        public static Dictionary<ModelType, Dictionary<ModelType, IDataRepository<IDataEntity>>> ChildDataSourceMappings = new Dictionary<ModelType, Dictionary<ModelType, IDataRepository<IDataEntity>>>();
        public static Dictionary<ModelType, Tuple<Type, Type>> TypeMappings = new Dictionary<ModelType, Tuple<Type, Type>>();
        public static Dictionary<ModelType, Expression<Func<IDataEntity, object>>> ChildSelectorMappings = new Dictionary<ModelType, Expression<Func<IDataEntity, object>>>();
        public static Dictionary<ModelType, IList> NavigationPropertyMappings = new Dictionary<ModelType, IList>();
        public static Dictionary<ModelType, Action<IModel, long>> NewChildActionMappings = new Dictionary<ModelType, Action<IModel, long>>();
        public static Dictionary<ModelType, System.Linq.Expressions.Expression<Func<IDataEntity, long>>> ParentKeyMappings = new Dictionary<ModelType, Expression<Func<IDataEntity, long>>>();
        public static Dictionary<ModelType, Type> ModelMappings = new Dictionary<ModelType, Type>();
        public static Dictionary<ModelType, Type> ContainerMappings = new Dictionary<ModelType, Type>();
        public static Dictionary<ModelType, Action<IDataEntity, byte[]>> UpdateModelDataActionMappings;

    }

}
