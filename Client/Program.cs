using Client.Exceptions;
using Client.Models;
using System.Text;

var client = new Client.Domain.Client();

var result = await client.RegisterAccountAsync(new RegistrationRequest
{
    Username = "Vitaliy Minaev",
    Email = "example@gmail.com",
    Password = "qwerty123"
}, new Uri("https://space-war.herokuapp.com/api/v1/Account/Register"));

string toDisplay = result.Match<string>(accessToken =>
{
    return accessToken.access_token;
}, exception =>
{
    if(exception is AuthorizationException authExeption)
        return ParseErrorsToString(authExeption.Errors);

    throw exception;
});

Console.WriteLine(toDisplay);

string ParseErrorsToString(string[] errors)
{
    var stringBuilder = new StringBuilder();

    stringBuilder.AppendLine("Errors: [");
    foreach (var item in errors)
    {
        stringBuilder.AppendLine("\t" + item);
    }
    stringBuilder.Append("]");

    return stringBuilder.ToString();
} 