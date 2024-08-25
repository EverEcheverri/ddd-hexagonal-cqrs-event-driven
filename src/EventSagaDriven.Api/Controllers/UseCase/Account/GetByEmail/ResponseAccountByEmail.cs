using EventSagaDriven.Domain.Entities.Account.Enums;

namespace EventSagaDriven.Api.Controllers.UseCase.Account.GetByEmail
{
    public class ResponseAccountByEmail
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public string Mobile { get; private set; }
        public string AccountType { get; private set; }
        public Guid CityId { get; private set; }
        public List<string> Genres { get; private set; }

        internal static ResponseAccountByEmail Map(Domain.Entities.Account.Account account)
        {
            if (account is null)
            {
                return null;
            }

            string accountType = Enum.GetName(typeof(AccountType), account.AccountType);

            List<string> genres = account.Genres()
                .Select(g => g.Name.Value)
                .ToList();


            return new ResponseAccountByEmail
            {
                Id = account.Id,
                Email = account.Email.Value,
                UserName = account.UserName.Value,
                Mobile = account.Mobile.Value,
                AccountType = accountType,
                CityId = account.CityId,
                Genres = genres
            };
        }
    }
}
