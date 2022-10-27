using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.DataModels.CustomModels;
using Shop.DataModels.Models;
using Shop.Logic.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        private readonly IWebHostEnvironment env;

        public AdminController(IAdminService adminService,IWebHostEnvironment env)
        {
            this.adminService = adminService;
            this.env = env;
        }


        [HttpPost]
         [Route("AdminLogin")]
         public IActionResult AdminLogin(LoginModel loginModel)
        {
            var data = adminService.AdminLogin(loginModel);
            return Ok(data);
        }

        [HttpPost]
        [Route("SaveCategory")]
        public IActionResult SaveCategory(CategoryModel newCategory)
        {
            var data = adminService.SaveCategory(newCategory);
            return Ok(data);
        }

        
        [HttpGet]
        [Route("GetCategories")]
        public IActionResult GetCategories()
        {
            var data = adminService.GetCategories();
            return Ok(data);
        }

        [HttpPost]
        [Route("UpdateCategory")]
        public IActionResult CategoryToUpdate(CategoryModel categoryToUpdate)
        {
            var data = adminService.UpdateCategory(categoryToUpdate);
            return Ok(data);
        }

        [HttpPost]
        [Route("DeleteCategory")]
        public IActionResult DeleteCategory(CategoryModel categoryToDelete)
        {
            var data = adminService.DeleteCategory(categoryToDelete);
            return Ok(data);
        }


        
        [HttpGet]
        [Route("GetProducts")]
        public IActionResult GetProducts()
        {
            var data = adminService.GetProduct();
            return Ok(data);
        }

        [HttpPost]
        [Route("SaveProduct")]
        public IActionResult SaveProduct(ProductModel newProduct)
        {
            int nextProductId = adminService.GetNewproductId();
            newProduct.ImageURL = @"Images/" + nextProductId + ".png";
            var path = $"{env.WebRootPath}\\Images\\{nextProductId + ".png"}";
            var fs = System.IO.File.Create(path);
            fs.Write(newProduct.FileContent, 0, newProduct.FileContent.Length);
            fs.Close();
            string uploadsfolder = Path.Combine(env.WebRootPath, "Images");
            var data = adminService.saveProduct(newProduct);
            return Ok(data);
        }

        [HttpPost]
        [Route("DeleteProduct")]
        public IActionResult Deleteproduct(ProductModel productToDelete)
        {
            var data = adminService.DeleteProduct(productToDelete);
            return Ok(data);
        }

        [HttpGet]
        [Route("GetProductStock")]
        public IActionResult GetProductStock()
        {
            var data = adminService.GetProductStock();
            return Ok(data);
        }
        [HttpPost]
        [Route("UpdateProductStock")]
        public IActionResult UpdateProductStock(StockModel _stk)
        {
            var data = adminService.UpdateProductStock(_stk);
            return Ok(data);
        }

        [HttpGet]
        [Route("GetOrders")]
        public IActionResult GetOrders()
        {
            var data = adminService.GetOrders();
            return Ok(data);
        }
        [HttpGet]
        [Route("GetOrderDetails")]
        public IActionResult GetOrderDetails(string order)
        {
            var data = adminService.GetOrderDetails(order);
            return Ok(data);
        }
        [HttpGet]
        [Route("GetOrderByDate")]
        public IActionResult GetOrderByDate(string date)
        {
            var data = adminService.GetOrderByDate(date);
            return Ok(data);
        }
    }
}
