using HomeControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeControlModel.Utility
{
    public class Scheduler
    {
        private Task saver;

        public Scheduler()
        {
            saver = new Task(SaveWithInterval);
            saver.Start();
        }

        private void SaveWithInterval()
        {
            while (true)
            {
                Thread.Sleep(1000*60*5); // 5 minutes interval
                HomeModel.GetInstance().SaveChanges();
            }
        }
    }
}
