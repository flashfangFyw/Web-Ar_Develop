using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using UObject = UnityEngine.Object;

namespace ffDevelopmentSpace
{


public class ResourceManagerController:SingletonMB<ResourceManagerController>
{
    //private static SingletonMB<ResourceManagerController> instance;

    private string[] m_Variants = { };
    private AssetBundleManifest manifest;
    private Dictionary<string, AssetBundle> bundles;
	private Dictionary<string, Dictionary<string,UObject>> allAssets;

	void Awake()
	{
		//instance = this;
		Init ();
	}

//    public static SingletonMB<ResourceManagerController> GetInstance()
//    {
////        if (null == instance)
////        {
////            instance = new SingletonMB<ResourceManagerController>();
////        }
//        return instance;
//    }

    public void Init()
    {
        bundles = new Dictionary<string, AssetBundle>();
		allAssets = new Dictionary<string, Dictionary<string, UObject>> ();
            string filePath = Util.DataPath + "streamingassets";
		AssetBundle assetbundle = CreateAssetBundle (filePath);
        manifest = assetbundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
    }

	AssetBundle CreateAssetBundle(string filePath)
	{
//		AssetBundle ab = AssetBundle.CreateFromFile (filePath);
//		Debuger.Log("CreateAB::>> " + filePath);
		byte[] stream = File.ReadAllBytes(filePath);
		AssetBundle ab = AssetBundle.LoadFromMemory(stream);
		return ab;
	}

	public GameObject LoadGameObject(string abname, string assetname)
	{
		abname = abname.ToLower();
		Dictionary<string, UObject> dic;
		GameObject obj;
		if (allAssets.ContainsKey (abname)) {
			dic = allAssets [abname];
			if (dic.ContainsKey (assetname)) 
			{
				obj = dic[assetname] as GameObject;
			}
			else
			{
				AssetBundle bundle = LoadAssetBundle(abname);
				obj = bundle.LoadAsset<GameObject>(assetname);
				dic[assetname] = obj;
			}
		} else {
			dic = new Dictionary<string, UObject>();
			AssetBundle bundle = LoadAssetBundle(abname);
			obj = bundle.LoadAsset<GameObject>(assetname);
			dic[assetname] = obj;
			allAssets [abname] = dic;
		}
		return obj;
	}

    public GameObject LoadAsset(string abname, string assetname)
    {
		return LoadGameObject (abname,assetname);
//        abname = abname.ToLower();
//        AssetBundle bundle = LoadAssetBundle(abname);
//        return bundle.LoadAsset<GameObject>(assetname);
    }

    public Sprite LoadSprite(string abname, string assetname)
    {
        abname = abname.ToLower();
        AssetBundle bundle = LoadAssetBundle(abname);
        return bundle.LoadAsset<Sprite>(assetname);
    }

    public Material LoadMaterial(string abname, string assetname)
    {
        abname = abname.ToLower();
        AssetBundle bundle = LoadAssetBundle(abname);
        return bundle.LoadAsset<Material>(assetname);
    }

    public Texture LoadTexture(string abname, string assetname)
    {
        abname = abname.ToLower();
        AssetBundle bundle = LoadAssetBundle(abname);
        return bundle.LoadAsset<Texture>(assetname);
    }

    public string LoadData(string abname, string assetname)
    {
        abname = abname.ToLower();
        AssetBundle bundle = LoadAssetBundle(abname);
//		Util.onTimeStart ();
        TextAsset data = bundle.LoadAsset<TextAsset>(assetname);
		string str = data.text;
//		string str = bundle.LoadAsset(assetname).ToString() ;
//		Util.onTimeEnd (assetname);
        return str;
    }

    public AudioClip LoadAudioClip(string abname, string assetname)
    {
        abname = abname.ToLower();
        AssetBundle bundle = LoadAssetBundle(abname);
        return bundle.LoadAsset<AudioClip>(assetname);
    }

    public AssetBundle LoadAssetBundle(string abname)
    {
        string keyName = abname;
        if (abname.Contains("."))
        {
            abname = abname.Substring(0, abname.IndexOf("."));
        }
        else
        {
            keyName += Const.ExtName;
        }
        AssetBundle bundle = null;
        if (!bundles.ContainsKey(abname))
        {
            LoadDependencies(keyName);
            if (!bundles.ContainsKey(abname))
            {
                string uri = Util.DataPath + keyName;
				bundle = CreateAssetBundle(uri);                
                bundles.Add(abname, bundle);
            }
            else
            {
                bundles.TryGetValue(abname, out bundle);
            }
        }
        else
        {
            bundles.TryGetValue(abname, out bundle);
        }
        return bundle;
    }

