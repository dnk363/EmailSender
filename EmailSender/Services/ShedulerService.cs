using EmailSender.Interfaces;
using EmailSender.Models;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;
using System.Windows;

namespace EmailSender.Services
{
    class ShedulerService
    {
        private EmailService _emailService;
        private IEmailSettings _emailSettings;
        private IMessage _message;
        private IScheduler _scheduler;

        public ShedulerService(EmailService emailService, IEmailSettings emailSettings, IMessage message)
        {
            _emailService = emailService;
            _emailSettings = emailSettings;
            _message = message;
        }

        public async void StartNowAsync()
        {
            await Start();
        }

        public async Task Start()
        {
            _scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await _scheduler.Start();

            IJobDetail job = JobBuilder.Create<JobSender>().Build();

            job.JobDataMap["emailService"] = _emailService;
            job.JobDataMap["emailSettings"] = _emailSettings;
            job.JobDataMap["message"] = _message;

            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1)
                    .RepeatForever())
                .Build();

            await _scheduler.ScheduleJob(job, trigger);
        }

        public async void Stop()
        {
            await _scheduler.Shutdown();
        }

        private class JobSender : IJob
        {
            private IEmailSettings _emailSettings;
            private IMessage _message;

            public async Task Execute(IJobExecutionContext context)
            {
                JobDataMap dataMap = context.JobDetail.JobDataMap;
                EmailService emailService = (EmailService)dataMap["emailService"];
                _emailSettings = (IEmailSettings)dataMap["emailSettings"];
                _message = (IMessage)dataMap["message"];
                await emailService.SendEmailAsync(_emailSettings, _message);
            }
        }
    }
}
