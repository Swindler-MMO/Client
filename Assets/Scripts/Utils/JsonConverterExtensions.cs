using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swindler.World.IslandRenderer;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Swindler.Json.Utils
{
	public static class ConverterInitializer
	{
		static bool initialized;

		//Initialize when the game starts
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		static void OnBeforeSceneLoadRuntimeMethod()
		{
			Initialize();
		}

		//Called when scripts are recompiled.
		//This is so Json.Net works in Editor Windows
#if UNITY_EDITOR
		[UnityEditor.Callbacks.DidReloadScripts]
		static void OnScriptsReloaded()
		{
			Initialize();
		}
#endif

		static void Initialize()
		{
			if (initialized)
				return;

			//These types have issues with serialization, because normalized/magnitude are properties
			//These properties return a new instance of the class, so during serialization
			//They cause an endless loop
			//These custom converters only save the x/y/z/w values
			JsonSerializerSettings currentSettings = JsonConvert.DefaultSettings?.Invoke() ?? new JsonSerializerSettings();

			currentSettings.Converters.Add(new JsonVector2Converter());
			currentSettings.Converters.Add(new IslandLayerConverter());
			currentSettings.Converters.Add(new JsonVector2IntConverter());
			//currentSettings.Converters.Add(new JsonQuaternionConverter());

			JsonConvert.DefaultSettings = () => currentSettings;

			initialized = true;
		}
	}

	public class JsonVector2Converter : JsonConverter<Vector2>
	{
		public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
		{
			JObject j = new JObject { { "x", value.x }, { "y", value.y } };

			j.WriteTo(writer);
		}

		//CanRead is false which means the default implementation will be used instead.
		public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			return existingValue;
		}

		public override bool CanWrite => true;

		public override bool CanRead => false;
	}

	class IslandLayerConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(List<IslandLayerView>);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			Debug.Log("Reading");
			JObject jo = JObject.Load(reader);
			List<IslandLayerView> items = new List<IslandLayerView>();

			//Debug.Log("Reading");

			foreach (JProperty prop in jo.Properties())
			{
				IslandLayerView item = prop.Value.ToObject<IslandLayerView>();
				items.Add(item);
			}
			return items;
		}

		public override bool CanWrite
		{
			get { return false; }
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}

	public class JsonVector2IntConverter : JsonConverter<Vector2Int>
	{
		public override void WriteJson(JsonWriter writer, Vector2Int value, JsonSerializer serializer)
		{
			JObject j = new JObject { { "x", value.x }, { "y", value.y } };

			j.WriteTo(writer);
		}

		//CanRead is false which means the default implementation will be used instead.
		public override Vector2Int ReadJson(JsonReader reader, Type objectType, Vector2Int existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			return existingValue;
		}

		public override bool CanWrite => true;

		public override bool CanRead => false;
	}
	
}
