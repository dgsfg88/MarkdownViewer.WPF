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
		private string filename = "";

		[ObservableProperty]
		private string markdownText = "";

		[ObservableProperty]
		private bool canOpenFile = true;

		[ObservableProperty]
		private bool isLoadingAFile = false;

		[ObservableProperty]
		private bool showText = false;

		[RelayCommand(CanExecute = nameof(CanOpenFile))]
		private async Task OpenFile()
		{
			CanOpenFile = false;

			//TODO replace with a interface for testing purpuse
			var dialog = new Microsoft.Win32.OpenFileDialog()
			{
				AddToRecent = true,
				CheckFileExists = true,
				Multiselect = false,
				Title = "Select a MD file",
			};

			if (dialog.ShowDialog() == true)
			{
				IsLoadingAFile = true;
				var fileName = dialog.FileName;

				MarkdownText = await System.IO.File.ReadAllTextAsync(fileName);
				Filename = System.IO.Path.GetFileName(fileName);

				IsLoadingAFile = false;
			}

			CanOpenFile = true;
		}
	}
}
