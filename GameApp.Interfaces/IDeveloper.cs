namespace Wojtalak_Szczerkowski.GameApp.Interfaces
{
    public interface IDeveloper
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Country { get; set; }


        IDeveloper CopyFrom(IDeveloper developer)
        {
            Id = developer.Id;
            Name = developer.Name;
            Country = developer.Country;

            return this;
        }
    }
}