    private void LoadDependencies(string name)
    {
        if (manifest == null)
        {
            Debuger.LogError("Please initialize AssetBundleManifest by calling AssetBundleManager.Initialize()");
            return;
        }
        // Get dependecies from the AssetBundleManifest object..
        string[] dependencies = manifest.GetAllDependencies(name);
        //		manifest.GetDirectDependencies
        if (dependencies.Length == 0) return;

		//游戏暂时不用多套资源
//        for (int i = 0; i < dependencies.Length; i++)
//            dependencies[i] = RemapVariantName(dependencies[i]);

        // Record and load all dependencies.
        for (int i = 0; i < dependencies.Length; i++)
        {
            LoadAssetBundle(dependencies[i]);
        }
    }

    // Remaps the asset bundle name to the best fitting asset bundle variant.
	//AssetBundle Variants是Unity5的新特性。你可以将两组不同的资源指定为同一个AssetBundle但是指定不同的Variants，这样，你可以根据平台的不同加载不同的资源（例如支持高清资源的设备加载hd的Variants，不支持高清资源的设备加载sd的Variants）。可是使用AssetImporter.assetBundleVariant设置Variants。
    private string RemapVariantName(string assetBundleName)
    {
        string[] bundlesWithVariant = manifest.GetAllAssetBundlesWithVariant();

        // If the asset bundle doesn't have variant, simply return.
        if (System.Array.IndexOf(bundlesWithVariant, assetBundleName) < 0)
            return assetBundleName;

        string[] split = assetBundleName.Split('.');

        int bestFit = int.MaxValue;
        int bestFitIndex = -1;
        // Loop all the assetBundles with variant to find the best fit variant assetBundle.
        for (int i = 0; i < bundlesWithVariant.Length; i++)
        {
            string[] curSplit = bundlesWithVariant[i].Split('.');
            if (curSplit[0] != split[0])
                continue;

            int found = System.Array.IndexOf(m_Variants, curSplit[1]);
            if (found != -1 && found < bestFit)
            {
                bestFit = found;
                bestFitIndex = i;
            }
        }
        if (bestFitIndex != -1)
            return bundlesWithVariant[bestFitIndex];
        else
            return assetBundleName;
    }

    public void UnloadAssetBundle(string abname, bool allLoadedObjects = false)
    {
        AssetBundle bundle;
        abname = abname.ToLower();
        bundles.TryGetValue(abname, out bundle);
        if (bundle)
        {
            bundles.Remove(abname);
            bundle.Unload(allLoadedObjects);
            //			bundle = null;
        }
        //		Resources.UnloadUnusedAssets ();
        //		GC.Collect ();
    }

	//以下部分异步加载
	class AssetBundleInfo 
	{
		public AssetBundle m_AssetBundle;
		public int m_ReferencedCount;
		
		public AssetBundleInfo(AssetBundle assetBundle) 
		{
			m_AssetBundle = assetBundle;
			m_ReferencedCount = 0;
		}
	}
	
	class LoadAssetRequest 
	{
		public Type assetType;
		public string[] assetNames;
		public Action<UObject[]> sharpFunc;
	}

	Dictionary<string, string[]> m_Dependencies = new Dictionary<string, string[]>();
	Dictionary<string, AssetBundleInfo> m_LoadedAssetBundles = new Dictionary<string, AssetBundleInfo>();
	Dictionary<string, List<LoadAssetRequest>> m_LoadRequests = new Dictionary<string, List<LoadAssetRequest>>();

	string GetRealAssetPath(string abName) 
	{
		string path = abName.ToLower();
		if (!abName.Contains("."))
		{
			path += Const.ExtName;
		}
		return path;
	}

	AssetBundleInfo GetLoadedAssetBundle(string abName) 
	{
		AssetBundleInfo bundle = null;
		m_LoadedAssetBundles.TryGetValue(abName, out bundle);
		if (bundle == null) return null;
		
		// No dependencies are recorded, only the bundle itself is required.
		string[] dependencies = null;
		if (!m_Dependencies.TryGetValue(abName, out dependencies))
			return bundle;
		
		// Make sure all dependencies are loaded
		foreach (var dependency in dependencies) 
		{
			AssetBundleInfo dependentBundle;
			m_LoadedAssetBundles.TryGetValue(dependency, out dependentBundle);
			if (dependentBundle == null) return null;
		}
		return bundle;
	}

