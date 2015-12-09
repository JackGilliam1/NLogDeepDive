using System;
using System.IO;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Targets;

namespace TypeCalculator
{
    [Target("TypeDebugCustomTargThingy")]
    public class TypeDebugCustomTarget : TargetWithLayout
    {
        public TypeDebugCustomTarget()
        {
            File = "debugFileCustom.txt";
        }

        [RequiredParameter]
        public string File { get; set; }

        protected override void Write(LogEventInfo logEvent)
        {
            var logMessage = Layout.Render(logEvent);
            
            WriteMessageToFile(File, logMessage);
        }

        protected override void Write(AsyncLogEventInfo logEvent)
        {
            var logMessage = Layout.Render(logEvent.LogEvent);

            WriteMessageToFile(File, logMessage);
        }

        private void WriteMessageToFile(string fileName, string message)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName); 
            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(message);
                }
            }
        }
    }
}