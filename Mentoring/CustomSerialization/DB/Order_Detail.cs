using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace CustomSerialization.DB
{
    [Table("Order Details")]
    [DataContract(IsReference = true)]
    public partial class Order_Detail
    {
        [DataMember]
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        [DataMember]
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [DataMember]
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        [DataMember]
        public short Quantity { get; set; }

        [DataMember]
        public float Discount { get; set; }

        [DataMember]
        public virtual Order Order { get; set; }

        [DataMember]
        public virtual Product Product { get; set; }

        internal void Print()
        {
            Console.WriteLine("*********************************");

            Console.WriteLine(OrderID);
            Console.WriteLine(UnitPrice);
            Console.WriteLine(Quantity);
            Console.WriteLine(Discount);

            Console.WriteLine("*********************************");
        }
    }
}
