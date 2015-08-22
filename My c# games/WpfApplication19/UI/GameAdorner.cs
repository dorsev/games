using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;

namespace WpfApplication19.UI
{
    // Adorners must subclass the abstract base class Adorner. 
    public class GameAdorner : Adorner
    {
        // Be sure to call the base class constructor. 
        public GameAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Rect adornedElementRect = new Rect(this.AdornedElement.DesiredSize);

            // Some arbitrary drawing implements.
            SolidColorBrush renderBrush = new SolidColorBrush(Colors.Green);
            renderBrush.Opacity = 0.2;
            Pen renderPen = new Pen(new SolidColorBrush(Colors.Green), 2.0);
            Pen ThickrenderPen = new Pen(new SolidColorBrush(Colors.Green), 4.0);

            // Draw a circle at each corner.
            //drawingContext.DrawVideo(new MediaPlayer() {  SpeedRatio = 1 }, adornedElementRect);
            
            drawingContext.DrawLine(renderPen, adornedElementRect.BottomLeft, adornedElementRect.TopLeft);
            drawingContext.DrawLine(renderPen, adornedElementRect.BottomRight, adornedElementRect.TopRight);
            drawingContext.DrawLine(ThickrenderPen, adornedElementRect.TopLeft, adornedElementRect.TopRight);
            drawingContext.DrawLine(ThickrenderPen, adornedElementRect.BottomLeft, adornedElementRect.BottomRight);

            
        }
    }
}
