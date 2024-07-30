using System;

namespace CreativeMinds.RDAP.Client {

	public class JCardParseException : Exception {
		public JCardParseException(Exception exception) : base("Could not parse jCard", exception) { }

		public JCardParseException(String message) : base(message) { }
	}
}
