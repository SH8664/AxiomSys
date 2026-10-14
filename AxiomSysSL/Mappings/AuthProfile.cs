using AutoMapper;
using AxiomSysBL.Models.Auth;
using Microsoft.AspNetCore.Identity;
namespace AxiomSysSL.Mappings
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<UserRegisterDto, IdentityUser>();
        }
    }
}
