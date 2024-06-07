using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using handmadeShop.Web.Models;
using handmadeShop.BusinessLogic.DTO;
using handmadeShop.BusinessLogic.Interfaces;
using handmadeShop.Web.Filters;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace handmadeShop.Web.Controllers
{




    [AllowAnonymous]
    [SessionTimeout]
    public class HomeController : Controller
    {

      
        private ICartService cartService;
        private IReservationService reservationService;
        public HomeController(ICartService _cartService, IReservationService reservationService)
        {
            cartService = _cartService;
            this.reservationService = reservationService;
        }


        [AllowAnonymous]
        public ActionResult Index()
        {
            LoadProductsIntoViewBag();

            var reservation = new ReservationViewModel();


            return View(reservation);
        }
        [Authorize]
        [SessionTimeout]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeReservation(ReservationViewModel model)
        {

         
            if(!ModelState.IsValid)
            {

                LoadProductsIntoViewBag();
                return View("Index",model);
            }
            if (Request.IsAuthenticated)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ReservationViewModel, ReservationTableDTO>()).CreateMapper();
                var ReservationDTO = mapper.Map<ReservationViewModel, ReservationTableDTO>(model);
                ReservationDTO.ApplicationUserId = User.Identity.GetUserId();

                reservationService.MakeReservation(ReservationDTO);

                return RedirectToAction("SuccessfulReservation");

            }
            return RedirectToAction("Login", "Account");
        }
        [Authorize]
        public ActionResult SuccessfulReservation()
        {
            var reservationDTO = reservationService.GetReservationsByUserId(User.Identity.GetUserId()).LastOrDefault();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ReservationTableDTO, ReservationViewModel>()).CreateMapper();
            var Reservation= mapper.Map<ReservationTableDTO, ReservationViewModel>(reservationDTO);
            return View(Reservation);
        }
        public ActionResult About()
        {
            ViewBag.CurrentAction = "About";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.CurrentAction = "Contact";

            return View();
        }
        public ActionResult Menu()
        {
            ViewBag.CurrentAction = "Menu";
            LoadProductsIntoViewBag();

            return View();
        }
        public ActionResult Services()
        {
            ViewBag.CurrentAction = "Services";

            return View();
        }
        public ActionResult Blog()
        {
            ViewBag.CurrentAction = "Blog";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            reservationService.Dispose();
            cartService.Dispose();

            base.Dispose(disposing);
        }
        private void LoadProductsIntoViewBag()
        {
            IEnumerable<ProductDTO> GetProducts(string category)
            {
                return cartService.GetProducts(category);
            }

            List<ProductModel> MapProducts(IEnumerable<ProductDTO> productDTOs)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductModel>()).CreateMapper();
                return mapper.Map<IEnumerable<ProductDTO>, List<ProductModel>>(productDTOs);
            }

            var jucariiHandmadeDTO = GetProducts("Jucării handmade");
            var paturiciHandmadeDTO = GetProducts("Păturică handmade");
            var botoseiDTO = GetProducts("Botoșei");
            var gentuteHandmadeDTO = GetProducts("Gentuțe handmade");
            var bijuteriiHandmadeDTO = GetProducts("Bijuterii handmade");

            ViewBag.JucariiHandmade = MapProducts(jucariiHandmadeDTO);
            ViewBag.PaturiciHandmade = MapProducts(paturiciHandmadeDTO);
            ViewBag.Botosei = MapProducts(botoseiDTO);
            ViewBag.GentuteHandmade = MapProducts(gentuteHandmadeDTO);
            ViewBag.BijuteriiHandmade = MapProducts(bijuteriiHandmadeDTO);

        }

    }
}