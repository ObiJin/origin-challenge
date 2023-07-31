using ATM.Entities;
using ATM.Interfaces;

namespace ATM.LogicLayer.Mappers
{
    public class OperationsMapper : IMap<Operation, DataLayer.DbModel.Operation>
    {
        public Operation? Map(DataLayer.DbModel.Operation source)
        {
            return new Operation
            {
                Id = source.Id,
                Amount = source.Amount,
                CardNumber = source.IdTarjetaNavigation.Number,
                Balance = source.IdTarjetaNavigation.Balance,
                Date = source.Date
            };
        }

        public DataLayer.DbModel.Operation? Map(Operation source)
        {
            return new DataLayer.DbModel.Operation
            {
                Id = source.Id,
                Amount = source.Amount,
                Code = source.Code,
                Date = source.Date,
            };
        }
    }
}
