using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;
using System.Windows.Interactivity;
using System.Windows.Controls;
using SnakesWpfed.Presenters;
using System.Windows.Shapes;
using System.Threading;
using System.Threading.Tasks;
using SnakesWpfed.Model;
using System.Windows.Threading;

namespace SnakesWpfed.Behaviors
{
    // Adorners must subclass the abstract base class Adorner. 
    public class FocusElement : Behavior<FrameworkElement>
    {
        public DispatcherTimer timer { get; set; }
        public FocusElement()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0,0,0,2);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (AssociatedObject != null)
            {
                try
                {
                    AssociatedObject.Focus();
                }
                catch
                {
                    //bummer
                }
            }
        }

        protected override void OnDetaching()
        {
 
            base.OnDetaching();
        }
        protected override void OnAttached()
        {
        
            try
            {
                AssociatedObject.Focus();
            }
            catch
            {
                //bummer
            }
            base.OnAttached();
        }

    }
}
