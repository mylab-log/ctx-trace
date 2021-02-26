# MyLab.Log.Ctx.Trace

For .NET Core 3.1+

[![NuGet](https://img.shields.io/nuget/v/MyLab.Log.Ctx.Trace.svg)](https://www.nuget.org/packages/MyLab.Log.Ctx.Trace/)

Extension for [MyLab.Log.Ctx](https://github.com/mylab-log/ctx) which add trace information into log message.

## Overview

Adds `trace_id` label with trace identifier from `HttpContext` (e.g. `0HM6Q6QM42ERV`)

Integration:

```c#
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
	    //....
	    
        services.AddLogCtx(registrar => 
        		registrar.Register<HttpTraceIdLogCtx>()
        	);	
        
        //....
    }
}
```

Write log example:

```c#
[Get("echo")]
public IActionResult Echo(string message)
{
	_log.Action("Echo").Write();

	return Ok(message);
}
```

Log:

```yaml
Message: Echo
Time: 2021-02-26T12:12:42.653
Labels:
  trace_id: 0HM6Q6QM42ERV
```

