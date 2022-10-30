namespace Grains.States
{
    public class ServerState
    {
        public readonly IDictionary<Guid, IList<PlayerGameState>> PlayerGameList;
        public readonly IList<Guid> GameList;

        public ServerState()
        {
            PlayerGameList = new Dictionary<Guid, IList<PlayerGameState>>();
            GameList = new List<Guid>();
        }
    }
}
