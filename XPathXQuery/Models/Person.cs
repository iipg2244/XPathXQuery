using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XPathXQuery.Models
{
    [XmlRoot(ElementName = "person")]
    public class Person : Thing
    {
        [XmlElement(ElementName = "dob")]
        public string Dob { get; set; } = "";
        [XmlElement(ElementName = "pob")]
        public string Pob { get; set; } = "";
        [XmlElement(ElementName = "hasActed")]
        public bool HasActed { get; set; } = false;
        [XmlElement(ElementName = "hasDirected")]
        public bool HasDirected { get; set; } = false;

        public Person()
        {
        }

        public Person(string id, string name, string dob, string pob, bool hasActed, bool hasDirected)
        {
            Id = id;
            Name = name;
            Dob = dob;
            Pob = pob;
            HasActed = hasActed;
            HasDirected = hasDirected;
        }

        public Person(Actor a)
        {
            Id = a.Id;
            Name = a.Name;
        }

        public Person(Director d)
        {
            Id = d.Id;
            Name = d.Name;
        }

        public override bool Equals(object? obj)
        {
            return obj is Person person &&
                   Id == person.Id &&
                   Name == person.Name &&
                   Dob == person.Dob &&
                   Pob == person.Pob &&
                   HasActed == person.HasActed &&
                   HasDirected == person.HasDirected;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Dob, Pob, HasActed, HasDirected);
        }
    }

}
