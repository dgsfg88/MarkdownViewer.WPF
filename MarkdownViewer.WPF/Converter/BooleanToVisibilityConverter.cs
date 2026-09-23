using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MarkdownViewer.WPF.Converter
{
	[ValueConversion(typeof(bool), typeof(Visibility))]
	internal class BooleanToVisibilityConverter : IValueConverter
	{
		public bool InvertSource { get; set; } = false;
		public bool UseHidden { get; set; } = false;

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool source)
			{
				if (source ^ InvertSource)
					return Visibility.Visible;
				else
					return UseHidden ? Visibility.Hidden : Visibility.Collapsed;
			}
			return DependencyProperty.UnsetValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> Binding.DoNothing;
	}
}
