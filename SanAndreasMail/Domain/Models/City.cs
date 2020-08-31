using System;

namespace SanAndreasMail.Domain
{
    /// <summary>
    /// Cities of state
    /// </summary>
    public class City
    {
        public Guid CityId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public override string ToString()
        {
            return "Nome: " + this.Name + " (" + this.Abbreviation + ")";
        }

    }


}
