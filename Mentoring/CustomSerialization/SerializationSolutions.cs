using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using CustomSerialization.DB;
using CustomSerialization.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomSerialization
{
    [TestClass]
    public class SerializationSolutions
    {
        Northwind dbContext;

        [TestInitialize]
        public void Initialize()
        {
            dbContext = new Northwind();
        }

        [TestMethod]
        public void SerializationCallbacks()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;

            var tester = new XmlDataContractSerializerTester<Category>(new NetDataContractSerializer(), true);
            var categories = dbContext.Categories.Include("Products").ToList();

            var c = categories.First();

            c.Print();

            var dc = tester.SerializeAndDeserialize(c);

            dc.Print();
        }

        [TestMethod]
        public void ISerializable()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;

            var tester = new XmlDataContractSerializerTester<Product>(new NetDataContractSerializer(), true);
            var products = dbContext.Products.Include("Order_Details").ToList();

            var p = products.First();

            p.Print();

            var dp = tester.SerializeAndDeserialize(p);

            dp.Print();
        }


        [TestMethod]
        public void ISerializationSurrogate()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;

            var selector = new SurrogateSelector();
            selector.AddSurrogate(
                typeof(Order_Detail),
                new StreamingContext(StreamingContextStates.Persistence, null),
                new OrderDetailSerializationSurrogate());

            var tester = new SoapFormatterTester<Order_Detail>(
                new SoapFormatter(selector, new StreamingContext()), true);

            var orderDetails = dbContext.Order_Details.ToList();

            var o = orderDetails.First(v => v.Discount > 0);

            o.Print();

            var od = tester.SerializeAndDeserialize(o);

            od.Print();
        }

        [TestMethod]
        public void IDataContractSurrogate()
        {
            dbContext.Configuration.ProxyCreationEnabled = true;
            dbContext.Configuration.LazyLoadingEnabled = true;

            //var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(new DataContractSerializer(typeof(IEnumerable<Order>)), true);
            var tester = new XmlDataContractSerializerTester<Order>(
               new DataContractSerializer(
                   typeof(Order),
                   new DataContractSerializerSettings
                   {
                       DataContractSurrogate = new OrderDataContractSurrogate()
                   }), true);

            var orders = dbContext.Orders.ToList();

            var order = orders.First();

            var dorder = tester.SerializeAndDeserialize(order);

        }
    }
}
