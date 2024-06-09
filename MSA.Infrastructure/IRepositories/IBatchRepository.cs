using MSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Infrastructure.IRepositories
{
    public interface IBatchRepository
    {
        void Add(Batch batch);
        void Delete(Batch batch);
        IEnumerable<Batch> GetAll();
        Batch? GetById(Guid id);
        void Save();
        void Update(Batch batch);
    }
}
