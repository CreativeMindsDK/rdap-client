using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreativeMinds.RDAP.Client.Dtos {

	public abstract class RDAPVCard {
		public String Value { get; set; }
		public String Type { get; set; }
	}
}
