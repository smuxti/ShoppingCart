using Microsoft.AspNetCore.Components;
using Shop.DataModels.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Shop.DataModels.Models;

namespace Shop.Web.Services
{
    public class UserPanelService : IUserPanelService
    {
        private readonly HttpClient httpClient;
        private readonly ProtectedBrowserStorage sessionStorage;

        public UserPanelService(HttpClient _httpClient,ProtectedSessionStorage _sessionStorage)
        {
            this.httpClient = _httpClient;
            this.sessionStorage = _sessionStorage;
        }

        public async Task<bool> IsUserLoggedIn()
        {
            bool flag = false;
            var result = await sessionStorage.GetAsync<string>("userKey");
            if (result.Success)
            {
                flag = true;
            }
            return flag;
        }
        public async Task<List<CategoryModel>> GetCategories()
        {
            return await httpClient.GetJsonAsync<List<CategoryModel>>("api/user/GetCategories");
        }
        public async Task<List<ProductModel>> GetProductByCategoryId(int categoryId)
        {
            return await httpClient.GetJsonAsync<List<ProductModel>>("api/User/GetProductByCategoryId/?categoryId=" + categoryId);
        }
        public async Task<ResponseModel> RegisterUser(RegisterModel  registerModel)
        {
            return await httpClient.PostJsonAsync<ResponseModel>("api/User/RegisterUser",registerModel);
        }

        public async Task<ResponseModel> LoginUser(LoginModel loginModel)
        {
            return await httpClient.PostJsonAsync<ResponseModel>("api/User/LoginUSer", loginModel);
        }
        public async Task<ResponseModel> CheckOut(List<CartModel> cartItem)
        {
            return await httpClient.PostJsonAsync<ResponseModel>("api/User/CheckOut", cartItem);
        }
        public async Task<List<CustomerOrder>> GetOrderByCustomerId(int customerId)
        {
            return await httpClient.GetJsonAsync<List<CustomerOrder>>("api/User/GetOrderByCustomerId/?customerId=" + customerId);
        }
        public async Task<List<CartModel>> GetOrderDetailForCustomer(int custId, string ordnum)
        {
            return await httpClient.GetJsonAsync<List<CartModel>>("api/User/GetOrderDetailForCustomer/?custId=" + custId + "&ordnum=" + ordnum);
        }
        public async Task<List<string>> GetShippingStatusForOrder(string ordnum)
        {
            return await httpClient.GetJsonAsync<List<string>>("api/User/GetShippingStatusForOrder/?ordnum="  + ordnum);
        }
        public async Task<ResponseModel> ChangePassword(PasswordModel passwordModel)
        {
            return await httpClient.PostJsonAsync<ResponseModel>("api/User/ChangePassword", passwordModel);
        }
    }
}
