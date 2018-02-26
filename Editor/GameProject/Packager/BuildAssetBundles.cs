using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;



public class BuildAssetBundles : EditorWindow
{

    static string AppDataPath
    {
        get { return Application.dataPath.ToLower(); }
    }
    static string RepelecePath
    {
        get { return AppDataPath + "/streamingassets/"; }
    }
    string resPathp;

    [MenuItem("GameProject/Packager/打包 Asset Bundles")]
    public static void BuildAssetBundle()
    {
        //		Caching.CleanCache ();
        string resPath = (AppDataPath + "/" + Const.AssetDirname + "/").ToLower();
        if (!Directory.Exists(resPath))
            Directory.CreateDirectory(resPath);
        BuildPipeline.BuildAssetBundles(resPath, BuildAssetBundleOptions.DeterministicAssetBundle, GetBuildTarget());
        //    |BuildAssetBundleOptions.DeterministicAssetBundle
        //		| BuildAssetBundleOptions.ForceRebuildAssetBundle 
        //	BuildAssetBundleOptions.UncompressedAssetBundle
        setBuildAssetBundle(resPath);
        //        AssetDatabase.Refresh();
    }

    #region TEST
    //[MenuItem("GameProject/Packager/压缩 Asset Bundles")]
    //public static void BuildZipPackager()
    //{
    //   //GzipHelper.CreateSample(Application.dataPath + "/123.zip", "", Application.dataPath + "/StreamingAssets");
    //    //Process pro = new Process();
    //}
    //[MenuItem("GameProject/Packager/解压 Asset Bundles")]
    //public static void UnZipPackager()
    //{
    //    //string[] lines = File.ReadAllLines(RepelecePath + "files.txt");
    //    //Dictionary<string, AbFileInfo> abFileInfos = new Dictionary<string, AbFileInfo>();
    //    //for (int i = 0; i < lines.Length; i++)
    //    //{
    //    //    AbFileInfo abFile = new AbFileInfo(lines[i]);
    //    //    abFileInfos[abFile.FileName] = abFile;
    //    //    string err = "";
    //    //    //bool result = GzipHelper.Zip(Application.dataPath + "/StreamingAssets/", Application.dataPath + "/123.zip", abFileInfos, out err);
    //    //    //if (!result) UnityEngine.Debug.Log(err);          
    //    //}
    //    GzipHelper.unZipFile(Application.dataPath + "/StreamingAssets/123.zip", Application.dataPath + "/StreamingAssets/");
    //}    
    #endregion

    #region BuildAssetBundle
    //string luaShowPath = "lua";
    static List<string> paths = new List<string>();
    static List<string> files = new List<string>();
    static Dictionary<string, string> manifestCrcs = new Dictionary<string, string>();
    // Add menu item to the Window menu

    //static void Init () {
    //    // Get existing open window or if none, make a new one:
    //    EditorWindow.GetWindow<BuildAssetBundles> (false, "Build Asset Bundles");
    //}
    //void OnGUI()
    //{
    //    EditorGUIUtility.LookLikeControls(85f, 85f);
    //    EditorGUILayout.BeginVertical();
    //    {
    //        OptionsGUI();
    //        CreateAndCancelButtonsGUI();
    //    } EditorGUILayout.EndVertical();
    //}
    //private void OptionsGUI()
    //{
    //    resPathp = (AppDataPath + "/" + Const.AssetDirname + "/");//.ToLower();
    //    EditorGUILayout.LabelField("根目录路径:", resPathp);
    //    GUILayout.Space(10);
    //    //Const.AssetDirname = EditorGUILayout.TextField("素材目录路径:", Const.AssetDirname);

    //    //luaShowPath = EditorGUILayout.TextField("lua目录路径:", luaShowPath);
    //    //BuildAssetResource();
    //}
    //    private void CreateAndCancelButtonsGUI()
    //    {


    //        // Cancel and create buttons
    //        GUILayout.BeginHorizontal();
    //        {
    //            GUILayout.FlexibleSpace();

    //            if (GUILayout.Button("Cancel", GUILayout.Width(80)))
    //            {
    //                Close();
    //                GUIUtility.ExitGUI();
    //            }

