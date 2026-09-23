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
		private string fullPath = "";

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

		[ObservableProperty]
		private bool canSaveFile = true;

		[RelayCommand(CanExecute = nameof(CanSaveFile))]
		private async Task SaveAs()
		{
			CanSaveFile = false;

			//TODO replace with a interface for testing purpuse
			var dialog = new Microsoft.Win32.OpenFileDialog()
			{
				AddToRecent = true,
				CheckFileExists = false,
				Multiselect = false,
				DefaultExt = ".md",
				FileName = fullPath,
				AddExtension = true,
				Title = "Choose where save the MD file",
			};

			if (dialog.ShowDialog() == true)
			{
				fullPath = dialog.FileName;
				Filename = System.IO.Path.GetFileName(fullPath);
				await Save();
			}
		}

		[RelayCommand(CanExecute = nameof(CanSaveFile))]
		private async Task Save()
		{
			CanSaveFile = false;

			if (string.IsNullOrWhiteSpace(fullPath))
				await SaveAs();
			else
				await System.IO.File.WriteAllTextAsync(fullPath, MarkdownText);

			CanSaveFile = true;
		}

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
				fullPath = dialog.FileName;

				MarkdownText = await System.IO.File.ReadAllTextAsync(fullPath);
				Filename = System.IO.Path.GetFileName(fullPath);

				IsLoadingAFile = false;
			}

			CanOpenFile = true;
		}
	}
}
