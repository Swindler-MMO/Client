using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Swindler.Editor.Windows
{
	public class DrawSelected<T> where T : ScriptableObject
	{

		[InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
		public T selected;
		
		[LabelWidth(100)]
		[PropertyOrder(-1)]
		[HorizontalGroup("CreateNew/Horizontal")]
		public string nameForNew;

		private string path;

		[HorizontalGroup("CreateNew/Horizontal")]
		[GUIColor(0.7f, 0.7f, 1f)]
		[Button]
		public void CreateNew()
		{
			if (nameForNew == "")
				return;

			T newItem = ScriptableObject.CreateInstance<T>();
			newItem.name = $"New {typeof(T)}";

			if (path == "")
				path = "Assets/Editor/Windows/";

			AssetDatabase.CreateAsset(newItem, path + "\\" + nameForNew + ".asset");
			AssetDatabase.SaveAssets();

			nameForNew = "";
		}

		[HorizontalGroup("CreateNew/Horizontal")]
		[GUIColor(1f, 0.7f, 0.7f)]
		[Button]
		public void DeleteSelected()
		{
			if (selected != null)
			{
				string currentPath = AssetDatabase.GetAssetPath(selected);
				AssetDatabase.DeleteAsset(currentPath);
				AssetDatabase.SaveAssets();
			}
				
		}

		public void SetSelected(object item)
		{
			T attempt = item as T;
			if (attempt != null)
				selected = attempt;
		}

		public void SetPath(string path)
		{
			this.path = path;
		}
		
	}
}