using AutoMapper;
using BankApi.Models;
using BankApi.Models.DTO;
using BankApi.Profiles.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Profiles
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<RegisterAccDTO, Account>();
            CreateMap<UpdateAccDTO, Account>();
            CreateMap<Account, GetAccountDTO>();
        }

    }
}
