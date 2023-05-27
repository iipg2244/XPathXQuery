using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace XPathXQuery.Models
{
    [XmlRoot(ElementName = "actors")]
    public class Actor : Thing
    {
        public Actor()
        {
        }

        public Actor(Person p)
        {
            Id = p.Id;
            Name = p.Name;
        }

        public override bool Equals(object? obj)
        {
            return obj is Actor actor &&
                   Id == actor.Id &&
                   Name == actor.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }

}
