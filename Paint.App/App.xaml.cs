using System.Windows;

namespace Paint.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// Occurs when the application is loading.
        /// </summary>
        private void OnStartup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();

            //TestWindow testWindow = new();
            //testWindow.Show();
        }
    }
}
