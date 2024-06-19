using System;
using System.Collections.Generic;

namespace CreativeMinds.RDAP.Client {

	public class DataNode {
		public IEnumerable<String> Tlds { get; set; }
		public IEnumerable<String> Servers { get; set; }
	}
}
