using EndProj_Books.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace EndProj_Books.Services
{
    public class BookService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BookService> _logger;

        public BookService(HttpClient httpClient, ILogger<BookService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<OpenLibraryBook?> GetBookDetailsByISBNAsync(string isbn)
        {
            try
            {
                var url = $"https://openlibrary.org/api/books?bibkeys=ISBN:{isbn}&jscmd=data&format=json";
                _logger.LogInformation($"Requesting Open Library API with URL: {url}");
                var response = await _httpClient.GetStringAsync(url);
                _logger.LogInformation($"Open Library API response: {response}");

                var data = JObject.Parse(response);
                var bookData = data[$"ISBN:{isbn}"];

                if (bookData == null)
                {
                    _logger.LogWarning($"No data found for ISBN: {isbn}");
                    return null;
                }

                var book = new OpenLibraryBook
                {
                    Title = (string?)bookData["title"],
                    Author = bookData["authors"]?.FirstOrDefault()?["name"]?.ToString(),
                    Genre = bookData["subjects"]?.FirstOrDefault()?["name"]?.ToString(),
                    Pages = (int?)bookData["number_of_pages"] ?? 0,
                    PublishDate = (string?)bookData["publish_date"]
                };

                _logger.LogInformation($"Deserialized Open Library API response: {book}");
                return book;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching book details.");
                return null;
            }
        }
    }
}
