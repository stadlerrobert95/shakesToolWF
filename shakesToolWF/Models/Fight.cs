namespace shakesToolWF.Models
{
    internal class Fight
    {
        public string name { get; set; }
        public string dungeonName { get; set; }
        public string pecentage { get; set; }

        public Fight(string name, string dungeonName, string pecentage)
        {
            this.name = name;
            this.dungeonName = dungeonName;
            this.pecentage = pecentage;
        }
    }
}
