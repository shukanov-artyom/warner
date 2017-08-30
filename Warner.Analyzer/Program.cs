using System;
using Autofac;
using Warner.Analyzer.Infrastructure;

namespace Warner.Analyzer
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No commands provided to the tool. Provide help command to get short usage guide.");
                return 0;
            }
            try
            {
                Run(args);
                return 0;
            }
            catch (ApplicationConfigurationException ex)
            {
                Console.WriteLine($"CommandLine line arguments are wrong, please use help commmand. {ex.Message}");
                return -1;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unhandled exception occured: {e.Message}");
                return -1;
            }
        }

        public static void Run(string[] args)
        {
            var containerSetup = new ContainerSetup();
            IContainer container = containerSetup.Build(args);
            IApplication app = container.Resolve<IApplication>();
            app.Run();
        }
    }
}