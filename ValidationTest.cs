using System;
using System.Text.RegularExpressions;

namespace piiCore
{
	
	public delegate bool PostValidator(string input);
	public delegate string SpecialRedactor(string input);
	
	public class ValidationTest
	{
		public string name;
		private Regex expression;
		public MatchCollection matches;
		private SpecialRedactor redactor;
		private PostValidator postValidator;


		public ValidationTest(string name, string regex, SpecialRedactor redactor) {
			this.name=name;
			this.redactor = redactor;
			expression=new Regex(regex);
		}

		public ValidationTest(string name, string regex, PostValidator postValidator,SpecialRedactor redactor) {
			this.name=name;
			this.postValidator = postValidator;
			this.redactor = redactor;
			expression=new Regex(regex);
		}

		/// <summary>
		/// constructor for regex and delegated PostValidator test
		/// </summary>
		/// <param name="theName">an identifying name of the validation type</param>
		/// <param name="regex">the regex pattern to setup the validator with</param>
		/// <param name="subFunc">the PostValidator delegate</param>
		public ValidationTest(string name, string regex, PostValidator postValidator) {
			this.name=name;
			this.postValidator = postValidator;
			expression=new Regex(regex);
		}

		public bool Test(string inputData) {
			bool returnVal=false;
			if (RegexTest(inputData)) {
				foreach (Match match in matches) {
					if (postValidator!=null) {
						returnVal=postValidator(match.Value);
					} else {
						returnVal=true;
					}
				}
			}
			return returnVal;
		}



		/// <summary>
		/// Run the regex test on input string and store matches
		/// </summary>
		/// <param name="inputData">string to test</param>
		/// <returns>true or false</returns>
		private bool RegexTest(string inputData) {
			bool returnVal=true;
			matches=expression.Matches(inputData);
			if (matches.Count<1) {
				returnVal=false;
			}
			return returnVal;
		}
	}



}

