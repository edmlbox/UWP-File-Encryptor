using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App46.ViewModel
{
    class SettingsView
    {

        async public void settingsPageInit()
        {
            TextBox PathTextBox = new TextBox()
            {

                VerticalAlignment = VerticalAlignment.Center,
                MinWidth = 100,
                Margin = new Thickness(5, 0, 0, 0)
            };


            Button SetDefaultFolder = new Button()
            {
                Content = "Choose Folder",
                VerticalAlignment = VerticalAlignment.Center
            };


            TextBlock SettingHeader = new TextBlock()
            {
                Text = "Encrypted files default folder location"
            };


            TextBlock FolderPathString = new TextBlock()
            {
                Text = "Folder Path:",
                VerticalAlignment = VerticalAlignment.Center
            };


            StackPanel stackPanel1 = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 10, 0, 0)
            };

            stackPanel1.Children.Add(FolderPathString);
            stackPanel1.Children.Add(PathTextBox);
            stackPanel1.Children.Add(SetDefaultFolder);

            StackPanel stackPanel = new StackPanel()
            {
                Width = 700,

                Orientation = Orientation.Vertical
            };


            stackPanel.Children.Add(SettingHeader);
            stackPanel.Children.Add(stackPanel1);


            ContentDialog noWifiDialog = new ContentDialog
            {


                Title = "Settings",
                Content = stackPanel,
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
    }
}
