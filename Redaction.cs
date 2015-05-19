using System;

namespace piiCore
{
	public class Redaction
	{
		private int redactionMode=0;
		private string censorString="REDACTED";


		public void SetReplacementString(string replacementString) {
			redactionMode=1;
			censorString=replacementString;
			Init();
		}


		public Redaction() {

		}

		public Redaction(SpecialRedaction processor) {

		}

		private void Init() {

		}

		public string Redact(string input) {
			return null;
		}

		private string ProcessRedaction(string input) {
			string returnValue="";
			switch (redactionMode) {
				case 0:
				returnValue="";
				break;
				case 1:
				returnValue=censorString;
				break;
				case 2:
				returnValue="A REDACTIONS!!";
				break;
			}
			return returnValue;
		}
	}
}

