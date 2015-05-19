using System;
using System.Data;

namespace piiCore
{
	public class Result
	{
		public int rowNumber;
		public int colNumber;
		public ValidationTest origin;
		public object value;

		public Result (int rowNumber,int colNumber,ValidationTest origin,object value)
		{
			this.rowNumber = rowNumber;
			this.colNumber = colNumber;
			this.origin = origin;
			this.value = value;
		}

		public object[] Get () 
		{
			object[] returner = new object[4];
			(int)returner[0]=rowNumber;
			(int)returner[1]=colNumber;
			(ValidationTest)returner [2] = origin.name;
			(string)returner [3] = value;
			return returner;
		}



	}
}

