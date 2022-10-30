namespace Domain.Utils
{
    public static class ConstantSettings
    {
        public static class Orleans
        {
            private const string SectionName = "Orleans";
            public static class Dashboard
            {
                public const string Enabled = $"{SectionName}:Dashboard:Enabled";
                public const string UserName = $"{SectionName}:Dashboard:UserName";
                public const string Password = $"{SectionName}:Dashboard:Password";
                public const string Port = $"{SectionName}:Dashboard:Port";
            }
        }
    }
}
