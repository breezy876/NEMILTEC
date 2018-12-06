using System.Collections.Generic;
using NEMILTEC.Interfaces.Service.Data.Expressions;
using NEMILTEC.Shared.Classes;

namespace NEMILTEC.Service.Data.Expressions.Abstract
{
    /// <summary>
    /// a data expression 
    /// for evaluating/parsing data expressions e.g rules in IntelliFlows
    /// 
    /// author: chris brown
    /// date created: 01/07/2015
    /// </summary>
    /// 


    public abstract class ACompositeExpression : ADataExpression, ICompositeExpression
    {

        public virtual IEnumerable<IDataExpression> Expressions { get; set; }

        protected ACompositeExpression()
        {
            Expressions = new List<IDataExpression>();
        }

        private void _RecurseExpression(IDataExpression exp, IDictionary<string, object> nameValuePairs, ref int totalUpdated)
        {
            if (totalUpdated < nameValuePairs.Count)
            {
                if (exp is ValueExpression)
                {
                    var valueExp = exp as ValueExpression;
                    if (!valueExp.Name.IsNullOrEmpty() && nameValuePairs.ContainsKey(valueExp.Name))
                    {
                        valueExp.Value = nameValuePairs[valueExp.Name];
                        totalUpdated++;
                    }
                }
                if (exp is ICompositeExpression)
                {
                    var compoundExp = exp as ICompositeExpression;
                    if (!compoundExp.Expressions.IsNullOrEmpty())
                    {
                        foreach (var childExp in compoundExp.Expressions)
                        {
                            _RecurseExpression(childExp, nameValuePairs, ref totalUpdated);
                        }
                    }
                }
            }
        }

        public int UpdateValues(IDictionary<string, object> nameValuePairs)
        {
            int totalUpdated = 0;
            try
            {

                if (!Expressions.IsNullOrEmpty())
                {
                    foreach (var childExp in Expressions)
                    {
                        _RecurseExpression(childExp, nameValuePairs, ref totalUpdated);
                    }
                }
            }
            catch { return 0; }
            return totalUpdated;
        }


    }
}
