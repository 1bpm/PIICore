using System;
using System.Data;
using Excel=Microsoft.Office.Interop.Excel;

namespace piiCore
{
	public class Validator
	{
		private ValidationSuite suite;
		private Redaction redaction;
		public ResultSet results=new ResultSet();
		public DataTable validated;

		public Validator(ValidationSuite suite,Redaction redaction) {
			this.suite = suite;
			this.redaction = redaction;
		}

		public Validator(ValidationSuite suite) {
			this.suite=suite;
		}
		
		


		public Excel.Workbook TestExcel(string filePath) {
			Excel.Application excel=new Excel.Application();
			Excel.Workbook wkBook=excel.Workbooks.Open(filePath, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
			foreach (Excel._Worksheet worksheet in wkBook.Worksheets) {
				Excel.Range range=worksheet.UsedRange;
				int rows=range.Rows.Count;
				int cols=range.Columns.Count;
				for (int rN=1;rN<=rows;rN++) {
					for (int cN=1;cN<=cols;cN++) {
						Excel.Range thisRange=(Excel.Range)range.Cells[rN,cN];
						string testVal=thisRange.Value2.ToString();
						foreach (ValidationTest vld in suite.validators) {
							bool thisResult=vld.Test(testVal);
							if (thisResult) {
								results.Add(new Result(rN,cN,vld,testVal,worksheet.Name));
								if (redaction!=null) {
									Excel.Range replaceRange=(Excel.Range)worksheet.Cells[rN,cN];
									replaceRange.Value2=redaction.Replace(testVal,vld);
								}
							}
						}
					}
				}
			}
			return wkBook;
		}

		/// <summary>
		/// validate a datatable
		/// </summary>
		/// <param name="table">the table to test</param>
		/// <param name="imposeRedactions">whether to call the redactor</param>
		/// <returns></returns>
		public DataTable TestDataTable(DataTable table) {    
			int rowNumber=1;    
			foreach (DataRow row in table.Rows) {
				int colNumber=1;
				foreach (var item in row.ItemArray) {
					foreach (ValidationTest vld in suite.validators) {
						bool thisResult=vld.Test(item.ToString());
						if (thisResult) {
							results.Add(new Result(rowNumber, colNumber,vld,item));
							if (redaction!=null) {
								table.Rows[rowNumber-1][colNumber-1]=redaction.Replace(item.ToString(),vld);
							}
						}
					}
					colNumber++;
				}
				rowNumber++;
			}
			return table;
		}

		
	
}
	
}

