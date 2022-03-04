using System;
using System.Collections.Generic;
using System.Linq;
using _1_MoviesAPI.Data;
using _1_MoviesAPI.Data.DTOs;
using _1_MoviesAPI.Models;
using AutoMapper;
using FluentResults;

namespace _1_MoviesAPI.Services
{
    public class CinemaService
    {
        private MovieContext _context;
        private IMapper _mapper;

        public CinemaService(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetCinemaDTO> GetCinemas(string movieTitle)
        {
            var foundCinemas = _context.Cinemas.ToList();
            if (foundCinemas == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(movieTitle))
            {
                var query = from cinema in foundCinemas
                            where cinema.Sessions.Any(session =>
                            session.Movie.Title == movieTitle)
                            select cinema;
                foundCinemas = query.ToList();

            }
            return _mapper.Map<List<GetCinemaDTO>>(foundCinemas);
        }

        public GetCinemaDTO GetCinemaById(int id)
        {
            var foundCinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (foundCinema != null)
            {
                var getCinemaDTO = _mapper.Map<GetCinemaDTO>(foundCinema);
                return getCinemaDTO;
            }

            return null;
        }

        public Models.Cinema CreateCinema(CreateCinemaDTO createCinemaDTO)
        {
            var newCinema = _mapper.Map<Cinema>(createCinemaDTO);

            _context.Cinemas.Add(newCinema);
            _context.SaveChanges();

            return newCinema;
        }

        public Result UpdateCinema(int id, UpdateCinemaDTO updateCinemaDTO)
        {
            var foundCinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (foundCinema != null)
            {
                _mapper.Map(updateCinemaDTO, foundCinema);
                _context.SaveChanges();

                return Result.Ok();
            }

            return Result.Fail("Cinema not found");
        }

        public Result DeleteCinema(int id)
        {
            var foundCinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (foundCinema != null)
            {
                _context.Remove(foundCinema);
                _context.SaveChanges();

                return Result.Ok();
            }

            return Result.Fail("Cinema not found");
        }
    }
}
