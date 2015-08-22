using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameCommon.Data;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using SnakesWpfed.Common;
using SnakesWpfed.Model;

namespace SnakesWpfed.Model.SnakePieces
{
    public class SnakePiece : GamePiece, INotifyPropertyChanged
    {
       
        public SnakePiece(decimal x, decimal y, Direction d)
            : base(x, y, d)
        {
         
        
        }

        
        public SnakePiece(decimal x, decimal y)
            : base(x, y)
        {

        }



        #region Methods

        public void changeAllProperties()
        {

        }

        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion INotifyPropertyChanged
    }
}
