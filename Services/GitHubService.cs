using GitHubBlipAPI.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace GitHubBlipAPI.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _httpClient;

        public GitHubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Este método não está implementado ainda
        public Task<List<RepositoryModel>> GetLatestRepositoriesAsync()
        {
            throw new NotImplementedException();
        }

        // Método para buscar os repositórios mais antigos escritos em C#
        public async Task<List<RepositoryModel>> GetOldestCSharpRepositoriesAsync()
        {
            var url = "https://api.github.com/users/takenet/repos";
            
            // Configura o cabeçalho da requisição para evitar problemas de autenticação
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "GitHubBlipAPI");

            // Envia a requisição para o GitHub e verifica se a resposta foi bem-sucedida
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Lê o conteúdo da resposta e converte para JSON
            var json = await response.Content.ReadAsStringAsync();
            var repos = JArray.Parse(json);

            // Filtra repositórios em C#, ordena por data de criação e retorna os 5 mais antigos
            return repos
                .Where(repo => repo["language"]?.ToString() == "C#") // Pega apenas repositórios C#
                .OrderBy(repo => repo["created_at"]?.ToObject<DateTime>()) // Ordena por data de criação
                .Take(5) // Seleciona os 5 mais antigos
                .Select(repo => new RepositoryModel
                {
                    FullName = repo["full_name"]?.ToString() ?? string.Empty, // Nome completo do repositório
                    Description = repo["description"]?.ToString() ?? string.Empty, // Descrição do repositório
                    AvatarUrl = repo["owner"]?["avatar_url"]?.ToString() ?? string.Empty, // Avatar do proprietário
                    Language = repo["language"]?.ToString() ?? string.Empty // Linguagem usada
                })
                .ToList();
        }
    }
}
