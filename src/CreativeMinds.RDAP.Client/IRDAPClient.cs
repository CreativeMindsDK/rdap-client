﻿using CreativeMinds.RDAP.Client.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CreativeMinds.RDAP.Client {

	public interface IRDAPClient {
		Task<Boolean> CanResolveAsync(String tld, CancellationToken cancellationToken);
		Task<RDAPResponse?> ResolveAsync(String tld, String domain, CancellationToken cancellationToken);
	}
}
