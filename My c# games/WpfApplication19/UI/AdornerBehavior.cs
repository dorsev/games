using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;
using System.Windows.Interactivity;
using System.Windows.Controls;

namespace WpfApplication19.UI
{
    // Adorners must subclass the abstract base class Adorner. 
    public class AdornerBehavior : Behavior<Button>
    {
        public AdornerBehavior()
        {

        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
        protected override void OnAttached()
        {
            var layer = AdornerLayer.GetAdornerLayer(AssociatedObject);
            layer.Add(new GameAdorner(AssociatedObject));

            base.OnAttached();
        }
    }
}
