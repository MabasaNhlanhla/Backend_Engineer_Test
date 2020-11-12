using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string Birth_Year { get; set; }
        public string Eye_Color { get; set; }
        public string Gender { get; set; }
        public string Hair_Color { get; set; }
        public string Mass { get; set; }
        public string Skin_Color { get; set; }
        public string Homeworld { get; set; }
        public ICollection<string> Films { get; set; }
        public ICollection<string> Species { get; set; }
        public ICollection<string> Starships { get; set; }
        public ICollection<string> Vehicles { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }

    }
}
