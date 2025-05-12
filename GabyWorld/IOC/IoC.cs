using GabyWorld.Data;

namespace GabyWorld.IOC
{

    public static class IoC
    {
        //scoped instance of ApplicationDbContext
        public static ApplicationDbContext ApplicationDbContext  => IoCContainer.Provider.GetService<ApplicationDbContext>();
    }
    /// <summary>
    /// dependency injection container making use ASP.NET Core service provider (ServiceProvider)
    /// </summary>
    public static class IoCContainer
    {
        //service provider for this application
        public static ServiceProvider Provider { get; set; }
    }


}
