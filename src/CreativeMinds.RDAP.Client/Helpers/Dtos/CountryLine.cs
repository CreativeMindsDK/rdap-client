using System;

namespace CreativeMinds.RDAP.Client.Helpers.Dtos {

	public record CountryLine {
		public String Name { get; set; }
		public String NameNormalised { get; set; }
		public String Alpha2 { get; set; }
		public String Alpha2Normalised { get; set; }
	}
}
