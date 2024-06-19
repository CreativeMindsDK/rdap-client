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
await client.ResolveAsync("cloud", "klassetrivsel.cloud");
