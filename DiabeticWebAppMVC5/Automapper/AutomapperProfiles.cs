using ApiInfrastructure.Models;
using AutoMapper;
using DiabeticWebAppMVC5.Models;

namespace DiabeticWebAppMVC5.Automapper
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            this.CreateMap<LoginApiModel, LoginViewModel>();
            this.CreateMap<MeasurementApiModel, MeasurementViewModel>();
        }
    }
}