namespace SpaceWar.Api.Contracts.v1;

public static class ApiRoutes
{
    public const string Root = "api";
    public const string Version = "v1";
    public const string Base = Root + "/" + Version;
    public static class Account
    {
        public const string Login = $"{Base}/account/login";
        public const string Register = $"{Base}/account/register";
        public const string ChangeUsername = $"{Base}/account/changeusername";
        public const string ChangePassword = $"{Base}/account/changepassword";
    }
}