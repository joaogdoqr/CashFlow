using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Tokens;
using Moq;

namespace CommonTestsUtilities.Token
{
    public class JwtTokenGeneratorBuilder
    {
        public static IAccessTokenGenerator Build()
        {
            var mock = new Mock<IAccessTokenGenerator>();
            var tokenExample = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InRlc3QiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiJlYTY5YmE0My00OWI1LTQ1YzctYmQ2Yy1hZjkzNGNmZjZmMGQiLCJuYmYiOjE3MjcyNjYwMzUsImV4cCI6MTcyNzI2OTYzNSwiaWF0IjoxNzI3MjY2MDM1fQ.rDePqEYHjixtbbWSjqsng3VPQz31ZTCjL8vQ1GtdO2s";

            mock.Setup(tokenGenerator => tokenGenerator.Generate(It.IsAny<User>())).Returns(tokenExample);

            return mock.Object;
        }
    }
}
