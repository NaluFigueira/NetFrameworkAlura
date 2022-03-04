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
    public class SessionService
    {
        private MovieContext _context;
        private IMapper _mapper;

        public SessionService(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetSessionDTO> GetSessions()
        {
            var foundSessions = _context.Sessions.ToList();

            if(foundSessions != null) return _mapper.Map<List<GetSessionDTO>>(foundSessions);

            return null;
        }

        public GetSessionDTO GetSessionById(int id)
        {
            var session = _context.Sessions.FirstOrDefault(session => session.Id == id);

            if (session != null)
            {
                var getSessionDTO = _mapper.Map<GetSessionDTO>(session);
                return getSessionDTO;
            }

            return null;
        }

        public Session CreateSession(CreateSessionDTO createSessionDTO)
        {
            var sessionMovie = _context.Movies.FirstOrDefault(movie => movie.Id == createSessionDTO.MovieID);
            var sessionCinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == createSessionDTO.CinemaID);

            if (sessionMovie != null && sessionCinema != null)
            {

                var newSession = _mapper.Map<Session>(createSessionDTO);
                newSession.EndTime = newSession.StartTime.AddMinutes(sessionMovie.Duration);

                _context.Sessions.Add(newSession);
                _context.SaveChanges();

                return newSession;
            }

            return null;
        }

        public Result UpdateSession(int id, UpdateSessionDTO updateSessionDTO)
        {
            var foundSession = _context.Sessions.FirstOrDefault(session => session.Id == id);

            if (foundSession != null)
            {
                _mapper.Map(updateSessionDTO, foundSession);
                _context.SaveChanges();

                return Result.Ok();
            }

            return Result.Fail("Session not found");
        }

        public Result DeleteSession(int id)
        {
            var session = _context.Sessions.FirstOrDefault(session => session.Id == id);

            if (session != null)
            {
                _context.Remove(session);
                _context.SaveChanges();

                return Result.Ok();
            }

            return Result.Fail("Session not found");
        }
    }
}
