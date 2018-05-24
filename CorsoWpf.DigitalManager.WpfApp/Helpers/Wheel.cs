using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CorsoWpf.DigitalManager.WpfApp.Helpers
{
    public class Wheel
    {
        #region Up
        public static ICommand GetUpCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(UpCommandProperty);
        }

        public static void SetUpCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(UpCommandProperty, value);
        }

        public static readonly DependencyProperty UpCommandProperty =
            DependencyProperty.RegisterAttached("UpCommand",
            typeof(ICommand),
            typeof(Wheel),
            new PropertyMetadata(null,
            new PropertyChangedCallback(Setup)));

        public static object GetUpCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(UpCommandParameterProperty);
        }

        public static void SetUpCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(UpCommandParameterProperty, value);
        }

        public static readonly DependencyProperty UpCommandParameterProperty =
            DependencyProperty.RegisterAttached("UpCommandParameter",
            typeof(object),
            typeof(Wheel),
            new PropertyMetadata(null));
        #endregion

        #region Down
        public static ICommand GetDownCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DownCommandProperty);
        }

        public static void SetDownCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DownCommandProperty, value);
        }

        public static readonly DependencyProperty DownCommandProperty =
            DependencyProperty.RegisterAttached("DownCommand",
            typeof(ICommand),
            typeof(Wheel),
            new PropertyMetadata(null,
            new PropertyChangedCallback(Setup)));

        public static object GetDownCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(DownCommandParameterProperty);
        }

        public static void SetDownCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(DownCommandParameterProperty, value);
        }

        public static readonly DependencyProperty DownCommandParameterProperty =
            DependencyProperty.RegisterAttached("DownCommandParameter",
            typeof(object),
            typeof(Wheel),
            new PropertyMetadata(null));
        #endregion

        #region Attached (to avoid to subscribe the event 'ManipulationCompleted' multiple times
        public static bool GetAttached(DependencyObject obj)
        {
            return (bool)obj.GetValue(AttachedProperty);
        }

        public static void SetAttached(DependencyObject obj,
            bool value)
        {
            obj.SetValue(AttachedProperty, value);
        }

        private static readonly DependencyProperty AttachedProperty =
            DependencyProperty.RegisterAttached("Attached",
            typeof(bool),
            typeof(UIElement),
            new PropertyMetadata(false));
        #endregion

        private static void Setup(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            UIElement ctl = obj as UIElement;

            if (ctl != null)
            {
                ICommand oldValue = (ICommand)e.OldValue;
                ICommand newValue = (ICommand)e.NewValue;

                if (oldValue == null && newValue != null)
                {
                    if (GetAttached(ctl) == false)
                    {
                        SetAttached(ctl, true);
                        ctl.MouseWheel += ctl_PointerWheelChanged;
                    }
                }
                else if (oldValue != null && newValue == null)
                {
                    ctl.SetValue(AttachedProperty, false);
                    ctl.MouseWheel -= ctl_PointerWheelChanged;
                }
            }
        }

        static void ctl_PointerWheelChanged(object sender, MouseWheelEventArgs e)
        {
            var element = sender as UIElement;
            ICommand command = null;
            object parameter = null;

            if (e.Delta < 0)
            {
                command = GetDownCommand(element);
                parameter = GetDownCommandParameter(element);
            }
            else
            {
                command = GetUpCommand(element);
                parameter = GetUpCommandParameter(element);
            }

            if (command != null)
            {
                if (command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
            }
        }
    }
}
