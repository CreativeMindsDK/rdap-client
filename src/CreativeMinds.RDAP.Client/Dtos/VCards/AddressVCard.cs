using System;

namespace CreativeMinds.RDAP.Client.Dtos.VCards {

	public class AddressVCard : RDAPVCard {
		public String Address1 { get; set; }
		public String Address2 { get; set; }
		public String Address3 { get; set; }
		public String City { get; set; }
		public String PostalCode {  get; set; }
		public String Province { get; set; }
		public String Country { get; set; }
	}
}
