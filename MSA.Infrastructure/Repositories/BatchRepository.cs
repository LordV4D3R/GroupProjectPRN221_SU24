using MSA.Application.IRepositories;
using MSA.Domain.Entities;
using MSA.Domain.Enums;
using MSA.Infrastructure.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Infrastructure.Repositories
{
    public class BatchRepository : IBatchRepository
    {
        public void Add(Batch batch) => BatchDAO.Instance.Add(batch);

        public void Delete(Batch batch) => BatchDAO.Instance.Delete(batch);

        public IEnumerable<Batch> GetAll() => BatchDAO.Instance.GetAll();

        public IEnumerable<Batch> GetAllByProductId(Guid id) => BatchDAO.Instance.GetAll().Where(b => b.ProductId == id && b.Status == BatchStatus.Active && b.ExpOn >= DateTime.Now);

        public Batch? GetById(Guid id) => BatchDAO.Instance.GetById(id);

        public void Save() => BatchDAO.Instance.Save();

        public void Update(Batch batch) => BatchDAO.Instance.Update(batch);

        public void Update2(Batch batch) => BatchDAO.Instance.Update2(batch, GetById(batch.Id));
    }
}
