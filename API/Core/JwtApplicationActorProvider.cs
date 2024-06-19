using Application;
using Implementation;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace API.Core
{
    public class JwtApplicationActorProvider : IApplicationActorProvider
    {
        private string authorizationHeader;
        private readonly ITokenStorage _tokenStorage;

        public JwtApplicationActorProvider(string authorizationHeader, ITokenStorage storage)
        {
            this.authorizationHeader = authorizationHeader;
            _tokenStorage = storage;
        }

        public IApplicationActor GetActor()
        {
            if (authorizationHeader.Split("Bearer ").Length != 2)
            {
                return new UnathorizedActor();
            }

            string token = authorizationHeader.Split("Bearer ")[1];

            var handler = new JwtSecurityTokenHandler();

            var tokenObj = handler.ReadJwtToken(token);

            var claims = tokenObj.Claims;

            //var jtiClaim = claims.FirstOrDefault(x => x.Type == "jti")?.Value;
            Guid guid = new Guid(claims.First(x => x.Type == "jti").Value);

            if (!_tokenStorage.Exists(guid))
            {
                return new UnathorizedActor();
            }

            //if (string.IsNullOrEmpty(jtiClaim) || !Guid.TryParse(jtiClaim, out Guid jtiGuid))
            //{
            //    return new UnathorizedActor();
            //}

            //if (!_tokenStorage.Exists(jtiGuid))
            //{
            //    return new UnathorizedActor();
            //}


            var claim = claims.First(x => x.Type == "jti").Value;

            var actor = new Actor
            {
                Email = claims.First(x => x.Type == "Username").Value,
                Username = claims.First(x => x.Type == "Username").Value,
                FirstName = claims.First(x => x.Type == "FirstName").Value,
                LastName = claims.First(x => x.Type == "LastName").Value,
                Id = int.Parse(claims.First(x => x.Type == "Id").Value),
                AllowedUseCases = JsonConvert.DeserializeObject<List<int>>(claims.First(x => x.Type == "UseCaseIds").Value)
            };

            return actor;
        }
    }
}
