using MSA.Domain.Entities;
using MSA.Infrastructure.DAOs;
using MSA.Application.IRepositories;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public void Add(Category category) => CategoryDAO.Instance.Add(category);

        public void Delete(Category category) => CategoryDAO.Instance.Delete(category);

        public IEnumerable<Category> GetAll() => CategoryDAO.Instance.GetAll();

        public Category? GetById(Guid id) => CategoryDAO.Instance.GetById(id);

        public void Save() => CategoryDAO.Instance.Save();

        public void Update(Category category) => CategoryDAO.Instance.Update(category);
    }
}
