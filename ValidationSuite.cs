using System;
using System.Collections.Generic;

namespace piiCore
{
	public class ValidationSuite
	{
		public List<ValidationTest> validators=new List<ValidationTest>();


		public ValidationSuite (List<ValidationTest> tests)
		{
			validators=tests;
		}

		public ValidationSuite()
		{
			validators.Add(ValidationDefinitions.address);
			validators.Add(ValidationDefinitions.dateOfBirth);
			validators.Add(ValidationDefinitions.mobilePhoneNumber);
			validators.Add(ValidationDefinitions.phoneNumber);
			validators.Add(ValidationDefinitions.postcode);
			validators.Add(ValidationDefinitions.nhsNumber);
		}

	}
}

