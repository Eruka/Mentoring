using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace CustomSerialization.DB
{
    [Serializable]
    public class Product : ISerializable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Order_Details = new HashSet<Order_Detail>();
        }

        private Product(SerializationInfo information, StreamingContext context)
        {
            ProductID = information.GetInt32("ProductID");
            ProductName = information.GetString("ProductName");
            QuantityPerUnit = information.GetString("QuantityPerUnit");
            UnitPrice = information.GetDecimal("UnitPrice");
            Order_Details = (HashSet<Order_Detail>)information.GetValue("Order_Details", typeof(HashSet<Order_Detail>));
        }

        public int ProductID { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        public int? SupplierID { get; set; }

        public int? CategoryID { get; set; }

        [StringLength(20)]
        public string QuantityPerUnit { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Detail> Order_Details { get; set; }

        public virtual Supplier Supplier { get; set; }

        internal void Print()
        {
            Console.WriteLine("*********************************");

            Console.WriteLine(ProductID);
            Console.WriteLine(ProductName);
            Console.WriteLine(QuantityPerUnit);
            Console.WriteLine(UnitPrice);

            Console.WriteLine(string.Format("Order Details Count: {0}", Order_Details.Count));
            foreach (var orderDetail in Order_Details)
            {
                Console.WriteLine(string.Format("   {0}", orderDetail.OrderID));
            }

            Console.WriteLine("*********************************");
        }

        public void GetObjectData(SerializationInfo information, StreamingContext context)
        {
            information.AddValue("ProductID", ProductID);
            information.AddValue("ProductName", ProductName);
            information.AddValue("QuantityPerUnit", QuantityPerUnit);
            information.AddValue("UnitPrice", UnitPrice);
            information.AddValue("Order_Details", Order_Details);
        }
    }
}
