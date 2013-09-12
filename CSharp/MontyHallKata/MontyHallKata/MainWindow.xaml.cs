using System.Windows;

namespace MontyHallKata
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : SimulationResultListener
    {
        private readonly SimulationRunner runner;

        public MainWindow(SimulationRunner runner)
        {
            InitializeComponent();
            this.runner = runner;
        }

        public void startSimulations_Click(object sender, RoutedEventArgs e)
        {
            runner.RunSimulations(this, int.Parse(this.numberOfSimulations.Text), checkboxSwitchDoors.IsChecked.GetValueOrDefault());
        }

        public void ReceiveSimulationResults(int nbSuccesses, int nbFailures)
        {
            this.numberOfSuccesses.Content = nbSuccesses;
            this.numberOfFailures.Content = nbFailures;
        }
    }
}
