using System;

namespace piiCore
{
	public static class ValidationDefinitions
	{
		public static ValidationTest postcode=
			new ValidationTest("postcode",
			                    @"(^| |\n|\r)([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9]?[A-Za-z])))) [0-9][A-Za-z]{2})(^| |\n|\r)",
			                    (SpecialRedactor) delegate(string input) {
				return input.Substring(0,3);
			}
			);
		public static ValidationTest phoneNumber=
			new ValidationTest("phone number",
			                    @"( |^|\n|\r)((\(?0\d{4}\)?\s?\d{3}\s?\d{3})|(\(?0\d{3}\)?\s?\d{3}\s?\d{4})|(\(?0\d{2}\)?\s?\d{4}\s?\d{4}))(\s?\#(\d{4}|\d{3}))( |$|\n|\r)",
			                   (SpecialRedactor) delegate(string input) {
				return input.Substring(0,4);
			}
			);
		public static ValidationTest address =
			new ValidationTest ("address",
		                    @"( |^|\n|\r)(flat |house |number )?\d+?[ ]?[,]? [A-Za-z]+ (street|road|lane|avenue|drive|close|crescent|gardens|grove|court|mews|place|plaza|walk)[ ]?[,]? [A-Za-z]+( |$|\n|\r)",
		                    (SpecialRedactor)delegate(string input) {
			return input.Substring (0, 1);
		}
		);

		public static ValidationTest mobilePhoneNumber =
				new ValidationTest ("mobile phone number",
		                     @"( |^|\n|\r)(\+44\s?7\d{3}|\(?07\d{3}\)?)\s?\d{3}\s?\d{3}( |$|\n|\r)",
		                     (SpecialRedactor)delegate(string input) {
			return input.Substring (0, 4);
		}
		);

		public static ValidationTest dateOfBirth=
			new ValidationTest("date of birth",
		                    @"( |^|\n|\r)(((?:0[0-9])|(?:[1-2][0-9])|(?:3[0-1]))\/((?:0[1-9])|(?:1[0-2]))\/([1-2][0-9][0-9][0-9]|[0-9][0-9])|([1-2][0-9][0-9][0-9])-((?:0[1-9])|(?:1[0-2]))-((?:0[0-9])|(?:[1-2][0-9])|(?:3[0-1]))|([1-2][0-9][0-9][0-9])((?:0[1-9])|(?:1[0-2]))((?:0[0-9])|(?:[1-2][0-9])|(?:3[0-1])))( |$|\n|\r)",
		                   (SpecialRedactor) delegate(string input) {
			DateTime dt=DateTime.Parse(input);
			return dt.Year.ToString()+"-"+dt.Month.ToString()+"-01";
		}
		);

		public static ValidationTest nhsNumber=
			new ValidationTest("nhs number",
		                    @"( |^|\n|\r)(\d{3}(| )\d{3}(| )\d{4})( |$|\n|\r)",
		                   (SpecialRedactor) delegate(string input) {
			long theNum=Convert.ToInt64(input.Replace(" ",""));
			long[] ignore={9999999999};
			if (theNum.ToString().Length!=10 || ignore.Contains(theNum)) {
				return false;
			}
			int result=0;
			for (int x=0;x<10;x++) {
				result+=(11-(x+1))* Convert.ToInt32(theNum.ToString()[x]);
			}
			int remainder=result % 11;
			if ((11-remainder)==11) {
				return true;
			} else {
				return false;
			}
		},
		(SpecialRedaction) delegate(string input) {
			return "";
		}
		);


	}
}

