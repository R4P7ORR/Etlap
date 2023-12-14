using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Etlap
{
	/// <summary>
	/// Interaction logic for FoodForm.xaml
	/// </summary>
	public partial class FoodForm : Window
	{
		FoodService service = new FoodService();
		public FoodForm()
		{
			InitializeComponent();
			inputCategory.ItemsSource = service.GetCategories();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if(inputName.Text.Trim().Length < 1)
			{
				MessageBox.Show("Kérem adjon meg egy nevet!");
				return;
			}
			if (inputDescript.Text.Trim().Length < 1)
			{
				MessageBox.Show("Kérem adjon meg egy leírást!");
				return;
			}
			if (inputPrice.Text.Trim().Length < 1)
			{
				MessageBox.Show("Kérem adjon meg egy árat!");
				return;
			}
			if (!int.TryParse(inputPrice.Text, out int a))
			{
				MessageBox.Show("Az ár csak egész szám lehet!");
				return;
			}
			if (inputCategory.Text == "")
			{
				MessageBox.Show("Kérem válasszon egy kategóriát!");
				return;
			}
			service.NewData(inputName.Text, inputDescript.Text, int.Parse(inputPrice.Text), inputCategory.Text);
            Console.WriteLine($"Added {inputName.Text} to database!");
        }
	}
}
