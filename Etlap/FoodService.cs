using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Etlap
{
	internal class FoodService
	{
		string conString = "SERVER=localhost;" + "PORT=3306;" + "DATABASE=etlapdb;" + "UID=root;" + "PWD=\"\";";
		
		public List<Etel> LoadData()
		{
			List<Etel> list = new List<Etel>();
			using (MySqlConnection conn = new MySqlConnection(conString))
			{
				conn.Open();

				string sql = "SELECT * FROM etlap ORDER BY id ASC";
				MySqlCommand command = conn.CreateCommand();
				command.CommandText = sql;
				using (MySqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						int id = reader.GetInt32("id");
						string nev = reader.GetString("nev");
						string leiras = reader.GetString("leiras");
						int ar = reader.GetInt32("ar");
						string kategoria = reader.GetString("kategoria");
						Etel etel = new Etel(id, nev, leiras, ar, kategoria);
						list.Add(etel);
                        Console.WriteLine(id + "	" + nev);
                    }
				}
				conn.Close();
			}
			Console.WriteLine("Loaded Data!");
			return list;
		}
		public void NewData(string inputName, string inputDesc, int inputPrice, string inputCategory)
		{
			using (MySqlConnection conn = new MySqlConnection(conString))
			{
				conn.Open();
				string sql = "INSERT INTO etlap (nev, leiras, ar, kategoria) VALUES (@nev,@leiras,@ar,@kategoria)";
				MySqlCommand command = conn.CreateCommand();
				command.CommandText = sql;
				command.Parameters.AddWithValue("@nev", inputName);
				command.Parameters.AddWithValue("@leiras", inputDesc);
				command.Parameters.AddWithValue("@ar", inputPrice);
				command.Parameters.AddWithValue("@kategoria", inputCategory);
				command.ExecuteNonQuery();
			}
			Console.WriteLine("Added Data!");
			MessageBox.Show($"Successfully added {inputName} to the list!");
		}
		public List<string> GetCategories()
		{
			List<string> list = new List<string>();
			using (MySqlConnection conn = new MySqlConnection(conString))
			{
				conn.Open();

				string sql = "SELECT kategoria FROM etlap";
				MySqlCommand command = conn.CreateCommand();
				command.CommandText = sql;
				using (MySqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						string kategoria = reader.GetString("kategoria");
						if (!list.Contains(kategoria))
						{
							list.Add(kategoria);
						}
					}
				}
				conn.Close();
			}
			Console.WriteLine("Loaded Categories!");
			return list;
		}
		public bool Delete(Etel selected)
		{
			using (MySqlConnection conn = new MySqlConnection(conString))
			{
				conn.Open();
				string sql = "DELETE FROM etlap WHERE id=@sel";
				MySqlCommand command = conn.CreateCommand();
				command.CommandText = sql;
				command.Parameters.AddWithValue("@sel", selected.id);
				command.ExecuteNonQuery();
				conn.Close();
			}
			return true;
		}
		public void ModData(Etel selected, double percent)
		{
			using (MySqlConnection conn = new MySqlConnection(conString))
			{
				conn.Open();
				string sql = "UPDATE etlap SET ar=ar+ar*@perc WHERE id=@sel";
				MySqlCommand command = conn.CreateCommand();
				command.CommandText = sql;
				command.Parameters.AddWithValue("@sel", selected.id);
				command.Parameters.AddWithValue("@perc", percent);
				command.ExecuteNonQuery();
				conn.Close();
			}
		}
		public void ModData(double percent)
		{
			using (MySqlConnection conn = new MySqlConnection(conString))
			{
				conn.Open();
				string sql = "UPDATE etlap SET ar=ar+ar*@perc";
				MySqlCommand command = conn.CreateCommand();
				command.CommandText = sql;
				command.Parameters.AddWithValue("@perc", percent);
				command.ExecuteNonQuery();
				conn.Close();
			}
		}
		public void ModData(Etel selected, int amount)
		{
			using (MySqlConnection conn = new MySqlConnection(conString))
			{
				conn.Open();
				string sql = "UPDATE etlap SET ar=ar+@amount WHERE id=@sel";
				MySqlCommand command = conn.CreateCommand();
				command.CommandText = sql;
				command.Parameters.AddWithValue("@sel", selected.id);
				command.Parameters.AddWithValue("@amount", amount);
				command.ExecuteNonQuery();
				conn.Close();
			}
		}
		public void ModData(int amount)
		{
			using (MySqlConnection conn = new MySqlConnection(conString))
			{
				conn.Open();
				string sql = "UPDATE etlap SET ar=ar+@amount";
				MySqlCommand command = conn.CreateCommand();
				command.CommandText = sql;
				command.Parameters.AddWithValue("@amount", amount);
				command.ExecuteNonQuery();
				conn.Close();
			}
		}
	}
}
