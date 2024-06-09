using MSA.Domain.Entities;
using MSA.Infrastructure.DAOs;
using MSA.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Infrastructure.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void Add(OrderDetail orderDetail) => OrderDetailDAO.Instance.Add(orderDetail);

        public void Delete(OrderDetail orderDetail) => OrderDetailDAO.Instance.Delete(orderDetail);

        public IEnumerable<OrderDetail> GetAll() => OrderDetailDAO.Instance.GetAll();

        public OrderDetail? GetById(Guid id) => OrderDetailDAO.Instance.GetById(id);

        public void Save() => OrderDetailDAO.Instance.Save();

        public void Update(OrderDetail orderDetail) => OrderDetailDAO.Instance.Update(orderDetail);
    }
}
