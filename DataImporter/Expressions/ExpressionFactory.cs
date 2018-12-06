using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Interfaces.Service.Data.Expressions;
using NEMILTEC.Interfaces.Service.Shared.Data;
using NEMILTEC.Service.Shared.Data;
using NEMILTEC.Shared.Classes;
using NEMILTEC.Shared.Classes.Serializers;

namespace NEMILTEC.Service.Data.Expressions
{
    public static class ExpressionFactory
    {
        private static Dictionary<ExpressionType, IDataExpression> _exp =
        new Dictionary<ExpressionType, IDataExpression>()
        {
                    {ExpressionType.ValueExpression, new ValueExpression() }
        };

        public static IDataExpression Create(ExpressionType type)
        {
            return _exp[type];
        }


        private static IDataExpression _Create(Domain.Expression exp, IDataContext context)
        {

            IDataRepository<Domain.Expression> exprRepos = new EFDataRepository<Domain.Expression>(context);

            var newExp = ExpressionFactory.Create((ExpressionType)exp.ExpressionTypeId);

            IDataExpression outExp = newExp;

            if (outExp is ICompositeExpression)
            {
                var compExp = newExp as ICompositeExpression;

                var childExp = exprRepos.Query(x => x.ParentExpressionId == (long)exp.Id).ToArray();

                if (!childExp.IsNullOrEmpty())
                {
                    var childExpList = new List<IDataExpression>();

                    foreach (var child in childExp)
                    {
                        var newChild = _Create(child, context);
                        childExpList.Add(newChild);
                    }
                    compExp.Expressions = childExpList;
                }

                outExp = compExp;
            }

            if (outExp is ValueExpression)
            {
                var valExp = outExp as ValueExpression;
                valExp.Value = BinarySerializer.Deserialize((NEMILTEC.Shared.Enums.Data.DataType)exp.DataTypeId, exp.Value);
                outExp = valExp;
            }

            return outExp;

        }


        public static IDataExpression Create(Domain.Expression exp, IDataContext context)
        {

            IDataRepository<Domain.Expression> exprRepos = new EFDataRepository<Domain.Expression>(context);

            var newExp = ExpressionFactory.Create((ExpressionType)exp.ExpressionTypeId);

            IDataExpression outExp = newExp;

            if (outExp is ICompositeExpression)
            {
                var compExp = newExp as ICompositeExpression;

                var childExp = exprRepos.Query(x => x.ParentExpressionId == (long)exp.Id).ToArray();

                if (!childExp.IsNullOrEmpty())
                {
                    var childExpList = new List<IDataExpression>();

                    foreach (var child in childExp)
                    {
                        var newChild = _Create(child, context);
                        childExpList.Add(newChild);
                    }
                    compExp.Expressions = childExpList;
                }

                outExp = compExp;
            }

            if (outExp is ValueExpression)
            {
                var valExp = outExp as ValueExpression;
                valExp.Value = BinarySerializer.Deserialize((NEMILTEC.Shared.Enums.Data.DataType)exp.DataTypeId, exp.Value);
                outExp = valExp;
            }

            return outExp;

        }
    }
}
