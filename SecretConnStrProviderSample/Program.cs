using SecretConnStrProviderSample;

Console.WriteLine(await new ConfigConnectionStringProvider().GetConnectionString("MyDbConnStr"));
