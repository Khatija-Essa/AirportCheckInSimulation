using System;
using System.Windows;
using System.Text;

namespace AirportCheckInSimulation
{
    public partial class MainWindow : Window
    {
        private const int EconomyPassengers = 100;
        private const int BusinessPassengers = 20;
        private const int EconomyCounters = 5;
        private const int BusinessCounters = 5;
        private const int EconomyCheckInTime = 10;
        private const int BusinessCheckInTime = 7;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RunSimulationLevel1_Click(object sender, RoutedEventArgs e)
        {
            RunSimulation(false);
        }

        private void RunSimulationLevel2_Click(object sender, RoutedEventArgs e)
        {
            RunSimulation(true);
        }

        private void RunSimulation(bool reallocateCounters)
        {
            CustomQueue<int> economyQueue = new CustomQueue<int>();
            CustomQueue<int> businessQueue = new CustomQueue<int>();

            for (int i = 0; i < EconomyPassengers; i++)
                economyQueue.Enqueue(EconomyCheckInTime);

            for (int i = 0; i < BusinessPassengers; i++)
                businessQueue.Enqueue(BusinessCheckInTime);

            int totalTime = 0;
            int economyTime = 0;
            int businessTime = 0;

            
            int economyCountersAvailable = EconomyCounters;
            int businessCountersAvailable = BusinessCounters;

           
            while (!economyQueue.IsEmpty() || !businessQueue.IsEmpty())
            {
                int timeProcessed = 0;

                
                int businessProcessed = ProcessQueue(businessQueue, businessCountersAvailable);
                timeProcessed = Math.Max(timeProcessed, businessProcessed);

                
                int economyProcessed = ProcessQueue(economyQueue, economyCountersAvailable);
                timeProcessed = Math.Max(timeProcessed, economyProcessed);

                
                if (reallocateCounters && businessQueue.IsEmpty() && !economyQueue.IsEmpty())
                {
                   
                    int reallocatableCounters = businessCountersAvailable;
                    economyProcessed = ProcessQueue(economyQueue, reallocatableCounters);
                    timeProcessed = Math.Max(timeProcessed, economyProcessed);
                    businessCountersAvailable = 0; 
                }

                totalTime += timeProcessed;
                economyTime = Math.Max(economyTime, totalTime);
                businessTime = Math.Max(businessTime, totalTime);
            }

            DisplayResults(economyTime, businessTime, totalTime, reallocateCounters);
        }

        private int ProcessQueue(CustomQueue<int> queue, int counters)
        {
            int timeProcessed = 0;
            for (int i = 0; i < counters && !queue.IsEmpty(); i++)
            {
                timeProcessed = Math.Max(timeProcessed, queue.Dequeue());
            }
            return timeProcessed;
        }

        private void DisplayResults(int economyTime, int businessTime, int totalTime, bool reallocateCounters)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Simulation Results (Level {(reallocateCounters ? "2" : "1")}):");
            sb.AppendLine($"Economy Class Check-in Time: {economyTime} minutes");
            sb.AppendLine($"Business Class Check-in Time: {businessTime} minutes");
            sb.AppendLine($"Total Check-in Time: {totalTime} minutes");

            ResultsTextBox.Text = sb.ToString();
            StatusTextBlock.Text = $"Simulation Level {(reallocateCounters ? "2" : "1")} completed.";
        }
    }
}
