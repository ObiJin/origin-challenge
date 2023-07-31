namespace ATM.DataLayer.DbModel
{
    public partial class Card
    {
        public Card()
        {
            Operations = new HashSet<Operation>();
        }

        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public string Pin { get; set; } = null!;
        public DateTime ExpireDate { get; set; }
        public bool IsBlocked { get; set; }
        public decimal Balance { get; set; }

        public virtual ICollection<Operation> Operations { get; set; }
    }
}
