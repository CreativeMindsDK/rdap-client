using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace CreativeMinds.RDAP.Client {

	public class RDAPClient {
		private TldData? data = null;
		private readonly IHttpClientFactory httpClientFactory;

		public RDAPClient(IHttpClientFactory httpClientFactory) {
			this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
		}

		public async Task<Object> ResolveAsync(String tld, String domain) {
			if (data == null) {
				await this.GetRootDataAsync();
			}

			var server = this.data?.Nodes.SingleOrDefault(s => s.Tlds.Contains(tld));

			using var client = this.httpClientFactory.CreateClient();

			var rawData = await client.GetStringAsync($"{server.Servers.First()}/domain/{domain}");

			return null;
		}

		private async Task GetRootDataAsync() {
			String uri = "https://data.iana.org/rdap/dns.json";

			using var client = this.httpClientFactory.CreateClient();

			var rawData = await client.GetFromJsonAsync<TldDataRaw>(uri, new JsonSerializerOptions(JsonSerializerDefaults.Web));

			this.data = new TldData {
				Description = rawData.Description,
				Publication = rawData.Publication,
				Version = rawData.Version,
				Nodes = rawData.Services.Select(n => new DataNode { Servers = n.Last(), Tlds = n.First() })
			};
		}
	}
}
