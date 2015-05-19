using System;
using System.Data;

namespace piiCore
{
	public class Validator
	{
		private ValidationSuite suite;
		private RedactionProfile profile;

		public Validator(ValidationSuite suite,RedactionProfile profile) {
			this.suite = suite;
			this.profile = profile;
		}




		public void TestDataTable(DataTable table) {
			TestDataTable(table,false);
		}

		public DataTable TestReader(IDataReader reader,bool imposeRedactions) {
			return null;
		}


		public DataTable GetResults() {
			return results;
		}

		/// <summary>
		/// validate a datatable
		/// </summary>
		/// <param name="table">the table to test</param>
		/// <param name="imposeRedactions">whether to call the redactor</param>
		/// <returns></returns>
		public DataTable TestDataTable(DataTable table, bool imposeRedactions) {    
			InitResultTable();
			int rowNumber=1;    
			foreach (DataRow row in table.Rows) {
				int colNumber=1;
				foreach (var item in row.ItemArray) {
					foreach (ValidatorObject vld in sessionTests) {
						bool thisResult=vld.Test(item.ToString());
						if (thisResult) {
							DataRow resultRow=results.NewRow();
							object[] toInsert=new object[3];
							toInsert[0]=rowNumber;
							toInsert[1]=colNumber;
							toInsert[2]=vld.GetName();
							toInsert[3]=item.ToString();
							results.Rows.Add(toInsert);
							if (imposeRedactions) {
								redactor.Redact(vld.redactor);

								//table[rowNumber-1][colNumber-1]=vld.Redactor(item.ToString());
							}
						}
					}
					colNumber++;
				}
				rowNumber++;
			}
			return table;
		}

		public bool TestString(string inputValue) {
			bool result=false;
			foreach (ValidatorObject vld in sessionTests) {
				bool thisResult=vld.Test(inputValue);
				if (thisResult==true) {
					result=true;
				}
			}
			return result;
		}
	
}

