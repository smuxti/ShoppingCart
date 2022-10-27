using Shop.DataModels.CustomModels;
using Shop.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Logic.Services
{
   public class UserService : IUserService
    {
        private readonly ShoppingCartDBContext appDbContext = null;

        public UserService(ShoppingCartDBContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public List<CategoryModel> GetCategories()
        {
            var data = appDbContext.Categories.ToList();
            List<CategoryModel> _categorylist = new List<CategoryModel>();
            foreach (var c in data)
            {
                CategoryModel _categoryModel = new CategoryModel();
                _categoryModel.Id = c.Id;
                _categoryModel.Name = c.Name;
                _categorylist.Add(_categoryModel);

            }
            return _categorylist;
        }

        public List<ProductModel> GetProductByCategoryId(int categoryId)
        {
            var data = appDbContext.Products.Where(x => x.CategoryId == categoryId).ToList();
            List<ProductModel> prodlist = new List<ProductModel>();
            foreach (var p in data)
            {
                ProductModel prodmodel = new ProductModel();
                prodmodel.Id = p.Id;
                prodmodel.Name = p.Name;
                prodmodel.Price = p.Price;
                prodmodel.ImageURL = p.ImageUrl;
                prodmodel.Stock = p.Stock;
                prodlist.Add(prodmodel);
            }
            return prodlist;
        }

        public ResponseModel RegisterUser(RegisterModel registerModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var existchk = appDbContext.Customers.Where(x => x.Email == registerModel.Email).Any();
                if(!existchk)
                {
                    Customer cust = new Customer();
                    cust.Name = registerModel.Name;
                    cust.MobileNo = registerModel.MobileNumber;
                    cust.Email = registerModel.Email;
                    cust.Password = registerModel.Password;
                    appDbContext.Add(cust);
                    appDbContext.SaveChanges();

                    LoginModel loginModel = new LoginModel()
                    {
                        EmailId = registerModel.Email,
                        Password = registerModel.Password
                    };
                    response = LoginUser(loginModel);
                }
                else
                {
                    response.Status = false;
                    response.Message = "Email Already Exist";
                }
                return response;
            }
            catch (Exception ex)
            {

                response.Status = false;
                response.Message = "An Error occured,please try again later";
                return response;
            }
        }

        public ResponseModel LoginUser(LoginModel loginModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var userdata = appDbContext.Customers.Where(x => x.Email == loginModel.EmailId).FirstOrDefault();
                if (userdata !=null)
                {
                    if(userdata.Password==loginModel.Password)
                    {
                        response.Status = true;
                        response.Message = Convert.ToString(userdata.Id) + "|" + userdata.Name + "|" + userdata.Email;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Your Password is incorrect";
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "Email not registerd plz check Your Email Id";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "Error try again later";
                return response;
            }
        }

        public ResponseModel CheckOut(List<CartModel> cartItems)
        {
            string OrderId = GenerteOrderId();
            var prods = appDbContext.Products.ToList();
            try
            {
                var details = cartItems.FirstOrDefault();
                CustomerOrder custorder = new CustomerOrder();
                custorder.CustomerId = details.UserId;
                custorder.OrderId = OrderId;
                custorder.PaymentMode = details.PaymentMod;
                custorder.ShippingAddress = details.ShippingAddress;
                custorder.ShippingCharges = details.ShippingCharges;
                custorder.SubTotal = details.SubTotal;
                custorder.Total = details.ShippingCharges + details.SubTotal;
                custorder.CreatedOn = DateTime.Now.ToString("dd/MM/yyyy");
                custorder.UpadatedOn = DateTime.Now.ToString("dd/MM/yyyy");
                appDbContext.CustomerOrders.Add(custorder);


                    foreach (var item in cartItems)
                {
                    OrderDetail orddetail = new OrderDetail();

                    orddetail.OrderId = OrderId;
                    orddetail.ProductId = item.PrductId;
                    orddetail.Quantity = item.Quntity;
                    orddetail.Price = item.Price;
                    orddetail.SubTotal = item.Price * item.Quntity;
                    orddetail.CreatedOn = DateTime.Now.ToString("dd/MM/yyyy");
                    orddetail.UpdatedOn = DateTime.Now.ToString("dd/MM/yyyy");
                    appDbContext.OrderDetails.Add(orddetail);

                    var SelectedProd = prods.Where(x => x.Id == item.PrductId).FirstOrDefault();
                    SelectedProd.Stock = SelectedProd.Stock - item.Quntity;
                    appDbContext.Products.Update(SelectedProd);
                }
                appDbContext.SaveChanges();

                ResponseModel response = new ResponseModel();
                response.Message = OrderId;

                return response;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public string GenerteOrderId()
        {
            string Orderid = string.Empty;
            Random generator = null;
            for (int i = 0; i < 1000; i++)
            {
                generator = new Random();
                Orderid = "P" + generator.Next(1, 10000000).ToString("D8");
                if (!appDbContext.CustomerOrders.Where(x => x.OrderId == Orderid).Any())
                {
                    break;
                }
            }
            return Orderid;
        }

        public List<CustomerOrder> GetOrderByCustomerId(int customerId)
        {
            var customerorder = appDbContext.CustomerOrders.Where(x => x.CustomerId == customerId).ToList();
            return customerorder;
        }
        public List<CartModel> GetOrderDetailForCustomer(int custId, string ordnum)
        {
            List<CartModel> cartdetails = new List<CartModel>();
            var custorder = appDbContext.CustomerOrders.Where(x => x.CustomerId == custId && x.OrderId == ordnum).FirstOrDefault();
            if (custorder !=null)
            {
                var orddetail = appDbContext.OrderDetails.Where(x => x.OrderId == ordnum).ToList();
                var prodlist = appDbContext.Products.ToList();
                foreach (var order in orddetail)
                {
                    var prod = appDbContext.Products.Where(x => x.Id == order.ProductId).FirstOrDefault();
                    CartModel cartmodel = new CartModel();
                    cartmodel.ProductName = prod.Name;
                    cartmodel.Price = Convert.ToInt32(order.Price);
                    cartmodel.ProductImage = prod.ImageUrl;
                    cartmodel.Quntity = Convert.ToInt32(order.Quantity);
                    cartdetails.Add(cartmodel);
                }
                cartdetails.FirstOrDefault().ShippingAddress = custorder.ShippingAddress;
                cartdetails.FirstOrDefault().ShippingCharges = Convert.ToInt32(custorder.ShippingCharges);
                cartdetails.FirstOrDefault().SubTotal = Convert.ToInt32(custorder.SubTotal);
                cartdetails.FirstOrDefault().Total = Convert.ToInt32(custorder.Total);
                cartdetails.FirstOrDefault().PaymentMod = custorder.PaymentMode;
            }
            return cartdetails;
        }

        public ResponseModel ChangePassword(PasswordModel passwordModel)
        {
            ResponseModel response = new ResponseModel();
            response.Status = true;
            var cust = appDbContext.Customers.Where(x => x.Id == passwordModel.UserKey).FirstOrDefault();
            if (cust !=null)
            {
                cust.Password = passwordModel.Password;
                appDbContext.Update(cust);
                appDbContext.SaveChanges();
                response.Message = "password updated successfully";
            }
            else
            {
                response.Message = "user doesnot exist.try again later";
            }
            return response;
        }
        public List<string> GetShippingStatusForOrder(string ordnum)
        {
            List<string> shippingstatus = new List<string>();
            var order = appDbContext.CustomerOrders.Where(x => x.OrderId == ordnum).FirstOrDefault();
            if (order != null && order.ShippingStatus !=null)
            {
                shippingstatus = order.ShippingStatus.Split("|").ToList();

            }
            return shippingstatus;
        }
    }

}
