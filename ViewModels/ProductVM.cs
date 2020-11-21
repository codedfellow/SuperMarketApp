using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApp.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Unit Price")]
        public double UnitPrice { get; set; }
        [Required]
        [Display(Name = "Units in Stock")]
        public int UnitsInStock { get; set; }
        [Display(Name = "Delete old image")]
        public bool DeleteOldPicture { get; set; }
        public string Picture { get; set; }
        [Display(Name = "Product image")]
        public IFormFile PictureContainer { get; set; }
    }
}
