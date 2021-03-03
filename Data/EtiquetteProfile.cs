using AutoMapper;
using WebApplicationAPI1.Data.Entities;
using WebApplicationAPI1.Models;

namespace WebApplicationAPI1.Data
{
    public class EtiquetteProfile : Profile
    {
        public EtiquetteProfile()
        {
            this.CreateMap<Etiquette, EtiquetteModel>().ReverseMap();
            this.CreateMap<Location, LocationModel>().ReverseMap();
        }
    }
}
