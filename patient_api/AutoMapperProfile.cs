using AutoMapper;
using patient_api.Data.dto;
using patient_api.Data.Models;

namespace patient_api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Patient_dto, Patient>().ReverseMap();
            CreateMap<Address_dto, Address>().ReverseMap();
        }
    }
}
