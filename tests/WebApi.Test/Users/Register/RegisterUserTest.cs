using CashFlow.Exception;
using CommonTestsUtilities.Requests.Users;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Users.Register
{
    public class RegisterUserTest(CustomWebApplicationFactory webApplicationFactory) : CashFlowFixture(webApplicationFactory)
    {
        private const string METHOD = "api/User";

        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserBuilder.Build();

            var result = await DoPost(METHOD, request);

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            response.RootElement.GetProperty("name").GetString().Should().Be(request.Name);
            response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_InvalidRequest(string culture)
        {
            var request = RequestRegisterUserBuilder.Build();
            request.Name = string.Empty;

            var result = await DoPost(METHOD, request, culture: culture);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            var errorList = response.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));

            errorList.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
        }
    }
}
