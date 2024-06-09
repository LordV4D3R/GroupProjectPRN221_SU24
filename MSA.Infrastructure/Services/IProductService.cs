using MSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IProductService
    {
        IEnumerable<Product> SearchByName(string name);
        IEnumerable<Product> GetAll();
        Product? GetById(Guid id);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        void Save();
    }
}
