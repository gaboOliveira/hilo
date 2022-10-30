using Grains.States;
using Orleans;

namespace Grains
{
    public interface IGameGrain : IGrainWithGuidKey
    {
        Task<GameState> GetGame();
    }
}
