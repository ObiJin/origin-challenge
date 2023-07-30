using ATM.Entities;
using ATM.LogicLayer.Interfaces;
using ChallengeATM.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeATM.Controllers
{
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardsLogic _cardLogic;

        public CardsController(ICardsLogic logic) {
            _cardLogic = logic;
        }

        [HttpGet("cards/{number}")]
        public IActionResult GetByNumber([FromRoute] string number)
        {
            var card = _cardLogic.Find(new Card { CardNumber = number });

            if (card is null)
                return NotFound("La tarjeta indicada no es válida. Si cree que se debe a un error comuníquese con el 0800-888-4358.");

            return Ok(card);
        }

        [HttpPost("cards/block")]
        public IActionResult BlockCard([FromBody] LoginRequest request)
        {
            var card = _cardLogic.BlockCard(new Card { CardNumber = request.CardNumber });

            if (card is null)
                return NotFound("Ha ocurrido un error al obtener la tarjeta.");

            return Ok(card);
        }

        [HttpPost("/login")]
        public IActionResult ValidatePIN([FromBody] LoginRequest request)
        {
            var login = _cardLogic.Authenticate(request.CardNumber, request.PinNumber);

            if (login is null)
                return Unauthorized("El PIN ingresado no corresponde con la tarjeta.");

            return Ok(login);
        }
    }
}
