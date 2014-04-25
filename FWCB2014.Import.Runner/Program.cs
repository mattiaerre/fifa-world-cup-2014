using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Windsor.Installer;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace FWCB2014.Import.Runner
{
    public class Program
    {
        private static IWindsorContainer BootstrapContainer()
        {
            return new WindsorContainer(new XmlInterpreter())
                .Install(FromAssembly.This());
        }

        public static void Main(string[] args)
        {
            var container = BootstrapContainer();

            //var schedulerFactory = new StdSchedulerFactory();

            //var scheduler = schedulerFactory.GetScheduler();
            //scheduler.Start();

            //var detail = JobBuilder.Create<HelloJob>()
            //    .WithIdentity("job-name", "grup-name")
            //    .Build();

            //var trigger = TriggerBuilder.Create()
            //    .WithIdentity("trigger-name", "group-name")
            //    .StartNow()
            //    .WithSimpleSchedule(e => e.WithIntervalInSeconds(10).RepeatForever())
            //    .Build();

            //scheduler.ScheduleJob(detail, trigger);

            Console.WriteLine("{0}", DateTime.Now.ToString("HH:mm:ss"));

            container.Dispose();
        }
    }

    public class HelloJob : IJob
    {
        private readonly IMessageService _service;

        public HelloJob()
            : this(new MessageService())
        {
        }

        public HelloJob(IMessageService service)
        {
            _service = service;
        }

        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine(_service.GetMessage());
        }
    }

    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return string.Format("{0} HelloJob is executing.", DateTime.Now.ToString("HH:mm:ss"));
        }
    }

    public interface IMessageService
    {
        string GetMessage();
    }
}
