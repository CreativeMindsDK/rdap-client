using CreativeMinds.RDAP.Client.Dtos;
using CreativeMinds.RDAP.Client.Dtos.VCards;
using CreativeMinds.RDAP.Client.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CreativeMinds.RDAP.Client {

	public class RDAPEntityDeserialiser : RDAPDeserialiser {

		public RDAPEntityDeserialiser(AddressHelper addressHelper, CountryLookupHelper countryLookupHelper) : base(countryLookupHelper, addressHelper) { }

		public async Task<RDAPEntityResponse> ParseAsync(String data, CancellationToken cancellationToken) {

			var output = JsonConvert.DeserializeObject<RDAPEntityResponse>(data);

			var temp = output.VCardArrayRaw as JArray;

			var child = temp?.Skip(1).FirstOrDefault();

			var array = child.Children<JToken>();

			var vcard = new List<RDAPVCard>();

			foreach (var a in array) {

				var type = a.First().Value<String>();

				switch (type) {
					case "version":
						vcard.Add(new VersionVCard { Value = a.Last().Value<String>(), Type = type });
						break;
					case "fn":
						vcard.Add(new FullNameVCard { Value = a.Last().Value<String>(), Type = type });
						break;
					case "adr":
						var address = new AddressVCard { Type = type };

						if (a.Skip(1).First() is JToken) {
							var obj = a.Skip(1).First() as JToken;

						}

						if (a.Last() is JArray) {
							var addressLines = a.Last() as JArray;

							List<String> lines = new();
							foreach (var token in addressLines) {
								var j = token as JValue;
								if (j != null && String.IsNullOrWhiteSpace(j.Value<String>()) == false) {
									lines.Add(j.Value<String>());
								}
							}

							address = await this.addressHelper.ParseAddressAsync(lines, cancellationToken);
						}
						vcard.Add(address);

						break;
					case "email":
						break;
					case "contact-uri":
						break;
					case "kind":
						break;
					case "lang":
						break;
					case "org":
						vcard.Add(new OrganisationVCard { Value = a.Last().Value<String>(), Type = type });
						break;
					case "role":
						break;
					case "tel":
						break;
					case "title":
						break;
					case "url":
						break;
				}
			}

			output.VCard = vcard;

			return output;
		}


	}
}