    //            bool guiEnabledTemp = GUI.enabled;
    //            GUI.enabled = true;//canCreate;
    //            if (GUILayout.Button("Create", GUILayout.Width(80)))
    //            {
    //                BuildAssetResource();
    //                Close();
    //                GUIUtility.ExitGUI();
    //            }
    //            GUI.enabled = guiEnabledTemp;
    //        } GUILayout.EndHorizontal();
    //    }
    //    //=========================================================
    //    private void BuildAssetResource()
    //    {
    //        Caching.CleanCache();
    //        //string assetfile = string.Empty;  //素材文件名
    //        resPathp = (AppDataPath + "/" + Const.AssetDirname + "/").ToLower();
    //        if (!Directory.Exists(resPathp))
    //            Directory.CreateDirectory(resPathp);
    ////        Debug.Log(resPath);
    //        //BuildPipeline.BuildAssetBundles(resPath);
    //        //BuildPipeline.BuildAssetBundles(resPath, BuildAssetBundleOptions.UncompressedAssetBundle, GetBuildTarget());
    //        BuildPipeline.BuildAssetBundles(resPathp, BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.ForceRebuildAssetBundle, GetBuildTarget());
    //        //setBuildAssetBundle(resPath);

    //        string luaPath = resPathp + "/" + luaShowPath + "/";

    //        //----------复制Lua文件----------------
    //        if (Directory.Exists(luaPath))
    //        {
    //            Directory.Delete(luaPath, true);
    //        }
    //        Directory.CreateDirectory(luaPath);

    //        paths.Clear(); files.Clear();
    //        string luaDataPath = Application.dataPath + "/lua/".ToLower();
    //        Recursive(luaDataPath);
    //        foreach (string f in files)
    //        {
    //            if (f.EndsWith(".meta")) continue;
    //            string newfile = f.Replace(luaDataPath, "");
    //            string newpath = luaPath + newfile;
    //            string path = Path.GetDirectoryName(newpath);
    //            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
    //            File.Copy(f, newpath, true);
    //        }

    //        ///----------------------创建文件列表-----------------------
    //        string newFilePath = resPathp + "/files.txt";
    //        if (File.Exists(newFilePath)) File.Delete(newFilePath);

    //        paths.Clear(); files.Clear();
    //        Recursive(resPathp);

    //        FileStream fs = new FileStream(newFilePath, FileMode.CreateNew);
    //        StreamWriter sw = new StreamWriter(fs);
    //        for (int i = 0; i < files.Count; i++)
    //        {
    //            string file = files[i];
    //            string ext = Path.GetExtension(file);
    //            if (ext.Equals(".meta")) continue;

    //            string value = file.Replace(resPathp, string.Empty);
    //            sw.WriteLine(value);
    //        }
    //        sw.Close(); fs.Close();
    //        AssetDatabase.Refresh();
    //    }

    static void setBuildAssetBundle(string resPath)
    {
        string newFilePath = resPath + "/files.txt";
        if (File.Exists(newFilePath)) File.Delete(newFilePath);

        paths.Clear(); files.Clear();
        Recursive(resPath);
        BuildCrcForAB();

        FileStream fs = new FileStream(newFilePath, FileMode.CreateNew);
        StreamWriter sw = new StreamWriter(fs);
        for (int i = 0; i < files.Count; i++)
        {
            string file = files[i];
            //		    string ext = Path.GetExtension(file);
            //		    if (ext.Equals(".meta")) continue;		
            string value = file.Replace(resPath, string.Empty);
#if UNITY_IPHONE
            if (value.ToLower().Contains(".ds_store")) continue;
#endif
            sw.WriteLine(value);
        }
        sw.Close(); fs.Close();
    }

    static void Recursive(string path)
    {
        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);

