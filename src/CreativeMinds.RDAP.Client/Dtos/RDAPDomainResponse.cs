using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreativeMinds.RDAP.Client.Dtos {

	public class RDAPDomainResponse {
		[JsonProperty("handle")]
		public String Handle { get; set; }

		[JsonProperty("startAddress")]
		public String StartAddress { get; set; }

		[JsonProperty("endAddress")]
		public String EndAddress { get; set; }

		[JsonProperty("ipVersion")]
		public String IpVersion { get; set; }

		[JsonProperty("name")]
		public String Name { get; set; }

		[JsonProperty("type")]
		public String Type { get; set; }

		[JsonProperty("errorCode")]
		public String ErrorCode { get; set; }

		[JsonProperty("title")]
		public String Title { get; set; }

		[JsonProperty("country")]
		public String Country { get; set; }

		[JsonProperty("entities")]
		public IList<RdapEntity> Entities { get; set; }

		[JsonProperty("links")]
		public IList<RdapLink> Links { get; set; }

		[JsonProperty("events")]
		public IList<RdapEvent> Events { get; set; }

		[JsonProperty("rdapConformance")]
		public IList<String> RdapConformance { get; set; }

		[JsonProperty("notices")]
		public IList<RdapNotice> Notices { get; set; }

		[JsonProperty("port43")]
		public String Port43 { get; set; }

		[JsonProperty("objectClassName")]
		public String ObjectClassName { get; set; }


		public static RDAPDomainResponse? Parse(String data) {

			// TODO:

			return JsonConvert.DeserializeObject<RDAPDomainResponse>(data);
		}
	}
}
