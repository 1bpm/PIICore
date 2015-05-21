using System;
using System.Text.RegularExpressions;

namespace piiCore
{
	public class Redaction
	{
		private int redactionMode;
		private string censorString="REDACTED";
		private SpecialRedactor redactor;

		public Redaction() {
			redactionMode=1;
		}

		public Redaction(string replacementString) {
			redactionMode=2;
			censorString=replacementString;
		}
		
			

		public Redaction(SpecialRedactor processor) {
			redactor=processor;
			redactionMode=3;
		}

		public string Replace(string input, ValidationTest origin) {
			foreach (Match matched in origin.matches) {
				input=input.Replace(matched.ToString(),GetRedacted(matched.ToString()));
			}
			return input;
		}
		
		private string GetRedacted(string input) {
			string returnValue="";
			switch (redactionMode) {
				case 1:
					returnValue="";
					break;
				case 2:
					returnValue=censorString;
					break;
				case 3:
					returnValue=redactor(input);
					break;
			}
			return returnValue;
		}
	}
}

