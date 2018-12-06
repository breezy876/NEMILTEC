using System;
using System.Collections.Generic;
using System.Linq;
using NEMILTEC.Domain;
using NEMILTEC.Interfaces.Service.Automation;
using NEMILTEC.Service.Automation.Abstract;
using NEMILTEC.Service.Automation.Enums;
using NEMILTEC.Service.Data;

using NEMILTEC.Service.Data.Expressions;
using NEMILTEC.Shared.Classes;
using NEMILTEC.Service.Shared.Data;
using NEMILTEC.Interfaces.Service.Shared.Data;
using NEMILTEC.Shared.Classes.Serializers;
using ProtoBuf.Data.Light;

namespace NEMILTEC.Service.Automation.Concrete
{
    public static class IntelliFlowFactory
    {

        private static Dictionary<Enums.IntelliFlowItemType, IIntelliFlowItemFactory> _itemFac =
        new Dictionary<Enums.IntelliFlowItemType, IIntelliFlowItemFactory>()
        {
            {Enums.IntelliFlowItemType.Action, new IntelliFlowActionFactory() }
        };


        public static IIntelliFlow Create(Domain.IntelliFlow data, IDataContext context)
        {
            IDataRepository<Domain.IntelliFlowItem> itemRepos = new EFDataRepository<Domain.IntelliFlowItem>(context);

            var items = itemRepos.Query(x => x.IntelliFlowId == (long)data.Id).ToArray();

            var intFlow = new IntelliFlow();

            if (!items.IsNullOrEmpty())
            {
                var itemList = new List<IIntelliFlowItem>();
                foreach (var item in items)
                {
                    var newItem = Create(item, context);
                    itemList.Add(newItem);
                }
                intFlow.Items = new IntelliFlowIterator<IIntelliFlowItem>(itemList.ToArray());
            }

            return intFlow;
        }




        public static IIntelliFlowItem Create(Domain.IntelliFlowItem item, IDataContext context)
        {

            var fac = _itemFac[(Enums.IntelliFlowItemType)item.ItemTypeId];

            var newItem = fac.Create(item.ItemTypeId);

            IDataRepository<Domain.IntelliFlowItem> itemRepos = new EFDataRepository<Domain.IntelliFlowItem>(context);
            IDataRepository<Domain.Expression> exprRepos = new EFDataRepository<Domain.Expression>(context);

            var childItems = itemRepos.Query(x => x.ParentItemId == (long)item.Id).ToArray();

            if (!childItems.IsNullOrEmpty())
            {
                var childItemList = new List<IIntelliFlowItem>();
                foreach (var child in childItems)
                {
                    var newChild = _Create(child, context, true);
                    childItemList.Add(newChild);
                }
                newItem.Children = new IntelliFlowIterator<IIntelliFlowItem>(childItemList.ToArray());
            }

            if (item.ParentItemId.HasValue)
            {
                var parent = itemRepos.Get(item.ParentItemId.Value);
                var newParent = _Create(parent, context, false);
                newItem.Parent = newParent;
            }

            if (!item.Data.IsNullOrEmpty())
            {
                newItem.Input.Data = BinarySerializer.Deserialize((NEMILTEC.Shared.Enums.Data.DataType)item.DataTypeId, item.Data);
            }

            var itemExp = exprRepos.Query(x => x.ParentItemId == (long)item.Id).ToArray();

            if (!itemExp.IsNullOrEmpty())
            {
                var paramList = new List<IntelliFlowItemInputParameter>();
                foreach (var exp in itemExp)
                {
                    var param = new IntelliFlowItemInputParameter();
                    var newExp = ExpressionFactory.Create(exp, context);
                    if (newExp is ValueExpression)
                    {
                        var valExp = newExp as ValueExpression;
                        param.Value = valExp.Value;
                    }
                    param.Expression = newExp;
                    paramList.Add(param);
                }

                newItem.Input.Parameters = paramList;
            }

            return newItem;
        }


        private static IIntelliFlowItem _Create(Domain.IntelliFlowItem item, IDataContext context, bool recurse)
        {

            IDataRepository<Domain.IntelliFlowItem> itemRepos = new EFDataRepository<Domain.IntelliFlowItem>(context);
            IDataRepository<Domain.Expression> exprRepos = new EFDataRepository<Domain.Expression>(context);

            var fac = _itemFac[(Enums.IntelliFlowItemType)item.ItemTypeId];

            var newItem = fac.Create(item.ItemTypeId);

            if (recurse)
            {
                var childItems = itemRepos.Query(x => x.ParentItemId == (long)item.Id).ToArray();

                if (childItems.IsNullOrEmpty())
                {
                    var childItemList = new List<IIntelliFlowItem>();

                    foreach (var child in childItems)
                    {
                        var newChild = _Create(child, context, true);
                        childItemList.Add(newChild);
                    }
                    newItem.Children = new IntelliFlowIterator<IIntelliFlowItem>(childItemList.ToArray());
                }
            }

            if (item.ParentItemId.HasValue)
            {
                var parent = itemRepos.Get(item.ParentItemId.Value);
                var newParent = _Create(parent, context, false);
                newItem.Parent = newParent;
            }

            if (!item.Data.IsNullOrEmpty())
            {
                newItem.Input.Data = BinarySerializer.Deserialize((NEMILTEC.Shared.Enums.Data.DataType)item.DataTypeId, item.Data);
            }

            var itemExp = exprRepos.Query(x => x.ParentItemId == (long)item.Id).ToArray();

            if (!itemExp.IsNullOrEmpty())
            {
                var paramList = new List<IntelliFlowItemInputParameter>();
                foreach (var exp in itemExp)
                {
                    var param = new IntelliFlowItemInputParameter();
                    var newExp = ExpressionFactory.Create(exp, context);
                    if (newExp is ValueExpression)
                    {
                        var valExp = newExp as ValueExpression;
                        param.Value = valExp.Value;
                    }
                    param.Expression = newExp;
                    paramList.Add(param);
                }

                newItem.Input.Parameters = paramList;
            }

            return newItem;
        }
    }
}
