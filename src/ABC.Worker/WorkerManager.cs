
using ABC.Worker.Interfaces;
using ABC.Worker.Worker;
using Quartz;
using System;

namespace ABC.Worker
{
    public class WorkerManager
    {
        private IScheduler Scheduler;
        private IImportAIJob _importAIJob;

        public WorkerManager(IScheduler scheduler, IImportAIJob importAIJob)
        {
            _importAIJob = importAIJob;
            Scheduler = scheduler;
            Console.WriteLine();
        }

        public void Run()
        {
            _importAIJob.ImportFolderImagesAsync();
            //Scheduler.Start();
            //ScheduleAllJobs();
        }

        protected void ScheduleAllJobs()
        {
            //ScheduleImportAIJob();
        }

        private void ScheduleImportAIJob()
        {
            //IJobDetail job = JobBuilder.Create<ImportAIJob>()
            //    .WithIdentity("ImportAIJob", "1")
            //    .Build();

            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithIdentity("ImportAIJobTrigger", "1")
            //    .StartNow()
            //    .Build();

            //Scheduler.ScheduleJob(job, trigger);
        }
    }
}