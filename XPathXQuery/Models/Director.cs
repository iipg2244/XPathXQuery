using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XPathXQuery.Models
{
    [XmlRoot(ElementName = "directors")]
    public class Director : Thing
    {
        public Director()
        {
        }

        public Director(Person p)
        {
            Id = p.Id;
            Name = p.Name;
        }

        public override bool Equals(object? obj)
        {
            return obj is Director director &&
                   Id == director.Id &&
                   Name == director.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }

}
