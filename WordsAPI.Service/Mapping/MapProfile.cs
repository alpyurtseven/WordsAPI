using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;

namespace WordsAPI.Service.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<English, WordDTO>().ReverseMap();
            CreateMap<Turkish, WordDTO>().ReverseMap();
            CreateMap<Turkish, string>()
        .ConvertUsing(x => x.Word);
            CreateMap<Category, string>()
       .ConvertUsing(x => x.Name);
        }
    }
}
