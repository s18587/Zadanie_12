using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieApp.Server.Services;
using MovieApp.Shared.DTOs;
using System.Threading.Tasks;


namespace MovieApp.Server.Controllers
{
    [Authorize]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMoviesDbService _dbService;

        public MoviesController(ILogger<MoviesController> logger, IMoviesDbService dbService)
        {
            _logger = logger;
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            return Ok(await _dbService.GetMovies());
        }

        [HttpGet("{IdMovie}")]
        public async Task<IActionResult> GetMovieById(int IdMovie)
        {
            return Ok(await _dbService.GetMovieById(IdMovie));
        }

        [HttpPost]
        public async Task<IActionResult> PostMovie([FromBody] MovieDto movie)
        {
            await _dbService.AddMovie(movie);
            return StatusCode(201, "Movie was succesfully added");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMovie(int IdMovie)
        {
            var movieExists = await _dbService.DoesMovieExist(IdMovie);
            if (!movieExists)
                return StatusCode(404, "Movie with such Id is not found");

            await _dbService.DeleteMovie(IdMovie);
            return StatusCode(204);
        }

        [HttpPut("{IdMovie}")]
        public async Task<IActionResult> UpdateMovie([FromBody] MovieDto movie, int IdMovie)
        {
            var movieExists = await _dbService.DoesMovieExist(IdMovie);
            if (!movieExists)
                return StatusCode(404, "Movie with such Id is not found");

            await _dbService.UpdateMovie(movie, IdMovie);
            return StatusCode(204);
        }
    }
}