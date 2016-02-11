using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace CustomSerialization.DB
{
    [Serializable]
    public class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryID { get; set; }

        [Required]
        [StringLength(15)]
        public string CategoryName { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        //[Column(TypeName = "image")]
        //public byte[] Picture { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }

        [OnSerializing]
        public void OnSerializing(StreamingContext context)
        {
            var slot = Thread.AllocateNamedDataSlot("CategoryName_HiddenValue");
            Thread.SetData(slot, CategoryName);

            CategoryName = Convert.ToBase64String(Encoding.UTF8.GetBytes(CategoryName));

        }

        [OnDeserialized]
        public void OnDeserializing(StreamingContext context)
        {
            var slot = Thread.GetNamedDataSlot("CategoryName_HiddenValue");
            CategoryName = (string)Thread.GetData(slot);
        }

        public void Print()
        {
            Console.WriteLine("*********************************");

            Console.WriteLine(CategoryID);
            Console.WriteLine(CategoryName);
            Console.WriteLine(Description);

            Console.WriteLine(string.Format("Product Count: {0}", Products.Count));
            foreach (var product in Products)
            {
                Console.WriteLine(string.Format("   {0}", product.ProductName));
            }

            Console.WriteLine("*********************************");
        }
    }
}
