using Application.Abstractions.Services;
using Application.Abstractions.Services.Models;
using Application.Abstractions.Services.Models.CustomerService;
using System.Xml.Serialization;

namespace Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
        private readonly XmlSerializer _xmlSerializer;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _xmlSerializer = new XmlSerializer(typeof(SoapEnvelope));
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(
            long customerId,
            CancellationToken cancellationToken = default)
        {
            // Build the exact endpoint URL specified by the external service
            var url = $"https://www.crcind.com/csp/samples/SOAP.Demo.cls?soap_method=FindPerson&id={customerId}";

            // Fetch the XML response efficiently using a stream
            using var response = await _httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

            // Deserialize the XML payload
            if (_xmlSerializer.Deserialize(stream) is not SoapEnvelope envelope)
            {
                return null;
            }

            // Return the customer DTO, or null if <FindPersonResult> is missing (id doesn't exist)
            return envelope.Body?.FindPersonResponse?.FindPersonResult;
        }
    }
}
