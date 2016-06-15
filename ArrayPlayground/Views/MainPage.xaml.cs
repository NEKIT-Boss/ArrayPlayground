using System;
using ArrayPlayground.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;
using System.Diagnostics;
using ArrayPlayground.Models;
using Windows.System;
using Windows.UI.Xaml.Input;
using Windows.UI.Core;

namespace ArrayPlayground.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        /// <summary>
        /// Thing to handle got focus on TextBox to Do the SelectedIndex thing
        /// </summary>
        /// <param name="sender">TextBox that got focus</param>
        /// <param name="e"></param>
        private void EditBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            DependencyObject itemRoot = VisualTreeHelper.GetParent(textBox);
            while (!(itemRoot is ListBoxItem))
            {
                itemRoot = VisualTreeHelper.GetParent(itemRoot);
            }
            
            (itemRoot as ListBoxItem).IsSelected = true;
            textBox.SelectAll();
        }

        private void AddingItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ViewModel.SelectedIndex != -1)
            {
                AddingItems.UpdateLayout();
                AddingItems.ScrollIntoView(e.AddedItems[0]);
            }
        }

        private void AddingItems_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            bool shiftPressed = CoreWindow.GetForCurrentThread()
                .GetKeyState(VirtualKey.Shift)
                .HasFlag(CoreVirtualKeyStates.Down);

            if (e.Key == VirtualKey.Tab
                && shiftPressed )
            {
                if (ViewModel.SelectedIndex == 0)
                {
                    e.Handled = true;
                    ViewModel.AddFront();
                }
            }
            else
            {

                if (e.Key == VirtualKey.Tab)
                {
                    if (ViewModel.SelectedIndex == ViewModel.LastIndex)
                    {
                        e.Handled = true;
                        ViewModel.AddBack();
                    }
                }
            }
        }
    }
}
