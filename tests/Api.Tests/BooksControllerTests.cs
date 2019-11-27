using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cemiyet.Core.Entities;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace Cemiyet.Api.Tests
{
    public class BooksControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public BooksControllerTests(WebApplicationFactory<Startup> webApplicationFactory)
        {
            using var scope = webApplicationFactory.Services.GetService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDataContext>();

            AppDataContextSeed.Seed(context);

            _httpClient = webApplicationFactory.CreateClient();
        }

        [Fact]
        public async Task List_WithoutCorrectPaging_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.GetAsync("books?page=-1&pageSize=-5");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task List_WithoutPaging_ShouldReturn_DefaultPagedResult()
        {
            var response = await _httpClient.GetAsync("books");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseData = await response.Content.ReadAsAsync<List<BookViewModel>>();
            Assert.NotEmpty(responseData);
        }

        [Fact]
        public async Task ListEdition_WithoutCorrectPaging_ShouldReturn_BadRequest()
        {
            var booksResponse = await _httpClient.GetAsync("books");
            Assert.Equal(HttpStatusCode.OK, booksResponse.StatusCode);

            var books = await booksResponse.Content.ReadAsAsync<List<BookViewModel>>();
            Assert.NotNull(books);

            var response = await _httpClient.GetAsync($"books/{books.First().Id}/editions?page=-1&pageSize=-5");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ListEdition_WithoutPaging_ShouldReturn_DefaultPagedResult()
        {
            var booksResponse = await _httpClient.GetAsync("books");
            Assert.Equal(HttpStatusCode.OK, booksResponse.StatusCode);

            var books = await booksResponse.Content.ReadAsAsync<List<BookViewModel>>();
            Assert.NotNull(books);

            var response = await _httpClient.GetAsync($"books/{books.First().Id}/editions");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseData = await response.Content.ReadAsAsync<List<BookEditionViewModel>>();
            Assert.NotEmpty(responseData);
        }

        [Fact]
        public async Task Details_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.GetAsync($"books/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Details_WithCorrectId_ShouldReturn_BookObject()
        {
            var booksResponse = await _httpClient.GetAsync("books");
            Assert.Equal(HttpStatusCode.OK, booksResponse.StatusCode);

            var books = await booksResponse.Content.ReadAsAsync<List<BookViewModel>>();
            Assert.NotNull(books);

            var response = await _httpClient.GetAsync($"books/{books.First().Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseData = await response.Content.ReadAsAsync<BookViewModel>();
            Assert.NotNull(responseData);
        }

        [Fact]
        public async Task DetailsEdition_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.GetAsync($"books/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DetailsEdition_WithCorrectId_ShouldReturn_BookEditionObject()
        {
            var booksResponse = await _httpClient.GetAsync("books");
            Assert.Equal(HttpStatusCode.OK, booksResponse.StatusCode);

            var books = await booksResponse.Content.ReadAsAsync<List<BookViewModel>>();
            Assert.NotNull(books);

            var editionsResponse = await _httpClient.GetAsync($"books/{books.First().Id}/editions");
            Assert.Equal(HttpStatusCode.OK, editionsResponse.StatusCode);

            var editions = await editionsResponse.Content.ReadAsAsync<List<BookEditionViewModel>>();
            Assert.NotNull(editions);

            var response = await _httpClient.GetAsync($"books/{books.First().Id}/editions/{editions.First().Isbn}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseData = await response.Content.ReadAsAsync<BookEditionViewModel>();
            Assert.NotNull(responseData);
        }
    }
}