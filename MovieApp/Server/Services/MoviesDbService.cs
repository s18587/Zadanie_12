using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Data;
using MovieApp.Shared.DTOs;
using MovieApp.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Server.Services
{
    public interface IMoviesRepository
    {

    }

    public interface IMoviesDbService
    {
        Task<List<Movie>> GetMovies();
        Task AddMovie(MovieDto movieDto);
        Task<Movie> GetMovieById(int IdMovie);
        Task DeleteMovie(int IdMovie);
        Task<bool> DoesMovieExist(int IdMovie);
        Task UpdateMovie(MovieDto movie, int IdMovie);
    }

    public class MoviesDbService : IMoviesDbService
    {
        private ApplicationDbContext _context;

        public MoviesDbService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddMovie(MovieDto movieDto)
        {
            Movie movie = new Movie
            {
                Title = movieDto.Title,
                Summary = movieDto.Summary,
                InTheaters = movieDto.InTheaters,
                ReleaseDate = movieDto.ReleaseDate,
                Poster = movieDto.Poster
            };

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieById(int IdMovie)
        {
            return await _context.Movies.Where(m => m.Id == IdMovie).SingleAsync();
        }

        public async Task<List<Movie>> GetMovies()
        {
            return await _context.Movies.OrderBy(m => m.Title).ToListAsync();
        }

        public async Task DeleteMovie(int IdMovie)
        {
            var movie = await _context.Movies
                .Where(m => m.Id == IdMovie)
                .SingleAsync();

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DoesMovieExist(int IdMovie)
        {
            var exists = await _context.Movies
                .Where(m => m.Id == IdMovie)
                .AnyAsync();
            return exists;
        }

        public async Task UpdateMovie(MovieDto movieDto, int IdMovie)
        {
            var movie = await _context.Movies.Where(m => m.Id == IdMovie).SingleAsync();

            movie.Title = movieDto.Title;
            movie.Summary = movieDto.Summary;
            movie.InTheaters = movieDto.InTheaters;
            movie.ReleaseDate = movieDto.ReleaseDate;
            movie.Poster = movieDto.Poster;

            await _context.SaveChangesAsync();
        }
    }
}