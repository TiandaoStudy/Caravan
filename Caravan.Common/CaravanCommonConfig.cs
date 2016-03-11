using AutoMapper;

namespace Finsa.Caravan.Common
{
    public sealed class CaravanCommonConfig
    {
        public static MapperConfiguration Mapper { get; } = new MapperConfiguration(cfg => { });
    }
}
