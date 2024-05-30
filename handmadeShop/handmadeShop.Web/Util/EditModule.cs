using handmadeShop.BusinessLogic.Interfaces;
using handmadeShop.BusinessLogic.Services;
using Ninject.Modules;

namespace handmadeShop.Web.Util
{
    public class EditModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEditProductService>().To<EditProductService>();
        }
    }
}