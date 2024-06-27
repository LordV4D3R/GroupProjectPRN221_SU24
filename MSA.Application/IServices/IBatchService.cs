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
        void Update2(Batch batch);
        void Save();
        IEnumerable<Batch> GetAll();
        IEnumerable<Batch> GetAllByProductId(Guid id);
        Batch? GetById(Guid id);
    }
}
