namespace FuelProject.Infrastructure;

public class ConfigurationManagerCustom
{
    public static IConfiguration AppSetting { get; }
    static ConfigurationManagerCustom()
    {
        AppSetting = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
    }
}
