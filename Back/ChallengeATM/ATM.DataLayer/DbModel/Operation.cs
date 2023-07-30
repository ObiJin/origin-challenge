using System;
using System.Collections.Generic;

namespace ATM.DataLayer.DbModel
{
    public partial class Operation
    {
        public int Id { get; set; }
        public int IdTarjeta { get; set; }
        public DateTime Date { get; set; }
        public string? Code { get; set; }
        public decimal? Amount { get; set; }

        public virtual Card IdTarjetaNavigation { get; set; } = null!;
    }
}
