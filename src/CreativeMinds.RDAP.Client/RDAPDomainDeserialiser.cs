using CreativeMinds.RDAP.Client.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreativeMinds.RDAP.Client {

	public class RDAPDomainDeserialiser : RDAPDeserialiser {

		public RDAPDomainDeserialiser(CountryLookupHelper countryLookupHelper, AddressHelper addressHelper) : base(countryLookupHelper, addressHelper) { }


	}
}
