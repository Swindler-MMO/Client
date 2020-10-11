using System;

namespace Swindler.Editor.Windows
{
	public enum Environments
	{
		Production,
		Local,
		Development
	}
	
	public static class EnvironmentsExtensions
	{
		public static string ToApiName(this Environments env)
		{
			switch (env)
			{
				case Environments.Production:
					return "prod";
				case Environments.Local:
					return "local";
				case Environments.Development:
					return "dev";
				default:
					throw new ArgumentOutOfRangeException(nameof(env), env, null);
			}
		}
		
		public static Environments FromApiName(string name)
		{
			switch (name)
			{
				case "prod": return Environments.Production;
				case "local": return Environments.Local;
				case "dev": return Environments.Development;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}

	public enum ConfigurationsNames
	{
		Client,
		Server
		
	}
	
	public static class ConfigurationsNamesExtensions
	{
		public static string ToApiName(this ConfigurationsNames env)
		{
			switch (env)
			{
				case ConfigurationsNames.Client:
					return "client";
				case ConfigurationsNames.Server:
					return "server";
				default:
					throw new ArgumentOutOfRangeException(nameof(env), env, null);
			}
		}
		
		public static ConfigurationsNames FromApiName(string name)
		{
			switch (name)
			{
				case "server": return ConfigurationsNames.Server;
				case "client": return ConfigurationsNames.Client;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
	
}