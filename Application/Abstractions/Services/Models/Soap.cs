using Application.Abstractions.Services.Models.CustomerService;
using System.Xml.Serialization;

namespace Application.Abstractions.Services.Models
{
    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SoapEnvelope
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public SoapBody Body { get; set; } = new();
    }

    public class SoapBody
    {
        [XmlElement(ElementName = "FindPersonResponse", Namespace = "http://tempuri.org")]
        public FindPersonResponse? FindPersonResponse { get; set; }
    }
}
