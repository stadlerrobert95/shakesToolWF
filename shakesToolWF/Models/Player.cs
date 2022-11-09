namespace shakesToolWF.Models
{
    internal class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Fight[] bosses { get; set; }
    }
}
