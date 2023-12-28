using System.Data;
using Autofac;
using ConsoleApp1;
using ConsoleApp1.Abstractions;
using ConsoleApp1.Objects;
using ConsoleApp1.Services;

try
{
    ArgumentsMonitor.ThrowIfInsufficiently(args);
    var (logPath, serviceName) = (args[0], args[1]);

    var container = new ContainerBuilder();

    container.RegisterType<LogsFileReader>().As<IFileReader>();
    container.RegisterType<ServiceLogRotator>().As<ILogRotator>();
    container.RegisterType<ServiceLogReporter>().As<ILogReporter>();

    container.RegisterType<DataRowLogParser>().As<ILogParser<LogLine>>();
    container.RegisterType<ConsoleLogViewer>().As<IViewer<LogReport>>();

    container.RegisterType<TableBuilder>().InstancePerLifetimeScope();

    var services = container.Build();

    var fileReader = services.Resolve<IFileReader>();
    var getLogsTask = fileReader.AllFilesContentAsync(logPath, $"{serviceName}.*");

    var logRotator = services.Resolve<ILogRotator>();
    var getRotationsTask = logRotator.LastOrDefaultAsync(serviceName, logPath);

    var tableBuilder = services.Resolve<TableBuilder>();
    tableBuilder.Name("Log Table")
        .AddColumn<DateTime>("Date")
        .AddColumn<string>("Severity")
        .AddColumn<string>("Category");

    var allContent = await getLogsTask;

    var parser = services.Resolve<ILogParser<LogLine>>();
    var getParsedLogsTask = parser.ParseAsync(allContent);

    var logLines = await getParsedLogsTask;
    foreach (var line in logLines)
    {
        tableBuilder.AddRow(x =>
        {
            x.SetField("Date", line.Date);
            x.SetField("Severity", line.Severity);
            x.SetField("Category", line.Category);
        });
    }

    var logTable = tableBuilder.GetCurrentBuild();

    var reporter = services.Resolve<ILogReporter>();
    var logReport = reporter.FromTable(logTable);

    logReport.Rotations = (await getRotationsTask).GetValueOrDefault();

    var viewer = services.Resolve<IViewer<LogReport>>();
    await viewer.ViewAsync(logReport);
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}



