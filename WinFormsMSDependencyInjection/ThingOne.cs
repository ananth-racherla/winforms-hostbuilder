using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WinFormsMSDependencyInjection
{
    public class ThingOne : IThing
    {
        public ThingOne(ILogger<ThingOne> logger)
        {

        }

        public string Hello()
        {
            return "I am thing one";
        }
    }

    public class ThingTwo : IThing
    {
        public ILogger<ThingTwo> Logger { get; set; }
        public RelayConfig Config { get; set; }

        public ThingTwo(ILogger<ThingTwo> logger, IOptions<RelayConfig> options)
        {
            Logger = logger;
            Config = options.Value;
        }

        public string Hello()
        {
            Logger.LogDebug("Hey I am in the Hello function of thing two. " + Config.Position);
            return "I am thing two";
        }
    }
}
