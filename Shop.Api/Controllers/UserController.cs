using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.DataModels.CustomModels;
using Shop.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IWebHostEnvironment env;

        public UserController(IUserService userService, IWebHostEnvironment env)
        {
            this.userService = userService;
            this.env = env;
        }


        [HttpGet]
        [Route("GetCategories")]
        public IActionResult GetCategories()
        {
            var data = userService.GetCategories();
            return Ok(data);
        }


        [HttpGet]
        [Route("GetProductByCategoryId")]
        public IActionResult GetProductByCategoryId(int categoryId)
        {
            var data = userService.GetProductByCategoryId(categoryId);
            return Ok(data);
        }


        [HttpPost]
        [Route("RegisterUser")]
        public IActionResult RegisterUser(RegisterModel registerModel)
        {
            var data = userService.RegisterUser(registerModel);
            return Ok(data);
        }

        [HttpPost]
        [Route("LoginUser")]
        public IActionResult LoginUser(LoginModel loginModel)
        {
            var data = userService.LoginUser(loginModel);
            return Ok(data);
        }

        [HttpPost]
        [Route("CheckOut")]
        public IActionResult CheckOut(List<CartModel> cartItem)
        {
            var data = userService.CheckOut(cartItem);
            return Ok(data);
        }

        [HttpGet]
        [Route("GetOrderByCustomerId")]
        public IActionResult GetOrderByCustomerId(int customerId)
        {
            var data = userService.GetOrderByCustomerId(customerId);
            return Ok(data);
        }


        [HttpGet]
        [Route("GetOrderDetailForCustomer")]
        public IActionResult GetOrderDetailForCustomer(int custId,string ordnum)
        {
            var data = userService.GetOrderDetailForCustomer(custId, ordnum);
            return Ok(data);
        }


        [HttpGet]
        [Route("GetShippingStatusForOrder")]
        public IActionResult GetShippingStatusForOrder(string ordnum)
        {
            var data = userService.GetShippingStatusForOrder(ordnum);
            return Ok(data);
        }


        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(PasswordModel passwordModel)
        {
            var data = userService.ChangePassword(passwordModel);
            return Ok(data);
        }
    }
}