        foreach (string filename in names)
        {
            string ext = Path.GetExtension(filename);
            if (ext.Equals(".meta")) continue;

            //记录AB文件记录
            StringBuilder sb = new StringBuilder();
            string RelativePath = filename.Replace(RepelecePath, string.Empty).Trim();
            RelativePath = RelativePath.Replace('\\', '/');
            sb.Append(RelativePath);
            sb.Append("  ");

            if (filename.Contains("version.txt"))
            {
                // Default implementation of UNIX time of the current UTC time  
                TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                string CRC = Convert.ToInt64(ts.TotalSeconds).ToString();
                string Key = "version.txt";
                manifestCrcs[Key] = CRC;
                sb.Append(CRC);
            }

            if (ext.Equals(".manifest"))
            {
                string[] fileName = File.ReadAllLines(filename);
                if (fileName != null && fileName.Length >= 2)
                {
                    string tempFileName = filename.ToLower();
                    if (fileName[1].Contains("CRC:"))
                    {
                        string[] crcPair = fileName[1].Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        string CRC = crcPair[1];
                        string Key = RelativePath.Replace(".manifest", string.Empty);
                        manifestCrcs[Key] = CRC;
                        sb.Append(CRC);
                    }
                    //StreamingAssets  StreamingAssets.manifest
                    else if (tempFileName.Contains("streamingassets") || tempFileName.Contains("streamingassets.manifest"))
                    {
                        // Default implementation of UNIX time of the current UTC time  
                        TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                        string CRC = Convert.ToInt64(ts.TotalSeconds).ToString();
                        string Key = RelativePath.Replace(".manifest", string.Empty);
                        manifestCrcs[Key] = CRC;
                        sb.Append(CRC);
                    }
                }
            }
            files.Add(sb.ToString());
        }
        foreach (string dir in dirs)
        {
            paths.Add(dir.Replace('\\', '/'));
            Recursive(dir);
        }
    }
    static void BuildCrcForAB()
    {
        foreach (KeyValuePair<string, string> pair in manifestCrcs)
        {
            for (int i = 0; i < files.Count; i++)
            {
                string checkname = files[i].ToLower();
                if ((files[i].Contains(pair.Key) && !files[i].Contains(pair.Value))
                    || (checkname.Contains(pair.Key) && !checkname.Contains(pair.Value)))
                {
                    files[i] = string.Concat(files[i], "  ", pair.Value);
                }
            }
        }
    }

    static private BuildTarget GetBuildTarget()
    {
        BuildTarget target = BuildTarget.StandaloneWindows;
#if UNITY_STANDALONE
		target = BuildTarget.StandaloneWindows;
#elif UNITY_IPHONE
		target = BuildTarget.iOS;
#elif UNITY_ANDROID
        target = BuildTarget.Android;
#endif
        return target;
    }
    /*
        [MenuItem("GameProject/Packager/打包 Protobuf File")]
        public static void BuildProtobufFile() {
            string dir = Application.dataPath + "/Protocal";
        
            paths.Clear(); 
            files.Clear(); 
            Recursive(dir);

            string protoc = "d:/protobuf-2.4.1/src/protoc.exe";
            string protoc_gen_dir = "\"d:/protoc-gen-lua/plugin/protoc-gen-lua.bat\"";

            foreach (string f in files) {
                string name = Path.GetFileName(f);
    //			if(name != "define.proto") name = "define.proto" + " " + name;
                string ext = Path.GetExtension(f);
                if (!ext.Equals(".proto")) continue;
	
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = protoc;
                info.Arguments = " --lua_out=" + Application.dataPath + "/Lua/pblua/ --plugin=protoc-gen-lua=" + protoc_gen_dir + " " + name;
                info.WindowStyle = ProcessWindowStyle.Minimized;
                info.UseShellExecute = true;
                info.WorkingDirectory = dir;
                info.ErrorDialog = true;
                Debuger.Log(info.FileName + " " + info.Arguments);

                Process pro = Process.Start(info);
                pro.WaitForExit();
            }
            AssetDatabase.Refresh();
        }
    */

    static string MD5CRC(string crc)
    {
        byte[] result = Encoding.Default.GetBytes(crc.Trim());
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] output = md5.ComputeHash(result);
        string Text = BitConverter.ToString(output).Replace("-", "");
        return Text;
    }
    static string MD5(byte[] source)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] output = md5.ComputeHash(source);
        string Text = BitConverter.ToString(output).Replace("-", "");
        return Text;
    }

    #endregion

    #region CompressAssetBundle


    #endregion

}