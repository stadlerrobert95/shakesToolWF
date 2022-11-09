namespace shakesToolWF.Models
{
    internal class Fight
    {
        string name { get; set; }
        string dungeonName { get; set; }
        public string pecentage { get; set; }

        public Fight(string name, string dungeonName, string pecentage)
        {
            this.name = name;
            this.dungeonName = dungeonName;
            this.pecentage = pecentage;
        }
    }
}
