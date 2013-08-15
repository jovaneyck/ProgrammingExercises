using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            runner.RunSimulations(this, int.Parse(this.numberOfSimulations.Text));
        }

        public void ReceiveSimulationResults(int numberOfSuccesses)
        {
            this.numberOfSuccesses.Content = numberOfSuccesses;
        }
    }
}
