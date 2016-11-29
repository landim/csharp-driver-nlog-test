using System;
using NLog.Extensions.Logging;
using Cassandra;
using NLog;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var provider = new NLogLoggerProvider();
            // Add it before initializing the Cluster
            Cassandra.Diagnostics.AddLoggerProvider(provider);
            Cluster cluster = Cluster.Builder().
                AddContactPoint("127.0.0.1").
                Build();
            ISession session = null;
            try
            {
                session = cluster.Connect();
            }
            catch (Exception)
            {
                
            }
        }
    }
}
