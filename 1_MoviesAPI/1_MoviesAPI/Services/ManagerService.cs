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
    public class ManagerService
    {
        private MovieContext _context;
        private IMapper _mapper;

        public ManagerService(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetManagerDTO> GetAllManagers()
        {
            var foundManagers = _context.Managers.ToList();

            return _mapper.Map<List<GetManagerDTO>>(foundManagers);
        }

        internal GetManagerDTO GetManagerById(int id)
        {
            var foundManager = _context.Managers.FirstOrDefault(manager => manager.Id == id);

            if (foundManager != null)
            {
                var getManagerDTO = _mapper.Map<GetManagerDTO>(foundManager);

                return getManagerDTO;
            }

            return null;
        }

        public Models.Manager CreateManager(CreateManagerDTO createManagerDTO)
        {
            var newManager = _mapper.Map<Manager>(createManagerDTO);

            _context.Add(newManager);
            _context.SaveChanges();

            return newManager;
        }

        public Result UpdateManager(int id, UpdateManagerDTO updateManagerDTO)
        {
            var foundManager = _context.Managers.FirstOrDefault(manager => manager.Id == id);

            if (foundManager != null)
            {
                _mapper.Map(updateManagerDTO, foundManager);
                var getManagerDTO = _mapper.Map<GetManagerDTO>(foundManager);

                _context.SaveChanges();

                return Result.Ok();
            }

            return Result.Fail("Manager not found");
        }

        public Result DeleteManager(int id)
        {
            var foundManager = _context.Managers.FirstOrDefault(manager => manager.Id == id);

            if (foundManager != null)
            {
                _context.Remove(foundManager);
                _context.SaveChanges();

                return Result.Ok();
            }

            return Result.Fail("Manager not found");
        }
    }
}
