using System;
using System.Collections.Generic;

namespace CreativeMinds.RDAP.Client {

	public class TldData {
		public String Description { get; set; }
		public DateTime Publication { get; set; }
		public IEnumerable<DataNode> Nodes { get; set; }
		public String Version { get; set; }
	}
}
