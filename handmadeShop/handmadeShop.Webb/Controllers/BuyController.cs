using AutoMapper;
using handmadeShop.Web.Models;
using handmadeShop.BusinessLogic.DTO;
using handmadeShop.BusinessLogic.Interfaces;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using handmadeShop.Web.Filters;
using Microsoft.Owin.Security;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System;
using handmadeShop.BusinessLogic.BusinessModels;

namespace handmadeShop.Web.Controllers
{
    [SessionTimeout]
    public class BuyController : Controller
    {
     
        private ICartService _cartService;
        private IOrderService _orderService;
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public BuyController(IOrderService orderService,ICartService cart_serv)
        {
            _cartService = cart_serv;
            _orderService = orderService;
     
        }
       
        [HttpGet]
        public ActionResult Shop()
        {
            IEnumerable<ProductDTO> haineNationaleDTO = _cartService.GetProducts("HaineNationale");
            IEnumerable<ProductDTO> jucariiDTO = _cartService.GetProducts("Jucarii");
            IEnumerable<ProductDTO> gentuteDTO = _cartService.GetProducts("Gentute");
            IEnumerable<ProductDTO> articoleCopiiDTO = _cartService.GetProducts("ArticoleCopii");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductModel>()).CreateMapper();
            var haineNationale = mapper.Map<IEnumerable<ProductDTO>, List<ProductModel>>(haineNationaleDTO);
            var jucarii = mapper.Map<IEnumerable<ProductDTO>, List<ProductModel>>(jucariiDTO);
            var gentute = mapper.Map<IEnumerable<ProductDTO>, List<ProductModel>>(gentuteDTO);
            var articoleCopii = mapper.Map<IEnumerable<ProductDTO>, List<ProductModel>>(articoleCopiiDTO);

            ViewBag.HaineNationale = haineNationale;
            ViewBag.Jucarii = jucarii;
            ViewBag.Gentute = gentute;
            ViewBag.ArticoleCopii = articoleCopii;

            return View(haineNationale);
        }

        

        protected override void Dispose(bool disposing)
        {
            _cartService.Dispose(); 
            _orderService.Dispose();   
            UserService.Dispose();
            base.Dispose(disposing);
        }

    
       
        public ActionResult Cart()
        {
            List<Item> cartItems = _cartService.GetCart();
            var articles = _cartService.GetProducts("HaineNationale");
            ViewBag.HaineNationale = articles.ToList();

            return View(cartItems);
        }
        [HttpPost]
        public JsonResult SetDiscount()
        {
            var discountDTO = _cartService.GetAllDiscounts().LastOrDefault();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DiscountDTO, DiscountViewModel>()).CreateMapper();

            if (discountDTO != null && discountDTO.ExpirationTime > DateTime.Now)
            {
                var discount = mapper.Map<DiscountDTO, DiscountViewModel>(discountDTO);
                return Json(new { Success = true, discount });
            }
            else if (discountDTO != null && discountDTO.ExpirationTime <= DateTime.Now)
            {
                
                return Json(new { Success = false,Message = "Discount has expired" });
            }
            else
            {
                
                return Json(new { Success = false, Message = "No discount available" });
            }
        }

        [HttpPost]
        public JsonResult SetDeliveyRequest()
        {
            var deliveryDTO = _cartService.GetAllDeliveriesCost().LastOrDefault();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DeliveryCostDTO, DeliveryCostViewModel>()).CreateMapper();

            if(deliveryDTO != null && deliveryDTO.Cost > 0)
            {
                var delivery = mapper.Map<DeliveryCostDTO,DeliveryCostViewModel>(deliveryDTO);
                return Json(new {Success = true, delivery });
            }
            else
            {
                return Json(new { Success = false, Message = "Delivery was not set yet!" });
            }
        }


        [HttpPost]
        public JsonResult AddToCart(int ProductId,string category)
        {
            _cartService.AddToCart(ProductId,category);
            List<Item> cartItems = _cartService.GetCart();
    
            return Json(new { success = true, cartItems });

        }
    

        public JsonResult RemoveFromTheCart(int ProductId)
        {
            _cartService.RemoveFromTheCart(ProductId);

            List<Item> cartItems = _cartService.GetCart();
            return Json(new { success = true, cartItems });
        }
        [RedirectToRegister]
        [SessionTimeout]
        [HttpGet]
        [Authorize]
        public ActionResult Checkout()
        {
            var products = _cartService.GetCart();
            var user = UserService.GetUserById(User.Identity.GetUserId());
            ViewBag.User = user;
            ViewBag.Products = products;
            return View();
        }
        [Authorize]
        [SessionTimeout]
     
        public ActionResult SuccessfullOrder()
        {
            string userId = User.Identity.GetUserId();
            var orderDto = _orderService.GetOrdersByUserId(userId).LastOrDefault();
            var ItemsDTO = orderDto.Items.ToList();
            var productModel = new List<ProductModel>();
            foreach(var item in ItemsDTO)
            {
                var productDTO = _orderService.GetProductWithouCategory(item.ProductId);
                var product = new ProductModel
                {
                    Id = item.ProductId,
                    Name = productDTO.Name,
                    Price = productDTO.Price,
                    PathImage = productDTO.PathImage,
                    Quantity = item.Quantity,
                };
                productModel.Add(product);
            }

            if (orderDto == null || orderDto.ApplicationUserId != userId) return HttpNotFound();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, OrderViewModel>()).CreateMapper();
            var order = mapper.Map<OrderDTO,OrderViewModel>(orderDto);
            order.PhoneNumber = orderDto.Phone;
            ViewBag.SuccessfulOrder = true;
            Session.Clear();

            ViewBag.CartItems = productModel;
            return View(order);
        }

        [HttpPost]
        [Authorize]
        [SessionTimeout]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Checkout(OrderViewModel model)
        {
            if (ModelState.IsValid)
            { 
                var user = await UserService.GetUserById(User.Identity.GetUserId());
                var cartItems = _cartService.GetCart();
                ViewBag.CartItems = cartItems.ToList();

                decimal totalSumToPay = _cartService.CalculateTotalPrice();


                if (totalSumToPay > 0)
                {
                    var orderDto = new OrderDTO
                    {

                        ApplicationUserId = (user.Id).ToString(),
                        Appartment = model.Appartment,
                        StreetAddress = model.Address,
                        City = model.City,
                        Phone = model.PhoneNumber,
                        PostCode = model.PostCode,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        TotalSumToPay = totalSumToPay,
                        
                    };
                    _orderService.MakeOrder(orderDto);
                   
                    return RedirectToAction("SuccessfullOrder", "Buy");
                }
                else
                {
                    return View(model);
                }

            }

            return View(model);  
        }

 
    }
}