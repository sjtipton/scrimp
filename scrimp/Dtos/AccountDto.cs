using scrimp.Domain;

namespace scrimp.Dtos
{
    public class AccountDto
    {
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
        public AccountType Type { get; set; }
        public bool IsNetWorth { get; set; }
        public int UserId { get; set; }
    }
}
