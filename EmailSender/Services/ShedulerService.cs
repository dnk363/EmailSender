using EmailSender.Interfaces;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace EmailSender.Services
{
    class ShedulerService
    {
        private IEmailService _emailService;
        private IFormMessageService _formMessageService;
        private IEmailSettings _emailSettings;
        private ISiteSettings _siteSettings;
        private IMessage _message;
        private IScheduler _scheduler;

        public ShedulerService(IEmailService emailService, IFormMessageService formMessageService, IEmailSettings emailSettings, IMessage message, ISiteSettings siteSettings)
        {
            _emailService = emailService;
            _formMessageService = formMessageService;
            _emailSettings = emailSettings;
            _siteSettings = siteSettings;
            _message = message;
        }

        public async void StartNowSync()
        {
            await Start();
        }

        public async Task Start()
        {
            _scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await _scheduler.Start();

            IJobDetail job = JobBuilder.Create<JobSender>().Build();

            job.JobDataMap["emailService"] = _emailService;
            job.JobDataMap["formMessageService"] = _formMessageService;
            job.JobDataMap["emailSettings"] = _emailSettings;
            job.JobDataMap["siteSettings"] = _siteSettings;
            job.JobDataMap["message"] = _message;

            ITrigger trigger = TriggerBuilder.Create()
                .WithCronSchedule(_emailSettings.TimeStartSettings)
                .Build();

            await _scheduler.ScheduleJob(job, trigger);
        }

        public async void Stop()
        {
            await _scheduler.Shutdown();
        }

        private class JobSender : IJob
        {
            private IEmailService _emailService;
            private IFormMessageService _formMessageService;
            private IEmailSettings _emailSettings;
            private IMessage _message;
            private ISiteSettings _siteSettings;

            public async Task Execute(IJobExecutionContext context)
            {
                JobDataMap dataMap = context.JobDetail.JobDataMap;
                _emailService = (IEmailService)dataMap["emailService"];
                _formMessageService = (IFormMessageService)dataMap["formMessageService"];
                _emailSettings = (IEmailSettings)dataMap["emailSettings"];
                _message = (IMessage)dataMap["message"];
                _siteSettings = (ISiteSettings)dataMap["siteSettings"];
                await _emailService.SendEmailAsync(_formMessageService, _emailSettings, _message, _siteSettings);
            }
        }
    }
}
