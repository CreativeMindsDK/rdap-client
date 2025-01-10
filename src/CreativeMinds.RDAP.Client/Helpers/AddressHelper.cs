using CreativeMinds.RDAP.Client.Dtos.VCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CreativeMinds.RDAP.Client.Helpers {

	public class AddressHelper {
		private readonly CountryLookupHelper countryLookupHelper;

		public AddressHelper(CountryLookupHelper countryLookupHelper) {
			this.countryLookupHelper = countryLookupHelper ?? throw new ArgumentNullException(nameof(countryLookupHelper));
		}

		public async Task<AddressVCard> ParseAddressAsync(IEnumerable<String> lines, CancellationToken cancellationToken) {
			var output = new AddressVCard { Type = "adr" };

			var country = await this.countryLookupHelper.LookupCountryAsync(lines.Last(), cancellationToken);
			if (country != null) {
				output.Country = country.Alpha2Normalised;

				if (lines.Count() == 2) {
					output.Address1 = lines.First();
				}
				else if (country.Alpha2 == "NL") {
					output.Address1 = lines.First();
					output.City = lines.Skip(1).First();
					if (lines.Count() == 5) {
						output.Province = lines.Skip(2).First();
						output.PostalCode = lines.Skip(3).First();
					}
					else {
						output.PostalCode = lines.Skip(2).First();
					}
				}
				else {
					if (lines.Count() == 3) {
						output.City = lines.First();
						output.PostalCode = lines.Skip(1).First();
					}
					else if (lines.Count() == 4) {
						output.Address1 = lines.First();
						output.City = lines.Skip(1).First();
						output.PostalCode = lines.Skip(2).First();
					}
					else {
						output.Address1 = lines.First();
						output.Address2 = lines.Skip(1).First();
						output.City = lines.Skip(2).First();
						output.PostalCode = lines.Skip(3).First();
					}
				}
			}
			else {
				output.Country = lines.Last();
				output.Address1 = lines.First();
			}

			return output;
		}



	}
}
