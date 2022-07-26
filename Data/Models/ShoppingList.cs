using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public Supermarket Supermarket { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DeleteDate { get; set; }
        public bool Active { get; set; }
        public bool Printed { get; set; }
        public bool IsDeleted { get; set; }
        public List<ShoppingListProduct> ListProducts { get; set; }
    }
}
