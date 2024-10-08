using FluentAssertions;
using System.Net;
using System.Net.Mime;

namespace WebApi.Tests.Expenses.Reports;
public class GenerateExpensesReportTest : CashFlowFixture
{
    private const string METHOD = "api/Report";

    private readonly string _adminToken;
    private readonly string _teamMemberToken;
    private readonly DateTime _expenseDate;

    public GenerateExpensesReportTest(CustomWebApplicationFactory webApplicationFactory): base(webApplicationFactory)
    {
        _adminToken = webApplicationFactory.UserAdmin.GetToken();
        _teamMemberToken = webApplicationFactory.UserTeamMember.GetToken();
        _expenseDate = webApplicationFactory.ExpenseAdmin.GetDate();
    }

    [Fact]
    public async Task Success_Pdf()
    {
        var result = await DoGet($"{METHOD}/pdf?date={_expenseDate:Y}", _adminToken);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Content.Headers.ContentType.Should().NotBeNull();
        result.Content.Headers.ContentType!.ToString().Should().Be(MediaTypeNames.Application.Pdf);
    }

    [Fact]
    public async Task Success_Excel()
    {
        var result = await DoGet($"{METHOD}/excel?date={_expenseDate:Y}", _adminToken);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Content.Headers.ContentType.Should().NotBeNull();
        result.Content.Headers.ContentType!.ToString().Should().Be(MediaTypeNames.Application.Octet);
    }

    [Fact]
    public async Task Error_Forbidden_TeamMember_Not_Allowd_Excel()
    {
        var result = await DoGet($"{METHOD}/excel?date={_expenseDate:Y}", _teamMemberToken);

        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Error_Forbidden_TeamMember_Not_Allowd_Pdf()
    {
        var result = await DoGet($"{METHOD}/pdf?date={_expenseDate:Y}", _teamMemberToken);

        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}
