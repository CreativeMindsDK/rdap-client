using Newtonsoft.Json;
using System;

namespace CreativeMinds.RDAP.Client.Dtos {

	public class RdapEvent {
		[JsonProperty("eventAction")]
		public String EventAction { get; set; }

		[JsonProperty("eventDate")]
		public DateTime EventDate { get; set; }
	}
}
