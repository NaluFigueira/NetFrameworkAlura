using System;
using AutoMapper;
using BookList.Data.DTOs;
using BookList.Models;

namespace BookList.Profiles
{
    public class ListProfile : Profile
    {
        public ListProfile()
        {
            CreateMap<CreateListDTO, List>();
            CreateMap<UpdateListDTO, List>();
        }
    }
}
