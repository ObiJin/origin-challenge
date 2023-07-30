using ATM.DataLayer.DbModel;
using ATM.Interfaces;
using System.Linq.Expressions;

namespace ATM.DataLayer
{
    public class CardsRepository : IRepository<Card>
    {
        private readonly challengeATMContext _challengeATMContext;

        public CardsRepository(challengeATMContext context)
        {
            _challengeATMContext = context;
        }

        public Card Create(Card entity)
        {
            _challengeATMContext.Cards.Add(entity);

            _challengeATMContext.SaveChanges();
            return entity;
        }

        public Card Update(Card entity)
        {
            _challengeATMContext.Cards.Update(entity);
            _challengeATMContext.SaveChanges();

            return entity;
        }

        public Card Delete(Card entity)
        {
            _challengeATMContext.Cards.Remove(entity);
            _challengeATMContext.SaveChanges();

            return entity;
        }

        public Card? Get(int id)
        {
            Card? result = _challengeATMContext.Cards.Where(c => c.Id == id).FirstOrDefault();
            return result;
        }

        public List<Card> Find(Expression<Func<Card, bool>> searchParams)
        {
            return _challengeATMContext.Cards.Where(searchParams).ToList();
        }

        public List<Card> GetAll()
        {
            return _challengeATMContext.Cards.ToList();
        }
    }
}