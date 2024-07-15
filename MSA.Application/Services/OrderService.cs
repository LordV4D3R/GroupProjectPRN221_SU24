using MSA.Domain.Entities;
using MSA.Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSA.Application.IServices;

namespace MSA.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Add(Order order)
        {
            _orderRepository.Add(order);
        }

        public void Delete(Order order)
        {
            _orderRepository.Delete(order);
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public Order? GetById(Guid id)
        {
            return _orderRepository.GetById(id);
        }

        public void Update(Order order)
        {
            _orderRepository.Update(order);
        }

        public void Update2(Order order)
        {
            _orderRepository.Update(order);
        }

        public void Save()
        {
            _orderRepository.Save();
        }
        public Order GetOrderInCartStatus()
        {
            return _orderRepository.GetAll().FirstOrDefault(x => x.OrderStatus == Domain.Enums.OrderStatus.InCart);
        }

        public Order? GetOrderInCartStatusByAccountId(Guid id)
        {
            return _orderRepository.GetAll().FirstOrDefault(x => x.OrderStatus == Domain.Enums.OrderStatus.InCart && x.CustomerId == id);
        }

        public IEnumerable<Order> GetOrderCompletedAndCancelStatusByAccountId(Guid id)
        {
            return _orderRepository.GetAll().Where
                (x => x.OrderStatus == Domain.Enums.OrderStatus.Completed || x.OrderStatus == Domain.Enums.OrderStatus.Cancelled);
        }
        
    }
}
