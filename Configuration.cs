namespace blog
{
    public static class Configuration
    {
        //Token - JWT (Json Web Token)
        public static string JwtKey  ="j6t2hybt26fwxgy2hvxjttbdy=wxp";
        //Método de autenticação para consulta com robô
        public static string ApiKeyName = "api_key";
        public static string ApiKey = "==9Bd_023_C7b6@";
        public static SmtpConfiguration Smtp = new();
    
        public class SmtpConfiguration
        {
            public string Host { get; set; }
            public int Port { get; set; } = 587;
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    
    
    }
}
