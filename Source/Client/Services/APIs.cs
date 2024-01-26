namespace PostOffice.Client.Services
{
    public static class APIs
    {
        public static readonly Uri baseAddress = new Uri("https://localhost:7053/api");

        public static string loginAPI = "/Users/authenticate";
        public static string registerAPI = "/Authentication/register";
        public static string comfirmEmailAPI = "/Authentication/ConfirmEmail";
        public static string createAreaAPI = "/Area/Create";
        public static string getAllAreaAPI = "/Area/GetAll";

    }
}
