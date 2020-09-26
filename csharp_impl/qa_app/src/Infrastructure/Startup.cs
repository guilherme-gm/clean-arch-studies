using Autofac;
using Controllers.Console;
using Domain.UseCases;
using Infrastructure.Authentication;
using Infrastructure.Authentication.Authenticators;
using Infrastructure.Authentication.PasswordHashers;
using Infrastructure.Console;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Mysql;
using qa_app;

namespace Infrastructure
{
    public static class Startup
    {
        public static IContainer Init()
        {
            System.Console.WriteLine("Initializing");
            var builder = new ContainerBuilder();

            #region MySQL Repostories
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<QuestionRepository>().As<IQuestionRepository>();
            builder.RegisterType<AnswerRepository>().As<IAnswerRepository>();
            #endregion

            #region Auth
            builder.RegisterType<PlainTextHasher>().As<IPasswordHasher>();
            builder.RegisterType<BasicAuth>().As<IAuthenticator>();
            #endregion

            #region UseCases
            builder.RegisterType<QuestionsUseCase>();
            #endregion

            #region Controllers
            builder.RegisterType<CreateQuestion>();
            builder.RegisterType<ListQuestion>();
            builder.RegisterType<ViewQuestion>();
            #endregion

            #region API
            builder.RegisterType<ConsoleApi>();
            #endregion

            System.Console.WriteLine("Initializing OK");
            
            return builder.Build();
        }
    }
}
