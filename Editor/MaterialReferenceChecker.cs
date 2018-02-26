using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class MaterialReferenceChecker : EditorWindow
{
	Dictionary<string, List<MatDetail>> dictMat = new Dictionary<string, List<MatDetail>>();
	int texWidth = 40;
	int texHeight = 40;
	Vector2 vec2 = new Vector2(0, 0);
	bool isCheck = false;
	bool isCheckScene = true;
	bool isCheckEffect = true;
	bool isCheckCharacter = true;
	GameObject effectObj = null;
	GameObject characterObj = null;
	Object[] objs;
	
	[MenuItem("GameProject/其他功能/无效Material检测")]
	public static void Init()
	{
		MaterialReferenceChecker window = GetWindow<MaterialReferenceChecker>();
		window.Show();
	}
	
	void OnGUI()
	{
		if (!isCheck)
		{
			if (objs != null && objs.Length > 100)  //防止选择太多物体，运算量太大
			{
				GUILayout.Label("你一次性选择太多资源（超过100个）！请重新选择资源，再点击“刷新”");
				if (GUILayout.Button("刷新"))
				{
					objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
				}
				return;
			}
			objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
			GUILayout.BeginHorizontal();
			if (objs == null || objs.Length <= 0)
			{
				GUILayout.Label("你没有选择任何物体，请在Project选择，按住Ctrl可以多选");
				return;
			}
			else
			{
				GUILayout.Label("你选择了以下物体：共（" + objs.Length + "）个");
			}
			GUILayout.EndHorizontal();
			vec2 = GUILayout.BeginScrollView(vec2);
			for (int i = 0; i < objs.Length; i++)
			{
				ListSelections(objs[i]);
			}
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("开始查找关联"))
			{
				OnCheckReferences();
				isCheck = true;
			}
			isCheckScene = GUILayout.Toggle(isCheckScene, "遍历场景", GUILayout.Width(70));
			isCheckEffect = GUILayout.Toggle(isCheckEffect, "遍历特效", GUILayout.Width(70));
			isCheckCharacter = GUILayout.Toggle(isCheckCharacter, "遍历角色", GUILayout.Width(70));
			GUILayout.EndHorizontal();
			ListMaterials();
			GUILayout.EndScrollView();
		}
		else
		{
			//刷新数据，必须刷新才能重新查找
			vec2 = GUILayout.BeginScrollView(vec2);
			for (int i = 0; i < objs.Length; i++)
			{
				ListSelections(objs[i]);
			}
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("刷新"))
			{
				isCheck = false;
				dictMat.Clear();
			}
			GUILayout.EndHorizontal();
			ListMaterials();
			GUILayout.EndScrollView();
		}
	}
	
	//选中物体列表
	void ListSelections(Object selection)
	{
		GUILayout.BeginHorizontal();
		if (selection is Material || selection is Texture)
		{
			Texture tex = null;
			if (selection is Material)
			{
				Material mat = selection as Material;
				if(mat.HasProperty("_MainTex")) tex = mat.mainTexture;
			}
			else if (selection is Texture)
			{
				tex = selection as Texture;
			}
			if (tex != null)
			{
				//在新窗口打开图片大图
				if (GUILayout.Button(tex, GUILayout.Width(texWidth), GUILayout.Height(texHeight)))
				{
					ZoomInTexture window = GetWindow<ZoomInTexture>();
					window.texture = tex;
					window.minSize = new Vector2(tex.width, tex.height);
				}
			}
			else
			{
				GUILayout.Box("N/A", GUILayout.Width(texWidth), GUILayout.Height(texHeight));
			}
		}
		else
		{
			GUILayout.Box("N/A", GUILayout.Width(texWidth), GUILayout.Height(texHeight));
		}
		GUILayout.Label("名称："+selection.name + "\n类型：" + selection.GetType().ToString().Replace("UnityEngine.", "") + "\n路径：" + AssetDatabase.GetAssetPath(selection));
		GUILayout.EndHorizontal();
		GUILayout.Space(10);
	}
	
	//关联物体列表
	void ListMaterials()
	{
		if (dictMat == null || dictMat.Count <= 0)
		{
			GUILayout.Label("没有关联物体，请点击“开始查找关联”按钮");
			return;
		}
		foreach (string item in dictMat.Keys)
		{
			List<MatDetail> detailList = dictMat[item];
			if (detailList != null && detailList.Count > 0)
			{
				GUILayout.Space(20);
				GUILayout.Label("==================《" + item + "》【Material】==================");
				foreach (MatDetail detail in detailList)
				{
					GUILayout.BeginHorizontal();
					//输出可以点击寻找路径的图片
					if (detail.mat != null && detail.mat.HasProperty("_MainTex") && detail.mat.mainTexture != null)
					{
						if (GUILayout.Button(detail.mat.mainTexture, GUILayout.Width(texWidth), GUILayout.Height(texHeight)))
						{
							Selection.activeObject = detail.mat;
						}
					}
					else
					{
						if (GUILayout.Button("N/A", GUILayout.Width(texWidth), GUILayout.Height(texHeight)))
						{
							Selection.activeObject = detail.mat;
						}
					}
					//输出信息
					string print = "";
					if(detail.type == "Effect" || detail.type == "Character")
						print = string.Format("所在路径：{0}\n关联类型：{1}\n关联物体：{2}", detail.assetPath, detail.type, detail.hierarcyPath);
					else
						print = string.Format("所在路径：{0}\n关联场景：{1}\n关联物体：{2}", detail.assetPath, detail.type, detail.hierarcyPath);
					if (detail.type == "NULL")
					{
						GUIStyle style = new GUIStyle();
						style.normal.textColor = Color.red;
						GUILayout.Label(print, style);
						if (GUILayout.Button("删除", GUILayout.Width(40)))
						{
							if (AssetDatabase.DeleteAsset(detail.assetPath))
							{
								ShowNotification(new GUIContent("删除成功"));
							}
						}
					}
					else
					{
						GUILayout.Label(print);
					}
					GUILayout.EndHorizontal();
				}
			}
		}
	}
	
	//开始查找引用
	void OnCheckReferences()
	{
		Object[] objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
		if (objs == null || objs.Length <= 0) return;
		
		dictMat.Clear();
		for (int i = 0; i < objs.Length; i++)
		{
			List<MatDetail> listDetail = new List<MatDetail>();
			if (objs[i] is Material)
			{
				//遍历所有场景关联物体
				if (isCheckScene)
				{
					if (EditorBuildSettings.scenes.Length <= 0)
					{
						Debug.LogError("你的可查找场景为空，请在Builder Setting中添加场景");
						return;
					}
					foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
					{
						if (scene.enabled)
						{
							EditorApplication.OpenScene(scene.path);
							if (objs[i] is Material)
							{
								MatDetail detail = SetMaterial(objs[i], scene.path);
								if (detail != null) listDetail.Add(detail);
							}
						}
					}
				}
				//遍历Resources特效目录
				if (isCheckEffect)
				{
					Object[] effects = Resources.LoadAll("Effects");  //Resources下的地址，根据需求修改
					for (int j = 0; j < effects.Length; j++)
					{
						if (objs[i] is Material)
						{
							MatDetail detail = SetEffectMaterial(objs[i], effects[j]);
							DestroyImmediate(effectObj);
							if (detail != null) listDetail.Add(detail);
						}
					}
				}
				//遍历角色目录
				if (isCheckCharacter)
				{
					Object[] characters = GetAssetsOfType(Application.dataPath + "/Characters/", typeof(GameObject), ".prefab"); //自定义地址，根据需求修改
					for (int j = 0; j < characters.Length; j++)
					{
						if (objs[i] is Material)
						{
							MatDetail detail = SetCharactersMaterial(objs[i], characters[j]);
							DestroyImmediate(characterObj);
							if (detail != null) listDetail.Add(detail);
						}
					}
				}
			}
			//找不到关联的处理
			if (listDetail.Count <= 0)
			{
				MatDetail detail = new MatDetail();
				detail.mat = objs[i] as Material;
				detail.assetPath = AssetDatabase.GetAssetPath(objs[i]);
				listDetail.Add(detail);
			}
			dictMat.Add(objs[i].name, listDetail);
		}
	}
	
	//获取prefab
	public static Object[] GetAssetsOfType(string directPath, System.Type type, string fileExtension)
	{
		List<Object> tempObjects = new List<Object>();
		DirectoryInfo directory = new DirectoryInfo(directPath);
		FileInfo[] goFileInfo = directory.GetFiles("*" + fileExtension, SearchOption.AllDirectories);
		
		int goFileInfoLength = goFileInfo.Length;
		FileInfo tempGoFileInfo;
		string tempFilePath;
		Object tempGO;
		for (int i = 0; i < goFileInfoLength; i++)
		{
			tempGoFileInfo = goFileInfo[i];
			if (tempGoFileInfo == null)
				continue;
			
			tempFilePath = tempGoFileInfo.FullName;
			tempFilePath = tempFilePath.Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			
			tempGO = AssetDatabase.LoadAssetAtPath(tempFilePath, typeof(Object)) as Object;
			if (tempGO == null)
			{
				Debug.LogWarning("Skipping Null");
				continue;
			}
			else if (tempGO.GetType() != type)
			{
				Debug.LogWarning("Skipping " + tempGO.GetType().ToString());
				continue;
			}
			
			tempObjects.Add(tempGO);
		}
		
		return tempObjects.ToArray();
	}
	
	//获取场景材质
	MatDetail SetMaterial(Object obj, string scenePath)
	{
		Renderer[] renderers = (Renderer[])FindObjectsOfType(typeof(Renderer)); //这种取法方便，但是disactive的物体取不到，待完善
		return GetMatDetail(renderers, obj, scenePath);
	}
	
	//获取特效材质
	MatDetail SetEffectMaterial(Object select, Object effect)
	{
		effectObj = PrefabUtility.InstantiatePrefab(effect) as GameObject;
		if (effectObj == null) return null;
		Renderer[] renderers = effectObj.GetComponentsInChildren<Renderer>(true);
		return GetMatDetail(renderers, select, "Effect");
	}
	
	//获取角色材质
	MatDetail SetCharactersMaterial(Object select, Object character)
	{
		characterObj = PrefabUtility.InstantiatePrefab(character) as GameObject;
		if (characterObj == null) return null;
		SkinnedMeshRenderer[] renderers = characterObj.GetComponentsInChildren<SkinnedMeshRenderer>(true);
		foreach (SkinnedMeshRenderer renderer in renderers)
		{
			Material[] mats = renderer.sharedMaterials;
			foreach (Material mat in mats)
			{
				string assetPath = AssetDatabase.GetAssetPath(select);
				if (assetPath == AssetDatabase.GetAssetPath(mat))
				{
					MatDetail detail = new MatDetail();
					detail.assetPath = assetPath;
					detail.mat = mat;
					detail.type = "Character";
					detail.hierarcyPath = GetHierarcyPath(renderer.gameObject);
					return detail;
				}
			}
		}
		return null;
	}
	
	//设置并获取MatDetail
	MatDetail GetMatDetail(Renderer[] renderers, Object select, string scenePath)
	{
		foreach (Renderer renderer in renderers)
		{
			//获取材质
			Material[] mats = renderer.sharedMaterials;
			foreach (Material mat in mats)
			{
				string assetPath = AssetDatabase.GetAssetPath(select);
				if (assetPath == AssetDatabase.GetAssetPath(mat))
				{
					MatDetail detail = new MatDetail();
					detail.assetPath = assetPath;
					detail.mat = mat;
					detail.type = scenePath;
					detail.hierarcyPath = GetHierarcyPath(renderer.gameObject);
					return detail;
				}
			}
		}
		return null;
	}
	
	//设置路径
	string GetHierarcyPath(GameObject go)
	{
		string path = "/" + go.name;
		while (go.transform.parent != null)
		{
			go = go.transform.parent.gameObject;
			path = "/" + go.name + path;
		}
		return path;
	}
	
	//材质详情
	class MatDetail
	{
		public Material mat;
		public string assetPath;
		public string type;
		public string hierarcyPath;
		public MatDetail()
		{
			mat = null;
			assetPath = "";
			type = "NULL";
			hierarcyPath = "NULL";
		}
	}
}

//查看大图片
public class ZoomInTexture : EditorWindow
{
	public Texture texture;
	
	void OnGUI()
	{
		GUILayout.Box(texture, GUILayout.Width(texture.width), GUILayout.Height(texture.height));
	}
}