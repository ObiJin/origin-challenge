namespace ATM.Entities
{
    public class Operation
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public DateTime Date { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Balance { get; set; }
        public string Code { get; set; }    
    }
}
