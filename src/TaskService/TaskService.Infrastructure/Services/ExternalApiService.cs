using System.Text.Json;
using TaskService.Application.DTOs;
using TaskService.Application.Interface;

namespace TaskService.Infrastructure.Services
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient httpClient;

        public ExternalApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<ExternalTaskDto>> GetTasks()
        {
            var response = await httpClient.GetAsync("todos");

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<ExternalTaskDto>>(json);

            if (result is null)
            {
                return new List<ExternalTaskDto>();
            }

            return result;
        }
    }
}
