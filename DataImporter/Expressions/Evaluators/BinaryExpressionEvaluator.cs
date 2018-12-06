using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NEMILTEC.Service.Data.Expressions.Abstract;
using NEMILTEC.Service.Data.Expressions.Binary.Boolean;
using NEMILTEC.Service.Data.Expressions.Binary.Math;
using NEMILTEC.Shared.Classes;

namespace NEMILTEC.Service.Data.Expressions.Evaluators
{
    public class BinaryExpressionEvaluator 
    {

        private object[] _GetParameterValues(
            ABinaryExpression binaryExp, 
            object data,
            IEnumerable<object> parameters = null)
        {
            return parameters.IsNullOrEmpty() ? new[] {binaryExp.LeftExpression.Evaluate(data, null), binaryExp.RightExpression.Evaluate(data, null)} : parameters.ToArray();
        }

        #region evaluate methods

        #region boolean
        public bool Evaluate(BinaryOrExpression binaryExp, object data, IEnumerable<object> parameters = null)
        {
            var paramArray = _GetParameterValues(binaryExp, data, parameters);

            var left = Convert.ToBoolean(paramArray[0]);
            var right = Convert.ToBoolean(paramArray[1]);

            return left || right;
        }


        public bool Evaluate(BinaryAndExpression binaryExp, object data, IEnumerable<object> parameters = null)
        {
            var paramArray = _GetParameterValues(binaryExp, data, parameters);

            var left = Convert.ToBoolean(paramArray[0]);
            var right = Convert.ToBoolean(paramArray[1]);

            return left && right;
        }


        public bool Evaluate(BinaryEqualsExpression binaryExp, object data, IEnumerable<object> parameters = null)
        {

            var paramArray = _GetParameterValues(binaryExp, data, parameters);

            var left = Convert.ToBoolean(paramArray[0]);
            var right = Convert.ToBoolean(paramArray[1]);

            return _Compare(left, right) == 0;

        }

        public bool Evaluate(BinaryNotEqualsExpression binaryExp, object data, IEnumerable<object> parameters = null)
        {

            var paramArray = _GetParameterValues(binaryExp, data, parameters);

            var left = Convert.ToBoolean(paramArray[0]);
            var right = Convert.ToBoolean(paramArray[1]);

            return _Compare(left, right) != 0;

        }

        public bool Evaluate(BinaryGreaterThanExpression binaryExp, object data, IEnumerable<object> parameters = null)
        {

            var paramArray = _GetParameterValues(binaryExp, data, parameters);

            var left = Convert.ToBoolean(paramArray[0]);
            var right = Convert.ToBoolean(paramArray[1]);

            return _Compare(left, right) > 0;

        }

        public bool Evaluate(BinaryLessThanExpression binaryExp, object data, IEnumerable<object> parameters = null)
        {

            var paramArray = _GetParameterValues(binaryExp, data, parameters);

            var left = Convert.ToBoolean(paramArray[0]);
            var right = Convert.ToBoolean(paramArray[1]);

            return _Compare(left, right) < 0;

        }

        public bool Evaluate(BinaryGreaterThanOrEqualsExpression binaryExp, object data, IEnumerable<object> parameters = null)
        {

            var paramArray = _GetParameterValues(binaryExp, data, parameters);

            var left = Convert.ToBoolean(paramArray[0]);
            var right = Convert.ToBoolean(paramArray[1]);

            return _Compare(left, right) >= 0;

        }

        public bool Evaluate(BinaryLessThanOrEqualsExpression binaryExp, object data, IEnumerable<object> parameters = null)
        {

            var paramArray = _GetParameterValues(binaryExp, data, parameters);

            var left = Convert.ToBoolean(paramArray[0]);
            var right = Convert.ToBoolean(paramArray[1]);

            return _Compare(left, right) <= 0;

        }

        #endregion

