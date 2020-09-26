using System;
using System.Threading.Tasks;
using Controllers.Console;
using Controllers.Console.DTO.Parameters;
using Infrastructure.Authentication;
using Infrastructure.Authentication.Authenticators;
using Infrastructure.Console.DTO;

namespace Infrastructure.Console
{
    public class ConsoleApi
    {
        private IAuthenticator Authenticator;

        public ConsoleApi(IAuthenticator authenticator)
        {
            this.Authenticator = authenticator;
        }

        public async Task<IControllerResult> Run(IController controller, ControllerParameters parameters) {
            var result = await controller.Run(parameters);
            return result;
        }

        public async Task<IControllerResult> Run(IAuthenticatedController controller, ControllerParameters parameters, ConsoleUser? user) {
            if (!(this.Authenticator is BasicAuth))
                throw new Exception("Unsuportted authenticator for ConsoleAPI");
            
            if (user == null)
                throw new Exception("Must be authenticated");
            
            var auth = (BasicAuth) this.Authenticator;
            auth.SetCredentials(user.Username, user.Password);
            
            var authenticatedUser = await auth.Authenticate();
            if (authenticatedUser == null)
                throw new Exception("Invalid username/passowrd");
            
            var result = await controller.Run(authenticatedUser, parameters);;
            return result;
        }
    }
}