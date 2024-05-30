using AutoMapper;
using handmadeShop.BusinessLogic.DTO;
using handmadeShop.BusinessLogic.Interfaces;
using handmadeShop.Domain.Entities;
using handmadeShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.BusinessLogic.Services
{
     public class EditProductService : IEditProductService
     {
          private IUnitOfWork DataBase { get; set; }
          public EditProductService(IUnitOfWork uow) { DataBase = uow; }

          public void UpdateProduct(ProductDTO productDto)
          {
               var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, Product>()).CreateMapper();
               var product = mapper.Map<ProductDTO, Product>(productDto);
               DataBase.Products.Update(product);
               DataBase.Save();
          }

          public void Dispose()
          {
               DataBase.Dispose();
          }
     }
}
