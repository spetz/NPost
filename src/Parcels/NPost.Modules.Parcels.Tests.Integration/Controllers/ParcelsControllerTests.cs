using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NPost.Modules.Parcels.Shared.Commands;
using NPost.Modules.Parcels.Shared.DTO;
using Shouldly;
using Xunit;

namespace NPost.Modules.Parcels.Tests.Integration.Controllers
{
    public class ParcelsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ParcelsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory.WithWebHostBuilder(b => b.UseEnvironment("test"));
        }

        [Fact]
        public async Task get_parcels_should_return_empty_collection()
        {
            var httpClient = _factory.CreateClient();

            var response = await httpClient.GetAsync("api/parcels");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var parcels = JsonConvert.DeserializeObject<IEnumerable<ParcelDto>>(content);
            parcels.ShouldBeEmpty();
        }
        
        [Fact]
        public async Task add_parcel_should_succeed()
        {
            var httpClient = _factory.CreateClient();
            var command = new AddParcel(Guid.Empty, "normal", "Test parcel", "Test address");
            var request = new StringContent(
                JsonConvert.SerializeObject(command), Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync("api/parcels", request);

            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            var parcelUrl = response.Headers.Location?.ToString();
            parcelUrl.ShouldNotBeEmpty();

            var getResponse = await httpClient.GetAsync(parcelUrl);

            getResponse.EnsureSuccessStatusCode();
            var content = await getResponse.Content.ReadAsStringAsync();
            var parcel = JsonConvert.DeserializeObject<ParcelDetailsDto>(content);
            parcel.ShouldNotBeNull();
            parcel.Size.ShouldBe(command.Size, StringCompareShould.IgnoreCase);
            parcel.Address.ShouldBe(command.Address);
            parcel.Name.ShouldBe(command.Name);
        }
    }
}