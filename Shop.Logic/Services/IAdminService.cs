using Shop.DataModels.CustomModels;
using Shop.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Logic.Services
{
    public interface IAdminService
    {
        ResponseModel AdminLogin(LoginModel loginModel);
        CategoryModel SaveCategory(CategoryModel newCategory);
        public List<CategoryModel> GetCategories();
        bool DeleteCategory(CategoryModel categoryToDelete);
        bool UpdateCategory(CategoryModel categoryToUpdate);


        List<ProductModel> GetProduct();
        bool DeleteProduct(ProductModel productToDelete);
        public int GetNewproductId();
        ProductModel saveProduct(ProductModel newProd);
        public bool UpdateProductStock(StockModel stock);
        public List<StockModel> GetProductStock();
        public List<CustomerOrder> GetOrders();
        List<CustomerOrder> GetOrderDetails(string order);
        List<CustomerOrder> GetOrderByDate(string date);





    }
}
