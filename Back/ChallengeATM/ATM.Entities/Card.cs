﻿namespace ATM.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNumber {get;set;}
        public decimal Balance { get;set;}
        public string ExpireDate { get;set;}
    }
}
