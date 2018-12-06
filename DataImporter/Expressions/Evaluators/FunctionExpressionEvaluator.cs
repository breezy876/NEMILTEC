using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NEMILTEC.Interfaces.Service.Data.Expressions;
using NEMILTEC.Service.Data.Expressions.Functions.Aggregate;
using NEMILTEC.Service.Data.Expressions.Functions.Collection;
using NEMILTEC.Service.Data.Expressions.Functions.Math;
using NEMILTEC.Service.Data.Expressions.Functions.String;
using NEMILTEC.Service.Data.Expressions.Abstract;
using NEMILTEC.Shared.Classes;

namespace NEMILTEC.Service.Data.Expressions.Evaluators
{
    public class FunctionExpressionEvaluator
    {
        #region math functions
        public object Evaluate(AbsFunction funcExp, object data, IEnumerable<object> parameters = null)
        {
            var paramValues = _GetParameterValues(funcExp, data, parameters);
            return (object)Math.Abs(Convert.ToDecimal(paramValues[0]));
        }

        public object Evaluate(FloorFunction funcExp, object data, IEnumerable<object> parameters = null)
        {
            var paramValues = _GetParameterValues(funcExp, data, parameters);
            return (object)Math.Floor(Convert.ToDecimal(paramValues[0]));
        }

        public object Evaluate(CeilingFunction funcExp, object data, IEnumerable<object> parameters = null)
        {
            var paramValues = _GetParameterValues(funcExp, data, parameters);
            return (object)Math.Ceiling(Convert.ToDecimal(paramValues[0]));
        }


        public object Evaluate(PowerFunction funcExp, object data, IEnumerable<object> parameters = null)
        {
            var paramValues = _GetParameterValues(funcExp, data, parameters);
            return (object)Math.Pow(Convert.ToDouble(paramValues[0]), Convert.ToDouble(paramValues[1]));
        }
        #endregion

        #region aggregate
        public object Evaluate(CountFunction funcExp, object data, IEnumerable<object> parameters = null)
        {
            var paramValues = _GetParameterValues(funcExp, data, parameters);
            return ((IEnumerable<object>)paramValues[0]).Count();
        }

        public object Evaluate(SumFunction funcExp, object data, IEnumerable<object> parameters = null)
        {
            var paramValues = _GetParameterValues(funcExp, data, parameters);
            return ((IEnumerable<object>)paramValues[0]).Sum(i => (int)i);
        }

        public object Evaluate(AvgFunction funcExp, object data, IEnumerable<object> parameters = null)
        {
            var paramValues = _GetParameterValues(funcExp, data, parameters);
            return ((IEnumerable<object>)paramValues[0]).Average(i => (decimal)i);
        }

        public object Evaluate(MinFunction funcExp, object data, IEnumerable<object> parameters = null)
        {
            var paramValues = _GetParameterValues(funcExp, data, parameters);
            return ((IEnumerable<object>)paramValues[0]).Min(i => (decimal)i);
        }

        public object Evaluate(MaxFunction funcExp, object data, IEnumerable<object> parameters = null)
        {
            var paramValues = _GetParameterValues(funcExp, data, parameters);
            return ((IEnumerable<object>)paramValues[0]).Max(i => (decimal)i);
        }
        #endregion

        #region collection
        public object Evaluate(ContainsFunction funcExp, object data, IEnumerable<object> parameters = null)
        {
            var paramValues = _GetParameterValues(funcExp, data, parameters);
            return ((IEnumerable<object>)paramValues[0]).Contains(paramValues[1]);
        }
        #endregion

        #region string
        public object Evaluate(ConcatFunction funcExp, object data, IEnumerable<object> parameters = null)
        {
            var paramValues = _GetParameterValues(funcExp, data, parameters);
            return (object)paramValues.Aggregate((x,y) => (string)x + y);

        }

        public object Evaluate(LeftFunction funcExp, object data, IEnumerable<object> parameters = null)
        {
            var paramValues = _GetParameterValues(funcExp, data, parameters);
            return (object) ((string)paramValues[0]).Substring(0, (int)paramValues[1]);

        }

        public object Evaluate(LikeFunction funcExp, object data, IEnumerable<object> parameters = null)
        {

            var paramValues = _GetParameterValues(funcExp, data, parameters);
            var regexp = new Regex((string)paramValues[1]);
            return regexp.Matches((string)paramValues[0]);
        }
        #endregion

        #region private shared methods
        private object[] _GetParameterValues(AFunctionExpression funcExp, object data, IEnumerable<object> parameters = null)
        {
            return parameters.IsNullOrEmpty() ? funcExp.Expressions.Select(e => e.Evaluate(data, null)).ToArray() : parameters.ToArray();
        }

        private string _ParametersToSQLCSV(AFunctionExpression funcExp)
        {
            if (funcExp.Expressions.IsNullOrEmpty())
                return null;
            var csv = funcExp.Expressions.Select(e => ((ISQLExpression)e).ToSQL()).ToCSV();
            return csv;
        }

        private IEnumerable<string> _ParametersToSQL(AFunctionExpression funcExp)
        {
            if (funcExp.Expressions.IsNullOrEmpty())
                return null;
            return funcExp.Expressions.Select(e => ((ISQLExpression)e).ToSQL());
        }
        #endregion


        #region ToString

        #region math
        public string ToSQL(AbsFunction funcExp)
        {
            return _FunctionToSQL(funcExp, "ABS");
        }

        public string ToSQL(FloorFunction funcExp)
        {
            return _FunctionToSQL(funcExp, "FLOOR");
        }

        public string ToSQL(CeilingFunction funcExp)
        {
            return _FunctionToSQL(funcExp, "CEILING");
        }

        public string ToSQL(PowerFunction funcExp)
        {
            return _FunctionToSQL(funcExp, "POW");
        }
        #endregion

        #region aggregate
        public string ToSQL(CountFunction funcExp)
        {
            return _FunctionToSQL(funcExp, "COUNT");
        }

        public string ToSQL(AvgFunction funcExp)
        {
            return _FunctionToSQL(funcExp, "AVG");
        }

        public string ToSQL(SumFunction funcExp)
        {
            return _FunctionToSQL(funcExp, "SUM");
        }

        public string ToSQL(MinFunction funcExp)
        {
            return _FunctionToSQL(funcExp, "MIN");
        }

        public string ToSQL(MaxFunction funcExp)
        {
            return _FunctionToSQL(funcExp, "MAX");
        }
        #endregion

        #region collection
        public string ToSQL(ContainsFunction funcExp)
        {
            var paramValues = _ParametersToSQL(funcExp).ToArray();
            return String.Format("{0} IN {1}", paramValues[0], paramValues[1]);
        }
        #endregion

        #region string
        public string ToSQL(LikeFunction funcExp)
        {
            var paramValues = _ParametersToSQL(funcExp).ToArray();
            return String.Format("{0} LIKE {1}", paramValues[0], paramValues[1]);
        }

        public string ToSQL(LeftFunction funcExp)
        {
            return _FunctionToSQL(funcExp, "LEFT");
        }

        public string ToSQL(ConcatFunction funcExp)
        {
            return _FunctionToSQL(funcExp, "CONCAT");
        }

        #endregion

        #endregion

        private string _FunctionToSQL(AFunctionExpression funcExp, string funcName)
        {
            var paramValues = _ParametersToSQLCSV(funcExp);
            return String.Format("{0}({1})", funcName, paramValues);
        }


  
    }
}
