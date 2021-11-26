namespace Oldsu.Enums
{
    public enum LoginResult
    {
        AuthenticationSuccessful = 0,
        AuthenticationFailed = -1,
        TestBuildButNotSupporter = -6,
        ServerSideError = -5,
        AccountNotActivated = -4,
        Banned = -3,
        TooOldVersion = -2
    }
}