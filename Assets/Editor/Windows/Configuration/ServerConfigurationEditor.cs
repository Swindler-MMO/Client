

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Swindler.Json.Utils;
using Swindler.Utilities;
using Swindler.Utilities.Extensions;
using UnityEngine;

namespace Swindler.Editor.Windows
{
	
	public class ServerConfigurationEditor : ScriptableObject
	{

		private const string HOST = "http://swindler.thebad.xyz/configs";
		//private const string HOST = "http://localhost:3000/configs";

		private ConfigurationsEditor _cfgEditor;
		private bool _isLoaded;
		private bool _create;
		private bool _hasErrors;
		
		[Space] [LabelWidth(100)] [ShowIf("_isLoaded")] [EnumToggleButtons]
		public Environments environment;
		
		[Space] [LabelWidth(100)][ShowIf("_isLoaded")] [EnumToggleButtons]
		public new ConfigurationsNames name;

		[Space] 
		
		[Header("General")]
		
		[LabelWidth(150)] [HorizontalGroup("General")]
		public int ticksPerSeconds;
		
		[Header("Resources")]
		
		[SuffixLabel("ms")]
		public int resourceInteractCooldown;

		[SuffixLabel("ms")]
		public int resourceRespawnTime;
		
		[Space]
		
		[TableList(AlwaysExpanded = true, DrawScrollView = false)]
		public List<ServerResource> resources;
		
		[Space]

		[ShowIf("_hasErrors")] [InfoBox("@error", InfoMessageType.Error)]
		public string error;
		
		public async void SetConfig(ConfigurationsEditor cfgEditor, Environments env, ConfigurationsNames cfgName)
		{
			_cfgEditor = cfgEditor;
			environment = env;
			name = cfgName;
			ServerConfigurationEditorView view = await HttpUtils.Get<ServerConfigurationEditorView>(HOST + $"/{name.ToApiName()}?env={environment.ToApiName()}");

			ticksPerSeconds = view.ServerConfig.UpdatesPerSeconds;
			resourceInteractCooldown = view.ServerConfig.ResourceInteractCooldown;
			resourceRespawnTime = view.ServerConfig.ResourceRespawnTime;
			resources = view.ServerConfig.Resources;
			
			_isLoaded = true;
		}
		public void SetCreate(ConfigurationsEditor cfgEditor)
		{
			_cfgEditor = cfgEditor;
			_create = true;
			_isLoaded = true;
		}
		
		[Button("Update")] [HideIf("_create")] [HorizontalGroup("UpdateButtons")] [ShowIf("_isLoaded")]
		public async void Update()
		{
			_isLoaded = false;
			_hasErrors = false;
			
			"Updating config".Log();
			
			await UpdateOrCreate();
			
			_isLoaded = true;
		}
		
		[Button("Add configuration")] [ShowIf("_create")] 
		public async void Create()
		{
			_isLoaded = false;

			await UpdateOrCreate();
			
			_cfgEditor.LoadAllConfiguration();
			
			_isLoaded = true;
		}

		private async Task UpdateOrCreate()
		{
			ServerConfigurationEditorView body = new ServerConfigurationEditorView()
			{
				Env = environment.ToApiName(),
				Name = name.ToApiName(),
				ServerConfig = new ServerConfigView()
				{
					Resources = resources,
					ResourceInteractCooldown = resourceInteractCooldown,
					ResourceRespawnTime = resourceRespawnTime,
					UpdatesPerSeconds = ticksPerSeconds
				}
			};
			try
			{
				await HttpUtils.Post<ServerConfigurationEditorView>(HOST + "/", body);
			}
			catch (HttpUtilsError e)
			{
				_hasErrors = true;
				error = e.Message;
			}
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

	public class ServerResource : ScriptableObject
	{
		[JsonProperty("id")] public byte id;
		[JsonProperty("name")] public new string name;
		[JsonProperty("minAmount")] public int minAmount;
		[JsonProperty("maxAmount")] public int maxAmount;
		[JsonProperty("hitsRequired")] public int hitsRequired;
	}

	public class ServerConfigurationEditorView
	{
		
		[JsonProperty("env")] public string Env { get; set; }
		[JsonProperty("name")] public string Name { get; set; }
		
		[JsonProperty("config")] public ServerConfigView ServerConfig { get; set; }

	}

	public class ServerConfigView
	{
		
		[JsonProperty("updatesPerSeconds")] public int UpdatesPerSeconds { get; set; }
		[JsonProperty("resourceInteractCooldown")] public int ResourceInteractCooldown { get; set; }
		[JsonProperty("resourceRespawnTime")] public int ResourceRespawnTime { get; set; }

		[JsonProperty("resources")] public List<ServerResource> Resources { get; set; }

	}
	
	public class DeleteConfigurationView
	{
		public bool Deleted { get; set; }
	}

}