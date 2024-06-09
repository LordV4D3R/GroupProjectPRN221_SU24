using MSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Application.IServices
{
    public interface IOrderService
    {
        void Add(Order order);
        void Delete(Order order);
        IEnumerable<Order> GetAll();
        Order? GetById(Guid id);
        void Update(Order order);
        void Save();
    }
}
