using CashFlow.Communication.Requests.Users;
using CashFlow.Exception;
using CommonTestsUtilities.Requests.Login;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Login
{
    public class LoginTest : CashFlowFixture
    {
        private const string METHOD = "api/Login";
        private readonly string _email;
        private readonly string _name;
        private readonly string _password;

        public LoginTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _email = webApplicationFactory.UserAdmin.GetEmail();
            _name = webApplicationFactory.UserAdmin.GetName();
            _password = webApplicationFactory.UserAdmin.GetPassword();
        }

        [Fact]
        public async Task Sucess()
        {
            var request = new RequestLogin { Email = _email, Password =  _password};

            var result = await DoPost(METHOD, request);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            response.RootElement.GetProperty("name").GetString().Should().Be(_name);
            response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Login_Invalid(string culture)
        {
            var request = RequestLoginBuilder.Build();

            var result = await DoPost(METHOD, request, culture: culture);

            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            var errorList = response.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID", new CultureInfo(culture));

            errorList.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
        }
    }
}
