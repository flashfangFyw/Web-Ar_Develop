using UnityEngine;
using System.Collections;

using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class BatchOperation : Editor
{
    [MenuItem("Tools/ButtonScale/GetLightmappingInfo")]
    static void TestLightmapingInfo()
    {
        GameObject tempObject;
        if (Selection.activeGameObject)
        {
            tempObject = Selection.activeGameObject;
            Debug.Log("Object name: " + tempObject.name);
            Debug.Log("Lightmaping Index: " + tempObject.GetComponent<Renderer>().lightmapIndex);
            Debug.Log("Lightmaping Offset: " + tempObject.GetComponent<Renderer>().lightmapScaleOffset);
        }
    }
    [MenuItem("Tools/CreateCopyMapPrefab")]
    static void CreateMapPrefab()
    {
        Debug.Log("---------------START----------------");
        string InputPath = @"Assets/Module/Copy/art/map_1/MapMater/";
        string OutPath = @"Assets/Module/Copy/MapPrefabs/Map/";
        string RePath = Application.dataPath + @"/Module/Copy/art/map_1/MapMater/";
        string matePath = @"Assets/Module/Copy/art/new main map/Materials/mainmap_00.mat";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(OutPath + "new_map_plane.prefab");
        Material MainMaterial = AssetDatabase.LoadAssetAtPath<Material>(matePath);

        List<Texture> SourceMaps = new List<Texture>();
        string[] Dirs = Directory.GetFiles(RePath);
        Debug.Log("Dir = " + Dirs.Length + "| RePath=" + RePath);
        foreach (string dir in Dirs)
        {
            Debug.Log("Dir = " + dir);
            if (dir.Contains(".meta")) continue;
            string filename = Path.GetFileName(dir);
            Texture texture = AssetDatabase.LoadAssetAtPath<Texture>(InputPath + filename);
            SourceMaps.Add(texture);
        }

        //遍历地图 制作Prefab
        foreach (Texture Source in SourceMaps)
        {
            GameObject oldgo = AssetDatabase.LoadAssetAtPath<GameObject>(OutPath + Source.name);
            GameObject CloneObj = Instantiate(prefab);
            MeshRenderer renderer = CloneObj.GetComponent<MeshRenderer>();
            MainMaterial.mainTexture = Source;
            renderer.material = MainMaterial;

            if (oldgo != null) PrefabUtility.ReplacePrefab(CloneObj, oldgo);
            else
            {
                PrefabUtility.CreatePrefab(OutPath + Source.name + ".prefab", CloneObj);
                AssetImporter asssImporter = AssetImporter.GetAtPath(OutPath + Source.name + ".prefab");
                asssImporter.assetBundleName = "copyasset.u3d";
                asssImporter.SaveAndReimport();
            }

            DestroyImmediate(CloneObj);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("---------------END----------------");
    }

    [MenuItem("Tools/ButtonScale/Add")]
    static void AddButtonScale()
    {
        Object[] sObjs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        foreach (Object sObj in sObjs)
        {
            if (sObj.name.Contains(".meta")) continue;
            GameObject sGo = sObj as GameObject;
            Button[] sGoChildTrans = sGo.transform.GetComponentsInChildren<Button>(true);
            Debug.Log(" AddButtonScale  sGoChildTrans Count=" + sGoChildTrans.Length);
            if (sGoChildTrans.Length > 0)
            {
                foreach (Button childTran in sGoChildTrans)
                {
                    ButtonScale bs = childTran.GetComponent<ButtonScale>();
                    if (!bs) childTran.gameObject.AddComponent<ButtonScale>();
                }
                SerializedObject so = new SerializedObject(sObj);
                so.ApplyModifiedProperties();
            }
        }
        Debug.Log(" AddButtonScale  OK !!! ");
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Tools/ButtonScale/Add(Toggle)")]
    static void AddButtonScaleToggle()
    {
        Object[] sObjs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        foreach (Object sObj in sObjs)
        {
            if (sObj.name.Contains(".meta")) continue;
            GameObject sGo = sObj as GameObject;
            Button[] sGoChildBtnTrans = sGo.transform.GetComponentsInChildren<Button>(true);
            Toggle[] sGoChildTogTrans = sGo.transform.GetComponentsInChildren<Toggle>(true);
            Debug.Log(" AddButtonScale  sGoChildTrans Count=" + sGoChildBtnTrans.Length);
            Debug.Log(" AddButtonScale  sGoChildTogTrans Count=" + sGoChildTogTrans.Length);
            if (sGoChildBtnTrans.Length != 0)
            {
                foreach (Button childTran in sGoChildBtnTrans)
                {
                    ButtonScale bs = childTran.GetComponent<ButtonScale>();
                    if (!bs) childTran.gameObject.AddComponent<ButtonScale>();
                }
            }

            if (sGoChildTogTrans.Length != 0)
            {
                foreach (Toggle childTran in sGoChildTogTrans)
                {
                    ButtonScale bs = childTran.GetComponent<ButtonScale>();
                    if (!bs) childTran.gameObject.AddComponent<ButtonScale>();
                }
            }

            SerializedObject so = new SerializedObject(sObj);
            so.ApplyModifiedProperties();
        }

        Debug.Log(" AddButtonScale  OK !!! ");
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Tools/ButtonScale/Clear")]
    static void ClearButtonScale()
    {
        Object[] sObjs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        foreach (Object sObj in sObjs)
        {
            if (sObj.name.Contains(".meta")) continue;
            GameObject sGo = sObj as GameObject;
            if (sGo.layer != (1 << 5)) continue;
            ButtonScale[] sGoChildTrans = sGo.transform.GetComponentsInChildren<ButtonScale>(true);
            if (sGoChildTrans.Length > 0)
            {
                foreach (ButtonScale childTran in sGoChildTrans)
                {
                    DestroyImmediate(childTran, true);
                }
                SerializedObject so = new SerializedObject(sObj);
                so.ApplyModifiedProperties();
            }
        }
        Debug.Log(" AddButtonScale  OK !!! ");
        AssetDatabase.SaveAssets();
    }

    //[MenuItem("Tools/Remind/Add")]
    //static void AddRemind()
    //{
    //    Object[] sObjs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

    //    foreach (Object sObj in sObjs)
    //    {
    //        if (sObj.name.Contains(".meta")) continue;
    //        GameObject sGo = sObj as GameObject;
    //        Button[] sGoChildTrans = sGo.transform.GetComponentsInChildren<Button>(true);          
    //        if (sGoChildTrans.Length > 0)
    //        {
    //            foreach (Button childTran in sGoChildTrans)
    //            {
    //                ModuleRemindAgent bs = childTran.GetComponent<ModuleRemindAgent>();
    //                if (!bs) childTran.gameObject.AddComponent<ModuleRemindAgent>();
    //            }
    //            SerializedObject so = new SerializedObject(sObj);
    //            so.ApplyModifiedProperties();
    //        }
    //    }
    //    Debug.Log(" AddButtonScale  OK !!! ");
    //    AssetDatabase.SaveAssets();
    //}

}


