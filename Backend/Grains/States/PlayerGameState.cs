namespace Grains.States
{
    public class PlayerGameState
    {
        public Guid GameId { get; set; }
        public int Attempts { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsGreater { get; set; }
    }
}
