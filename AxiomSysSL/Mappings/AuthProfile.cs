using AutoMapper;
using AxiomSysBL.IServices.IAuthServices;
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
