using Shop.DataModels.CustomModels;
using Shop.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Admin.Services
{
    public interface IAdminPanelService
    {
        public  Task<ResponseModel> AdminLogin(LoginModel loginModel);
        public Task<CategoryModel> SaveCatgory(CategoryModel newCategory);
        public Task<List<CategoryModel>> GetCategories();
        public Task<bool> UpdateCategory(CategoryModel categoryToUpdate);
        public Task<bool> DeleteCategory(CategoryModel categoryToDelete);

        public Task<List<ProductModel>> GetProducts();
        public Task<bool> DeleteProduct(ProductModel productToDelete);
        public Task<ProductModel> SaveProduct(ProductModel newProduct);
        Task<List<StockModel>> GetProductStock();
        Task<bool> UpdateProductStock(StockModel newStock);
        Task<List<CustomerOrder>> GetOrders();
        Task<List<CustomerOrder>> GetOrderDetails(string order);
        Task<List<CustomerOrder>> GetOrderByDate(string date);
    }
}
