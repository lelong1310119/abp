using AutoMapper;
using Todo.Provinces;
using Todo.Districts;
using Todo.Communes;

namespace Todo;

public class TodoApplicationAutoMapperProfile : Profile
{
    public TodoApplicationAutoMapperProfile()
    {
        CreateMap<Province, ProvinceDto>();
        CreateMap<CreateUpdateProvinceDto, Province>();
        CreateMap<District, DistrictDto>();
        CreateMap<CreateUpdateDistrictDto, District>();
        CreateMap<Province, ProvinceLookupDto>();
        CreateMap<District, DistrictLookupDto>();
        CreateMap<Commune, CommuneDto>();
        CreateMap<CreateUpdateCommuneDto, Commune>();
    }
}
