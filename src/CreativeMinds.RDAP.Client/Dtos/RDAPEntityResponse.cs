using CreativeMinds.RDAP.Client.Dtos.VCards;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreativeMinds.RDAP.Client.Dtos {

	public class RDAPEntityResponse {
		[JsonProperty("objectClassName")]
		public String ObjectClassName { get; set; }

		[JsonProperty("handle")]
		public String Handle { get; set; }

		private IList<RDAPVCard> vcard = new List<RDAPVCard>();
		public IEnumerable<RDAPVCard> VCard { get { return this.vcard; } }

		// vcardArray
		[JsonProperty("vcardArray")]
		public Object? VCardArrayRaw {
			get { return null; }
			set {
				var temp = value as JArray;

				var child = temp?.Skip(1).FirstOrDefault();

				var array = child.Children<JToken>();

				foreach (var a in array) {

					var type = a.First().Value<String>();

					switch (type) {
						case "version":
							this.vcard.Add(new VersionVCard { Value = a.Last().Value<String>(), Type = type });
							break;
						case "fn":
							this.vcard.Add(new FullNameVCard { Value = a.Last().Value<String>(), Type = type });
							break;
						case "adr":


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
							this.vcard.Add(new OrganisationVCard { Value = a.Last().Value<String>(), Type = type });
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
			}
		}

		[JsonProperty("roles")]
		public String[] Roles { get; set; }

		[JsonProperty("publicIds")]
		public RDAPPublicId[] PublicIds { get; set; }

		[JsonProperty("entities")]
		public IList<RdapEntity> Entities { get; set; }

		[JsonProperty("remarks")]
		public IList<RdapLink> Remarks { get; set; }

		[JsonProperty("links")]
		public IList<RdapLink> Links { get; set; }

		[JsonProperty("events")]
		public IList<RdapEvent> Events { get; set; }

		// asEventActor

		[JsonProperty("port43")]
		public String Port43 { get; set; }

		[JsonProperty("status")]
		public String[] Status { get; set; }

		[JsonProperty("notices")]
		public IList<RdapNotice> Notices { get; set; }

		[JsonProperty("rdapConformance")]
		public IList<String> RdapConformance { get; set; }

		//[JsonProperty("startAddress")]
		//public String StartAddress { get; set; }

		//[JsonProperty("endAddress")]
		//public String EndAddress { get; set; }

		//[JsonProperty("ipVersion")]
		//public String IpVersion { get; set; }

		//[JsonProperty("name")]
		//public String Name { get; set; }

		//[JsonProperty("type")]
		//public String Type { get; set; }

		//[JsonProperty("errorCode")]
		//public String ErrorCode { get; set; }

		//[JsonProperty("title")]
		//public String Title { get; set; }

		//[JsonProperty("country")]
		//public String Country { get; set; }

		public static RDAPEntityResponse? Parse(String data) {

			// TODO:

			return JsonConvert.DeserializeObject<RDAPEntityResponse>(data);
		}
	}
}
