using AutoMapper;
using SupermarketApp.Models;
using SupermarketApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketApp.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<AppUser, LoginVM>().ReverseMap();
            CreateMap<AppUser, CreateSAVM>().ReverseMap();
            CreateMap<Product, ProductVM>().ReverseMap();
            CreateMap<Cart, CartVM>().ReverseMap();
        }
    }
}

