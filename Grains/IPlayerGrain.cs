using Grains.States;
using Orleans;

namespace Grains
{
    public interface IPlayerGrain : IGrainWithGuidKey
    {
        Task<GameState> RequestGame();
        Task SetName(string name);
        Task<PlayerState> GetPlayer();
    }
}
