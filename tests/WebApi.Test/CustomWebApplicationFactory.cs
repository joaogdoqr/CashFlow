using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Infrastructure.DataAccess;
using CommonTestsUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private User _user;
        private string _password;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    services.AddDbContext<CashFlowDbContext>(config =>
                    {
                        config.UseInMemoryDatabase("InMemoryDbForTesting");
                        config.UseInternalServiceProvider(provider);
                    });

                    var scope = services.BuildServiceProvider().CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<CashFlowDbContext>();
                    var passwordEncrypter = scope.ServiceProvider.GetRequiredService<IPasswordEncrypter>();

                    Setup(dbContext, passwordEncrypter);
                });
        }

        public string GetSetupEmail() => _user.Email;
        public string GetSetupName() => _user.Name;
        public string GetSetupPassword() => _password;

        private void Setup(CashFlowDbContext dbContext, IPasswordEncrypter passwordEncrypter)
        {
            _user = UserBuilder.Build();
            _password = _user.Password;
            _user.Password = passwordEncrypter.Encrypt(_user.Password);

            dbContext.Users.Add(_user);
            dbContext.SaveChanges();
        }
    }
}
