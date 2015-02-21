using System;
using System.IO;
using NLog.Config;

namespace NLog.InstallNLogConfig
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                using (var context = new InstallationContext())
                {
                    context.LogOutput = Console.Out;

                    XmlLoggingConfiguration configuration = null;

                    bool uninstallMode = false;

                    for (int i = 0; i < args.Length; ++i)
                    {
                        switch (args[i])
                        {
                            case "-q":
                                context.LogOutput = TextWriter.Null;
                                break;

                            case "-consolelog":
                                context.LogOutput = Console.Out;
                                break;

                            case "-loglevel":
                                context.LogLevel = LogLevel.FromString(args[++i]);
                                break;

                            case "-i":
                                context.IgnoreFailures = true;
                                break;

                            case "-log":
                                context.LogOutput = File.CreateText(args[++i]);
                                break;

                            case "-p":
                                string arg = args[++i];
                                int p = arg.IndexOf('=');
                                if (p < 0)
                                {
                                    Console.WriteLine("Parameter '{0}' must be NAME=VALUE", arg);
                                    Usage();
                                    return 1;
                                }

                                string paramName = arg.Substring(0, p);
                                string paramValue = arg.Substring(p + 1);
                                context.Parameters.Add(paramName, paramValue);
                                break;

                            case "-u":
                                uninstallMode = true;
                                break;

                            case "-?":
                                Usage();
                                return 0;

                            default:
                                if (args[i].StartsWith("-"))
                                {
                                    Usage();
                                    return 1;
                                }

                                configuration = new XmlLoggingConfiguration(args[i]);
                                break;
                        }
                    }

                    if (configuration == null)
                    {
                        Usage();
                        return 1;
                    }

                    if (uninstallMode)
                    {
                        configuration.Uninstall(context);
                    }
                    else
                    {
                        configuration.Install(context);
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: {0}", ex);
                return 1;
            }
        }

        /// <summary>
        /// Displays the usage.
        /// </summary>
        static void Usage()
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Usage: InstallNLogConfig [options] NLog.config...");
            Console.ForegroundColor = oldColor;
            Console.WriteLine();
            Console.WriteLine("Performs installation/uninstallation that requires administrative permissions");
            Console.WriteLine("(such as Event Log sources, databases, etc).");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  -u                uninstall");
            Console.WriteLine("  -log file.txt     save installation log to a file");
            Console.WriteLine("  -q                quiet (do not write a log)");
            Console.WriteLine("  -i                ignore failures");
            Console.WriteLine("  -consolelog       write installation log to the console");
            Console.WriteLine("  -loglevel level   set log level (Trace, Debug, Info, Warn, Error or Fatal)");
            Console.WriteLine("  -p NAME=VALUE     set installation parameter value");
            Console.WriteLine();
            Console.WriteLine("Parameters can be referenced in NLog.config using ${install-context:NAME}");
        }
    }
}
