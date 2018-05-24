using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CorsoWpf.DigitalManager.WpfApp.Helpers
{
    public static class WindowStateHelper
    {
        static readonly string Separator = ";";

        public static bool GetActive(DependencyObject obj)
        {
            return (bool)obj.GetValue(ActiveProperty);
        }

        public static void SetActive(DependencyObject obj, bool value)
        {
            obj.SetValue(ActiveProperty, value);
        }

        public static readonly DependencyProperty ActiveProperty =
            DependencyProperty.RegisterAttached("Active",
            typeof(bool),
            typeof(WindowStateHelper),
            new PropertyMetadata(false, new PropertyChangedCallback(Setup)));

        private static void Setup(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Window wnd = obj as Window;

            if (wnd != null)
            {
                if (GetActive(wnd))
                {
                    wnd.Closed += ctl_Closed;
                    wnd.Loaded += ctl_Loaded;
                }
                else
                {
                    wnd.Closed -= ctl_Closed;
                    wnd.Loaded -= ctl_Loaded;
                }
            }
        }

        static void ctl_Closed(object sender, EventArgs e)
        {
            try
            {
                var wnd = sender as Window;

                string filename = wnd.GetType().ToString().Replace('.', '-') + ".txt";
                string fullfilename = Path.Combine(System.Environment.CurrentDirectory, filename);

                switch (wnd.WindowState)
                {
                    case WindowState.Minimized:
                        {
                            break;
                        }
                    case WindowState.Maximized:
                        {
                            File.WriteAllText(fullfilename, "Maximized");
                            break;
                        }
                    case WindowState.Normal:
                        {
                            string content = string.Concat(wnd.Top, Separator, wnd.Left, Separator, wnd.ActualWidth, Separator, wnd.ActualHeight);
                            File.WriteAllText(fullfilename, content);
                            break;
                        }
                }
            }
            catch (Exception) { }
        }

        static void ctl_Loaded(object sender, RoutedEventArgs e)
        {
            var wnd = sender as Window;

            try
            {
                string filename = wnd.GetType().ToString().Replace('.', '-') + ".txt";
                string fullfilename = Path.Combine(System.Environment.CurrentDirectory, filename);

                if (File.Exists(fullfilename))
                {
                    string content = File.ReadAllText(fullfilename);

                    if (content != "Maximized")
                    {
                        var position = content.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                        var top = double.Parse(position[0]);
                        var left = double.Parse(position[1]);
                        var width = double.Parse(position[2]);
                        var height = double.Parse(position[3]);

                        if (top < System.Windows.SystemParameters.VirtualScreenTop || top > System.Windows.SystemParameters.VirtualScreenHeight)
                        {
                            top = 0;
                        }

                        if (left < System.Windows.SystemParameters.VirtualScreenLeft || left > System.Windows.SystemParameters.VirtualScreenWidth)
                        {
                            left = 0;
                        }

                        wnd.Top = top;
                        wnd.Left = left;
                        wnd.Width = width;
                        wnd.Height = height;
                    }
                    else
                    {
                        wnd.WindowState = WindowState.Maximized;
                    }
                }
            }
            catch (Exception)
            {
                wnd.Top = 0;
                wnd.Left = 0;
                wnd.Width = 320;
                wnd.Height = 240;
            }
        }
    }
}
