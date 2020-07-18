using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApp.ViewModels
{
    public class CartVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductVM Product { get; set; }
        [Required]
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
}
