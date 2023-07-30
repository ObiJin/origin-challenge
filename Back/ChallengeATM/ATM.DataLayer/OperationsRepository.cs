using ATM.DataLayer.DbModel;
using ATM.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ATM.DataLayer
{
    public class OperationsRepository : IRepository<Operation>
    {
        private readonly challengeATMContext _challengeATMContext;

        public OperationsRepository(challengeATMContext context)
        {
            _challengeATMContext = context;
        }

        public Operation Create(Operation entity)
        {
            _challengeATMContext.Operations.Add(entity);

            _challengeATMContext.SaveChanges();
            return entity;
        }

        public Operation Delete(Operation entity)
        {
            _challengeATMContext.Operations.Remove(entity);
            _challengeATMContext.SaveChanges();

            return entity;
        }

        public List<Operation> Find(Expression<Func<Operation, bool>> searchParams)
        {
            return _challengeATMContext.Operations.Where(searchParams).ToList();
        }

        public Operation? Get(int id)
        {
            Operation? result = _challengeATMContext.Operations.Include(o => o.IdTarjetaNavigation).Where(c => c.Id == id).FirstOrDefault();
            return result;
        }

        public List<Operation> GetAll()
        {
            return _challengeATMContext.Operations.ToList();
        }

        public Operation Update(Operation entity)
        {
            _challengeATMContext.Operations.Update(entity);
            _challengeATMContext.SaveChanges();

            return entity;
        }
    }
}
