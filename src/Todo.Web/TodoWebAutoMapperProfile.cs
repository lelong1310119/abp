using AutoMapper;
using Todo.Provinces;
using Todo.Districts;
using Todo.Communes;

namespace Todo.Web;

public class TodoWebAutoMapperProfile : Profile
{
    public TodoWebAutoMapperProfile()
    {
        CreateMap<DistrictDto, CreateUpdateDistrictDto>();
        CreateMap<DistrictDto, ExportDistrictDto>();
        CreateMap<ProvinceDto, CreateUpdateProvinceDto>();
        CreateMap<ProvinceDto, ExportProvinceDto>();
        CreateMap<CommuneDto, CreateUpdateCommuneDto>();
        CreateMap<CommuneDto, ExportCommuneDto>();
        CreateMap<Pages.Districts.CreateModalModel.CreateDistrictViewModel, CreateUpdateDistrictDto>();
        CreateMap<DistrictDto, Pages.Districts.EditModalModel.EditDistrictViewModel>();
        CreateMap<Pages.Districts.EditModalModel.EditDistrictViewModel, CreateUpdateDistrictDto>();
        CreateMap<Pages.Communes.CreateModalModel.CreateCommuneViewModel, CreateUpdateCommuneDto>();
        CreateMap<CommuneDto, Pages.Communes.EditModalModel.EditCommuneViewModel>();
        CreateMap<Pages.Communes.EditModalModel.EditCommuneViewModel, CreateUpdateCommuneDto>();
    }
}