        #region math
        public decimal Evaluate(BinaryAddExpression binaryExp, object data, IEnumerable<object> parameters = null)
        {

            var paramArray = _GetParameterValues(binaryExp, data, parameters);

            var left = paramArray[0];
            var right = paramArray[1];

            return Convert.ToDecimal(left) + Convert.ToDecimal(right);

        }

        public decimal Evaluate(BinarySubtractExpression binaryExp, object data, IEnumerable<object> parameters = null)
        {

            var paramArray = _GetParameterValues(binaryExp, data, parameters);

            var left = paramArray[0];
            var right = paramArray[1];

            return Convert.ToDecimal(left) - Convert.ToDecimal(right);

        }

        public decimal Evaluate(BinaryDivideExpression binaryExp, object data, IEnumerable<object> parameters = null)
        {
            var paramArray = _GetParameterValues(binaryExp, data, parameters);

            var left = paramArray[0];
            var right = paramArray[1];

            return Convert.ToDecimal(left) / Convert.ToDecimal(right);

        }

        public decimal Evaluate(BinaryMultiplyExpression binaryExp, object data, IEnumerable<object> parameters = null)
        {

            var paramArray = _GetParameterValues(binaryExp, data, parameters);

            var left = paramArray[0];
            var right = paramArray[1];

            return Convert.ToDecimal(left) * Convert.ToDecimal(right);

        }
        #endregion

        #endregion

        #region tostring methods

        #region boolean
        public string ToSQL(BinaryOrExpression exp)
        {
            return String.Format("{0} OR {1}", exp.LeftExpression.ToSQL(), exp.RightExpression.ToSQL());
        }

        public string ToSQL(BinaryAndExpression exp)
        {
            return String.Format("{0} AND {1}", exp.LeftExpression.ToSQL(), exp.RightExpression.ToSQL());
        }

        public string ToSQL(BinaryLessThanExpression exp)
        {
            return String.Format("{0} < {1}", exp.LeftExpression.ToSQL(), exp.RightExpression.ToSQL());
        }

        public string ToSQL(BinaryGreaterThanExpression exp)
        {
            return String.Format("{0} > {1}", exp.LeftExpression.ToSQL(), exp.RightExpression.ToSQL());
        }

        public string ToSQL(BinaryLessThanOrEqualsExpression exp)
        {
            return String.Format("{0} <= {1}", exp.LeftExpression.ToSQL(), exp.RightExpression.ToSQL());
        }

        public string ToSQL(BinaryGreaterThanOrEqualsExpression exp)
        {
            return String.Format("{0} >= {1}", exp.LeftExpression.ToSQL(), exp.RightExpression.ToSQL());
        }

        public string ToSQL(BinaryEqualsExpression exp)
        {
            return String.Format("{0} EQUALS {1}", exp.LeftExpression.ToSQL(), exp.RightExpression.ToSQL());
        }

        public string ToSQL(BinaryNotEqualsExpression exp)
        {
            return String.Format("{0} NOT EQUALS {1}", exp.LeftExpression.ToSQL(), exp.RightExpression.ToSQL());
        }
        #endregion

        #region math

        public string ToSQL(BinaryAddExpression exp)
        {
            return String.Format("{0} + {1}", exp.LeftExpression.ToSQL(), exp.RightExpression.ToSQL());
        }

        public string ToSQL(BinarySubtractExpression exp)
        {
            return String.Format("{0} - {1}", exp.LeftExpression.ToSQL(), exp.RightExpression.ToSQL());
        }

        public string ToSQL(BinaryDivideExpression exp)
        {
            return String.Format("{0} / {1}", exp.LeftExpression.ToSQL(), exp.RightExpression.ToSQL());
        }

        public string ToSQL(BinaryMultiplyExpression exp)
        {
            return String.Format("{0} * {1}", exp.LeftExpression.ToSQL(), exp.RightExpression.ToSQL());
        }
        #endregion

        #endregion

        #region private shared methods

        private int _Compare(object left, object right)
        {
            return Comparer.Default.Compare(left,
                        Convert.ChangeType(right, left.GetType()));
        }

        #endregion

   
}
}
