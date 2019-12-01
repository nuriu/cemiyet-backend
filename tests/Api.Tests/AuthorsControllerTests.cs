using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cemiyet.Api.Tests.Extensions;
using Cemiyet.Application.Authors.Commands.DeleteMany;
using Cemiyet.Core.Entities;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Cemiyet.Api.Tests
{
    public class AuthorsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public AuthorsControllerTests(WebApplicationFactory<Startup> webApplicationFactory)
        {
            using var scope = webApplicationFactory.Services.GetService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDataContext>();

            AppDataContextSeed.Seed(context);

            _httpClient = webApplicationFactory.CreateClient();
        }

        [Fact]
        public async Task Add_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.PostAsJsonAsync("authors/", default(Author));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            response = await _httpClient.PostAsJsonAsync("authors/", new Author
            {
                Name = "",
                Surname = "V3L1"
            });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Add_WithCorrectData_ShouldReturn_OK()
        {
            var response = await _httpClient.PostAsJsonAsync("authors/", new Author
            {
                Name = "Yazar",
                Surname = "Veli"
            });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task List_WithoutCorrectPaging_ShouldReturn_BadRequest()
        {
            await _httpClient.AssertedGetAsync("authors?page=-1&pageSize=-5", HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task List_WithoutPaging_ShouldReturn_DefaultPagedResult()
        {
            await _httpClient.AssertedGetEntityListFromUri<AuthorViewModel>("authors");
        }

        [Fact]
        public async Task ListBooks_WithoutCorrectPaging_ShouldReturn_BadRequest()
        {
            var authors = await _httpClient.AssertedGetEntityListFromUri<AuthorViewModel>("authors");
            await _httpClient.AssertedGetAsync($"authors/{authors.First().Id}/books?page=-1&pageSize=-5", HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ListBooks_WithoutPaging_ShouldReturn_DefaultPagedResult()
        {
            var authors = await _httpClient.AssertedGetEntityListFromUri<AuthorViewModel>("authors");
            await _httpClient.AssertedGetAsync($"authors/{authors.First().Id}/books", HttpStatusCode.OK);
        }

        [Fact]
        public async Task Details_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            await _httpClient.AssertedGetAsync($"authors/{Guid.NewGuid()}", HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Details_WithCorrectId_ShouldReturn_AuthorObject()
        {
            var authors = await _httpClient.AssertedGetEntityListFromUri<AuthorViewModel>("authors");
            var response = await _httpClient.AssertedGetAsync($"authors/{authors.First().Id}", HttpStatusCode.OK);
            var responseData = await response.Content.ReadAsAsync<AuthorViewModel>();
            Assert.NotNull(responseData);
        }

        [Fact]
        public async Task UpdatePartially_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var authors = await _httpClient.AssertedGetEntityListFromUri<AuthorViewModel>("authors");
            var response = await _httpClient.SendRequestMessageAsync(HttpMethod.Patch, $"authors/{authors.First().Id}", new { });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePartially_WithCorrectData_ShouldReturn_OK()
        {
            var authors = await _httpClient.AssertedGetEntityListFromUri<AuthorViewModel>("authors");
            var response = await _httpClient.SendRequestMessageAsync(HttpMethod.Patch, $"authors/{authors.First().Id}", new
            {
                Name = "YAZAR"
            });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.PutAsJsonAsync($"authors/{Guid.NewGuid()}", default(Author));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var authors = await _httpClient.AssertedGetEntityListFromUri<AuthorViewModel>("authors");

            var response = await _httpClient.PutAsJsonAsync($"authors/{authors.First().Id}", default(Author));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            response = await _httpClient.PutAsJsonAsync($"authors/{authors.First().Id}", new Author
            {
                Name = "5",
                Surname = null,
                Bio = default
            });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithCorrectData_ShouldReturn_OK()
        {
            var authors = await _httpClient.AssertedGetEntityListFromUri<AuthorViewModel>("authors");
            var response = await _httpClient.PutAsJsonAsync($"authors/{authors.Last().Id}", new
            {
                Name = "Name",
                Surname = "Surname",
                Bio = "Bio"
            });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOne_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.DeleteAsync($"authors/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOne_WithCorrectId_ShouldReturn_OK()
        {
            var authors = await _httpClient.AssertedGetEntityListFromUri<AuthorViewModel>("authors");
            var response = await _httpClient.DeleteAsync($"authors/{authors.Last().Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMany_WithoutCorrectIds_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.SendRequestMessageAsync(HttpMethod.Delete, "authors", new []
            {
                Guid.NewGuid().ToString(), Guid.NewGuid().ToString()
            });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMany_WithCorrectIds_ShouldReturn_OK()
        {
            var authors = await _httpClient.AssertedGetEntityListFromUri<AuthorViewModel>("authors");
            var response = await _httpClient.SendRequestMessageAsync(HttpMethod.Delete, "authors", new DeleteManyCommand
            {
                Ids = authors.TakeLast(2).Select(g => g.Id).ToArray()
            });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
