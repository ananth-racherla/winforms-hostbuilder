using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Windows.Forms;
using WinFormsMSDependencyInjection.utils;

namespace WinFormsMSDependencyInjection
{
    public partial class MainForm : Form
    {
        public IThing Thing { get; }
        public ILogger<MainForm> Logger { get; }
        public IFormOpener FormOpener { get; }
        public AppConfig Config{ get; set; }
        public RelayConfig RelayConfig { get; set; }

        public MainForm(IThing thing, ILogger<MainForm> logger, IOptions<AppConfig> options, IOptions<RelayConfig> relayOptions, IFormOpener formOpener)
        {
            InitializeComponent();
            Logger = logger;
            FormOpener = formOpener;
            Config = options.Value;
            RelayConfig = relayOptions.Value;

            logger.LogDebug(thing.Hello());

            Capture();
        }


        public void Capture()
        {
            
            Logger.LogDebug("Hey I am in Capture:" + Config.Description);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            FormOpener.ShowModelessForm<ChildForm>();
        }
    }
}
