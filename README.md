# csharp-driver-nlog-test

Project to test [csharp-driver NLog integration](https://github.com/datastax/csharp-driver/pull/269)

Using the following [nlog.config](nlog.config), the logs of cassandra driver were stored in a different file. 
```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      autoReload="true"
      internalLogLevel="Warn"
      internalLogFile="internal-nlog.txt">

  <!-- define various log targets -->
  <targets>
    <!-- write logs to file -->
    <target xsi:type="File" name="allfile" fileName="nlog-all-${shortdate}.log"
                 layout="${longdate}|${event-properties:item=EventId.Id}|${logger}|${uppercase:${level}}|${message} ${exception}" />

    <target xsi:type="File" name="driverfile" fileName="nlog-driver-${shortdate}.log"
                 layout="${longdate}|${event-properties:item=EventId.Id}|${logger}|${uppercase:${level}}|${message} ${exception}" />
   
    <target xsi:type="Null" name="blackhole" />
  </targets>

  <rules>
    <!--All logs, including from Microsoft-->
    <logger name="*" minlevel="Trace" writeTo="allfile" />
    <logger name="Cassandra.*" minlevel="Debug" writeTo="driverfile" />

    <!--Skip Microsoft logs and so log only own logs-->
    <logger name="Microsoft.*" minlevel="Trace" writeTo="blackhole" final="true" />
  </rules>
</nlog>
```

Sample of log output file:
```
2016-11-29 13:20:26.2801|0|Cassandra.ControlConnection|INFO|Trying to connect the ControlConnection 
2016-11-29 13:20:26.3518|0|Cassandra.TcpSocket|DEBUG|Socket connected, start reading using SocketEventArgs interface 
2016-11-29 13:20:26.3518|0|Cassandra.Connection|DEBUG|Sending #0 for StartupRequest to 127.0.0.1:9042 
2016-11-29 13:20:26.3818|0|Cassandra.Connection|DEBUG|Received #0 from 127.0.0.1:9042 
2016-11-29 13:20:26.4003|0|Cassandra.ControlConnection|INFO|Connection established to 127.0.0.1:9042 
2016-11-29 13:20:26.4003|0|Cassandra.Connection|DEBUG|Sending #0 for RegisterForEventRequest to 127.0.0.1:9042 
2016-11-29 13:20:26.4003|0|Cassandra.Connection|DEBUG|Received #0 from 127.0.0.1:9042 
2016-11-29 13:20:26.4003|0|Cassandra.ControlConnection|INFO|Refreshing node list 
2016-11-29 13:20:26.4121|0|Cassandra.Connection|DEBUG|Sending #0 for QueryRequest to 127.0.0.1:9042 
2016-11-29 13:20:26.4121|0|Cassandra.Connection|DEBUG|Received #0 from 127.0.0.1:9042 
2016-11-29 13:20:26.4452|0|Cassandra.Connection|DEBUG|Sending #0 for QueryRequest to 127.0.0.1:9042 
2016-11-29 13:20:26.4452|0|Cassandra.Connection|DEBUG|Received #0 from 127.0.0.1:9042 
2016-11-29 13:20:26.4452|0|Cassandra.ControlConnection|INFO|Node list retrieved successfully 
2016-11-29 13:20:26.4592|0|Cassandra.ControlConnection|INFO|Retrieving keyspaces metadata 
2016-11-29 13:20:26.4592|0|Cassandra.Connection|DEBUG|Sending #0 for QueryRequest to 127.0.0.1:9042 
2016-11-29 13:20:26.4592|0|Cassandra.Connection|DEBUG|Received #0 from 127.0.0.1:9042 
2016-11-29 13:20:26.4748|0|Cassandra.ControlConnection|INFO|Rebuilding token map 
2016-11-29 13:20:26.4903|0|Cassandra.Cluster|INFO|Cluster Connected using binary protocol version: [4] 
2016-11-29 13:20:26.4903|0|Cassandra.HostConnectionPool|INFO|Initializing pool to 127.0.0.1:9042 
2016-11-29 13:20:26.4903|0|Cassandra.HostConnectionPool|INFO|Creating a new connection to the host 127.0.0.1:9042 
2016-11-29 13:20:26.5068|0|Cassandra.TcpSocket|DEBUG|Socket connected, start reading using SocketEventArgs interface 
2016-11-29 13:20:26.5068|0|Cassandra.Connection|DEBUG|Sending #0 for StartupRequest to 127.0.0.1:9042 
2016-11-29 13:20:26.5068|0|Cassandra.Connection|DEBUG|Received #0 from 127.0.0.1:9042 
2016-11-29 13:20:26.5223|0|Cassandra.Cluster|INFO|Session connected (16578980) 
```
