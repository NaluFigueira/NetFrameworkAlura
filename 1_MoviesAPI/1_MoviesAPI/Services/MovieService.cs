using System;
using System.Collections.Generic;
using System.Linq;
using _1_MoviesAPI.Data;
using _1_MoviesAPI.Data.DTOs;
using AutoMapper;
using FluentResults;
using MoviesAPI.Models;

namespace _1_MoviesAPI.Services
{
    public class MovieService
    {
        private MovieContext _context;
        private IMapper _mapper;

        public MovieService(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetMovieDTO> GetMovies(Movie.MovieGenre? genre)
        {
            List<Movie> foundMovies;

            if (genre != null)
            {
                foundMovies = _context.Movies.Where(movie => movie.Genre == genre).ToList();
            }
            else
            {
                foundMovies = _context.Movies.ToList();
            }

            if (foundMovies != null)
            {
                List<GetMovieDTO> getMovieDTOs = _mapper.Map<List<GetMovieDTO>>(foundMovies);

                return getMovieDTOs;
            }

            return null;
        }

        public GetMovieDTO GetMovieById(int id)
        {
            var foundMovie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (foundMovie != null)
            {
                var getMovieDTO = _mapper.Map<GetMovieDTO>(foundMovie);
                return getMovieDTO;
            }

            return null;
        }

        public GetMovieDTO CreateMovie(CreateMovieDTO createMovieDTO)
        {
            var newMovie = _mapper.Map<Movie>(createMovieDTO);

            _context.Movies.Add(newMovie);
            _context.SaveChanges();

            return _mapper.Map<GetMovieDTO>(newMovie);
        }

        public Result UpdateMovie(int id, UpdateMovieDTO updateMovieDTO)
        {
            var foundMovie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (foundMovie != null)
            {
                _mapper.Map(updateMovieDTO, foundMovie);
                _context.SaveChanges();

                return Result.Ok();
            }

            return Result.Fail("Movie not found");
        }

        public Result DeleteMovie(int id)
        {
            var foundMovie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (foundMovie != null)
            {
                _context.Remove(foundMovie);
                _context.SaveChanges();
                return Result.Ok();
            }

            return Result.Fail("Movie not found");
        }
    }
}
