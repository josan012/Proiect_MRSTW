using AutoMapper;
using handmadeShop.BusinessLogic.DTO;
using handmadeShop.BusinessLogic.Interfaces;
using handmadeShop.Domain.Entities;
using handmadeShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.BusinessLogic.Services
{
    public class ManageProductService : IManageProducts
    {
        private IUnitOfWork DataBase;
        public ManageProductService(IUnitOfWork DataBase) { this.DataBase = DataBase; }

        public void AddProduct(ProductDTO productDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, Product>()).CreateMapper();
            var product = mapper.Map<ProductDTO, Product>(productDTO);
            DataBase.Products.Create(product,product.Category);
            DataBase.Save();
        }

        public void DeleteProduct(int productId)
        {
            var product = DataBase.Products.GetProduct(productId); 
         
            DataBase.Products.Delete(productId,product.Category);
            DataBase.Save();
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }

        public void UpdateProduct(ProductDTO productDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, Product>()).CreateMapper();
            var product = mapper.Map<ProductDTO, Product>(productDTO);
            DataBase.Products.Update(product);
            DataBase.Save();
        }
    }
}
