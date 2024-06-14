using MSA.Domain.Entities;
using MSA.Infrastructure.DAOs;
using MSA.Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public void Add(Order order) => OrderDAO.Instance.Add(order);

        public void Delete(Order order) => OrderDAO.Instance.Delete(order);

        public IEnumerable<Order> GetAll() => OrderDAO.Instance.GetAll();

        public Order? GetById(Guid id) => OrderDAO.Instance.GetById(id);

        public void Save() => OrderDAO.Instance.Save();

        public void Update(Order order) => OrderDAO.Instance.Update(order);
    }
}
