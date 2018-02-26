using UnityEditor;
using UnityEngine;
using System.Collections;

public class LightmapOperation : EditorWindow
{
	[MenuItem("GameProject/其他功能/光照贴图信息写入")]
	public static void TestLightmapingInfo()
	{
		GameObject[] tempObject;
		if (Selection.activeGameObject)
		{
			tempObject = Selection.gameObjects;
			for (int i = 0; i < tempObject.Length; i++)
			{
				Debug.Log("Object name: "  + tempObject[i].name);
				Renderer ren = tempObject[i].GetComponent<Renderer>();
				ren.lightmapIndex = 0;
				ren.lightmapScaleOffset = new Vector4(0.3089021f,0.3089021f,0.432469f,0.2078477f);
				EditorUtility.SetDirty(ren);
			}
		}

	}
}
