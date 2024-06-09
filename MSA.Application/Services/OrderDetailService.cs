using MSA.Application.IServices;
using MSA.Domain.Entities;
using MSA.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Application.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public void Add(OrderDetail orderDetail)
        {
            _orderDetailRepository.Add(orderDetail);
        }

        public void Update(OrderDetail orderDetail) 
        {
            _orderDetailRepository.Update(orderDetail);
        }

        public void Delete(OrderDetail orderDetail)
        {
            _orderDetailRepository.Delete(orderDetail);
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return _orderDetailRepository.GetAll();
        }

        public OrderDetail? GetById(Guid id)
        {
            return _orderDetailRepository.GetById(id);
        }

        public void Save()
        {
            _orderDetailRepository.Save();
        }
    }
}
