using MSA.Application.IRepositories;
using MSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Add(Category category)
        {
            _categoryRepository.Add(category);
        }

        public void Delete(Category category)
        {
            _categoryRepository.Delete(category);
        }

        public IEnumerable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category? GetById(Guid id)
        {
            return _categoryRepository.GetById(id);
        }

        public void Save()
        {
            _categoryRepository.Save();
        }

        public IEnumerable<Category> SearchByName(string name)
        {
            return _categoryRepository.GetAll().Where(x => x.CategoryName!.Contains(name, StringComparison.OrdinalIgnoreCase) && x.IsDeleted == false).ToList();
        }

        public void Update(Category category)
        {
            _categoryRepository.Update(category);
        }
    }
}
