using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using Swindler.Editor.Windows;
using Swindler.Game;
using UnityEditor;
using UnityEngine;

/***
 * Code from https://www.youtube.com/watch?v=S29XkTlD9bw videos series
 */
public class GameManagerEditor : OdinMenuEditorWindow
{
	[MenuItem("Swindler/Game Manager %g")]
	public static void OpenWindow()
	{
		GetWindow<GameManagerEditor>().Show();
	}

	[LabelText("Manager view")] [LabelWidth(100f)] [EnumToggleButtons] [ShowInInspector] [OnValueChanged("StateChange")]
	private ManagerState _managerState;

	private int _enumIndex = 0;
	private bool treeRebuild;

	//private DrawSelected<Configurations> _configurations = new DrawSelected<Configurations>();

	protected override void Initialize()
	{
		//_configurations.SetPath(Configurations.ASSET_PATH);
	}


	private void StateChange()
	{
		treeRebuild = true;
	}

	protected override void OnGUI()
	{

		if (treeRebuild && Event.current.type == EventType.Layout)
		{
			ForceMenuTreeRebuild();
			treeRebuild = false;
		}
			
		
		SirenixEditorGUI.Title("Swindler Game Manager", "Game manager settings", TextAlignment.Center, false);

		//Call DrawEditor(_enumIndex) here where left menu is needed
		switch (_managerState)
		{
			case ManagerState.GameManager:
				break;
			case ManagerState.Player:
				break;
			case ManagerState.WorldManager:
				break;
			case ManagerState.WaterRenderer:
				break;
			case ManagerState.Configurations:
				DrawEditor(_enumIndex);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

		base.OnGUI();
	}

	protected override void DrawEditors()
	{
		//Call DrawEditor(_enumIndex) here where left menu is NOT needed
		switch (_managerState)
		{
			case ManagerState.GameManager:
				DrawEditor(_enumIndex);
				break;
			case ManagerState.Player:
				DrawEditor(_enumIndex);
				break;
			case ManagerState.WorldManager:
				DrawEditor(_enumIndex);
				break;
			case ManagerState.WaterRenderer:
				DrawEditor(_enumIndex);
				break;
			case ManagerState.Configurations:
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	protected override IEnumerable<object> GetTargets()
	{
		var targets = new List<object> {base.GetTarget()};

		_enumIndex = targets.Count - 1;

		return targets;
	}

	protected override void DrawMenu()
	{
		switch (_managerState)
		{
			case ManagerState.GameManager:
				break;
			case ManagerState.Player:
				break;
			case ManagerState.WorldManager:
				break;
			case ManagerState.WaterRenderer:
				break;
			case ManagerState.Configurations:
				base.DrawMenu();
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	protected override OdinMenuTree BuildMenuTree()
	{
		OdinMenuTree menuTree = new OdinMenuTree();

		switch (_managerState)
		{
			case ManagerState.GameManager:
				break;
			case ManagerState.Player:
				break;
			case ManagerState.WorldManager:
				break;
			case ManagerState.WaterRenderer:
				break;
			case ManagerState.Configurations:
				//menuTree.AddAllAssetsAtPath("Configurations", Configurations.ASSET_PATH, typeof(Configurations));
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

		return menuTree;
	}

	private enum ManagerState
	{
		GameManager,
		Player,
		WorldManager,
		WaterRenderer,
		Configurations
	}
}