	public void LoadAssetAsync<T>(string abName, string[] assetNames, Action<UObject[]> action = null)
	{
		abName = GetRealAssetPath(abName);
		
		LoadAssetRequest request = new LoadAssetRequest();
		request.assetType = typeof(T);
		request.assetNames = assetNames;
		request.sharpFunc = action;
		
		List<LoadAssetRequest> requests = null;
		if (!m_LoadRequests.TryGetValue(abName, out requests)) 
		{
			requests = new List<LoadAssetRequest>();
			requests.Add(request);
			m_LoadRequests.Add(abName, requests);
			StartCoroutine(OnLoadAsset<T>(abName));
		} else {
			requests.Add(request);
		}
	}

	IEnumerator OnLoadAsset<T>(string abName)
	{
		AssetBundleInfo bundleInfo = GetLoadedAssetBundle(abName);
		if (bundleInfo == null) {
			yield return StartCoroutine(OnLoadAssetBundle(abName, typeof(T)));
			
			bundleInfo = GetLoadedAssetBundle(abName);
			if (bundleInfo == null) {
				m_LoadRequests.Remove(abName);
				Debuger.LogError("OnLoadAsset--->>>" + abName);
				yield break;
			}
		}
		List<LoadAssetRequest> list = null;
		if (!m_LoadRequests.TryGetValue(abName, out list)) {
			m_LoadRequests.Remove(abName);
			yield break;
		}
		for (int i = 0; i < list.Count; i++) {
			string[] assetNames = list[i].assetNames;
			List<UObject> result = new List<UObject>();
			
			AssetBundle ab = bundleInfo.m_AssetBundle;
			for (int j = 0; j < assetNames.Length; j++) {
				string assetPath = assetNames[j];
//				AssetBundleRequest request = ab.LoadAssetAsync<T>(assetPath, list[i].assetType);
				AssetBundleRequest request = ab.LoadAssetAsync<T>(assetPath);
				yield return request;
				result.Add(request.asset);
			}
			if (list[i].sharpFunc != null) {
				list[i].sharpFunc(result.ToArray());
				list[i].sharpFunc = null;
			}
			bundleInfo.m_ReferencedCount++;
		}
		m_LoadRequests.Remove(abName);
	}

	IEnumerator OnLoadAssetBundle(string abName, Type type) 
	{
		string url = Util.DataPath + abName;
		
		WWW download = null;
		string[] dependencies = manifest.GetAllDependencies(abName);
		if (dependencies.Length > 0) {
			m_Dependencies.Add(abName, dependencies);
			for (int i = 0; i < dependencies.Length; i++) {
				string depName = dependencies[i];
				AssetBundleInfo bundleInfo = null;
				if (m_LoadedAssetBundles.TryGetValue(depName, out bundleInfo)) {
					bundleInfo.m_ReferencedCount++;
				} else if (!m_LoadRequests.ContainsKey(depName)) {
					yield return StartCoroutine(OnLoadAssetBundle(depName, type));
				}
			}
		}
		download = WWW.LoadFromCacheOrDownload(url, manifest.GetAssetBundleHash(abName), 0);
		yield return download;
		
		AssetBundle assetObj = download.assetBundle;
		if (assetObj != null) {
			m_LoadedAssetBundles.Add(abName, new AssetBundleInfo(assetObj));
		}
	}

	public void UnloadAssetBundle(string abName) {
		abName = GetRealAssetPath(abName);
		Debuger.Log(m_LoadedAssetBundles.Count + " assetbundle(s) in memory before unloading " + abName);
		UnloadAssetBundleInternal(abName);
		UnloadDependencies(abName);
		Debuger.Log(m_LoadedAssetBundles.Count + " assetbundle(s) in memory after unloading " + abName);
	}
	
	void UnloadDependencies(string abName) {
		string[] dependencies = null;
		if (!m_Dependencies.TryGetValue(abName, out dependencies))
			return;
		
		// Loop dependencies.
		foreach (var dependency in dependencies) {
			UnloadAssetBundleInternal(dependency);
		}
		m_Dependencies.Remove(abName);
	}
	
	void UnloadAssetBundleInternal(string abName) {
		AssetBundleInfo bundle = GetLoadedAssetBundle(abName);
		if (bundle == null) return;
		
		if (--bundle.m_ReferencedCount == 0) {
			bundle.m_AssetBundle.Unload(false);
			m_LoadedAssetBundles.Remove(abName);
			Debuger.Log(abName + " has been unloaded successfully");
		}
	}

}
}