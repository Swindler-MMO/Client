using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using Swindler.API;
using Swindler.Utilities.Extensions;
using UnityEditor;
using UnityEngine;

namespace Swindler.Editor.Windows
{
	public class ConfigurationsEditor : OdinMenuEditorWindow
	{

		private bool _treeRebuild;
		private Dictionary<string, List<string>> _configs = new Dictionary<string, List<string>>();

		[MenuItem("Swindler/Configurations %h")]
		private static void OpenWindow()
		{
			GetWindow<ConfigurationsEditor>().Show();
		}

		protected override void OnEnable()
		{
			LoadAllConfiguration();
		}

		protected override void OnGUI()
		{
			if (_treeRebuild && Event.current.type == EventType.Layout)
			{
				ForceMenuTreeRebuild();
				_treeRebuild = false;
			}

			SirenixEditorGUI.Title("Swindler Game Manager", "Game manager settings", TextAlignment.Center, false);

			base.OnGUI();
		}

		protected override OdinMenuTree BuildMenuTree()
		{
			OdinMenuTree tree = new OdinMenuTree();

			tree.Add("Create new", new AddNewConfiguration(this));

			AddConfigsMenu(tree);

			return tree;
		}

		private void AddConfigsMenu(OdinMenuTree tree)
		{
			foreach (var cfg in _configs)
			{
				Environments env = EnvironmentsExtensions.FromApiName(cfg.Key);
				foreach (string cfgName in cfg.Value)
					tree.AddMenuItemAtPath(env.ToString(),
						new OdinMenuItem(tree, cfgName, new ConfigurationEditor(env, cfgName, this)));
			}
			
		}

		public async void LoadAllConfiguration()
		{
			_configs = await ConfigAPI.List();
			_treeRebuild = true;
		}
	}

	public class ConfigurationEditor
	{

		[InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
		public Configuration cfg;
		
		private string _name;
		private Environments _env;
		
		public ConfigurationEditor(Environments env, string name, ConfigurationsEditor cfgEditor)
		{

			_name = name;
			_env = env;

			cfg = ScriptableObject.CreateInstance<Configuration>();
			cfg.SetConfig(cfgEditor, env, name);
		}
	}
	
	public class AddNewConfiguration
	{
		[InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
		public Configuration cfg;

		public AddNewConfiguration(ConfigurationsEditor cfgEditor)
		{
			cfg = ScriptableObject.CreateInstance<Configuration>();
			cfg.SetCreate(cfgEditor);
		}
	}

}