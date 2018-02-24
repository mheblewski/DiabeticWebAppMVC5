using AutoMapper;

namespace DiabeticWebAppMVC5.Automapper
{
    public static class AutomapperConfiguration
    {
        public static IMapper Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<AutomapperProfiles>();
            });

            return Mapper.Instance;
        }
    }
}