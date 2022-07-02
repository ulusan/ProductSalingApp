using AutoMapper;
using ProductSaling.Core.DTOs;
using ProductSaling.Data;

namespace ProductSaling.Core.Configurations
{
    /// <summary>
    /// Automapper is a simple library for mapping 2 objects together. 
    /// It allows us to get rid of complex codes and save time.
    /// In other words, we can define it as a library that
    /// we use to map our ViewModels and Entity Models.
    /// </summary>
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<ApiUser, UserDTO>().ReverseMap();
        }
    }
}
