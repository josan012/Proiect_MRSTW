using AutoMapper;
using handmadeShop.BusinessLogic.DTO;
using handmadeShop.BusinessLogic.Infrastructure;
using handmadeShop.BusinessLogic.Interfaces;
using handmadeShop.Web.Filters;
using handmadeShop.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using handmadeShop.BusinessLogic.BusinessModels;
using handmadeShop.Web.Models;

namespace handmadeShop.Web.Controllers
{

    public class AccountController : Controller
    {
        private IOrderService orderService;
        private ICartService cartService;
        private IEditProductService editProductService;
        private IReservationService reservationService;
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
        public AccountController(IOrderService orderService, ICartService cartService, IEditProductService editProductService, IReservationService reservationService)
        {
            this.orderService = orderService;
            this.cartService = cartService;
            this.editProductService = editProductService;
            this.reservationService = reservationService;
        }

        [HttpGet]
        [SessionTimeout]
        [Authorize]
        public ActionResult ChangePassword()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> ChangePassword(PasswordModel model)
        {
            var user = await UserService.GetUserById(User.Identity.GetUserId());
            if (user == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var passwordHasher = new PasswordHasher();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user.Password, model.CurrentPassword);

            if(passwordVerificationResult == PasswordVerificationResult.Success)
            {
                var newPasswordHash = passwordHasher.HashPassword(model.Password);
                user.Password = newPasswordHash;
                OperationDetails operationDetails = await UserService.UpdateClient(user);

                if (operationDetails.Succeeded)
                {
                    ViewBag.PasswordChanged = true;
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Error changing password. Please try again.");
                    return View(model);
                }
            }
            else
            {
                
                ModelState.AddModelError("", "The current password is incorrect.");
                return View(model);
            }
          

        }

        [HttpGet]
        [SessionTimeout]
        [Authorize]
        public async Task<ActionResult> EditClientProfile()
        {   
            var user = await UserService.GetUserById(User.Identity.GetUserId());
            var editModel = new EditModel { UserName = user.UserName, Email = user.Email,Address=user.Address,Name = user.Name };
            return View(editModel);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditClientProfile(EditModel model)
        {
            var user = await UserService.GetUserById(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                
            }
            if (ModelState.IsValid)
            {
                // Update the properties only if the model properties are not empty
                if (!string.IsNullOrEmpty(model.Email))
                {
                    user.Email = model.Email;
                }
                if (!string.IsNullOrEmpty(model.Name))
                {
                    user.Name = model.Name;
                }

                if (!string.IsNullOrEmpty(model.Address))
                {
                    user.Address = model.Address;
                }


                if (!string.IsNullOrEmpty(model.UserName))
                {
                    user.UserName = model.UserName;
                }
                if (!string.IsNullOrEmpty(model.Email))
                {

                }
                OperationDetails operationDetails = await UserService.UpdateClient(user);

                if (operationDetails.Succeeded)
                {
                    if (User.IsInRole("admin"))
                    {
                        return RedirectToAction("AdminDashboard");
                    }
                    else
                    {
                        return RedirectToAction("ClientProfile");
                    }
                }
            }
        
            return View(model);
        }

        [Authorize(Roles ="admin")]
        [HttpGet]
        public async Task<ActionResult> OtherUsers()
        {
            var users = await GetAllUsers();

           
            return View(users);
        }


        [Authorize(Roles = "user")]
        [SessionTimeout]
        public async Task<ActionResult> ClientProfile()
        {
            
            var userDto = await UserService.GetUserById(User.Identity.GetUserId());
            var user = new UserModel { Id = userDto.Id,Email=userDto.Email,Address=userDto.Address,Name=userDto.Name,UserName = userDto.UserName };
            if(user == null) return HttpNotFound();
        
            return View(user);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult EditExistingProduct(int Id,string category)
        {
           var productDTO = cartService.GetProduct(Id,category);
            var productModel = new ProductModel { Id = productDTO.Id, 
                Category = category,Price = productDTO.Price,Name = productDTO.Name,
                PathImage = productDTO.PathImage };

            return View(productModel);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult ViewAllProducts()
        {
            var productsDTO = cartService.RetrieveAllProducts();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductModel>()).CreateMapper();
            var products = mapper.Map<IEnumerable<ProductDTO>, List<ProductModel>>(productsDTO);
            

            return View(products);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]    
        
        public ActionResult EditExistingProduct(ProductModel model)
        {
           
            if (ModelState.IsValid)
            {
               
                var ProductDTO = new ProductDTO
                {
                    Id = model.Id,
                    Name = model.Name,
                    Category = model.Category,
                    Price = model.Price,
                    PathImage = model.PathImage,
              
                };
                editProductService.UpdateProduct(ProductDTO);
                return RedirectToAction("AdminDashboard");
            }
           
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles ="admin")]
        public ActionResult DiscountPage()
        {

            return View();
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult DeliveryPage()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult SetDelivery(DeliveryCostViewModel deliveryCostViewModel)
        {
            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DeliveryCostViewModel, DeliveryCostDTO>()).CreateMapper();
                var delivery = mapper.Map<DeliveryCostViewModel, DeliveryCostDTO>(deliveryCostViewModel);
                cartService.SetDelivery(delivery);
                return RedirectToAction("AdminDashboard");
            }
            return View("DeliveryPage",deliveryCostViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult SetDiscount(DiscountViewModel discountViewModel)
        {
            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DiscountViewModel, DiscountDTO>()).CreateMapper();
                var discount = mapper.Map<DiscountViewModel, DiscountDTO>(discountViewModel);
                cartService.SetDiscount(discount);

                return RedirectToAction("AdminDashboard");
            }

            return View("DiscountPage",discountViewModel);
        }

