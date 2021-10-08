using AutoMapper;
using SkullTribalIntrusionServer.Infrastructure.Entities;
using SkullTribalIntrusionServer.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkullTribalIntrusionServer.ServerSystems
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<PlayerEntity, PlayerModel>().ReverseMap();
        }
    }
}
