using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Swindler.Utilities;
using Swindler.Utilities.Extensions;
using UnityEngine;

namespace Swindler.Editor.Windows
{
	public class ClientConfigurationEditor : ScriptableObject
	{
		private const string HOST = "http://swindler.thebad.xyz/configs";
		// private const string HOST = "http://localhost:3000/configs";

		private ConfigurationsEditor _cfgEditor;
		private bool _hasErrors;

		[Space] [LabelWidth(100)] [ShowIf("_isLoaded")] [EnumToggleButtons]
		public Environments environment;

		[Space] [LabelWidth(100)] [ShowIf("_isLoaded")] [EnumToggleButtons]
		public new ConfigurationsNames name;

		[Space]
		
		[Header("General")]
		
		[SuffixLabel("s")]
		public float interactCooldown;

		[SuffixLabel("s")]
		public float movementUpdateTime;

		public string gameServer;

		[Space]
		
		[Header("Items")]
		
		[TableList(AlwaysExpanded = true, DrawScrollView = false)]
		public List<ClientItem> items;
		
		[ShowIf("_hasErrors")] [InfoBox("@error", InfoMessageType.Error)]
		public string error;
		
		private bool _isLoaded;
		private bool _create;

		public async void SetConfig(ConfigurationsEditor cfgEditor, Environments env, ConfigurationsNames cfgName)
		{
			_cfgEditor = cfgEditor;
			environment = env;
			name = cfgName;
			ClientConfigurationEditorView view =
				await HttpUtils.Get<ClientConfigurationEditorView>(HOST + $"/{name.ToApiName()}?env={environment.ToApiName()}");

			interactCooldown = view.Config.InteractCooldown;
			movementUpdateTime = view.Config.MovementUpdateTime;
			gameServer = view.Config.GameServer;
			items = view.Config.Items;

			_isLoaded = true;
		}

		[PropertySpace]
		
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
			ClientConfigurationEditorView body = new ClientConfigurationEditorView()
			{
				Env = environment.ToApiName(),
				Name = name.ToApiName(),
				Config = new ClientConfigView()
				{
					InteractCooldown = interactCooldown,
					MovementUpdateTime = movementUpdateTime,
					GameServer = gameServer,
					Items = items
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
		
	}

	public class ClientConfigurationEditorView
	{
		[JsonProperty("env")] public string Env { get; set; }
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("config")] public ClientConfigView Config { get; set; }
	}
	
	public class ClientConfigView
	{
		[JsonProperty("interactCooldown")] public float InteractCooldown { get; set; }
		[JsonProperty("movementUpdateTime")] public float MovementUpdateTime { get; set; }
		[JsonProperty("gameServer")] public string GameServer { get; set; }
		[JsonProperty("items")] public List<ClientItem> Items { get; set; }
	}

	public class ClientItem : ScriptableObject
	{
		[JsonProperty("id")] public byte id;
		[JsonProperty("name")] public new string name;
		[JsonProperty("stackSize")] public int stackSize;
		[JsonProperty("isStackable")] public bool isStackable;
	}
}