using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;

namespace GameCommon.Data
{
    public class UIBaseItem : INotifyPropertyChanged
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }

        public UIBaseItem(int XPosition, int YPosition)
        {
            XPosition = XPosition;
            YPosition = YPosition;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
