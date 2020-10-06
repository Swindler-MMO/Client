

using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Swindler.Json.Utils;
using Swindler.Utilities;
using Swindler.Utilities.Extensions;
using UnityEngine;

namespace Swindler.Editor.Windows
{
	[CreateAssetMenu(fileName = "Config", menuName = "Swindler/Config", order = 0)]
	public class Configuration : ScriptableObject
	{

		//private const string HOST = "http://swindler.thebad.xyz/configs";
		private const string HOST = "http://localhost:3000/configs";

		private ConfigurationsEditor _cfgEditor;
		private bool _isLoaded;
		private bool _create;
		
		[Space] [LabelWidth(100)] [ShowIf("_isLoaded")] [EnumToggleButtons]
		public Environments environment;
		
		[Space] [LabelWidth(100)][ShowIf("_isLoaded")]
		public new string name;

		[Space]
		
		[TextArea(20, 30)] [ShowIf("_isLoaded")]
		public string configuration;

		public async void SetConfig(ConfigurationsEditor cfgEditor, Environments env, string cfgName)
		{
			_cfgEditor = cfgEditor;
			environment = env;
			name = cfgName;
			ConfigurationEditorView view = await HttpUtils.Get<ConfigurationEditorView>(HOST + $"/{name}?env={environment.ToApiName()}");
			configuration = view.Config;
			_isLoaded = true;
		}
		public void SetCreate(ConfigurationsEditor cfgEditor)
		{
			_cfgEditor = cfgEditor;
			_create = true;
			_isLoaded = true;
		}
		
		[Button("Update")] [HideIf("_create")] [HorizontalGroup("UpdateButtons")] [ShowIf("_isLoaded")]
		public void Update()
		{
			_isLoaded = false;
			
			ConfigurationEditorView body = new ConfigurationEditorView()
			{
				Env = environment.ToApiName(),
				Name = name,
				Config = configuration
			};

			HttpUtils.Post<ConfigurationEditorView>(HOST + "/", body);

			_isLoaded = true;
		}
		
		[Button("Add configuration")] [ShowIf("_create")] 
		public async void Create()
		{
			_isLoaded = false;
			
			ConfigurationEditorView body = new ConfigurationEditorView()
			{
				Env = environment.ToApiName(),
				Name = name,
				Config = configuration
			};

			await HttpUtils.Post<ConfigurationEditorView>(HOST + "/", body);

			_cfgEditor.LoadAllConfiguration();
			
			_isLoaded = true;
		}

		[Button("Delete")] [HideIf("_create")] [HorizontalGroup("UpdateButtons")] [ShowIf("_isLoaded")]
		[GUIColor(1f, 0.7f, 0.7f)]
		public async void Delete()
		{
			"Deleting config".Log();
			var r = await HttpUtils.Delete<DeleteConfigurationView>(HOST + $"/{name}?env={environment.ToApiName()}");
			"Deleted".Log();
			_cfgEditor.LoadAllConfiguration();
		}
		
	}

	public class ConfigurationEditorView
	{
		
		[JsonProperty("env")] public string Env { get; set; }
		[JsonProperty("name")] public string Name { get; set; }
		
		[JsonConverter(typeof(JsonConverterObjectToString))]
		[JsonProperty("config")] public string Config { get; set; }
		
	}

	public class DeleteConfigurationView
	{
		public bool Deleted { get; set; }
	}

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
	
}