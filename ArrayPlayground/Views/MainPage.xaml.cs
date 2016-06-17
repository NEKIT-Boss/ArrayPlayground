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

            if (InputViewModel.SelectedIndex != -1)
            {
                AddingItems.UpdateLayout();
                AddingItems.ScrollIntoView(e.AddedItems[0]);
            }
        }

        private void AddingItems_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            bool shiftPressed = CoreWindow.GetForCurrentThread()
                .GetKeyState(VirtualKey.Shift) == CoreVirtualKeyStates.Down;

            if (e.Key == VirtualKey.Tab
                && shiftPressed )
            {
                if (InputViewModel.SelectedIndex == 0)
                {
                    e.Handled = true;
                    InputViewModel.AddFront();
                }
            }
            else
            {

                if (e.Key == VirtualKey.Tab)
                {
                    if (InputViewModel.SelectedIndex == InputViewModel.LastIndex)
                    {
                        e.Handled = true;
                        InputViewModel.AddBack();
                    }
                }
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            FillRandomDialog dialog = new FillRandomDialog();
            dialog.InitialItemsCount = InputViewModel.Items.Count;

            int prevSelected = InputViewModel.SelectedIndex;
            InputViewModel.SelectedIndex = -1;

            var result = await dialog.ShowAsync();

            if (result != ContentDialogResult.Primary)
            {
                InputViewModel.SelectedIndex = prevSelected;
            }
        }

        private void PivotSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InputViewModel.SelectedIndex = -1;
            SearchViewModel.Reset();
        }

        private void SearchingItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedIndex != SearchViewModel.FoundIndex)
            {
                (sender as ListBox).SelectedIndex = SearchViewModel.FoundIndex;
            }
        }
    }
}
