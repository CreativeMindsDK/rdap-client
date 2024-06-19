using System;
using System.Collections.Generic;

namespace CreativeMinds.RDAP.Client {

	public class TldDataRaw {
		public String Description { get; set; }
		public DateTime Publication { get; set; }
		public List<List<List<String>>> Services { get; set; }
		public String Version { get; set; }
	}
}
