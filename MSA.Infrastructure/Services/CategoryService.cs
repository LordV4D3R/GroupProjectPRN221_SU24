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
        private readonly ICategoryService _categoryService;

        public CategoryService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public void Add(Category category)
        {
            _categoryService.Add(category);
        }

        public void Delete(Category category)
        {
            _categoryService.Delete(category);
        }

        public IEnumerable<Category> GetAll()
        {
            return _categoryService.GetAll();
        }

        public Category? GetById(Guid id)
        {
            return _categoryService.GetById(id);
        }

        public void Save()
        {
            _categoryService.Save();
        }

        public IEnumerable<Category> SearchByName(string name)
        {
            return _categoryService.GetAll().Where(x => x.CategoryName!.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void Update(Category category)
        {
            _categoryService.Update(category);
        }
    }
}
