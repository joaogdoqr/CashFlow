using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public Guid UserIdentifier { get; set; }

        public required string Role { get; set; } = Roles.TEAM_MEMBER;
    }
}
