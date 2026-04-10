namespace UserService.Domain.ValueObject;

public record Email
{
    public string EmailString { get; private set; } = string.Empty;

    private Email() { }
    private Email(string emailString) 
    {

    }

    public static Email CreateEmail(string emailString)
    {
        emailString = emailString.Trim(); // Might just use email regex instead of this 

        if (string.IsNullOrEmpty(emailString) || emailString.Length < 5)
            throw new Exception(); // ToDo: Domain Exception

        if (!emailString.Contains("@") && !emailString.Contains("."))
            throw new Exception();

        return new Email(emailString);
    }
}
