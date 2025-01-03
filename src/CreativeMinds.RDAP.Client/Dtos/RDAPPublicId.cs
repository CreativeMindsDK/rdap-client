using Newtonsoft.Json;
using System;

namespace CreativeMinds.RDAP.Client.Dtos {

	public class RDAPPublicId {
		[JsonProperty("type")]
		public String Type { get; set; }

		[JsonProperty("identifier")]
		public String Identifier { get; set; }
	}
}
