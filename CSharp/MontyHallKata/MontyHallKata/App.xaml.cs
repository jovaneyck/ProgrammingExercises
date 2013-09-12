using System.Windows;

namespace MontyHallKata
{
    public partial class App
    {
        private void StartApplication(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(new Runner(new RandomSimulationParametersFactory(new RandomDoorNumberGenerator())));
            mainWindow.Show();   
        }
    }
}
