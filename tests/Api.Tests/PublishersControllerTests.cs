using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cemiyet.Api.Tests.Extensions;
using Cemiyet.Application.Publishers.Commands.DeleteMany;
using Cemiyet.Core.Entities;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Cemiyet.Api.Tests
{
    public class PublishersControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public PublishersControllerTests(WebApplicationFactory<Startup> webApplicationFactory)
        {
            using var scope = webApplicationFactory.Services.GetService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDataContext>();

            AppDataContextSeed.Seed(context);

            _httpClient = webApplicationFactory.CreateClient();
        }

        [Fact]
        public async Task Add_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.PostAsJsonAsync("publishers/", default(Publisher));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            response = await _httpClient.PostAsJsonAsync("publishers/", new Publisher
            {
                Name = ""
            });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Add_WithCorrectData_ShouldReturn_OK()
        {
            var response = await _httpClient.PostAsJsonAsync("publishers/", new Publisher
            {
                Name = "Yayıncılık",
                Description = "Veli"
            });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task List_WithoutCorrectPaging_ShouldReturn_BadRequest()
        {
            await _httpClient.AssertedGetAsync("publishers?page=-1&pageSize=-5", HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task List_WithoutPaging_ShouldReturn_DefaultPagedResult()
        {
            await _httpClient.AssertedGetEntityListFromUri<PublisherViewModel>("publishers");
        }

        [Fact]
        public async Task ListBooks_WithoutCorrectPaging_ShouldReturn_BadRequest()
        {
            var publishers = await _httpClient.AssertedGetEntityListFromUri<PublisherViewModel>("publishers");
            await _httpClient.AssertedGetAsync($"publishers/{publishers.First().Id}/books?page=-1&pageSize=-5", HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ListBooks_WithoutPaging_ShouldReturn_DefaultPagedResult()
        {
            var publishers = await _httpClient.AssertedGetEntityListFromUri<PublisherViewModel>("publishers");
            await _httpClient.AssertedGetAsync($"publishers/{publishers.First().Id}/books", HttpStatusCode.OK);
        }

        [Fact]
        public async Task Details_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            await _httpClient.AssertedGetAsync($"publishers/{Guid.NewGuid()}", HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Details_WithCorrectId_ShouldReturn_PublisherObject()
        {
            var publishers = await _httpClient.AssertedGetEntityListFromUri<PublisherViewModel>("publishers");
            var response = await _httpClient.AssertedGetAsync($"publishers/{publishers.First().Id}", HttpStatusCode.OK);
            var responseData = await response.Content.ReadAsAsync<PublisherViewModel>();
            Assert.NotNull(responseData);
        }

        [Fact]
        public async Task UpdatePartially_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var publishers = await _httpClient.AssertedGetEntityListFromUri<PublisherViewModel>("publishers");
            await _httpClient.AssertedSendRequestMessageAsync(HttpMethod.Patch, $"publishers/{publishers.First().Id}",
                                                              new { }, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdatePartially_WithCorrectData_ShouldReturn_OK()
        {
            var publishers = await _httpClient.AssertedGetEntityListFromUri<PublisherViewModel>("publishers");
            await _httpClient.AssertedSendRequestMessageAsync(HttpMethod.Patch, $"publishers/{publishers.First().Id}", new
            {
                Name = "YAYINEVİ BİR"
            }, HttpStatusCode.OK);
        }

        [Fact]
        public async Task Update_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.PutAsJsonAsync($"publishers/{Guid.NewGuid()}", default(Publisher));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var publishers = await _httpClient.AssertedGetEntityListFromUri<PublisherViewModel>("publishers");
            var response = await _httpClient.PutAsJsonAsync($"publishers/{publishers.First().Id}", default(Publisher));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            response = await _httpClient.PutAsJsonAsync($"publishers/{publishers.First().Id}", new Publisher
            {
                Name = "5",
                Description = null
            });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithCorrectData_ShouldReturn_OK()
        {
            var publishers = await _httpClient.AssertedGetEntityListFromUri<PublisherViewModel>("publishers");
            var response = await _httpClient.PutAsJsonAsync($"publishers/{publishers.Last().Id}", new
            {
                Name = "YKY",
                Description = "ABCDEFGH"
            });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOne_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.DeleteAsync($"publishers/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOne_WithCorrectId_ShouldReturn_OK()
        {
            var publishers = await _httpClient.AssertedGetEntityListFromUri<PublisherViewModel>("publishers");
            var response = await _httpClient.DeleteAsync($"publishers/{publishers.Last().Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMany_WithoutCorrectIds_ShouldReturn_BadRequest()
        {
            await _httpClient.AssertedSendRequestMessageAsync(HttpMethod.Delete, "publishers", new []
            {
                Guid.NewGuid().ToString(), Guid.NewGuid().ToString()
            }, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteMany_WithCorrectIds_ShouldReturn_OK()
        {
            var publishers = await _httpClient.AssertedGetEntityListFromUri<PublisherViewModel>("publishers");
            await _httpClient.AssertedSendRequestMessageAsync(HttpMethod.Delete, "publishers", new DeleteManyCommand
            {
                Ids = publishers.TakeLast(2).Select(g => g.Id).ToArray()
            }, HttpStatusCode.OK);
        }
    }
}
