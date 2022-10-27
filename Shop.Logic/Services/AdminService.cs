using Shop.DataModels.CustomModels;
using Shop.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Logic.Services
{
    public class AdminService : IAdminService
    {
        private readonly ShoppingCartDBContext appDbContext = null;

        public AdminService(ShoppingCartDBContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public ResponseModel AdminLogin(LoginModel loginModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var UserData = appDbContext.AdminInfos.Where(x => x.Email == loginModel.EmailId).FirstOrDefault();
                if(UserData != null)
                {
                   
                       if (UserData.Password == loginModel.Password)
                        {
                            response.Status = true;
                            response.Message = Convert.ToString(UserData.Id) + "|" + UserData.Name + "|" + UserData.Email;
                        }
                        
                    
                    else
                    {
                        response.Status = false;
                        response.Message = "your password is incorrect";
                    }



                }
                else
                {
                    response.Status = false;
                    response.Message = "Email does Not Exist";
                }
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
            
            
            
        }

        public CategoryModel SaveCategory(CategoryModel newCategory)
        {
            try
            {
                Category category = new Category();
                category.Name = newCategory.Name;
                appDbContext.Add(category);
                appDbContext.SaveChanges();
                return newCategory;
            }
            catch (Exception ex)
            {

                throw;
            }
            
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
        public bool UpdateCategory(CategoryModel categoryToUpdate)
        {
            bool flag = false;
            var _category= appDbContext.Categories.Where(x=>x.Id == categoryToUpdate.Id ).First();
            if (_category !=null)
            {
                _category.Name = categoryToUpdate.Name;
                appDbContext.Categories.Update(_category);
                appDbContext.SaveChanges();
                flag = true;
            }
            return flag;
        }
        public bool DeleteCategory(CategoryModel categoryToDelete)
        {
            bool flag = false;
            var cat = appDbContext.Categories.Where(x => x.Id == categoryToDelete.Id).FirstOrDefault();
            if (cat !=null)
            {
                appDbContext.Categories.Remove(cat);
                appDbContext.SaveChanges();
                flag = true;
            }
            return flag;
        }


        public List<ProductModel> GetProduct()

        {
            List < Category > CategoryData= appDbContext.Categories.ToList();
            List<Product> ProductData = appDbContext.Products.ToList();
            List<ProductModel> productList = new List<ProductModel>();
            foreach (var p in ProductData)
            {
                ProductModel _prodModel = new ProductModel();
                _prodModel.Id = p.Id;
                _prodModel.Name = p.Name;
                _prodModel.Price = p.Price;
                _prodModel.Stock = p.Stock;
                _prodModel.ImageURL = p.ImageUrl;
                _prodModel.CategoryId = p.CategoryId;
                _prodModel.CategoryName = CategoryData.Where(x => x.Id == p.CategoryId).Select(x => x.Name).FirstOrDefault();
                productList.Add(_prodModel);
            }
            return productList;
        }

        public int GetNewproductId()
        {
            try
            {
                int nextProdId = appDbContext.Products.ToList().OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                return nextProdId;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

       public ProductModel saveProduct(ProductModel newProd)
        {
            try
            {
                Product _prodModel = new Product();
                _prodModel.Name = newProd.Name;
                _prodModel.Price = newProd.Price;
                _prodModel.Stock = newProd.Stock;
                _prodModel.ImageUrl = newProd.ImageURL;
                _prodModel.CategoryId = newProd.CategoryId;
                appDbContext.Add(_prodModel);
                appDbContext.SaveChanges();
                return newProd;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool DeleteProduct(ProductModel productToDelete)
        {
            bool flag = false;
            var prod = appDbContext.Products.Where(x => x.Id == productToDelete.Id).FirstOrDefault();
            if (prod != null)
            {
                appDbContext.Products.Remove(prod);
                appDbContext.SaveChanges();
                flag = true;
            }
            return flag;
        }

        public List<StockModel> GetProductStock()
        {
            List<StockModel> productStock = new List<StockModel>();
            List<Category> categoryData = appDbContext.Categories.ToList();
            List<Product> productdata = appDbContext.Products.ToList();
            foreach (var p in productdata)
            {
                StockModel _product = new StockModel();
                _product.Id = p.Id;
                _product.Name = p.Name;
                _product.Stock = p.Stock;
                _product.CategoryName = categoryData.Where(x => x.Id == p.CategoryId).Select(x =>x.Name).FirstOrDefault();
                productStock.Add(_product);
            }
            return productStock;
        }

        public bool UpdateProductStock(StockModel stock)
        {
            bool flag = false;
            var _product = appDbContext.Products.Where(x => x.Id == stock.Id).First();
            if (_product != null)
            {
                _product.Stock = stock.Stock + stock.NewStock;
                appDbContext.Products.Update(_product);
                appDbContext.SaveChanges();
                flag = true;
            }
            return flag;
        }

        public List<CustomerOrder> GetOrders()
        {
            List<CustomerOrder> custorder = appDbContext.CustomerOrders.ToList();
            return custorder;
        }

        public List<CustomerOrder> GetOrderDetails(string order)
        {
            List<CustomerOrder> customerOrders = appDbContext.CustomerOrders.Where(x => x.OrderId == order).ToList();
            return customerOrders;
        }

        public List<CustomerOrder> GetOrderByDate(string date)
        {
            List<CustomerOrder> customerOrders = new List<CustomerOrder>();
            if (date !=null)
            {
                 customerOrders = appDbContext.CustomerOrders.Where(x => x.CreatedOn == date).ToList();
                
            }
           else
            {
                customerOrders = GetOrders();
            }
            return customerOrders;
        }
    }
}
