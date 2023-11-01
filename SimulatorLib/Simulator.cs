using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using BlApi;
using BlImplementation;
using BO;
using BlApi;

namespace SimulatorLib
{
    public static class Simulator
    {
        public delegate void StatusChanged(BO.Order order, eCondition newCondition, DateTime prev, DateTime next);
        public static event StatusChanged? StatusChangedEvent = null;
        public delegate void FinishSimulator(DateTime end);
        public static event FinishSimulator? FinishSimulatorEvent = null;
        volatile private static bool stopRequestFlag = false;
        public static bool IsAlive { get; set; } = false;
        public static void startSimulator()
        {
            IsAlive = true;
            Thread thread = new Thread(Run);
            stopRequestFlag = false;
            thread.Start();
        }
        public static void Run()
        {
            IBL bl = BlApi.Factory.Get();
            Random random = new Random();
            eCondition newCondition;

            while (!stopRequestFlag)
            {
                int? currentOrderId = bl?.Order.LongestNotProcessed();
                if (currentOrderId == 0)
                {
                    stopRequestFlag = true;
                    break;
                }
                BO.Order? currentOrder = bl?.Order.GetOrdersDetails(Convert.ToInt32(currentOrderId));
                int CurrentHandleTime = random.Next(1000, 5000);
                BO.eCondition? prevStatus = currentOrder?.orderCondition;
                DateTime startOfChange = DateTime.Now;
                Thread.Sleep(CurrentHandleTime);
                if (currentOrder?.orderCondition == BO.eCondition.OrderSent)
                {
                    bl?.Order.UpdateDelivered(Convert.ToInt32(currentOrderId));
                    newCondition = eCondition.OrderSupllied;
                }
                else
                {
                    bl?.Order.UpdateSent(Convert.ToInt32(currentOrderId));
                    newCondition = eCondition.OrderSent;
                }
                DateTime endOfChange = DateTime.Now;
                StatusChangedEvent?.Invoke(currentOrder ?? throw new Exception(), newCondition, startOfChange, endOfChange);
                Thread.Sleep(1000);
            }
            FinishSimulatorEvent?.Invoke(DateTime.Now);
        }

        public static void Stop()
        {
            stopRequestFlag = true;
            IsAlive = false;
        }
    }
}

