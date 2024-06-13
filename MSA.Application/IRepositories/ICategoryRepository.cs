using MSA.Domain.Entities;

namespace MSA.Application.IRepositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category GetById(Guid id);
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
        void Save();
    }
}
