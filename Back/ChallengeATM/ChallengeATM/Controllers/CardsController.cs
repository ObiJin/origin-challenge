using ATM.Entities;
using ATM.Interfaces;
using ATM.LogicLayer.Interfaces;
using ChallengeATM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeATM.Controllers
{
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardsLogic _cardLogic;
        private readonly IOperationLogic _operationLogic;
        private readonly IMap<CardModel, Card> _mapper;

        public CardsController(ICardsLogic logic, IOperationLogic opLogic, IMap<CardModel, Card> mapper) {
            _cardLogic = logic;
            _mapper = mapper;
            _operationLogic = opLogic;
        }

        [HttpGet("cards/{number}")]
        public IActionResult GetByNumber([FromRoute] string number)
        {
            var card = _cardLogic.Find(new Card { CardNumber = number });

            if (card is null)
                return NotFound("La tarjeta indicada no es válida. Si cree que se debe a un error comuníquese con el 0800-888-4358.");

            return Ok(_mapper.Map(card));
        }

        [HttpPost("cards/block")]
        public IActionResult BlockCard([FromBody] LoginRequest request)
        {
            var card = _cardLogic.BlockCard(new Card { CardNumber = request.CardNumber });

            if (card is null)
                return NotFound("Ha ocurrido un error al obtener la tarjeta.");

            return Ok(_mapper.Map(card));
        }

        [HttpPost("/login")]
        public IActionResult ValidatePIN([FromBody] LoginRequest request)
        {
            var login = _cardLogic.Authenticate(request.CardNumber, request.PinNumber);

            if (login is null)
                return Unauthorized("El PIN ingresado no corresponde con la tarjeta.");

            return Ok(login);
        }

        [Authorize]
        [HttpGet("cards/balance/{number}")]
        public IActionResult GetBalance([FromRoute] string number)
        {
            var card = _cardLogic.Find(new Card { CardNumber = number });

            var operation = _operationLogic.Create(card, 0, DateTime.Now, "BLNCE");

            return Ok(card);
        }

        [Authorize]
        [HttpPost("cards/withdraw")]
        public IActionResult Withdraw([FromBody] WithdrawRequest request)
        {
            var card = _cardLogic.Find(new Card { CardNumber = request.CardNumber });

            if (card is null)
                return NotFound("Ocurrió un error al actualizar la tarjeta.");

            if (card.Balance < request.Amount)
                return ValidationProblem("No se puede extraer ese monto.");

            card = _cardLogic.Withdraw(request.CardNumber, request.Amount);

            var operation = _operationLogic.Create(card, request.Amount, DateTime.Now, "WTDRWL");

            return Ok(operation);
        }

        [Authorize]
        [HttpGet("operation/{id}")]
        public IActionResult GetOperation([FromRoute] int id)
        {
            var op = _operationLogic.Find(id);

            return Ok(op);
        }
    }
}
