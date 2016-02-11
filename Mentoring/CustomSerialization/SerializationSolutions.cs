using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(new NetDataContractSerializer(), true);
            var orderDetails = dbContext.Order_Details.ToList();

            tester.SerializeAndDeserialize(orderDetails);
        }

        [TestMethod]
        public void IDataContractSurrogate()
        {
            dbContext.Configuration.ProxyCreationEnabled = true;
            dbContext.Configuration.LazyLoadingEnabled = true;

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(new DataContractSerializer(typeof(IEnumerable<Order>)), true);
            var orders = dbContext.Orders.ToList();

            tester.SerializeAndDeserialize(orders);
        }
    }
}
