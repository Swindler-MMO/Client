using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Swindler.Game;
using Swindler.Utilities;
using Swindler.Utilities.Extensions;

namespace Swindler.API
{
	public static class ConfigAPI
	{
		private const string HOST = "http://swindler.thebad.xyz";

		public static async Task Load()
		{
			ConfigData v = await HttpUtils.Get<ConfigData>(HOST + $"/configs/client");
			Config.FromView(v.Config);
		}


		public static async Task<Dictionary<string, List<string>>> List()
		{
			var t = await HttpUtils.Get<Dictionary<string, List<string>>>(HOST + "/configs");
			return t;
		}
		
	}
	
	public class ConfigData
	{
		[JsonProperty("config")] public ConfigView Config { get; set; }
	}
}