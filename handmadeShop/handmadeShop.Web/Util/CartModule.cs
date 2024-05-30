using handmadeShop.BusinessLogic.Interfaces;
using handmadeShop.BusinessLogic.Services;
using Ninject.Modules;

namespace handmadeShop.Web.Util
{
    public class CartModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICartService>().To<CartService>();
        }
    }
}