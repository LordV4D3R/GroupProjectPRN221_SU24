using MSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Application.IServices
{
    public interface IBatchService
    {
        void Add(Batch batch);
        void Delete(Batch batch);
        void Update(Batch batch);
        void Save();
        IEnumerable<Batch> GetAll();
        Batch? GetById(Guid id);
    }
}
