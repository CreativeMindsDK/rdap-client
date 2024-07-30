using CreativeMinds.RDAP.Client.Dtos;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreativeMinds.RDAP.Client {

	public class JCardParser {

		public void Parse(IList<Object> content, RdapEntity entity) {
			AssignVCard(entity, content.Skip(1).FirstOrDefault() as JArray);
		}

		private void AssignVCard(RdapEntity entity, JArray jArray) {
			if (jArray == null)
				throw new JCardParseException("Invalid JCard received");

			foreach (var child in jArray) {
				var grandChildren = child.Children<JToken>();
				if (grandChildren.Any()) {
					var type = grandChildren.First().Value<string>();
					switch (type) {
						case "fn":
							entity.FullName = GetStringContentFromVCard(grandChildren);
							break;
						case "kind":
							entity.Kind = GetStringContentFromVCard(grandChildren);
							break;
						case "org":
							entity.Organisation = GetStringContentFromVCard(grandChildren);
							break;
						case "tel":
							var tel = GetStringContentFromVCard(grandChildren);
							if (!string.IsNullOrWhiteSpace(tel)) {
								entity.Telephones.Add(tel);
							}
							break;
						case "email":
							var email = GetStringContentFromVCard(grandChildren);
							if (!String.IsNullOrWhiteSpace(email)) {
								entity.Emails.Add(email);
							}
							break;
						case "adr":
							var adr = GetArrayContentFromVCard(grandChildren);
							if (adr != null) {
								entity.Address = adr.ToArray();
							}
							break;
					}
				}
			}
		}

		private static String GetStringContentFromVCard(IEnumerable<JToken> grandChildren) {
			return grandChildren.Skip(1).OfType<JValue>().Select(x => x.Value<String>()).FirstOrDefault(x => x != null && x != "text");
		}

		private static IEnumerable<String> GetArrayContentFromVCard(IEnumerable<JToken> grandChildren) {
			var array = grandChildren.Skip(1).OfType<JArray>().FirstOrDefault();
			if (array != null) {
				return array.Children<JValue>().Select(x => x.Value<String>()).Where(x => !String.IsNullOrWhiteSpace(x));
			}
			return Enumerable.Empty<String>();
		}
	}
}
