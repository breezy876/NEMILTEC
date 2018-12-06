using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Domain;
using NEMILTEC.MVC.Models;
using NEMILTEC.MVC.Models.Query;
using NEMILTEC.MVC.Models.Report;

namespace NEMILTEC.MVC.Code
{
    public static class Mapper
    {

        public static void Initialize()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                #region model to vm mappings
                cfg.CreateMap<FileType, FileTypeModel>().MaxDepth(1);
                cfg.CreateMap<Document, DocumentModel>().MaxDepth(1);

                //cfg.CreateMap<Expression, ExpressionModel>();

                cfg.CreateMap<Category, CategoryModel>();

                cfg.CreateMap<Project, ProjectModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

                #region query
                cfg.CreateMap<QueryProjection, QueryProjectionModel>();

                cfg.CreateMap<QueryJoinType, QueryJoinTypeModel>();

                cfg.CreateMap<QueryJoin, QueryJoinModel>();


                cfg.CreateMap<Query, QueryModel>()
                    .ForMember(dest => dest.Projections, opt => opt.MapFrom(src => src.QueryProjections))
                    .ForMember(dest => dest.Joins, opt => opt.MapFrom(src => src.QueryJoins))
                    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                    .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project));
                #endregion

                #region report
                cfg.CreateMap<ReportOutputType, ReportOutputTypeModel>();

                cfg.CreateMap<ReportElementType, ReportElementTypeModel>();

                cfg.CreateMap<ReportElement, ReportElementModel>().MaxDepth(1)
                    .ForMember(dest => dest.Query, opt => opt.MapFrom(src => src.Query))
                    .ForMember(dest => dest.ReportElementType, opt => opt.MapFrom(src => src.ReportElementType));

                cfg.CreateMap<Report, ReportModel>().MaxDepth(1)
                    .ForMember(dest => dest.Elements, opt => opt.MapFrom(src => src.Elements))
                    .ForMember(dest => dest.TemplateFile, opt => opt.MapFrom(src => src.TemplateFile))
                    .ForMember(dest => dest.OutputType, opt => opt.MapFrom(src => src.OutputType));
                #endregion

                #endregion


                #region vm to model mappings
                cfg.CreateMap<FileTypeModel, FileType>().MaxDepth(1);

                cfg.CreateMap<DocumentModel, Document>().MaxDepth(1);

                cfg.CreateMap<CategoryModel, Category>();

                cfg.CreateMap<ProjectModel, Project>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

                #region query
                cfg.CreateMap<QueryProjectionModel, QueryProjection>();

                cfg.CreateMap<QueryJoinTypeModel, QueryJoinType>();

                cfg.CreateMap<QueryJoinModel, QueryJoin>();

                cfg.CreateMap<QueryModel, Query>()
                    .ForMember(dest => dest.QueryProjections, opt => opt.MapFrom(src => src.Projections))
                    .ForMember(dest => dest.QueryJoins, opt => opt.MapFrom(src => src.Joins))
                    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                    .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project));
                #endregion

                #region report
                cfg.CreateMap<ReportOutputTypeModel, ReportOutputType>();

                cfg.CreateMap<ReportElementTypeModel, ReportElementType>();

                cfg.CreateMap<ReportElementModel, ReportElement>().MaxDepth(1)
                    .ForMember(dest => dest.Query, opt => opt.MapFrom(src => src.Query))
                    .ForMember(dest => dest.ReportElementType, opt => opt.MapFrom(src => src.ReportElementType));

                cfg.CreateMap<ReportModel, Report>().MaxDepth(1)
                    .ForMember(dest => dest.Elements, opt => opt.MapFrom(src => src.Elements))
                    .ForMember(dest => dest.TemplateFile, opt => opt.MapFrom(src => src.TemplateFile))
                    .ForMember(dest => dest.OutputType, opt => opt.MapFrom(src => src.OutputType));
                #endregion

                #endregion

            });
        }

        public static IEnumerable<M> CreateModels<VM, M>(IEnumerable<VM> vmCol)
        {
            return vmCol.Select<VM, M>(vm => CreateModel<VM, M>(vm));
        }

        public static IEnumerable<VM> CreateViewModels<M, VM>(IEnumerable<M> mCol)
        {
            return mCol.Select<M, VM>(m => CreateViewModel<M, VM>(m));
        }

        public static M CreateModel<VM, M>(VM vm)
        {

           var model = AutoMapper.Mapper.Map<VM,M>(vm);

            return model;
        }

        public static VM CreateViewModel<M, VM>(M model)
        {

            var vm = AutoMapper.Mapper.Map<M, VM>(model);

            return vm;
        }


    }
}
