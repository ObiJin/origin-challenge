﻿using ATM.Entities;
using ATM.Entities.Config;
using ATM.Interfaces;
using ATM.LogicLayer.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ATM.LogicLayer
{
    public class CardsLogic : ICardsLogic
    {
        private readonly IRepository<DataLayer.DbModel.Card> _cardRepository;
        private readonly IMap<Card, DataLayer.DbModel.Card> _cardsMapper;
        private readonly AppSettings _appSettings;

        public CardsLogic(IRepository<DataLayer.DbModel.Card> repo, IMap<Card, DataLayer.DbModel.Card> cardsMapper, IOptions<AppSettings> appSettings)
        {
            _cardRepository = repo;
            _cardsMapper = cardsMapper;
            _appSettings = appSettings.Value;
        }

        public List<Card> AllCards()
        {
            var cards = _cardRepository.GetAll().Select(c => _cardsMapper.Map(c)).ToList();
            return cards;
        }

        public Login? Authenticate(string username, string password)
        {
            var dbCard = _cardRepository.Find(c => c.Pin == password && c.Number == username && !c.IsBlocked).FirstOrDefault();

            Card? card = _cardsMapper.Map(dbCard);

            if (card != null)
            {
                byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                int expiryDuration = _appSettings.ExpiryDuration;

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("cardNumber", card.CardNumber)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                    Issuer = _appSettings.Issuer,
                    Audience = _appSettings.Audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                string finalToken = tokenHandler.WriteToken(token);

                return new Login { UserName = card.CardNumber, Token = finalToken };
            }

            return null;
        }

        public Card? BlockCard(Card card)
        {
            var dbCard = _cardRepository.Find(c => c.Number == card.CardNumber).FirstOrDefault();

            if (dbCard is not null)
            {
                dbCard.IsBlocked = true;
                _cardRepository.Update(dbCard);
            }

            return _cardsMapper.Map(dbCard);
        }

        public Card? Find(Card toFind)
        {
            var dbCard = _cardRepository.Find(c => c.Number == toFind.CardNumber && !c.IsBlocked).FirstOrDefault();

            Card? card = dbCard is not null ? _cardsMapper.Map(dbCard) : null;

            return card;
        }

        public Card? Find(int id)
        {
            var dbCard = _cardRepository.Get(id);

            return _cardsMapper.Map(dbCard);
        }

        public Card? Withdraw(string number, decimal amount)
        {
            var dbCard = _cardRepository.Find(c => c.Number == number && !c.IsBlocked).First();

            dbCard.Balance -= amount;
            _cardRepository.Update(dbCard);

            return _cardsMapper.Map(dbCard);
        }
    }
}