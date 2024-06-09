﻿using MSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Infrastructure.IRepositories
{
    public interface IOrderRepository
    {
        void Add(Order order);
        void Delete(Order order);
        IEnumerable<Order> GetAll();
        Order? GetById(Guid id);
        void Save();
        void Update(Order order);
    }
}
