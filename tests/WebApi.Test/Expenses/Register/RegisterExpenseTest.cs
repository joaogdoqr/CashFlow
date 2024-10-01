using CashFlow.Exception;
using CommonTestsUtilities.Requests.Expenses;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Expenses.Register
{
    public class RegisterExpenseTest : CashFlowFixture
    {
        private const string METHOD = "api/Expenses";

        private readonly string _token;

        public RegisterExpenseTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _token = webApplicationFactory.UserAdmin.GetToken();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestExpenseBuilder.Build();

            var result = await DoPost(METHOD, request, _token);

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            response.RootElement.GetProperty("title").GetString().Should().Be(request.Title);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Title_Empty(string culture)
        {
            var request = RequestExpenseBuilder.Build();
            request.Title = string.Empty;

            var result = await DoPost(METHOD, request, _token, culture);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("TITLE_REQUIRED", new CultureInfo(culture));

            errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
        }
    }
}
