using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GameCommon;
using GameCommon.Presenters;
using GameCommon.Interfaces;
using SnakesWpfed.Common;
using System.Windows.Threading;
using SnakesWpfed.Model;
using System.Threading;
using Microsoft.Win32;
using System.IO;

namespace SnakesWpfed.Presenters
{
    public class UserCustomizationPresenter : BasePresenter, INotifyPropertyChanged
    {
    

        public DorCommand BrowserPathForHead { get; set; }
        public DorCommand BrowsePathForApple { get; set; }
        public DorCommand BrowsePathForGrass { get; set; }
        

        public UserCustomizationPresenter()
            : base()
        {
            BrowserPathForHead = new DorCommand((param) => ChangeHead(), (() => true));
            BrowsePathForApple = new DorCommand((param) => ChangeApple(), (() => true));
            BrowsePathForGrass = new DorCommand((param) => ChangeGrass(), (() => true));
        }

        private void ChangeHead()
        {
            OpenFileDialog o = new OpenFileDialog() { InitialDirectory = Environment.CurrentDirectory };
            o.Multiselect = false;
            o.ShowDialog();
            if (File.Exists(o.FileName))
            {
                ImageHelper.changeHeadImage(o.FileName);
            }
        }
        private void ChangeBody()
        {
            OpenFileDialog o = new OpenFileDialog() { InitialDirectory = Environment.CurrentDirectory };
            o.Multiselect = false;
            o.ShowDialog();
            if (File.Exists(o.FileName))
            {
                ImageHelper.changeBodyImage(o.FileName);
            }
        }
        private void ChangeGrass()
        {
            OpenFileDialog o = new OpenFileDialog() { InitialDirectory = Environment.CurrentDirectory };
            o.Multiselect = false;
            o.ShowDialog();
            if (File.Exists(o.FileName))
            {
                ImageHelper.changeGrassImage(o.FileName);
            }
        }
        private void ChangeApple()
        {
            OpenFileDialog o = new OpenFileDialog() { InitialDirectory = Environment.CurrentDirectory };
            o.Multiselect = false;
            o.ShowDialog();
            if (File.Exists(o.FileName))
            {
                ImageHelper.changeAppleImage(o.FileName);
            }
        }

        #region InoitfyPropetyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void notifyProperty(string property)
        {
            if (property != null)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(property, new PropertyChangedEventArgs(property));
                }
            }
        }

        #endregion INOitfyPropetyChanged


    }
}
