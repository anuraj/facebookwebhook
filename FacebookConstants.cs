namespace Microsoft.AspNetCore.WebHooks
{
    public static class FacebookConstants
    {
        public static string ReceiverName => "facebook";
        public static int SecretKeyMinLength => 1;
        public static int SecretKeyMaxLength => 100;
        public static string VerifyTokenParameterName => "hub.verify_token";
        public static string ChallengeParameterName => "hub.challenge";
    }
}
