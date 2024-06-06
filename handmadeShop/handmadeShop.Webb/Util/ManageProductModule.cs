using handmadeShop.BusinessLogic.Interfaces;
using handmadeShop.BusinessLogic.Services;
using Ninject.Modules;

namespace handmadeShop.Web.Util
{
    public class ManageProductModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IManageProducts>().To<ManageProductService>();
        }
    }
}