using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BO;
using SimulatorLib;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        BackgroundWorker worker;
        BackgroundWorker timerWorker;
        private Stopwatch stopWatch;
        private bool isTimerRun;
        public SimulatorWindow()
        {
            InitializeComponent();
            stopWatch = new Stopwatch();
            timerWorker = new BackgroundWorker();
            timerWorker.DoWork += TimerWorker_DoWork;
            timerWorker.ProgressChanged += TimerWorker_TimerProgressChanged;
            timerWorker.WorkerReportsProgress = true;
            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();
            if (!isTimerRun)
            {
                stopWatch.Restart();
                isTimerRun = true;
                timerWorker.RunWorkerAsync();
            }

        }
        private void worker_DoWork(object? sender, DoWorkEventArgs e)
        {

            Simulator.startSimulator();
            Simulator.StatusChangedEvent += StatusChanged;
            Simulator.FinishSimulatorEvent += finishSimulator;
            if (!Dispatcher.Thread.IsAlive)
            {
                e.Cancel = false;
            }
        }
        public void StatusChanged(BO.Order order, eCondition newCondition, DateTime prev, DateTime next)
        {
            this.Dispatcher.Invoke(() =>
            {
                info.Content = $"information of this order: " + order.orderId.ToString() + "\n" +
                $"previous status: " + order.orderCondition.ToString() + "\n" +
                $"current status: " + newCondition + "\n" +
                    $"begin to change at: " + $" {prev} \n" +
                    $"now:  {next}  ";
            });
        }

        private void TimerWorker_TimerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string timer = stopWatch.Elapsed.ToString();
            timer = timer.Substring(0, 8);
            this.timerTextBlock.Text = timer;
        }
        private void TimerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                timerWorker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }

        private void Button_Click_finishSimulator(object sender, RoutedEventArgs e)
        {
            Simulator.Stop();
            Simulator.StatusChangedEvent -= StatusChanged;
            Simulator.FinishSimulatorEvent -= finishSimulator;
            if (isTimerRun)
            {
                stopWatch.Stop();
                isTimerRun = false;
            }
            Close();
        }

        public void finishSimulator(DateTime end)
        {
            this.Dispatcher.Invoke(() =>
            {
                isTimerRun = false;
                MessageBox.Show("No further orders for treatment: " + end.ToString());
                Close();
            });
        }

    }
}
