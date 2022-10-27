using Shop.DataModels.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shop.DataModels.Models;
using System.Runtime.InteropServices.ComTypes;

namespace Shop.Admin.Services
{
    public class AdminPanelService : IAdminPanelService
    {
        private readonly HttpClient httpClient;

        public AdminPanelService(HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
        }
        public async Task<ResponseModel> AdminLogin(LoginModel loginModel)
        {
            return await httpClient.PostJsonAsync<ResponseModel>("api/admin/AdminLogin", loginModel);
        }


        public async Task<List<CategoryModel>> GetCategories()
        {
            return await httpClient.GetJsonAsync<List<CategoryModel>>("api/admin/GetCategories");
        }
      
        public async Task<CategoryModel>SaveCatgory (CategoryModel newCategory)
        {
            return await httpClient.PostJsonAsync<CategoryModel>("api/admin/SaveCategory", newCategory);

        }

        public async Task<bool> UpdateCategory(CategoryModel categoryToUpdate)
        {
            return await httpClient.PostJsonAsync<bool>("api/admin/UpdateCategory", categoryToUpdate);
        }

        public async Task<bool> DeleteCategory(CategoryModel categoryToDelete)
        {
            return await httpClient.PostJsonAsync<bool>("api/admin/DeleteCategory", categoryToDelete);
        }

        public async Task<bool> DeleteProduct(ProductModel productToDelete)
        {
            return await httpClient.PostJsonAsync<bool>("api/admin/DeleteProduct", productToDelete);
        }

        public async Task<ProductModel> SaveProduct(ProductModel newProduct)
        {
            return await httpClient.PostJsonAsync<ProductModel>("api/admin/SaveProduct", newProduct);

        }
        public async Task<List<ProductModel>> GetProducts()
        {
            return await httpClient.GetJsonAsync<List<ProductModel>>("api/admin/GetProducts");
        }

        public async Task<List<StockModel>> GetProductStock()
        {
            return await httpClient.GetJsonAsync<List<StockModel>>("api/admin/GetProductStock");
        }

        public async Task<bool> UpdateProductStock(StockModel newStock)
        {
            return await httpClient.PostJsonAsync<bool>("api/admin/UpdateProductStock", newStock);

        }
        public async Task<List<CustomerOrder>> GetOrders()
        {
            return await httpClient.GetJsonAsync<List<CustomerOrder>>("api/admin/GetOrders");
        }
        public async Task<List<CustomerOrder>> GetOrderDetails(string order)
        {
            return await httpClient.GetJsonAsync<List<CustomerOrder>>("api/admin/GetOrderDetails/?order=" + order);
        }

        public async Task<List<CustomerOrder>> GetOrderByDate(string date)
        {
            return await httpClient.GetJsonAsync<List<CustomerOrder>>("api/admin/GetOrderByDate/?date=" + date);
        }
    }
}
