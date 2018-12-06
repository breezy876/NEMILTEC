using System.Collections.Generic;
using System.Linq;
using NEMILTEC.Interfaces.Service.Data.Expressions;
using NEMILTEC.Interfaces.Service.Data.Objects;
using NEMILTEC.Shared.Classes;

namespace NEMILTEC.Service.Data.Expressions.Abstract
{
    public abstract class ABinaryExpression : ACompositeExpression, ISQLObject
    {

        private ISQLExpression[] _expressions;

        protected ABinaryExpression()
        {
            _expressions = Expressions.Select(x => x as ISQLExpression).ToArray();
        }

        public ISQLExpression LeftExpression
        {
            get
            {
                if (!_expressions.IsNullOrEmpty())
                {
                    return _expressions[0];
                }
                return null;
            }

            set
            {
                if (!_expressions.IsNullOrEmpty())
                    _expressions[0] = value;
            }
        }

        public ISQLExpression RightExpression
        {
            get
            {
                if (!_expressions.IsNullOrEmpty())
                {
                    return _expressions[1];
                }
                return null;

            }
            set
            {
                if (!_expressions.IsNullOrEmpty())
                    _expressions[1] = value;
            }
        }

        public abstract string ToSQL();
    }
}
