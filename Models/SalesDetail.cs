using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApp.Models
{
    public class SalesDetail
    {
        public int Id { get; set; }
        public int SalesId { get; set; }
        [ForeignKey("SalesId")]
        public Sales Sale { get; set; }
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
