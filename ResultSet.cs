using System;
using System.Data;
using System.Collections.Generic;

namespace piiCore
{
	public class ResultSet
	{
		public List<Result> results=new List<Result>();

		public ResultSet ()
		{
		}

		public void Add(Result result) {
			results.Add (result);
		}

		public DataTable GetAsDataTable() {
			DataTable table = new DataTable ();
			table.Columns.Add("row",typeof(Int64));
			table.Columns.Add("column",typeof(Int64));
			table.Columns.Add("type",typeof(String));
			table.Columns.Add("value",typeof(String));
			foreach (Result thisResult in results) {
				table.Rows.Add (thisResult.Get ());
			}
		}


	}
}

