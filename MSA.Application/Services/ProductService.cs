using MSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductService _productService;

        public ProductService(IProductService productService)
        {
            _productService = productService;
        }

        public void Add(Product product)
        {
            _productService.Add(product);
        }

        public void Delete(Product product)
        {
            _productService.Delete(product);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productService.GetAll();
        }

        public Product? GetById(Guid id)
        {
            return _productService.GetById(id);
        }

        public void Save()
        {
            _productService.Save();
        }

        public IEnumerable<Product> SearchByName(string name)
        {
           return _productService.GetAll().Where(x => x.ProductName!.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void Update(Product product)
        {
            _productService.Update(product);
        }
    }
}
