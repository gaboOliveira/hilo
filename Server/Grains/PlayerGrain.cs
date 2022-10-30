using Grains;
using Grains.States;
using Orleans;

namespace Server.Grains
{
    public class PlayerGrain : BaseGrain<PlayerGrain>, IPlayerGrain
    {
        public readonly PlayerState State;

        public PlayerGrain(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            State = new PlayerState();
        }

        public Task SetName(string name)
        {
            State.Name = name;
            return Task.CompletedTask;
        }

        public async Task<GameState> RequestGame()
        {
            var game = await GrainFactory.GetGrain<IServerGrain>(0).RequestGame(this.GetPrimaryKey());

            State.ActiveGame = game.Id;

            return game;
        }

        public Task<PlayerState> GetPlayer()
        {
            return Task.FromResult(State);
        }
    }
}