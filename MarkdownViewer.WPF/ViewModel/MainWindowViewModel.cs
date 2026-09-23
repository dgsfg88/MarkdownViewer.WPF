using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownViewer.WPF.ViewModel
{
    internal partial class MainWindowViewModel : ObservableObject
    {
		[ObservableProperty]
		private string markdownText = "";

		[RelayCommand]
		private async Task OpenFile()
		{

		}
	}
}
