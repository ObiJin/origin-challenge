using ATM.Entities;
using ATM.Interfaces;
using ATM.LogicLayer.Interfaces;

namespace ATM.LogicLayer
{
    public class OperationsLogic : IOperationLogic
    {
        private readonly IRepository<DataLayer.DbModel.Operation> _operationsRepository;
        //private readonly IRepository<DataLayer.DbModel.Card> _cardsRepository;
        private readonly IMap<Operation, DataLayer.DbModel.Operation> _mapper;

        public OperationsLogic(IRepository<DataLayer.DbModel.Operation> repo, IMap<Operation, DataLayer.DbModel.Operation> mapper)
        {
            _operationsRepository = repo;
            _mapper = mapper;
        }

        public Operation Create(Card card, decimal amount, DateTime date, string code)
        {
            var operation = new Operation
            {
                Amount = amount,
                Balance = card.Balance,
                CardNumber = card.CardNumber,
                Date = date,
                Code = code,
            };
            var toAdd = _mapper.Map(operation);
            toAdd.IdTarjeta = card.Id;

            _operationsRepository.Create(toAdd);

            operation.Id = toAdd.Id;

            return operation;
        }

        public Operation? Find(Operation param)
        {
            var dbOperation = _operationsRepository.Find(o => param.CardNumber == o.IdTarjetaNavigation.Number && o.Date == param.Date).FirstOrDefault();

            return _mapper.Map(dbOperation);
        }

        public Operation? Find(int id)
        {
            var dbOperation = _operationsRepository.Get(id);

            return _mapper.Map(dbOperation);
        }
    }
}
