using MagicVilla_API.Models.DTO;

namespace MagicVilla_API.Datos
{
    public static class VillaStore
    {
        public static List<VillaDTO> VillaList = new List<VillaDTO>
        {
            new VillaDTO{Id=1,Name="villa capcana",Occupants=5,squaremeter=50 },
            new VillaDTO{Id=2,Name="villa bonao",Occupants=10,squaremeter=100 },

        };
    }
}
