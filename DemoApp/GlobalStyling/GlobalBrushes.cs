using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DemoApp.GlobalStyling
{
    /// <summary>
    /// Contains global brushes used in UI.
    /// </summary>
    public static class GlobalBrushes
    {
        /// <summary>
        /// The white background color of Emergency Code on an Incident, when Emergency code is unknown <see cref="EmergencyCode"/>.
        /// </summary>
        public static LinearGradientBrush Brush_HeaderBackground = 
            new LinearGradientBrush()
            {
                StartPoint = new System.Windows.Point(0.5, 0),
                EndPoint = new System.Windows.Point(0.5, 1),
                GradientStops = new GradientStopCollection()
                {
                    new GradientStop()
                    {
                        Color = (Color)ColorConverter.ConvertFromString("#66000088"),
                        Offset = 0

                    },
                    new GradientStop()
                    {
                        Color = (Color)ColorConverter.ConvertFromString("#BB000088"),
                        Offset = 1
                    },
                }
            };

        /*
             <LinearGradientBrush x:Key="Brush_HeaderBackground" StartPoint="0.5,0" EndPoint="0.5,1">
              <GradientStop Color="#66000088" Offset="0" />
              <GradientStop Color="#BB000088" Offset="1" />
            </LinearGradientBrush>
         */

    }
}
