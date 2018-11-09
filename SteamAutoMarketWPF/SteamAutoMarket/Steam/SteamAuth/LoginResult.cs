namespace Steam.SteamAuth
{
    public enum LoginResult
    {
        LoginOkay,

        GeneralFailure,

        BadRSA,

        BadCredentials,

        NeedCaptcha,

        Need2FA,

        NeedEmail,

        TooManyFailedLogins
    }
}