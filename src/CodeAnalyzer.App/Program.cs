
using CodeAnalyzer.App.Factory;
using CodeAnalyzer.App.Enums;
using CodeAnalyzer.App.Report;
using System.Text;
using System.CommandLine;
using CodeAnalyzer.App.Output;

RootCommand rootCommand = new RootCommand(description: "Converts an image file from one format to another.");

var projectOption = new Option<string>(aliases: new string[] { "--project", "-p" }, description: "Project full path"){ IsRequired = true };
var fileOption = new Option<string>(aliases: new string[] { "--file", "-f" }, description: "Output to file"){ IsRequired = false };

rootCommand.AddOption(projectOption);
rootCommand.AddOption(fileOption);

rootCommand.SetHandler((projectOption, fileOption) =>
{
    Console.WriteLine("Starting...");

    try
    {
        if (fileOption != null)
        {
            var report = new ReportToFile(projectOption, fileOption);
            report.Generate(Language.CSharp);
        }
        else
        {
            var report = new ReportConsole(projectOption);
            report.Generate(Language.CSharp); 
        }
    }
    catch(Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

}, projectOption, fileOption);


return rootCommand.Invoke(args);




