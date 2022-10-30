using System.Net.Mime;
using System.Text.Json;
using Grains;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/player")]
    public class PlayerController : Controller
    {
        private readonly IGrainFactory grainFactory;

        public PlayerController(IGrainFactory grainFactory)
        {
            this.grainFactory = grainFactory ?? throw new ArgumentNullException(nameof(grainFactory));
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> SetPlayerUP([FromBody] JsonElement bodyElement)
        {
            var newPlayerId = Guid.NewGuid();
            var nameIsProvided = bodyElement.TryGetProperty("name", out var name);

            if (nameIsProvided)
                await grainFactory.GetGrain<IPlayerGrain>(newPlayerId).SetName(name.ToString());

            return Ok(new PlayerViewModel
            {
                Id = newPlayerId,
                Name = name.ToString()
            });
        }

        [HttpGet("{playerId}/game")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RequestGame([FromRoute] Guid playerId)
        {
            var game = await grainFactory.GetGrain<IPlayerGrain>(playerId).RequestGame();

            return Ok(new GameViewModel
            {
                Min = game.Min,
                Max = game.Max,
            });
        }

        [HttpPost("{playerId}/attempt/{number}")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GameAttempt([FromRoute] Guid playerId, [FromRoute] int number)
        {
            var player = await grainFactory.GetGrain<IPlayerGrain>(playerId).GetPlayer();
            var result = await grainFactory.GetGrain<IServerGrain>(0).NewAttempt(playerId, player.ActiveGame!.Value, number);

            return Ok(new AttemptViewModel 
            { 
                CorrectAnswer = result.IsCompleted,
                Attempts = result.Attempts,
                IsGreater = result.IsGreater
            });
        }
    }
}
