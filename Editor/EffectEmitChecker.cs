using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class EffectEmitChecker : EditorWindow
{
	float ThumbnailWidth = 40;
	float ThumbnailHeight = 40;
	GUIStyle style = new GUIStyle();
	Vector2 vec2 = new Vector2(0, 0);
	List<EffectParticle> listEffect = new List<EffectParticle>();   //缓存特效信息
	
	[MenuItem("GameProject/其他功能/特效发射粒子数查找")]
	static void MainTask()
	{
		EffectEmitChecker window = GetWindow<EffectEmitChecker>();
		window.LoadEffect();   //加载特效
		window.Show();
	}
	
	void OnGUI()
	{
		ListEffect();
	}
	
	void LoadEffect()
	{
		Object[] objs = Resources.LoadAll("Effects/");  //读取所有特效文件，可以根据情况改变地址
		for (int i = 0; i < objs.Length; i++)
		{
			GameObject go = PrefabUtility.InstantiatePrefab(objs[i]) as GameObject; //创建实例
			if (go == null) continue;
			ParticleRenderer[] renderers = go.GetComponentsInChildren<ParticleRenderer>(true);  //获取特效实例下的所有ParticleRenderer组件
			foreach (ParticleRenderer render in renderers)
			{
				EffectParticle effect = new EffectParticle();
				ParticleEmitter emitter = render.GetComponent<ParticleEmitter>();   //获取ParticleEmitter组件
				effect.name = objs[i].name;
				effect.material = render.sharedMaterial;
				if (emitter != null)
				{
					effect.maxEmission = emitter.maxEmission;   //最大发射粒子数赋值
				}
				effect.prefab = objs[i];
				listEffect.Add(effect);
			}
			DestroyImmediate(go);   //销毁实例
		}
		listEffect.Sort((x, y) => { return y.maxEmission.CompareTo(x.maxEmission); });  //从大到小排序
		style.normal.textColor = Color.red;
		style.fixedWidth = 120;
	}
	
	void ListEffect()
	{
		vec2 = EditorGUILayout.BeginScrollView(vec2);
		foreach (EffectParticle effectParticle in listEffect)
		{
			if (effectParticle != null)
			{
				GUILayout.BeginHorizontal();
				
				Material mat = effectParticle.material;
				if (mat != null)
				{
					//根据材质找到相应的纹理显示
					Texture texture = mat.mainTexture;
					if (texture != null)
						GUILayout.Box(texture, GUILayout.Width(ThumbnailWidth), GUILayout.Height(ThumbnailHeight));
					else
						GUILayout.Box("N/A", GUILayout.Width(ThumbnailWidth), GUILayout.Height(ThumbnailHeight));
					
					GUILayout.Label("Shader:" + mat.shader.name, GUILayout.Width(140)); //Shader名称
					
					//特效主颜色显示
					if (mat.HasProperty("_Color"))
						EditorGUILayout.ColorField(mat.color, GUILayout.Width(50));
					else if (mat.HasProperty("_TintColor"))
						EditorGUILayout.ColorField(mat.GetColor("_TintColor"), GUILayout.Width(50));
					else
						GUILayout.Box("N/A", GUILayout.Width(50));
				}
				
				//发射粒子数判断
				float emission = effectParticle.maxEmission;
				if (emission < 50)
					GUILayout.Label("MaxEmission:" + emission.ToString(), GUILayout.Width(120));
				else
					GUILayout.Label("MaxEmission:" + emission.ToString(), style);   //字体标红
				
				//特效名称，并可定位到相应文件
				if (GUILayout.Button(effectParticle.name))
					Selection.activeObject = effectParticle.prefab;
				
				//文件所在路径节点
				GUILayout.TextField("Node:" + AssetDatabase.GetAssetPath(effectParticle.prefab));
				
				GUILayout.EndHorizontal();
			}
		}
		EditorGUILayout.EndScrollView();
	}
	
	//特效信息实体类
	class EffectParticle
	{
		public string name;
		public Material material;
		public float maxEmission;
		public Object prefab;
		public bool bScaleWithTransform;
		public EffectParticle()
		{
			maxEmission = 0;
		}
	}
}
