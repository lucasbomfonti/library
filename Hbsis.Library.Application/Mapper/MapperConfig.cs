namespace Hbsis.Library.Application.Mapper
{
    public class MapperConfig
    {
        private static bool _isMapped;

        public static void RegisterMappings()
        {
            if (_isMapped)
                return;

            _isMapped = true;
            AutoMapper.Mapper.Initialize(mapper =>
            {
                mapper.AddProfile<MapperViewModel2Domain>();
                mapper.AddProfile<Domain2Dto>();
            });
        }
    }
}