using handmadeShop.BusinessLogic.Interfaces;
using handmadeShop.BusinessLogic.Services;
using Ninject.Modules;

namespace handmadeShop.Web.Util
{
    public class ReservationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IReservationService>().To<ReservationService>();
        }
    }
}