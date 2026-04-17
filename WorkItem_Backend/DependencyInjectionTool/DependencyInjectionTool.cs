using Common.Helpers;

namespace WorkItem_Backend.DependencyInjectionTool
{
    public static class DependencyInjectionTool
    {
        public static void AddDIContainer(IServiceCollection services)
        {
            //其他註冊
            services.AddSingleton<JwtHelper>();

            // 掃描 Service 和 Repository 類別
            var servs = typeof(BusinessRule.Services.UserService).Assembly;
            var repos = typeof(DataAccess.Repository.UserRepository).Assembly;

            // Service 類別
            var serviceTypes = servs.GetExportedTypes()
                .Where(x => !x.IsAbstract && !x.IsInterface && x.Name.EndsWith("Service"))
                .ToList();

            // Repository 類別
            var repositoryTypes = repos.GetExportedTypes()
                .Where(x => !x.IsAbstract && !x.IsInterface && x.Name.EndsWith("Repository"))
                .ToList();

            // IService 介面
            var serviceInterfaces = servs.GetExportedTypes()
                .Where(x => x.IsInterface && x.Name.EndsWith("Service"))
                .ToList();

            // IRepository 介面
            var repositoryInterfaces = repos.GetExportedTypes()
                .Where(x => x.IsInterface && x.Name.EndsWith("Repository"))
                .ToList();

            // 將所有要註冊的類別和介面組合起來進行註冊
            foreach (var obj in serviceTypes.Concat(repositoryTypes))
            {
                var intf = serviceInterfaces.Concat(repositoryInterfaces)
                    .FirstOrDefault(x => x.IsAssignableFrom(obj));

                if (intf != null)
                {
                    services.AddScoped(intf, obj);
                }
            }
        }
    }
}
