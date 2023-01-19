using System.Threading.Tasks;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Squirrel;

namespace SqTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            UpdateMyApp().ConfigureAwait(false);
        }

        private async Task UpdateMyApp()
        {
            // I used ./publish and ./releases folders in project dir
            // change accordingly
            using var mgr = new UpdateManager(@"C:\Users\Johannes\RiderProjects\SqTest\SqTest\releases");
            var newVersion = await mgr.UpdateApp();

            // optionally restart the app automatically, or ask the user if/when they want to restart
            if (newVersion != null)
            {
                var settings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "Update now",
                    NegativeButtonText = "Remind me later",
                    //DialogButtonFontSize = 20D
                };

                MessageDialogResult result = await this.ShowMessageAsync("New Update available",
                    "There is a new update available for ASI!" + SquirrelRuntimeInfo.EntryExePath,
                    MessageDialogStyle.AffirmativeAndNegative,
                    settings);

                if (result == MessageDialogResult.Affirmative)
                {
                    UpdateManager.RestartApp();
                }
            }
        }
    }
}