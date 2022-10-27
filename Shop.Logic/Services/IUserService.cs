using Shop.DataModels.CustomModels;
using Shop.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Logic.Services
{
    public interface IUserService
    {
        public List<CategoryModel> GetCategories();
        List<ProductModel> GetProductByCategoryId(int categoryId);
        ResponseModel RegisterUser(RegisterModel registerModel);
        ResponseModel LoginUser(LoginModel loginModel);
        public ResponseModel CheckOut(List<CartModel> cartItems);
        List<CustomerOrder> GetOrderByCustomerId(int customerId);
        List<CartModel> GetOrderDetailForCustomer(int custId, string ordnum);
        ResponseModel ChangePassword(PasswordModel passwordModel);
        public List<string> GetShippingStatusForOrder(string ordnum);
    }
}
