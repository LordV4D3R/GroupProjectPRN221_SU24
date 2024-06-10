using MSA.Domain.Entities;
using MSA.Infrastructure.DAOs;
using MSA.Infrastructure.IRepositories;


namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        public void Add(Product product) => ProductDAO.Instance.Add(product);

        public void Delete(Product product) => ProductDAO.Instance.Delete(product);

        public IEnumerable<Product> GetAll() => ProductDAO.Instance.GetAll();

        public Product GetById(Guid id) => ProductDAO.Instance.GetById(id);

        public void Save() => ProductDAO.Instance.Save();

        public void Update(Product product) => ProductDAO.Instance.Update(product);
    }
}