        [Authorize(Roles = "admin")]
        [SessionTimeout]
        public async Task<ActionResult> AdminDashboard()
        {
            var userAdmin = await GetUserAdmin();
            var adminModel = MapUserToUserModel(userAdmin);
            
            return View(adminModel);
        }

        [HttpGet]
        [Authorize(Roles="admin")]
        [RedirectToRegister]
        public ActionResult ItemsBought(int orderId)
        {
            var orderDto = orderService.GetOrder(orderId);
            var products = new List<ProductModel>();
            if (orderDto != null)
            {
                var order = MapOrderToOrderViewModel(orderDto);
                products = order.Items.Select(item => new ProductModel
                {
                    Id = item.ProductId,
                    Name = item.Product.Name,
                    Quantity = item.Quantity,
                    PathImage = item.Product.PathImage,
                    Price = item.Product.Price,
                }).ToList();
            }
            return View(products);
        }

        [Authorize(Roles ="admin")]
        public async Task<ActionResult> OrdersDetails(string userId)
        {
            var user = await GetUserWithOrders(userId);
            return View(user);
        }

        [Authorize(Roles ="admin")]
        public async Task<ActionResult> ReservationDetails(string userId)
        {
            var user = await GetUserWithReservations(userId);
            return View(user);
        }
   
        public ActionResult Login()
        {
            ViewBag.CurrentAction = "Login";
            return View();  
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            //await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDTO = new UserDTO { UserName = model.UserName,Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDTO);
                if(claim == null)
                {
                    ModelState.AddModelError("", "Incorrect login or password!");
                }
                else
                {
                    var userRole = claim.FindFirst(ClaimTypes.Role)?.Value;

                    if (userRole == "admin")
                    {
                        Session["UserId"] = "admin";
                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);
                        return RedirectToAction("AdminDashboard");

                    }
                    else
                    {
                        Session["UserId"] = "user";
                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);
                        return RedirectToAction("ClientProfile", "Account");
                    }
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.CurrentAction = "Register";
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
           /* await SetInitialDataAsync();*/
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    UserName = model.UserName,
                    Role = "user"
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succeeded) return RedirectToAction("Login");
                else ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

      

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> DeleteUser(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
            var result = await UserService.DeleteUserByUsername(username);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "User deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete user.";
            }

