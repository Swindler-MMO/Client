using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Swindler.Utilities.Extensions;

namespace Swindler.Utilities
{
	public static class HttpUtils
	{
		private static readonly HttpClient client = new HttpClient();

		public static Task<T> Get<T>(string url)
		{
			return Request<T>(HttpMethod.Get, url);
		}

		public static Task<T> Post<T>(string url, object body)
		{
			return Request<T>(HttpMethod.Post, url, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
		}

		public static Task<T> Delete<T>(string url)
		{
			return Request<T>(HttpMethod.Delete, url);
		}

		public static Task<T> Delete<T>(string url, object body)
		{
			return Request<T>(HttpMethod.Delete, url, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
		}

		public static Task<T> Patch<T>(string url, object body)
		{
			// HttpMethod.Patch is available from .net Standard 2.1, unity use 2.0
			return Request<T>(new HttpMethod("PATCH"), url, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
		}

		public static async Task<T> Request<T>(HttpMethod method, string url, HttpContent content = null)
		{
			using (var request = new HttpRequestMessage(method, url))
			{
				if (content != null)
					request.Content = content;
				//TODO: Use cancellation token ?
				using (var response = await client.SendAsync(request))
				{

					if (response.StatusCode == HttpStatusCode.InternalServerError)
						throw new HttpUtilsError(response.StatusCode, await response.Content.ReadAsStringAsync());

					//(await response.Content.ReadAsStringAsync()).Log();
					
					return DeserializeJsonFromStream<T>(await response.Content.ReadAsStreamAsync());
				}
			}
		}

		//Method from: https://johnthiriet.com/efficient-api-calls/
		private static T DeserializeJsonFromStream<T>(Stream stream)
		{
			if (stream == null || !stream.CanRead)
				return default;

			using (var jtr = new JsonTextReader(new StreamReader(stream)))
			{
				return new JsonSerializer().Deserialize<T>(jtr);
			}
		}

	}

	public class HttpUtilsError : Exception
	{
		public HttpStatusCode Code { get; }
		public new string Message { get; }
		
		public HttpUtilsError(HttpStatusCode code, string message)
		{
			Code = code;
			Message = JsonConvert.DeserializeObject<Dictionary<string, object>>(message)["message"].ToString();
		}
	}

}
