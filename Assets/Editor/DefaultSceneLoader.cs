#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Swindler.Editor
{
	[InitializeOnLoadAttribute]
	public static class DefaultSceneLoader
	{

		//This code sets the default scene as the loading screen when in the editor
		static DefaultSceneLoader()
		{
			string pathOfFirstScene = EditorBuildSettings.scenes[0].path;
			SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfFirstScene);
			EditorSceneManager.playModeStartScene = sceneAsset;
		}
	}
}

#endif