            return RedirectToAction("OtherUsers");
        }

        [HttpPost]
        [Authorize(Roles = "admin")] 
        public ActionResult DeleteDiscount()
        {
            bool response = cartService.RemoveDiscount();
            if (!response)
            {
                ModelState.AddModelError("", "The discount was already removed!");
                return View("DiscountPage");
            }
           
            return RedirectToAction("DiscountPage", "Account");
        }

        protected override void Dispose(bool disposing)
        {
            UserService.Dispose();
            cartService.Dispose();
            orderService.Dispose();
            editProductService.Dispose();
            reservationService.Dispose();   
            base.Dispose(disposing);
        }

        #region Helpers
        private async Task<UserModel> GetUserWithOrders(string userId)
        {
            var userDTO = await UserService.GetUserById(userId);
            var ordersDTO = orderService.GetOrdersByUserId(userId);
            var user = new UserModel();

            if (userDTO != null)
            {
                user = MapUserToUserModel(userDTO);
                AddOrdersToUser(user, ordersDTO);
            }

            return user;
        }
        private async Task<UserModel> GetUserWithReservations(string userId)
        {
            var userDTO = await UserService.GetUserById(userId);
            var reservationsDTO = reservationService.GetReservationsByUserId(userId);
            var ordersDto = orderService.GetOrdersByUserId(userId);
            var user = new UserModel();

            if (userDTO != null)
            {
                user = MapUserToUserModel(userDTO);

                AddReservationsToUser(user, reservationsDTO);
            }

            return user;
        }
        
        private void AddReservationsToUser(UserModel user, IEnumerable<ReservationTableDTO> reservationsDTO)
        {
            if (reservationsDTO != null)
            {
                user.Reservations = reservationsDTO.Select(r => MapReservationToViewModel(r)).ToList();
            }
        }
        private ReservationViewModel MapReservationToViewModel(ReservationTableDTO reservationDTO)
        {
            return new ReservationViewModel
            {
                Id = reservationDTO.Id,
                FirstName = reservationDTO.FirstName,
                LastName = reservationDTO.LastName,
                PhoneNumber = reservationDTO.PhoneNumber,
                Message = reservationDTO.Message,
                ReservationTime = reservationDTO.ReservationTime,
                ReservationDate = reservationDTO.ReservationDate,
            };
        }
      
        private void AddOrdersToUser(UserModel user, IEnumerable<OrderDTO> ordersDTO)
        {
            if (ordersDTO != null)
            {
                user.Orders = ordersDTO.Select(o => MapOrderToOrderViewModel(o)).ToList();
            }
        }

        private UserDTO GetAdminInfo()
        {
            return new UserDTO
            {
                Email = "drogomankatalin@gmail.com",
                UserName = "Catalin",
                Password = "1234",
                Name = "Application Admin",
                Address = "Studentilor 9/11",
                Role = "admin",
            };
        }
        private OrderViewModel MapOrderToOrderViewModel(OrderDTO orderDto)
        {
            return new OrderViewModel
            {
                Id = orderDto.Id,
                FirstName = orderDto.FirstName,
                LastName = orderDto.LastName,
                PhoneNumber = orderDto.Phone,
                Email = orderDto.Email,
                Address = orderDto.StreetAddress,
                Appartment = orderDto.Appartment,
                City = orderDto.City,
                Country = orderDto.Country,
                PostCode = orderDto.PostCode,
                BuyingTime = orderDto.BuyingTime,
                ApplicationUserId = orderDto.ApplicationUserId,
                TotalSumToPay = orderDto.TotalSumToPay,
                Items = orderDto.Items
            };
        }
        private async Task SetInitialDataAsync()
        {
            UserDTO adminUser = GetAdminInfo();
            await UserService.SetInitialData(adminUser, new List<string> { "user", "admin" });
        }
        private async Task<UserDTO> GetUserAdmin()
        {
            var userId = User.Identity.GetUserId();
            return await UserService.GetUserById(userId);
        }

        private UserModel MapUserToUserModel(UserDTO user)
        {
            return new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                UserName = user.UserName,
                Address = user.Address
            };
        }
    

      

        private async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            var usersDto = await UserService.GetAllUsers();
            if (usersDto == null)
                return Enumerable.Empty<UserModel>();

            var users = new List<UserModel>();
            foreach (var userDto in usersDto)
            {
                var userModel = MapUserToUserModel(userDto);
                users.Add(userModel);
            }
            return users;
        }

        private IEnumerable<OrderDTO> GetOrdersByUserId(string userId)
        {
            return orderService.GetOrdersByUserId(userId);
        }
        #endregion
    }

}