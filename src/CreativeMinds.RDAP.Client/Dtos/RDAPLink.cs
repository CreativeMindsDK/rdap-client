using Newtonsoft.Json;
using System;

namespace CreativeMinds.RDAP.Client.Dtos {

	public class RdapLink {
		[JsonProperty("value")]
		public String Value { get; set; }

		[JsonProperty("rel")]
		public String Rel { get; set; }

		[JsonProperty("href")]
		public String Href { get; set; }

		[JsonProperty("type")]
		public String Type { get; set; }
	}
}
