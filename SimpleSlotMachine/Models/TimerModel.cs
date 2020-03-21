using SimpleSlotMachine.Services;
using System;
using System.Windows.Threading;

namespace SimpleSlotMachine.Models
{
    public class TimerModel : ObjectBase
    {
        public DispatcherTimer Timer { get; }

        public TimerModel()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 5);
            
        }
    }
}
