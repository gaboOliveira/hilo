using Grains.States;
using Orleans;

namespace Grains
{
    public interface IServerGrain : IGrainWithIntegerKey
    {
        Task<PlayerGameState> NewAttempt(Guid playerId, Guid gameId, int misteryNumber);
        Task<GameState> RequestGame(Guid playerId);
    }
}
