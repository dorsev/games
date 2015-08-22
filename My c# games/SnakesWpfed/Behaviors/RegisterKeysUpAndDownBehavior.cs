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

namespace SnakesWpfed.Behaviors
{
    // Adorners must subclass the abstract base class Adorner. 
    public class RegisterKeysUpAndDownBehavior : Behavior<Canvas>
    {
        public RegisterKeysUpAndDownBehavior()
        {

        }

        protected override void OnDetaching()
        {
            AssociatedObject.KeyDown -= new System.Windows.Input.KeyEventHandler(AssociatedObject_KeyDown);
            base.OnDetaching();
        }
        protected override void OnAttached()
        {
            AssociatedObject.KeyDown += new System.Windows.Input.KeyEventHandler(AssociatedObject_KeyDown);
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

        void AssociatedObject_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            DependencyObject canvasItem = AssociatedObject;
            if (canvasItem is Canvas)
            {
                Canvas s = canvasItem as Canvas;
                var sPresenter = s.DataContext as SnakesPresenter;
                if (e.Key == System.Windows.Input.Key.Up)
                {
                    sPresenter.changeDirection(Direction.Up);
                }
                if (e.Key == System.Windows.Input.Key.Down)
                {
                    sPresenter.changeDirection(Direction.Down);
                }
                if (e.Key == System.Windows.Input.Key.Right)
                {
                    sPresenter.changeDirection(Direction.Right);
                }
                if (e.Key == System.Windows.Input.Key.Left)
                {
                    sPresenter.changeDirection(Direction.Left);
                }
            }
        }
    }
}
