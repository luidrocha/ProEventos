using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Domain.Identity;

namespace ProEventos.API.Helpers
{
    // profile vem de AutoMaper

    public class ProEventosProfile : Profile

    {
        // metodo que faz o mapeamento da Classe do dominio para o Dtos
        public ProEventosProfile()
        {
            CreateMap<Evento, EventoDto>().ReverseMap(); // Acrescentando ReverseMap() faz os 2 sentidos
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteDto>().ReverseMap();
            
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();


             // CreateMap<EventoDto, Evento>(); faria o mapeamento reverso
        }
    }
}
