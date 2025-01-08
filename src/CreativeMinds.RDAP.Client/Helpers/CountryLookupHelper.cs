using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CreativeMinds.RDAP.Client.Helpers {

	public class CountryLookupHelper {
		private IEnumerable<Dtos.CountryLine> lines = null;
		private readonly IHttpClientFactory httpClientFactory;

		public CountryLookupHelper(IHttpClientFactory httpClientFactory) {
			this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
		}

		public async Task<Dtos.CountryLine?> LookupCountryAsync(String value, CancellationToken cancellationToken) {
			if (String.IsNullOrWhiteSpace(value) == true) {
				throw new ArgumentNullException(nameof(value));
			}

			if (this.lines == null) {
				using var client = this.httpClientFactory.CreateClient();

				using (HttpResponseMessage response = await client.GetAsync("https://raw.githubusercontent.com/lukes/ISO-3166-Countries-with-Regional-Codes/refs/heads/master/all/all.csv", cancellationToken)) {
					response.EnsureSuccessStatusCode();

					String data = await response.Content.ReadAsStringAsync();

					List<Dtos.CountryLine> countries = new List<Dtos.CountryLine>();
					String[] countryLines = data.Split("\n", StringSplitOptions.RemoveEmptyEntries);
					foreach (var line in countryLines.Skip(1)) {
						String name = GetNextValue(line);
						String rest = line.Substring(name.Length);
						rest = rest.Substring(rest.IndexOf(',') + 1);
						String iso = GetNextValue(rest);

						countries.Add(new Dtos.CountryLine { Name = name, NameNormalised = name.ToLowerInvariant(), Alpha2 = iso, Alpha2Normalised = iso.ToLowerInvariant() });
					}

					//countries.Add(new Dtos.CountryLine { Name = "Netherlands", NameNormalised = "netherlands", Alpha2 = "NL", Alpha2Normalised = "nl" });
					this.lines = countries;
				}
			}

			return this.lines?.SingleOrDefault(l => l.NameNormalised == value.ToLowerInvariant() || l.Alpha2Normalised == value.ToLowerInvariant());
		}

		private static String GetNextValue(String data) {
			String output = String.Empty;
			Int32 index = 0;
			Boolean stringStarted = false;
			while (true) {
				if (data[index] == '\"') {
					if (stringStarted == false) {
						stringStarted = true;
						index++;
					}
					else {
						stringStarted = false;
						break;
					}
				}

				if (stringStarted == false && data[index] == ',') {
					break;
				}
				else {
					output += data[index];
				}

				index++;
			}
			return output;
		}
	}
}
