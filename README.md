# NLog.InstallNLogConfig
[![Version](https://img.shields.io/nuget/v/NLog.InstallNLogConfig.svg)](https://www.nuget.org/packages/NLog.InstallNLogConfig)
[![AppVeyor](https://img.shields.io/appveyor/ci/nlog/nlog-InstallNLogConfig/master.svg)](https://ci.appveyor.com/project/nlog/nlog-InstallNLogConfig/branch/master)

Extensions to [NLog](https://github.com/NLog/NLog/).

Install and uninstall target specific objects


===

Usage: InstallNLogConfig [options] NLog.config...

Performs installation/uninstallation that requires administrative permissions
(such as Event Log sources, databases, etc).


Options:
<pre>
  -u                uninstall
  -log file.txt     save installation log to a file
  -q                quiet (do not write a log)
  -i                ignore failures
  -consolelog       write installation log to the console
  -loglevel level   set log level (Trace, Debug, Info, Warn, Error or Fatal)
  -p NAME=VALUE     set installation parameter value
</pre>
