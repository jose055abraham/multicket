using System;
using System.Windows;
using System.Windows.Input;

namespace Multicket.Module.Components
{
    public static class Focus
    {
        public static readonly DependencyProperty IsFocusedProperty =
        DependencyProperty.RegisterAttached("IsFocused", typeof(bool?), typeof(Focus),
            new FrameworkPropertyMetadata(IsFocusedChanged) { BindsTwoWayByDefault = true });

        public static readonly DependencyProperty IsKeyboardFocusedProperty =
        DependencyProperty.RegisterAttached("IsKeyboardFocused", typeof(bool?), typeof(Focus),
            new FrameworkPropertyMetadata(IsKeyboardFocused) { BindsTwoWayByDefault = true });


        public static bool? GetIsFocused(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            return (bool?)element.GetValue(IsFocusedProperty);
        }

        public static bool? GetIsKeyboardFocused(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            return (bool?)element.GetValue(IsKeyboardFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject element, bool? value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            element.SetValue(IsFocusedProperty, value);
        }

        public static void SetIsKeyboardFocused(DependencyObject element, bool? value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            element.SetValue(IsKeyboardFocusedProperty, value);
        }

        private static void IsFocusedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            if (e.OldValue == null)
            {
                element.GotFocus += FrameworkElement_GotFocus;
                element.LostFocus += FrameworkElement_LostFocus;
            }

            if (!element.IsVisible)
            {
                element.IsVisibleChanged += new DependencyPropertyChangedEventHandler(FrameworkElement_IsVisibleChanged);
            }

            if (e.NewValue != null && (bool)e.NewValue)
            {
                element.Focus();
            }
        }

        private static void IsKeyboardFocused(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            if (e.OldValue == null)
            {
                element.GotFocus += FrameworkElement_GotFocus;
                element.LostFocus += FrameworkElement_LostFocus;
                element.LostKeyboardFocus += FrameworkElement_KeyboardFocus;
            }

            if (!element.IsVisible)
            {
                element.IsVisibleChanged += new DependencyPropertyChangedEventHandler(FrameworkElement_IsVisibleChanged);
            }

            if (e.NewValue != null && (bool)e.NewValue)
            {
                element.Focus();
            }
        }

        private static void FrameworkElement_KeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            if (element.IsVisible && (bool)element.GetValue(IsFocusedProperty))
            {
                element.IsVisibleChanged -= FrameworkElement_IsVisibleChanged;
                element.Focus();
            }
        }

        private static void FrameworkElement_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            if (element.IsVisible && (bool)element.GetValue(IsFocusedProperty))
            {
                element.IsVisibleChanged -= FrameworkElement_IsVisibleChanged;
                element.Focus();
            }
        }

        private static void FrameworkElement_GotFocus(object sender, RoutedEventArgs e)
        {
            ((FrameworkElement)sender).SetValue(IsFocusedProperty, true);

        }

        private static void FrameworkElement_LostFocus(object sender, RoutedEventArgs e)
        {
            ((FrameworkElement)sender).SetValue(IsFocusedProperty, false);
        }

    }
}
