using System.Xml.Serialization;

namespace Application.Abstractions.Services.Models.CustomerService
{
    public class FindPersonResponse
    {
        [XmlElement(ElementName = "FindPersonResult", Namespace = "http://tempuri.org")]
        public CustomerDto? FindPersonResult { get; set; }
    }

    public class CustomerDto
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; } = string.Empty;

        [XmlElement(ElementName = "SSN")]
        public string Ssn { get; set; } = string.Empty;

        [XmlElement(ElementName = "DOB")]
        public string Dob { get; set; } = string.Empty;

        [XmlElement(ElementName = "Age")]
        public int Age { get; set; }

        [XmlElement(ElementName = "Home")]
        public Address? Home { get; set; }

        [XmlElement(ElementName = "Office")]
        public Address? Office { get; set; }

        [XmlArray(ElementName = "FavoriteColors")]
        [XmlArrayItem(ElementName = "FavoriteColorsItem")]
        public List<string> FavoriteColors { get; set; } = new();
    }

    public class Address
    {
        [XmlElement(ElementName = "Street")]
        public string Street { get; set; } = string.Empty;

        [XmlElement(ElementName = "City")]
        public string City { get; set; } = string.Empty;

        [XmlElement(ElementName = "State")]
        public string State { get; set; } = string.Empty;

        [XmlElement(ElementName = "Zip")]
        public string Zip { get; set; } = string.Empty;
    }

}
