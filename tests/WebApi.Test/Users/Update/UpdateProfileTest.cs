using CashFlow.Exception;
using CommonTestsUtilities.Requests.Users;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Users.Update
{
    public class UpdateProfileTest : CashFlowFixture
    {
        private const string METHOD = "api/User";
        private readonly string _token;

        public UpdateProfileTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _token = webApplicationFactory.UserTeamMember.GetToken();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestUpdateUserBuilder.Build();

            var result = await DoPut(METHOD, request, _token);

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_InvalidRequest(string culture)
        {
            var request = RequestUpdateUserBuilder.Build();
            request.Name = string.Empty;

            var result = await DoPut(METHOD, request, _token, culture);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            var errorList = response.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));

            errorList.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
        }
    }
}
