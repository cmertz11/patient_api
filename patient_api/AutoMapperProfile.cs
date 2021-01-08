using AutoMapper;
using patient_api.Data.dto;
using patient_api.Data.Models;
using System.Collections;
using System.Collections.Generic;

namespace patient_api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Patient_dto, Patient>().ReverseMap();
            //CreateMap<List<Patient_dto>, List<Patient>>().ReverseMap();
        }
    }
}
