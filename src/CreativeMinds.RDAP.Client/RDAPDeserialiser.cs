using CreativeMinds.RDAP.Client.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreativeMinds.RDAP.Client {

	public abstract class RDAPDeserialiser {
		protected readonly CountryLookupHelper countryLookupHelper;
		protected readonly AddressHelper addressHelper;

		protected RDAPDeserialiser(CountryLookupHelper countryLookupHelper, AddressHelper addressHelper) {
			this.countryLookupHelper = countryLookupHelper ?? throw new ArgumentNullException(nameof(countryLookupHelper));
			this.addressHelper = addressHelper ?? throw new ArgumentNullException(nameof(addressHelper));
		}




	}
}
