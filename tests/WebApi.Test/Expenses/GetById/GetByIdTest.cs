using CashFlow.Communication.Enums;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.Expenses.GetById
{
    public class GetByIdTest : CashFlowFixture
    {
        private const string METHOD = "api/Expenses";

        private readonly string _token;
        private readonly long _expenseId;

        public GetByIdTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _token = webApplicationFactory.UserTeamMember.GetToken();
            _expenseId = webApplicationFactory.ExpenseTeamMember.GetId();
        }

        [Fact]
        public async Task Success()
        {
            var result = await DoGet($"{METHOD}/{_expenseId}", _token);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            response.RootElement.GetProperty("id").GetInt64().Should().Be(_expenseId);
            response.RootElement.GetProperty("title").GetString().Should().NotBeNullOrWhiteSpace();
            response.RootElement.GetProperty("description").GetString().Should().NotBeNullOrWhiteSpace();
            response.RootElement.GetProperty("date").GetDateTime().Should().NotBeAfter(DateTime.UtcNow);
            response.RootElement.GetProperty("amount").GetDecimal().Should().BeGreaterThan(0);
            response.RootElement.GetProperty("tags").EnumerateArray().Should().NotBeNullOrEmpty();
            
            var paymentType = response.RootElement.GetProperty("paymentType").GetInt32();
            Enum.IsDefined(typeof(PaymentType), paymentType).Should().BeTrue();
        }
    }
}
