using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public Supermarket Supermarket { get; set; }
        public Room Room { get; set; }
        /// <summary>
        /// 1 - Comida
        /// 2 - Congelado
        /// 3 - Limpieza
        /// </summary>
        public string Type { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime DeleteDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
