using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Windows.Forms;

namespace WinFormsMSDependencyInjection
{
    public partial class MainForm : Form
    {
        public IThing Thing { get; }
        public ILogger<MainForm> Logger { get; }
        public AppConfig Config{ get; set; }
        public RelayConfig RelayConfig { get; set; }

        public MainForm(IThing thing, ILogger<MainForm> logger, IOptions<AppConfig> options, IOptions<RelayConfig> relayOptions)
        {
            InitializeComponent();
            Logger = logger;

            Config = options.Value;
            RelayConfig = relayOptions.Value;

            logger.LogDebug(thing.Hello());

            Capture();
        }


        public void Capture()
        {
            
            Logger.LogDebug("Hey I am in Capture:" + Config.Description);
        }

    }
}
