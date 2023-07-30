namespace ChallengeATM.Models
{
    public class WithdrawRequest
    {
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
    }
}
