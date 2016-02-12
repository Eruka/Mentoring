using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using CustomSerialization.DB;

namespace CustomSerialization.TestHelpers
{
    class OrderDataContractSurrogate : IDataContractSurrogate
    {
        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public object GetCustomDataToExport(System.Reflection.MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public Type GetDataContractType(Type type)
        {
            if (type.BaseType != null && type.Namespace == "System.Data.Entity.DynamicProxies")
            {
                type = type.BaseType;
            }

            return type;
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            return obj;
        }

        public void GetKnownCustomDataTypes(System.Collections.ObjectModel.Collection<Type> customDataTypes)
        {
            throw new NotImplementedException();
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {

            if ( obj.GetType().Namespace == "System.Data.Entity.DynamicProxies" && obj.GetType().BaseType != null)
            {
                Order orderProxy = obj as Order;
                if (orderProxy != null)
                {
                    Order order = new Order();

                    order.OrderID = orderProxy.OrderID;
                    order.CustomerID = orderProxy.CustomerID;
                    order.EmployeeID = orderProxy.EmployeeID;
                    order.OrderDate = orderProxy.OrderDate;
                    order.Order_Details = orderProxy.Order_Details.Select(o => new Order_Detail()
                    {
                        OrderID = o.OrderID,
                        Discount = o.Discount,
                        Quantity = o.Quantity,
                        UnitPrice =  o.UnitPrice
                    }).ToList();

                    return order;
                };
            }
            return obj;
        }

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException();
        }

        public System.CodeDom.CodeTypeDeclaration ProcessImportedType(System.CodeDom.CodeTypeDeclaration typeDeclaration, System.CodeDom.CodeCompileUnit compileUnit)
        {
            throw new NotImplementedException();
        }
    }
}
