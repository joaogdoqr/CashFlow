using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.Expenses.GetAll
{
    public class GetAllExpenseTest : CashFlowFixture
    {
        private const string METHOD = "api/Expenses";

        private readonly string _token;

        public GetAllExpenseTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _token = webApplicationFactory.UserAdmin.GetToken();
        }

        [Fact]
        public async Task Success()
        {
            var result = await DoGet(METHOD, _token);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            response.RootElement.GetProperty("expenses").EnumerateArray().Should().NotBeNullOrEmpty();
        }
    }
}
