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

    public static Email CreateEmail(string emailString)
    {
        if (!IsValidEmail(emailString))
            throw new DomainException("Provided email address is invalid.");

        return new Email(emailString);
    }

    public static bool IsValidEmail(string emailString)
    {
        var emailValidator = new EmailAddressAttribute();
        if (string.IsNullOrWhiteSpace(emailString) || !emailValidator.IsValid(emailString))
            return false;

        return true;
    }
}
