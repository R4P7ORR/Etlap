using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Etlap
{
	public partial class MainWindow : Window
	{
		FoodService service = new FoodService();
		public MainWindow()
		{
			InitializeComponent();
			foodTable.ItemsSource = service.LoadData();
		}

		private void btnFoodForm_Click(object sender, RoutedEventArgs e)
		{
			FoodForm form = new FoodForm();
			form.Closed += (_, _) =>
			{
				foodTable.ItemsSource = service.LoadData();
			};
			form.ShowDialog();
		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			Etel selected = foodTable.SelectedItem as Etel;
			if (selected == null)
			{
				MessageBox.Show("Adat törléséhez először válasszon ki egy elemet!");
				return;
			}
			MessageBoxResult result = MessageBox.Show($"Biztosan törölni akarja {selected.nev}-t a listából?", "Törlés", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				if (!service.Delete(selected))
				{
					MessageBox.Show($"Hiba történt {selected.nev} eltávolítása közben!");
				}
				else
				{
					MessageBox.Show($"Sikeresen törölte {selected.nev}-t a listából!");
				}
				foodTable.ItemsSource = service.LoadData();
			}
		}

		private void btnPercent_Click(object sender, RoutedEventArgs e)
		{
			if (raisePercent.Text.Trim().Length < 1)
			{
				MessageBox.Show("Módosításhoz adjon meg egy értéket!");
				return;
			}
			if (!int.TryParse(raisePercent.Text.Trim(), out int a))
			{
				MessageBox.Show("Módosítást csak egész számokkal végezhet!");
				return;
			}
			Etel selected = foodTable.SelectedItem as Etel;
			if (foodTable.SelectedItem is Etel)
			{
				MessageBoxResult result = MessageBox.Show($"Biztosan módosítani akarja {selected.nev}-t?", "Százalékos Módosítás", MessageBoxButton.YesNo);
				if (result == MessageBoxResult.Yes)
				{
					service.ModData(selected, double.Parse(raisePercent.Text) / 100);
					foodTable.ItemsSource = service.LoadData();
				}
			}
			else
			{
				MessageBoxResult result = MessageBox.Show($"Biztosan módosítani akarja az összes ételt?", "Százalékos Módosítás", MessageBoxButton.YesNo);
				if (result == MessageBoxResult.Yes)
				{
					service.ModData(double.Parse(raisePercent.Text) / 100);
					foodTable.ItemsSource = service.LoadData();
				}
			}
			raisePercent.Text = "";
		}

		private void btnAmount_Click(object sender, RoutedEventArgs e)
		{
			if (raiseAmount.Text.Trim().Length < 1)
			{
				MessageBox.Show("Módosításhoz adjon meg egy értéket!");
				return;
			}
			if (!int.TryParse(raiseAmount.Text.Trim(), out int a))
			{
				MessageBox.Show("Módosítást csak egész számokkal végezhet!");
				return;
			}
			Etel selected = foodTable.SelectedItem as Etel;
			if (foodTable.SelectedItem is Etel)
			{
				MessageBoxResult result = MessageBox.Show($"Biztosan módosítani akarja {selected.nev}-t?", "Értékes Módosítás", MessageBoxButton.YesNo);
				if (result == MessageBoxResult.Yes)
				{
					service.ModData(selected, int.Parse(raiseAmount.Text));
					foodTable.ItemsSource = service.LoadData();
				}
			}
			else
			{
				MessageBoxResult result = MessageBox.Show($"Biztosan módosítani akarja az összes ételt?", "Értékes Módosítás", MessageBoxButton.YesNo);
				if (result == MessageBoxResult.Yes)
				{
					service.ModData(int.Parse(raiseAmount.Text));
					foodTable.ItemsSource = service.LoadData();
				}
			}
			raiseAmount.Text = "";
		}
	}
}
