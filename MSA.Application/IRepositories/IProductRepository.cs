using MSA.Domain.Entities;

namespace MSA.Application.IRepositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(Guid id);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        void Save();
    }
}
