using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApp.Models
{
    public class Sales
    {
        public int Id { get; set; }
        public DateTime SalesDate { get; set; }
        public double Amount { get; set; }
    }
}
