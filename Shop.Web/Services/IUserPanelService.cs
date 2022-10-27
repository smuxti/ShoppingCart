using Shop.DataModels.CustomModels;
using Shop.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Services
{
    public interface IUserPanelService
    {
        Task<List<CategoryModel>> GetCategories();
        Task<List<ProductModel>> GetProductByCategoryId(int categoryId);
        Task<bool> IsUserLoggedIn();
        Task<ResponseModel> RegisterUser(RegisterModel registerModel);
        Task<ResponseModel> LoginUser(LoginModel loginModel);
        Task<ResponseModel> CheckOut(List<CartModel> cartItem);
        Task<List<CustomerOrder>> GetOrderByCustomerId(int customerId);
        Task<List<string>> GetShippingStatusForOrder(string ordnum);
        Task<List<CartModel>> GetOrderDetailForCustomer(int custId, string ordnum);
        Task<ResponseModel> ChangePassword(PasswordModel passwordModel);
    }
}
