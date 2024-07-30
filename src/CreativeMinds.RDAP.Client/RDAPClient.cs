using CreativeMinds.RDAP.Client.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CreativeMinds.RDAP.Client {

	public class RDAPClient : IRDAPClient {
		private TldData? data = null;
		private readonly IHttpClientFactory httpClientFactory;

		public RDAPClient(IHttpClientFactory httpClientFactory) {
			this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
		}

		public async Task<RDAPResponse?> ResolveAsync(String tld, String domain, CancellationToken cancellationToken) {
			Activity? activity = Activity.Current;
			activity?.AddEvent(new ActivityEvent("Trying to resolve a domain", tags: new ActivityTagsCollection([new KeyValuePair<String, Object?>("rdap.resolve.tld", tld), new KeyValuePair<String, Object?>("rdap.resolve.domain", domain)])));

			DataNode? server = await this.GetServerAsync(tld, cancellationToken);
			if (server == null) {
				activity?.SetStatus(ActivityStatusCode.Error, "No server found for tld");
				return null;
			}

			using var client = this.httpClientFactory.CreateClient();

			using (HttpResponseMessage response = await client.GetAsync($"{server.Servers.First()}domain/{domain}", cancellationToken)) {
				if (response.IsSuccessStatusCode == true) {
					return JsonConvert.DeserializeObject<RDAPResponse>(await response.Content.ReadAsStringAsync());
				}
				else {
					// TODO: Handle 404 etc...!!
					return null;
				}
			}
			//var rawData = await client.GetStringAsync($"{server.Servers.First()}domain/{domain}");
		}

		private async Task<DataNode?> GetServerAsync(String tld, CancellationToken cancellationToken) {
			if (this.data == null) {
				await this.GetRootDataAsync(cancellationToken);
			}

			return this.data?.Nodes?.SingleOrDefault(s => s.Tlds.Contains(tld));
		}

		private async Task GetRootDataAsync(CancellationToken cancellationToken) {
			// TODO: Local files cache ?!?!?!?
			String uri = "https://data.iana.org/rdap/dns.json";

			using var client = this.httpClientFactory.CreateClient();

			using (HttpResponseMessage response = await client.GetAsync(uri, cancellationToken)) {
				if (response.IsSuccessStatusCode == true) {
					var rawData = await response.Content.ReadFromJsonAsync<TldDataRaw>(cancellationToken);

					this.data = new TldData {
						Description = rawData.Description,
						Publication = rawData.Publication,
						Version = rawData.Version,
						Nodes = rawData.Services.Select(n => new DataNode { Servers = n.Last(), Tlds = n.First() })
					};
				}
				else {
					// TODO: Handle errors!!
				}
			}

			//var rawData = await client.GetFromJsonAsync<TldDataRaw>(uri, new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken);
		}

		public async Task<Boolean> CanResolveAsync(String tld, CancellationToken cancellationToken) {
			return (await this.GetServerAsync(tld, cancellationToken) != null);
		}
	}
}
