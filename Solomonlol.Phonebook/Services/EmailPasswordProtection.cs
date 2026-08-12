using Microsoft.AspNetCore.DataProtection;

namespace Backend.Services
{
    public class EmailPasswordProtection
    {
        private readonly IDataProtector _protector;

        public EmailPasswordProtection(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("EmailPassword");
        }

        public string Protect(string password) 
            => _protector.Protect(password);

        public string Unprotect(string protectedPassword)
            =>_protector.Unprotect(protectedPassword);
    }
}
