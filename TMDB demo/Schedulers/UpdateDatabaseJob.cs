using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using TMDB_demo.Services.Interfaces;

namespace TMDB_demo.Schedulers
{
    [DisallowConcurrentExecution]
    public class UpdateDatabaseJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<UpdateDatabaseJob> _logger;

        public UpdateDatabaseJob(
            IServiceProvider serviceProvider,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<UpdateDatabaseJob> logger)
        {
            _serviceProvider = serviceProvider;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var updateSevice = scope.ServiceProvider.GetService<IUpdateSevice>();
                    updateSevice.HandleChanges();                   
                }
                
                return Task.CompletedTask;
            } catch (Exception ex) {
                var msg = $"An error occured while executing {nameof(UpdateDatabaseJob)} job.";
                _logger.LogError(msg + ex.Message);
                throw new Exception(msg, ex);                
            }
        }
    }
}
