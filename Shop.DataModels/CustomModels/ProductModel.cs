using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataModels.CustomModels
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Product name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Product price is required")]
        public int? Price { get; set; }
        [Required(ErrorMessage = "Product stock is required")]
        public int? Stock { get; set; }
        [Required(ErrorMessage = "Product category is required")]
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImageURL { get; set; }
        [Required(ErrorMessage = "Kindly upload product Image")]
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public bool CartFlag { get; set; }

    }
}
