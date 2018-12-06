using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Interfaces.Service.Domain;

namespace NEMILTEC.Interfaces.Service.Shared.Data
{
    public interface IDataContext
    {
        IDbSet<NEMILTEC.Domain.Category> Categories { get; set; }
        IDbSet<NEMILTEC.Domain.Project> Projects { get; set; }
        IDbSet<NEMILTEC.Domain.Query> Queries { get; set; }
        IDbSet<NEMILTEC.Domain.QueryProjection> QueryProjections { get; set; }
        IDbSet<NEMILTEC.Domain.QueryJoin> QueryJoins { get; set; }
        IDbSet<NEMILTEC.Domain.QueryJoinType> QueryJoinTypes { get; set; }

        IDbSet<NEMILTEC.Domain.Report> Reports { get; set; }
        IDbSet<NEMILTEC.Domain.ReportOutput> ReportOutputs { get; set; }
        IDbSet<NEMILTEC.Domain.ReportOutputType> ReportOutputTypes { get; set; }
        IDbSet<NEMILTEC.Domain.ReportElement> ReportElements { get; set; }
        IDbSet<NEMILTEC.Domain.ReportElementType> ReportElementTypes { get; set; }
        IDbSet<NEMILTEC.Domain.ReportElementParameter> ReportElementParameters { get; set; }

        IDbSet<NEMILTEC.Domain.FileType> FileTypes { get; set; }
        IDbSet<NEMILTEC.Domain.Document> Documents { get; set; }

        IDbSet<T> GetSet<T>() where T : class, IDataEntity;
        DbSet Set(Type type);

        int SaveChanges();
    }
}
