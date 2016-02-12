using System.Runtime.Serialization;
using CustomSerialization.DB;

namespace CustomSerialization.TestHelpers
{
    public class OrderDetailSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var od = (Order_Detail)obj;
            info.AddValue("OrderID", od.OrderID);
            info.AddValue("UnitPrice", od.UnitPrice);
            info.AddValue("Quantity", od.Quantity);
            info.AddValue("Discount", od.Discount);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var od = (Order_Detail)obj;
            od.OrderID = info.GetInt32("OrderID");
            od.UnitPrice = info.GetDecimal("UnitPrice");
            od.Quantity = info.GetInt16("Quantity");
            od.Discount = info.GetSingle("Discount") / 2;
            return od;
        }
    }
}
