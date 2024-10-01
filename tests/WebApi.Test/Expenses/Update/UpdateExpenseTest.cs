using Bogus;
using CashFlow.Exception;
using CommonTestsUtilities.Requests.Expenses;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Expenses.Update
{
    public class UpdateExpenseTest : CashFlowFixture
    {
        private const string METHOD = "api/Expenses";

        private readonly string _token;
        private readonly long _expenseId;

        public UpdateExpenseTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _token = webApplicationFactory.UserTeamMember.GetToken();
            _expenseId = webApplicationFactory.ExpenseTeamMember.GetId();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestExpenseBuilder.Build();

            var result = await DoPut($"{METHOD}/{_expenseId}", request, _token);

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_TitleEmpty(string culture)
        {
            var request = RequestExpenseBuilder.Build();
            request.Title = string.Empty;

            var result = await DoPut($"{METHOD}/{_expenseId}", request, _token, culture);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            var errorList = response.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("TITLE_REQUIRED", new CultureInfo(culture));

            errorList.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Expense_Not_Found(string culture)
        {
            var id = new Faker().Random.Int(min: 99999);
            var request = RequestExpenseBuilder.Build();

            var result = await DoPut($"{METHOD}/{id}", request, _token, culture);

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            var errorList = response.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("EXPENSE_NOT_FOUND", new CultureInfo(culture));

            errorList.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
        }
    }
}
