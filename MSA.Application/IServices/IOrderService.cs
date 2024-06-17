using MSA.Domain.Entities;

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
