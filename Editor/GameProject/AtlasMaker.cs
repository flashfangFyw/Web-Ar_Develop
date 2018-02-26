using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

//将图片纹理生成独立的perfab
public class AtlasMaker : EditorWindow {
	
	// Add menu item to the Window menu
   
    //static void Init () {
    //    // Get existing open window or if none, make a new one:
    //    EditorWindow.GetWindow<AtlasMaker> (false, "Atlas Maker");
    //}
     [MenuItem("GameProject/UI管理/AtlasMaker")]
    static private void MakeAtlas()
    {
        string spriteDir = Application.dataPath + "/UI/perfabs/UIPerfabs";

        if (!Directory.Exists(spriteDir))
        {
            Directory.CreateDirectory(spriteDir);
        }

        DirectoryInfo rootDirInfo = new DirectoryInfo(Application.dataPath + "/UI/UIResources");
        //Debug.Log("step1:" + rootDirInfo.GetDirectories());
        foreach (DirectoryInfo dirInfo in rootDirInfo.GetDirectories())
        {
            //Debug.Log("step2");
            foreach (FileInfo pngFile in dirInfo.GetFiles("*.png", SearchOption.AllDirectories))
            {
                string allPath = pngFile.FullName;
                string assetPath = allPath.Substring(allPath.IndexOf("Assets"));
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
				GameObject go = new GameObject(sprite.name);
                go.AddComponent<SpriteRenderer>().sprite = sprite;
				allPath = spriteDir + "/" + sprite.name + ".prefab";
                //Debug.Log("pppp==" + allPath);
                string prefabPath = allPath.Substring(allPath.IndexOf("Assets"));
                PrefabUtility.CreatePrefab(prefabPath, go);
                GameObject.DestroyImmediate(go);
            }
        }
    }
	// Implement your own editor GUI here.
	void OnGUI () {
		
	}
	
}