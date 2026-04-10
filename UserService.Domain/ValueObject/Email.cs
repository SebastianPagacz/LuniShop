using System.ComponentModel.DataAnnotations;
using UserService.Domain.Exceptions;

namespace UserService.Domain.ValueObject;

public record Email
{
    public string EmailString { get; private set; } = string.Empty;

    private Email() { }
    private Email(string emailString) 
    {
        EmailString = emailString;
    }

    internal static Email CreateEmail(string emailString)
    {
        var emailValidator = new EmailAddressAttribute();
        
        if (!emailValidator.IsValid(emailString))
            throw new DomainException("Provided email address is not valid");

        return new Email(emailString);
    }
}
