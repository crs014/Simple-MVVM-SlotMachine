using SimpleSlotMachine.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSlotMachine.Models
{
    public class SlotModel : ObjectBase
    {
        private Slot _slot;

        public SlotModel()
        {
            _slot = new Slot();
        }

        public int Id 
        {
            get
            {
                return _slot.Id;
            }
            set
            {
                if(_slot.Id != value)
                {
                    _slot.Id = value;
                    OnPropertyChanged();
                }
                
            }
        }

        public string Name 
        {
            get
            {
                return _slot.Name;
            }
            set
            {
                if(_slot.Name != value)
                {
                    _slot.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ImagePath 
        {
            get
            {
                return _slot.ImagePath;
            }
            set
            {
                if(_slot.ImagePath != value)
                {
                    _slot.ImagePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Point
        {
            get
            {
                return _slot.Point;
            }
            set
            {
                if(_slot.Point != value)
                {
                    _slot.Point = value;
                    OnPropertyChanged();
                }
            }
        }

        protected class Slot
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string ImagePath { get; set; }

            public int Point { get; set; }
        }
    }
}
