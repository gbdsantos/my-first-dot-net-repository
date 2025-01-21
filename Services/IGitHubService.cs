using GitHubBlipAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHubBlipAPI.Services
{
    public interface IGitHubService
    {
        Task<List<RepositoryModel>> GetLatestRepositoriesAsync(); 
        Task<List<RepositoryModel>> GetOldestCSharpRepositoriesAsync();
    }
}
