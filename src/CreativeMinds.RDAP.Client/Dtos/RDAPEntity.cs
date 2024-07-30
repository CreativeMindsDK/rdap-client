using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CreativeMinds.RDAP.Client.Dtos {

	public class RdapEntity {
		private readonly JCardParser jCardParser = new JCardParser();
		private IList<Object> vcardArrayRaw;

		public RdapEntity() {
			this.Telephones = new List<String>();
			this.Emails = new List<String>();
			this.Address = new List<String>();
		}

		[JsonProperty("objectClassName")]
		public String ObjectClassName { get; set; }

		[JsonProperty("handle")]
		public String Handle { get; set; }

		[JsonProperty("vcardArray")]
		public IList<Object> VcardArrayRaw {
			get { return this.vcardArrayRaw; }
			set {
				this.vcardArrayRaw = value;
				try {
					// Ugly but necessary JCard parsing. For jCard, see https://www.rfc-editor.org/info/rfc7095
					this.jCardParser.Parse(this.vcardArrayRaw, this);
				}
				catch (Exception ex) {
					throw new JCardParseException(ex);
				}
			}
		}

		[JsonProperty("roles")]
		public IList<String> Roles { get; set; }

		[JsonProperty("links")]
		public IList<RdapLink> Links { get; set; }

		[JsonProperty("lang")]
		public String Lang { get; set; }

		[JsonProperty("networks")]
		public IList<String> Networks { get; set; }

		[JsonProperty("autnums")]
		public IList<String> Autnums { get; set; }

		[JsonProperty("entities")]
		public IList<RdapEntity> Entities { get; set; }

		[JsonProperty("events")]
		public IList<RdapEvent> Events { get; set; }

		[JsonIgnore]
		public String FullName { get; set; }

		[JsonIgnore]
		public String Kind { get; set; }

		[JsonIgnore]
		public String Organisation { get; set; }

		[JsonIgnore]
		public ICollection<String> Telephones { get; set; }

		[JsonIgnore]
		public ICollection<String> Emails { get; set; }

		[JsonIgnore]
		public ICollection<String> Address { get; set; }
	}
}
