using Grains;
using Grains.States;
using Orleans;

namespace Server.Grains
{
    public class GameGrain : BaseGrain<GameGrain>, IGameGrain
    {
        public readonly GameState State;

        public GameGrain(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            State = new GameState();
        }

        public override Task OnActivateAsync()
        {
            State.Id = this.GetPrimaryKey();
            State.Min = Random.Shared.Next(0, 1000);
            State.Max = Random.Shared.Next(State.Min, State.Min * 3);
            State.MisteryNumber = Random.Shared.Next(State.Min, State.Max);

            return base.OnActivateAsync();
        }

        public Task<GameState> GetGame()
        {
            return Task.FromResult(State);
        }
    }
}
