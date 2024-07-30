using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CreativeMinds.RDAP.Client.Dtos {

	public class RdapNotice {
		[JsonProperty("title")]
		public String Title { get; set; }

		[JsonProperty("description")]
		public IList<String> Description { get; set; }

		[JsonProperty("links")]
		public IList<RdapLink> Links { get; set; }
	}
}
