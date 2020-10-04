using System.Threading.Tasks;
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
			ConfigView v = await HttpUtils.Get<ConfigView>(HOST + "/config/client");
			Config.FromView(v);
		}
		
	}
}