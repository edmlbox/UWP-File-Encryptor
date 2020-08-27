using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace App46.ViewModel
{
    public enum Icon { Folder}
    public class ViewText
    {
        public string OpenFolder { get; } = "Open Folder";
        public string Remove { get; } = "Remove";
        public string Delete { get; } = "Delete";
        public string File { get; } = "File";
        public string AddFiles { get; } = "Add Files";
        public string AddFolder { get; } = "Add Folder";
        public string Exit { get; } = "Exit";
        public string Edit { get; } = "Edit";
        public string RemoveAll { get; } = "Remove All";
        public string RemoveSelected { get; } = "Remove Selected";
        public string SelectMultiply { get; } = "Select Multiply";
        public string SelectAll { get; } = "Select All";
        public string DeselectAll { get; } = "Deselect All";
        public string Folder { get; } = "Folder";
        public string Items__ { get; } = "Selected Files: ";
        public string SelectedItems__ { get; } = "Added Files: ";
        public string Selection { get; } = "Selection";
        public string StopButton { get; } = "Stop";



    }
}
