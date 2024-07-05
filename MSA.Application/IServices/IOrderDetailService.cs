using MSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Application.IServices
{
    public interface IOrderDetailService
    {
        void Add(OrderDetail orderDetail);
        void Update(OrderDetail orderDetail);
        void Delete(OrderDetail orderDetail);
        IEnumerable<OrderDetail> GetAll();
        OrderDetail? GetById(Guid id);
        void Save();
        IEnumerable<OrderDetail> GetAllOrderDetailOrderId(Guid orderId);
    }
}
