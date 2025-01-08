using CreativeMinds.RDAP.Client;
using CreativeMinds.RDAP.Client.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;


var host = Host.CreateDefaultBuilder(args)
	.ConfigureServices(services => {
		services.AddHttpClient();
		services.AddScoped<RDAPClient>();
		services.AddSingleton<CountryLookupHelper>();
		services.AddSingleton<AddressHelper>();
		services.AddScoped<RDAPEntityDeserialiser>();
	})
	.Build();

var client = host.Services.GetRequiredService<RDAPClient>();


//await client.ResolveAsync("com", "google.com");
//await client.ResolveAsync("org", "iana.org");
//await client.ResolveAsync("arpa", "152.112.149.in-addr.arpa");
//var response = await client.ResolveAsync("cloud", "klassetrivsel.cloud",new  CancellationTokenSource().Token);
//var response = await client.ResolveAsync("com", "staytransparent.com", new CancellationTokenSource().Token);
//var response = await client.ResolveAsync("com", "cnn.com", new CancellationTokenSource().Token);
//var response = await client.ResolveAsync("fi", "philips.fi", new CancellationTokenSource().Token);
//var response = await client.ResolveAsync("eu", "philips.eu", new CancellationTokenSource().Token);
//var response = await client.ResolveAsync("de", "philips.de", new CancellationTokenSource().Token);
//var response = await client.ResolveAsync("be", "philips.be", new CancellationTokenSource().Token);
//var response = await client.ResolveAsync("no", "philips.no", new CancellationTokenSource().Token);
//var response = await client.ResolveAsync("fr", "philips.fr", new CancellationTokenSource().Token);


List<String> domains = new List<String> {
	"hirrest.fi",
	"longtailboat.fr",
	"picabot.fr",
	"yvadev.fr",
	"nr1gratisadverteren.nl",
	"bldigitalagency.fr",
	"philips.fi",
	"oleva.fi",
	"azur-courtage.fr",
	"kshoes.fr",
	"philips.cz",
	"logyx.fr",
	"coach-enligne.fr",
	"coach-enligne.fr",
	"garage-reungoat.fr",
	"pacificcoaching.nl",
	"gessl-ines.fr",
	"descenesetdefeu.fr",
	"vlielands.nl"
};

foreach (String domain in domains) {
	var response = await client.ResolveDomainAsync(domain.Substring(domain.IndexOf(".") + 1), domain, new CancellationTokenSource().Token);
	var entity = response?.Entities.FirstOrDefault(e => e.Roles.Contains("registrant"));

	if (entity != null && entity.Address != null && entity.Address.Any() == true) {
		var addy = await host.Services.GetRequiredService<AddressHelper>().ParseAddressAsync(entity.Address, new CancellationTokenSource().Token);
	}
	else if (entity?.Links?.Any() == true) {
		var entityResponse = await client.ResolveEntityAsync(domain.Substring(domain.IndexOf(".") + 1), entity.Links.First().Value.Substring(entity.Links.First().Value.LastIndexOf("/") + 1), new CancellationTokenSource().Token);
	}

	String temp = response.Country;
}


