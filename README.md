This project illustrates a Winforms application that uses Microsoft's [Dependency Injection Framework](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0) and [Logging Framework](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-5.0). The Logging framework uses [Serilog](https://serilog.net/) implementation behind the scenes.

The App uses a Generic Host Builder to bootstrap the application.  HostBuilder patterns are common across Web, Desktop and Console applications. Contrast the bootstrap logic within this [Program.cs](./WinFormsMSDependencyInjection/Program.cs)  with the [Program.cs](https://github.com/ananth-racherla/hello-winforms/blob/main/DITest/Program.cs) within the [hello-winforms project](https://github.com/ananth-racherla/hello-winforms/)

Finally the code illustrates the use of [Options Pattern](https://docs.microsoft.com/en-us/dotnet/core/extensions/options) for reading application config settings. 

References
1. https://www.thecodebuzz.com/dependency-injection-console-app-using-generic-hostbuilder/
2. https://dfederm.com/building-a-console-app-with-.net-generic-host/#:~:text=NET%20Generic%20Host%20is%20a,)%2C%20logging%2C%20and%20configuration.
