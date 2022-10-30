using Grains;
using Grains.States;

namespace Server.Grains
{
    public class ServerGrain : BaseGrain<ServerGrain>, IServerGrain
    {
        public readonly ServerState State;

        public ServerGrain(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            State = new ServerState();
        }

        public async Task<GameState> RequestGame(Guid playerId)
        {
            if (!State.PlayerGameList.ContainsKey(playerId))
                State.PlayerGameList.Add(playerId, new List<PlayerGameState>());

            var openGame = State.PlayerGameList[playerId].FirstOrDefault(item => !item.IsCompleted);
            if (openGame != null)
            {
                return await GrainFactory.GetGrain<IGameGrain>(openGame.GameId).GetGame();
            }
            else
            {
                var newGameId = Guid.NewGuid();
                var game = await GrainFactory.GetGrain<IGameGrain>(newGameId).GetGame();
                State.PlayerGameList[playerId].Add(new PlayerGameState
                {
                    Attempts = 0,
                    GameId = game.Id,
                    IsCompleted = false
                });
                return game;
            }
        }

        public async Task<PlayerGameState> NewAttempt(Guid playerId, Guid gameId, int misteryNumber)
        {
            if (!State.PlayerGameList.ContainsKey(playerId)) throw new InvalidOperationException("Invalid player");

            var playerGame = State.PlayerGameList[playerId].FirstOrDefault(item => item.GameId == gameId);
            if (playerGame == null) throw new InvalidOperationException("Game hasn't been requested by this player");

            var game = await GrainFactory.GetGrain<IGameGrain>(playerGame.GameId).GetGame();

            playerGame.Attempts++;
            playerGame.IsCompleted = game.MisteryNumber == misteryNumber;
            playerGame.IsGreater = game.MisteryNumber > misteryNumber;
            return playerGame;
        }
    }
}
