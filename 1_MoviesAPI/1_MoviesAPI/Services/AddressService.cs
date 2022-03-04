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
    public class AddressService
    {
        private MovieContext _context;
        private IMapper _mapper;

        public AddressService(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetAddressDTO> GetAddresses()
        {
            var foundAddresses = _context.Addresses.ToList();

            if(foundAddresses != null)
            {
                return _mapper.Map<List<GetAddressDTO>>(foundAddresses);
            }

            return null;
        }

        public GetAddressDTO GetAddressById(int id)
        {
            var foundAddress = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (foundAddress != null)
            {
                var getAddressDTO = _mapper.Map<GetAddressDTO>(foundAddress);
                return getAddressDTO;
            }

            return null;
        }

        public Address CreateAddress(CreateAddressDTO createAddressDTO)
        {
            var newAddress = _mapper.Map<Address>(createAddressDTO);

            _context.Addresses.Add(newAddress);
            _context.SaveChanges();

            return newAddress;
        }

        public Result UpdateAddress(int id, UpdateAddressDTO updateAddressDTO)
        {
            var foundAddress = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (foundAddress != null)
            {
                _mapper.Map(updateAddressDTO, foundAddress);
                _context.SaveChanges();

                return Result.Ok();
            }

            return Result.Fail("Address not found");
        }

        public Result DeleteAddress(int id)
        {
            var foundAddress = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (foundAddress != null)
            {
                _context.Remove(foundAddress);
                _context.SaveChanges();

                return Result.Ok();
            }

            return Result.Fail("Address not found");
        }
    }
}
