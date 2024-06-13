using MSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Application.IRepositories
{
    public interface IOrderDetailRepository
    {
        void Add(OrderDetail orderDetail);
        void Delete(OrderDetail orderDetail);
        IEnumerable<OrderDetail> GetAll();
        OrderDetail? GetById(Guid id);
        void Save();
        void Update(OrderDetail orderDetail);
    }
}
