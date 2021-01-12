using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace gsm.MVVM.Behaviors
{
    public static class InputBindingsManager
    {

        public static readonly DependencyProperty UpdatePropertySourceWhenEnterPressedProperty =
            DependencyProperty.RegisterAttached(
                "UpdatePropertySourceWhenEnterPressed", typeof(DependencyProperty),
                typeof(InputBindingsManager),
                new PropertyMetadata(null, OnUpdatePropertySourceWhenEnterPressedPropertyChanged));

        public static void SetUpdatePropertySourceWhenEnterPressed(DependencyObject dp,
                                                                   DependencyProperty value)
        {
            dp.SetValue(UpdatePropertySourceWhenEnterPressedProperty, value);
        }

        public static DependencyProperty GetUpdatePropertySourceWhenEnterPressed(DependencyObject dp)
        {
            return (DependencyProperty)dp.GetValue(UpdatePropertySourceWhenEnterPressedProperty);
        }

        private static void OnUpdatePropertySourceWhenEnterPressedPropertyChanged(DependencyObject dp,
                                                                                  DependencyPropertyChangedEventArgs e)
        {
            var element = dp as UIElement;

            if (element != null)
            {
                if (e.OldValue != null)
                {
                    element.PreviewKeyDown -= HandlePreviewKeyDown;
                }

                if (e.NewValue != null)
                {
                    element.PreviewKeyDown += new KeyEventHandler(HandlePreviewKeyDown);
                }
            }


        }

        static void HandlePreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DoUpdateSource(e.Source);
            }
        }

        static void DoUpdateSource(object source)
        {
            var property =
                GetUpdatePropertySourceWhenEnterPressed(source as DependencyObject);

            if (property != null)
            {
                var elt = source as UIElement;

                if (elt != null)
                {
                    var binding = BindingOperations.GetBindingExpression(elt, property);

                    if (binding != null)
                    {
                        binding.UpdateSource();
                    }
                }
            }


        }
    }

}
