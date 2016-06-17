using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ArrayPlayground.Views
{
    public sealed partial class FillRandomDialog : ContentDialog
    {
        public int InitialItemsCount
        {
            set
            {
                ViewModel.ItemsRequired = value;
            }
        }

        public FillRandomDialog()
        {
            this.InitializeComponent();
        }

        //Actually there are two ways of doing stuff, content dialog can do the logic itself,
        //bc we have singletone provider, right? Or we can just pass the parameters to the InputWindow, hmm,
        //If we go with pass on tactics, then the separation will not be that clear, we will not be able to do clear DI, bc
        //The page would be responsible for too much things. Yep I think we just go an create the view model for this thing?
        //Seems ok. Well, do the ViewModel, but not ssure how the navigate to will be invoked
        
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var what = args.GetDeferral();
            //Do the checkings 
            //if (ItemsRequiredTextBox)
            //Make use of it, for long things
            //But at first we need to da it at least somehow.
            //When done, first thing is to separate ViewModel, and start doing the Search thing
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //
        }
    }
}
