﻿using MSA.Domain.Entities;
using MSA.Infrastructure.DAOs;
using MSA.Application.IRepositories;


namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        public void Add(Product product) => ProductDAO.Instance.Add(product);

        public void Delete(Product product) => ProductDAO.Instance.Delete(product);

        public IEnumerable<Product> GetAll(string include = "") => ProductDAO.Instance.GetAll(include);


		public Product GetById(Guid id) => ProductDAO.Instance.GetById(id);

        public void Save() => ProductDAO.Instance.Save();

        public void Update(Product product) => ProductDAO.Instance.Update(product);
        public void Update2(Product product) => ProductDAO.Instance.Update2(product, GetById(product.Id));
    }
}
