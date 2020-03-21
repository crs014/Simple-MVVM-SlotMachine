using SimpleSlotMachine.Models;
using SimpleSlotMachine.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SimpleSlotMachine.ViewModels
{
    public class MainViewModel : ObjectBase
    {
        private IList<SlotModel> _slotList;
        private SlotModel _slot1;
        private SlotModel _slot2;
        private SlotModel _slot3;
        private readonly TimerModel _timerModel1 = new TimerModel();
        private readonly TimerModel _timerModel2 = new TimerModel();
        private readonly TimerModel _timerModel3 = new TimerModel();
        private bool _isStillRollSlot1;
        private bool _isStillRollSlot2;
        private bool _isStillRollSlot3;
        private bool _lockBet;
        private int _remainingCoin;
        private int _bet;
        private string _description;

        public int RemainingCoin 
        {
            get
            {
                return _remainingCoin;
            }
            set
            {
                if(_remainingCoin != value)
                {
                    _remainingCoin = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Bet
        {
            get
            {
                return _bet;
            }
            set
            {
                if(_bet != value)
                {
                    _bet = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool LockBet
        {
            get
            {
                return _lockBet;
            }
            set
            {
                if(_lockBet != value)
                {
                    _lockBet = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if(_description != value)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        public SlotModel Slot1
        {
            get
            {
                return _slot1;
            }
            set
            {
                if (_slot1 != value)
                {
                    _slot1 = value;
                    OnPropertyChanged();
                }
            }
        }

        public SlotModel Slot2
        {
            get
            {
                return _slot2;
            }
            set
            {
                if (_slot2 != value)
                {
                    _slot2 = value;
                    OnPropertyChanged();
                }
            }
        }

        public SlotModel Slot3
        {
            get
            {
                return _slot3;
            }
            set
            {
                if (_slot3 != value)
                {
                    _slot3 = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand RollSlotMachineCommand
        {
            get;
            private set;
        }

        public ICommand StopRollSlot1Command
        {
            get;
            private set;
        }

        public ICommand StopRollSlot2Command
        {
            get;
            private set;
        }

        public ICommand StopRollSlot3Command
        {
            get;
            private set;
        }

        public MainViewModel()
        {
            _slotList = new List<SlotModel>()
            {
                new SlotModel()
                {
                    Id = 1,
                    Name = "Seven",
                    ImagePath = AppDomain.CurrentDomain.BaseDirectory + @".\Resources\Seven.png",
                    Point = 7
                },
                new SlotModel()
                {
                    Id = 2,
                    Name = "Cherry",
                    ImagePath = AppDomain.CurrentDomain.BaseDirectory + @".\Resources\Cherry.png",
                    Point = 2
                },
                new SlotModel()
                {
                    Id = 3,
                    Name = "Bell",
                    ImagePath = AppDomain.CurrentDomain.BaseDirectory + @".\Resources\Bell.png",
                    Point = 4
                }
            };
            _slot1 = _slotList[0];
            _slot2 = _slotList[0];
            _slot3 = _slotList[0];
            _remainingCoin = 200;

            #region patch timer
            _timerModel1.Timer.Tick += new EventHandler(_Slot1TimeMove);
            _timerModel2.Timer.Tick += new EventHandler(_Slot2TimeMove);
            _timerModel3.Timer.Tick += new EventHandler(_Slot3TimeMove);
            #endregion

            #region Initialize Commands
            RollSlotMachineCommand = new DelegatingCommand(_RollSlotMachineAction);
            StopRollSlot1Command = new DelegatingCommand(_StopRollSlot1Action);
            StopRollSlot2Command = new DelegatingCommand(_StopRollSlot2Action);
            StopRollSlot3Command = new DelegatingCommand(_StopRollSlot3Action);
            #endregion
        }

        private void _Slot1TimeMove(object sender, EventArgs e)
        {
            Slot1 = _ChangeSlotModel();
        }

        private void _Slot2TimeMove(object sender, EventArgs e)
        {
            Slot2 = _ChangeSlotModel();
        }

        private void _Slot3TimeMove(object sender, EventArgs e)
        {
            Slot3 = _ChangeSlotModel();
        }

        private void _StopRollSlot1Action()
        {
            _timerModel1.Timer.Stop();
            _isStillRollSlot1 = false;
            _ConditionMachine();
        }

        private void _StopRollSlot2Action()
        {
            _timerModel2.Timer.Stop();
            _isStillRollSlot2 = false;
            _ConditionMachine();
        }

        private void _StopRollSlot3Action()
        {
            _timerModel3.Timer.Stop();
            _isStillRollSlot3 = false;
            _ConditionMachine();
        }

        private void _RollSlotMachineAction()
        {
            if(!_isStillRollSlot1 && !_isStillRollSlot2 && !_isStillRollSlot3)
            {
                if (_bet > 0)
                {
                    if (_remainingCoin > 0 && _remainingCoin >= _bet)
                    {
                        _timerModel1.Timer.Start();
                        _timerModel2.Timer.Start();
                        _timerModel3.Timer.Start();
                        _isStillRollSlot1 = true;
                        _isStillRollSlot2 = true;
                        _isStillRollSlot3 = true;
                        RemainingCoin = _remainingCoin - _bet;
                        LockBet = true;
                    }
                }
            }
        }

        private void _ConditionMachine()
        {
            if(!_isStillRollSlot1 && !_isStillRollSlot2 && !_isStillRollSlot3)
            {
                if (_slot1.Id == _slot2.Id && _slot2.Id == _slot3.Id)
                {
                    Description = "You Won";
                    RemainingCoin = _remainingCoin + (_bet * _slot1.Point);
                }
                else
                {
                    Description = "You Lose";
                }
                LockBet = false;
            }
        }

        private SlotModel _ChangeSlotModel()
        {
            Random random = new Random();
            int number = random.Next(0, 3);
            return _slotList[number];
        }
    }
}
