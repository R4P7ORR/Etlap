using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etlap
{
	internal class Etel
	{
		public Etel(int id, string nev, string leiras, int ar, string kategoria)
		{
			this.id = id;
			this.nev = nev;
			this.leiras = leiras;
			this.ar = ar;
			this.kategoria = kategoria;
		}

		public int id { get; set; }
		public string nev { get; set; }
		public string leiras { get; set; }
		public int ar { get; set; }
		public string kategoria { get; set; }
	}
}
