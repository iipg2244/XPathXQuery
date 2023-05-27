using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XPathXQuery.Views;

namespace XPathXQuery.Models
{
    //[XmlRoot(ElementName = "movie")] && [XmlRoot(ElementName = "person")]
    public class Thing : IId, IName
    {
        [XmlElement(ElementName = "id")]
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        [XmlElement(ElementName = "_id")]
        public string _id { get; set; } = "";
        [XmlElement(ElementName = "name")]
        public string Name { get; set; } = "";

        public Thing()
        {
        }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Thing thing &&
                   Id == thing.Id &&
                   _id == thing._id &&
                   Name == thing.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, _id, Name);
        }
    }
}
