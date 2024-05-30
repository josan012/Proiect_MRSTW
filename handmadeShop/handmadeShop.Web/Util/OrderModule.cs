using handmadeShop.BusinessLogic.Interfaces;
using handmadeShop.BusinessLogic.Services;
using Ninject.Modules;

namespace handmadeShop.Web.Util
{
    public class OrderModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOrderService>().To<OrderService>();
        }
    }
}