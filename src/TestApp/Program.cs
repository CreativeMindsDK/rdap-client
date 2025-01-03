using System.Net.Http;
using CreativeMinds.RDAP.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var host = Host.CreateDefaultBuilder(args)
	.ConfigureServices(services => {
		services.AddHttpClient();
		services.AddScoped<RDAPClient>();
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
var response = await client.ResolveDomainAsync("cz", "philips.cz", new CancellationTokenSource().Token);


var entity = response?.Entities.FirstOrDefault(e => e.Roles.Contains("registrant"));

if (entity?.Links?.Any() == true) {
	var entityResponse = await client.ResolveEntityAsync("cz", "13766990-CZ-REG", new CancellationTokenSource().Token);
}


String temp = response.Country;