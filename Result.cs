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
		public string sourceRef;

		
		public Result(int rowNumber,int colNumber,ValidationTest origin,object value) {
			Init(rowNumber,colNumber,origin,value,null);
		}
		
		public Result(int rowNumber,int colNumber,ValidationTest origin,object value,string sourceRef) {
			Init(rowNumber,colNumber,origin,value,sourceRef);
		}
		
		private void Init (int rowNumber,int colNumber,ValidationTest origin,object value,string sourceRef)
		{ 
			this.rowNumber = rowNumber;
			this.colNumber = colNumber;
			this.origin = origin;
			this.value = value;
			this.sourceRef=sourceRef;
		}

		public object[] Get () 
		{
			object[] returner = new object[4];
			returner[0]=rowNumber;
			returner[1]=colNumber;
			returner [2] = origin.name;
			returner [3] = value.ToString();
			return returner;
		}



	}
}

