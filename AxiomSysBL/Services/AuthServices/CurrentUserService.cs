using AxiomSysBL.IServices.IAuthServices;


namespace AxiomSysBL.Services.AuthServices
{
    public class CurrentUserService : ICurrentUserService
    {

        public string? UserId { get; private set; }
        public CurrentUserService()
        {
            // read the "Id" claim of the JWT and set the UserId property


        }
    }
}
