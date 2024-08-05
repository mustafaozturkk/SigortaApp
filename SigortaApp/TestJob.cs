using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace SigortaApp.Web
{
	public class TestJob : IJob
	{
        private readonly ILogger<TestJob> _logger;

        public TestJob(ILogger<TestJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Job çalışıyor: {time}", DateTime.Now);
            return Task.CompletedTask;
        }
    }
}

