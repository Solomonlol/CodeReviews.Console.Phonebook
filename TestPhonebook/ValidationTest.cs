using Backend.Validation;

namespace TestPhonebook
{
    public class ValidationTest
    {
        [Theory]
        // --- Валидные номера (должны вернуть true) ---
        [InlineData("+375295368203", true)]
        [InlineData("375295368203", true)]
        [InlineData("295368203", true)]
        [InlineData("375 29 5368203", true)]
        [InlineData("+375(29)5368203", true)]
        [InlineData("375 29 536-82-03", true)]
        [InlineData("5368203", true)]
        [InlineData("375 5368203", true)]
        [InlineData("+375 33 1234567", true)]
        [InlineData("+375 (44) 765-43-21", true)]
        [InlineData("37525 123 45 67", true)]
        [InlineData("375 29 1234567", true)]
        [InlineData("+375291234567", true)]
        [InlineData("375 29 123-45-67", true)]
        [InlineData("+375 29 123 45 67", true)]
        [InlineData("375(29)1234567", true)]
        [InlineData("1234567", true)] 
        [InlineData("123-45-67", true)]
        [InlineData("+375 29 1234567", true)]
        [InlineData("+3752953682031", true)]
        [InlineData("29 5368203", true)]        
        [InlineData("+375 99 1234567", true)]    
        [InlineData("37529536820", true)]    

        // --- Невалидные номера (должны вернуть false) ---
        [InlineData("+375 29 5368203 ", false)]  
        [InlineData("375-29-123-45-67", false)]
        [InlineData("+375)295368203", false)]
        [InlineData("37529(5368203", false)]
        [InlineData("abc123", false)]
        [InlineData("+375 (29) 536-82-0", false)] 
        [InlineData("375 29 536-82-03!", false)]  
        [InlineData("++375295368203", false)]    
        [InlineData("", false)]                 
        [InlineData("   ", false)]               
        [InlineData("375-29-536-82-0", false)]   
        [InlineData("375 29 536820", false)]     
        public void PhoneValidation_ReturnExpected(string phoneNumber, bool expected)
        {
            bool result = Validation.IsPhoneNumber(phoneNumber);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("test@example.com", true)]
        [InlineData("user@domain.com", true)]
        [InlineData("first.last@domain.co.uk", true)]
        [InlineData("email@sub.domain.com", true)]
        [InlineData("mail@server.org", true)]
        [InlineData("name+tag@domain.com", true)]
        [InlineData("user_name@domain.com", true)]
        [InlineData("user-name@domain.com", true)]
        [InlineData("user@domain.name", true)]
        [InlineData("admin@mail.net", true)]
        [InlineData("support@company.io", true)]
        [InlineData("info@example.museum", true)]
        [InlineData("contact@domain.com.au", true)]
        [InlineData("postmaster@domain.com", true)]
        [InlineData("webmaster@domain.org", true)]
        [InlineData("simple@example.co", true)]
        [InlineData("plainaddress", false)]         
        [InlineData("user@domain", false)]          
        [InlineData("user.domain.com", false)]      
        [InlineData("", false)]                      
        [InlineData("   ", false)]
        public void EmailValidation_ReturnExpected(string email, bool expected)
        {
            bool result = Validation.IsEmailAddress(email);

            Assert.Equal(expected, result);
        }
    }
}
