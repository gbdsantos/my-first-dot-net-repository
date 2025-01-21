using GitHubBlipAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GitHubBlipAPI.Models; 

namespace GitHubBlipAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GitHubController : ControllerBase
    {
        private readonly IGitHubService _gitHubService;

        // Construtor com injeção de dependência
        public GitHubController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        
        [HttpGet("repositories")]
        public async Task<IActionResult> GetRepositories()
        {
            var repositories = await _gitHubService.GetOldestCSharpRepositoriesAsync();
            return Ok(repositories);
        }

       
        [HttpGet("repositories/latest")]
        public async Task<IActionResult> GetLatestRepositories()
        {
           
            List<RepositoryModel> repositories = await _gitHubService.GetLatestRepositoriesAsync();


            return Ok(repositories);
        }
    }
} 
