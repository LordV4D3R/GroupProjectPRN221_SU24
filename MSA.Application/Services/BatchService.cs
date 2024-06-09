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
    public class BatchService : IBatchService
    {
        private readonly IBatchRepository _batchRepository;
        public BatchService(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }

        public void Add(Batch batch)
        {
            _batchRepository.Add(batch);
        }

        public void Delete(Batch batch)
        {
            _batchRepository.Delete(batch);
        }

        public void Update(Batch batch)
        {
            _batchRepository.Update(batch);
        }

        public void Save()
        {
            _batchRepository.Save();
        }

        public IEnumerable<Batch> GetAll()
        {
            return _batchRepository.GetAll();
        }

        public Batch? GetById(Guid id)
        {
            return _batchRepository.GetById(id);
        }
    }
}
