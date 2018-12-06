using NEMILTEC.Domain;
using NEMILTEC.MVC.Code.Enums;
using NEMILTEC.Shared.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.MVC.Models.IntelliFlows;
using NEMILTEC.Shared.Classes.Serializers;

namespace NEMILTEC.MVC.Models
{
    [Serializable]
    public class ExpressionModel : Model
    {
        public ExpressionModel()
        {
            Name = "Expression";

            Type = ModelType.Expression;

            Properties = Properties.Append((new Dictionary<string, ModelProperty>()
            {
                {"CategoryId", new ModelProperty() {IsVisible = false}},
                {"ProjectId", new ModelProperty() {IsVisible = false}},
                {"DataTypeId", new ModelProperty() {IsVisible = false}},
                {"IntelliFlowId", new ModelProperty() {IsVisible = false}},
                {"ParentId", new ModelProperty() {IsVisible = false}},
                {"ParentItemId", new ModelProperty() {IsVisible = false}},
                {"Value", new ModelProperty() {IsVisible = false}},
            }).ToArray());
        }

        public string Description { get; set; }

        public byte[] Value { get; set; }



        public long? CategoryId { get; set; }

        public CategoryModel Category { get; set; }


        public long? DataTypeId { get; set; }

        public DataTypeModel DataType { get; set; }


        public long ExpressionTypeId { get; set; }

        public ExpressionTypeModel ExpressionType { get; set; }


        public long? IntelliFlowId { get; set; }

        public IntelliFlowModel IntelliFlow { get; set; }


        public long? ParentExpressionId { get; set; }

        public ExpressionModel ParentExpression { get; set; }


        public long? ParentItemId { get; set; }

        public IntelliFlowItemModel ParentItem { get; set; }


        public long? ProjectId { get; set; }

        public ProjectModel Project { get; set; }

        public override IModel Copy()
        {

            var copy = BinarySerializer.Serialize(this);
            return (IModel)BinarySerializer.Deserialize<ExpressionModel>(copy);
        }

    }
}
