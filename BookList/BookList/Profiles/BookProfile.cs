using System;
using AutoMapper;
using BookList.Data.DTOs;
using BookList.Models;

namespace BookList.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<CreateBookDTO, Book>();
            CreateMap<UpdateBookDTO, Book>();
        }
    }
}
