using CorsoWpf.DigitalManager.ViewModels.VM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace CorsoWpf.DigitalManager.WpfApp.Converters
{
    public class WeightToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush brush = Brushes.Transparent;
            string status = value.ToString();
            // PersonVM p = parameter as PersonVM;

            // WeightLOW : Green
            // WeightMEDIUM : Orange
            // WeightHIGH : Magenta
            string color = Application.Current.Resources["Weight" + status].ToString();
            brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));

            //switch (status)
            //{
            //    case "LOW":
            //        {
            //            brush = new SolidColorBrush(Colors.Yellow);
            //            break;
            //        }
            //    case "MEDIUM":
            //        {
            //            brush = new SolidColorBrush(Colors.DarkGray);
            //            break;
            //        }
            //    case "HIGH":
            //        {
            //            brush = new SolidColorBrush(Colors.Magenta);
            //            break;
            //        }
            //    default:
            //        break;
            //}

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}