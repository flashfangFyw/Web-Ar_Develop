#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.IO;
using System.Text;


using System.Xml;
using System.Xml.Serialization;

using Microsoft.Win32;

using UnityEditor;
using UnityEngine;
using System.Collections;


public class PListCreate : EditorWindow
{
    private const string EComFileTypePath = "EComFileTypePath";
    private const string EZIPFileName = "EZIPFileName";

    private const string FilesTxtPath = "/Version/HistoricalTxt/";
    private const string PluginConfigPath = "/Editor/GameProject/Packager/ZipPlistConfig.xml";

    private static PListCreate window;
    [MenuItem("GameProject/发布打包")]
    public static void OpenWindow()
    {
        window = GetWindow<PListCreate>();
        InitViewValue();
        InitDirectoryConfig();
        window.Show();
    }

    //View 1
    public string FileName = "";
    public string ClientVersion = "";
    public string GameVersion = "";
    public string ClientUpdatePackageURL = "";
    public string ApkSize = "";
    public string AssetPackageURL = "";

    //View2
    public string CompressionFlieType = "";
    public string ZipFileName = "";

    public bool isDifferencePackage = true;

    public static string NewStreamingAssets
    {
        get
        {
            string filePath_1 = Application.dataPath + "/streamingassets/";
            if (Directory.Exists(filePath_1)) return filePath_1;
            string filePath_2 = Application.dataPath + "/StreamingAssets/";
            if (Directory.Exists(filePath_2)) return filePath_2;
            Directory.CreateDirectory(filePath_2);
            return filePath_2;
        }
    }

    public static string SavePath
    {
        get { return Application.dataPath.Replace("Assets", "") + "/Version/"; }
    }
    public static string StreamingAssets
    {
        get { return Application.dataPath + "/StreamingAssets/"; }
    }

    //生成的Version 和 files 文件
    public static string SaveReleasePath
    {
        get { return SavePath + "Release/"; }
    }
    //存放所有资源文件 （启动之前必须清空）
    public static string SaveAssetDataPath
    {
        get { return SavePath + "AssetData/"; }
    }
    //存放所有版本控制文件
    public static string SaveHistoricalPath
    {
        get { return SavePath + "HistoricalTxt/"; }
    }

    private void OnGUI()
    {

        #region TITLE CREATE PLIST
        EditorGUILayout.BeginVertical("box");
        {
            EditorGUILayout.LabelField("----->>STEP[1] TITLE CREATE PLIST");
            EditorGUILayout.LabelField("保存文件路径：", SavePath);
            ClientVersion = EditorGUILayout.TextField("客户端版本号：", ClientVersion);
            GameVersion = EditorGUILayout.TextField("游戏版本号：", GameVersion);
            ClientUpdatePackageURL = EditorGUILayout.TextField("客户端更新包URL：", ClientUpdatePackageURL);
            AssetPackageURL = EditorGUILayout.TextField("资源配置文件URL：", AssetPackageURL);
            ApkSize = EditorGUILayout.TextField("客户端大小（MB）:", ApkSize);

            EditorGUILayout.Space();
            isDifferencePackage = EditorGUILayout.Toggle("生成服务器资源文件：", isDifferencePackage);
            EditorGUILayout.LabelField("----->>STEP[2] BUILDING ASSETBUNDLE");
            EditorGUILayout.LabelField("保存文件路径：", StreamingAssets);
            if (GUILayout.Button("发布打包 Asset Bundles"))
            {

                CreatePlist(ClientVersion);
                SaveViewValue(null, ClientVersion, GameVersion,
                    ClientUpdatePackageURL, AssetPackageURL, ApkSize);

                BuildAssetBundles.BuildAssetBundle();

                if (isDifferencePackage)
                {                    
                    CreateSeviserFilesData();
                }

                if (EditorUtility.DisplayDialog("发布打包完成", "完成所有任务", "ok"))
                {

                }
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();
        #endregion

        #region TITLE RELEASE COMPRESSION

        //EditorGUILayout.BeginVertical("box");
        //{
        //    EditorGUILayout.LabelField("----->>STEP[3] TITLE RELEASE COMPRESSION");
        //    //显示 上一次操作时的版本号
        //    EditorGUILayout.LabelField("压缩文件的路径 =" + StreamingAssets);
        //    EditorGUILayout.LabelField("输入发布版数据路径 =" + SaveReleasePath);
        //    ZipFileName = EditorGUILayout.TextField("ZIP包的名称(xxx.zip)：", ZipFileName);
        //    CompressionFlieType = EditorGUILayout.TextField("压缩类型(例:*.u3d|*.txt)", CompressionFlieType);

        //    if (GUILayout.Button("发布版 一键生成ZIP"))
        //    {
        //        EditorPrefs.SetString(EZIPFileName, ZipFileName);
        //        EditorPrefs.SetString(EComFileTypePath, CompressionFlieType);

        //        CompressZip(StreamingAssets, SaveReleasePath, ZipFileName, CompressionFlieType);
        //    }
        //}
        //EditorGUILayout.EndVertical();
        //EditorGUILayout.Space();
        #endregion

        #region TITLE DIFFERENCE ZIP
        //EditorGUILayout.BeginVertical("box");
        //{
        //    EditorGUILayout.LabelField("----->>TITLE RELEASE DIFFERECT PACKAGE");
        //    //比对版本号
        //    if (GUILayout.Button("对比版本号 生成差异包"))
        //    {

        //    }
        //}
        //EditorGUILayout.EndVertical();
        //EditorGUILayout.Space();
        #endregion

        #region TITLE DELECT STREAMING ASSETS
        //EditorGUILayout.BeginVertical("box");
        //{
        //    EditorGUILayout.LabelField("----->>TITLE DELECT STREAMING ASSETS");
        //    //删除Application.StreamingAssets
        //    if (GUILayout.Button("删除Application.StreamingAssets下所有文件"))
        //    {
        //        Directory.Delete(StreamingAssets, true);
        //        Directory.CreateDirectory(StreamingAssets);
        //        EditorUtility.FocusProjectWindow();
        //    }
        //}
        //EditorGUILayout.EndVertical();
        //EditorGUILayout.Space();
        #endregion

    }

    private static void InitViewValue()
    {
        string path = Application.dataPath + PluginConfigPath;
        PListConfigInfo pListConfig;
        try
        {
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            XmlSerializer xml = new XmlSerializer(typeof(PListConfigInfo));
            pListConfig = xml.Deserialize(sr) as PListConfigInfo;
            sr.Dispose();
        }
        catch
        {
            pListConfig = new PListConfigInfo();
        }
        if (EditorPrefs.GetString(EComFileTypePath) == "")
        {
            EditorPrefs.SetString(EComFileTypePath, "*.unity3d|*.manifest|*.u3d|*.txt");
        }

        window.ClientVersion = pListConfig.ClientVersion ?? "";
        window.GameVersion = pListConfig.GameVersion ?? "";
        window.ClientUpdatePackageURL = pListConfig.ClientUpdatePackageURL ?? "";
        window.AssetPackageURL = pListConfig.AssetPackageURL ?? "";
        window.ApkSize = pListConfig.ApkSize ?? "";
        window.CompressionFlieType = EditorPrefs.GetString(EComFileTypePath);
        window.ZipFileName = EditorPrefs.GetString(EZIPFileName);
    }
    private static void InitDirectoryConfig()
    {
        //历史 files文件  
        string path = SaveHistoricalPath;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string assetPath = SaveAssetDataPath;
        if (Directory.Exists(assetPath))
        {
            Directory.Delete(assetPath, true);
        }
        Directory.CreateDirectory(assetPath);

    }
    private static void SaveViewValue(List<string> versions, params string[] pams)
    {
        string path = Application.dataPath + PluginConfigPath;
        PListConfigInfo pList = new PListConfigInfo();
        pList.ClientVersion = pams[0];      // window.ClientVersion;
        pList.GameVersion = pams[1];       //window.GameVersion;
        pList.ClientUpdatePackageURL = pams[2];  //window.ClientUpdatePackageURL;       
        pList.AssetPackageURL = pams[3];   //window.AssetPackageURL;
        pList.ApkSize = pams[4];                 //window.ApkSize;     

        XmlSerializer ser = new XmlSerializer(typeof(PListConfigInfo));
        TextWriter textWriter = new StreamWriter(path, false);
        ser.Serialize(textWriter, pList);
        textWriter.Close();
    }
    private static void FindFilesTextCopyTo(string version)
    {
        string outPath = Path.Combine(SavePath, FilesTxtPath) + GetHistoricalConfigFilesName(version);
        string path = StreamingAssets + Const.AssetDetailed;
        if (File.Exists(path))
        {
            File.Copy(path, outPath);
        }
    }

    private void CreatePlist(string version = "")
    {
        string path = NewStreamingAssets + Const.VersionFile;
        FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
        XmlTextWriter w = new XmlTextWriter(fs, Encoding.UTF8);

        w.WriteStartDocument();
        {
            //w.WriteComment("DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd");
            w.WriteStartElement("plist");
            w.WriteStartElement("dict");
            {
                w.WriteElementString("key", "clientversion");
                w.WriteElementString("string", ClientVersion);
                w.WriteElementString("key", "gameversion");
                w.WriteElementString("string", GameVersion);
                w.WriteElementString("key", "clientupdateurl");
                w.WriteElementString("string", ClientUpdatePackageURL);
                w.WriteElementString("key", "packageurl");
                w.WriteElementString("string", AssetPackageURL);
                w.WriteElementString("key", "apksize");
                w.WriteElementString("string", ApkSize);
            }
            w.WriteEndElement();
        }
        w.WriteEndDocument();
        w.Flush();
        fs.Close();
    }
    private static string GetHistoricalConfigFilesName(string version)
    {
        return Const.AssetDetailed.Replace(".", "_" + version + ".");
    }
    /// <summary>
    /// 生成Zip
    /// </summary>
    /// <param name="path">文件夹路径</param>
    /// <param name="rarPath">生成压缩文件的路径</param>
    /// <param name="rarName">生成压缩文件的文件名</param>
    public static void CompressZip(String path, String rarPath, String rarName, string pats)
    {
        try
        {
            string openFile = rarPath;
            String winRarPath = null;
            if (!ExistsRar(out winRarPath))
            {
                EditorUtility.DisplayDialog("插件提示", "当前系统缺少Winrar软件", "确定");
                return;
            }

            string[] patList = pats.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            sb.Append("a ");
            sb.Append("-afzip ");
            sb.Append("-m5 ");
            sb.Append("-r ");
            for (int i = 0; i < patList.Length; i++)
            {
                sb.Append(string.Concat("-n", patList[i], "  "));
            }

            sb.Append("-ep1 ");
            sb.Append(string.Format("\"{0}\"  \"{1}\"", rarName, path));
            //验证WinRar是否安装。
            //var pathInfo = String.Format("a -afzip -m5 -r -n*.unity3d -n*.manifest -n*.u3d -n*.txt  -ep1  \"{0}\" \"{1}\"", rarName, path);
            var pathInfo = sb.ToString();
            #region WinRar 用到的命令注释
            //[a] 添加到压缩文件
            //afzip 执行zip压缩方式，方便用户在不同环境下使用。
            //（取消该参数则执行rar压缩）
            //-m0 存储 添加到压缩文件时不压缩文件。共6个级别【0-5】，值越大效果越好，也越慢
            //ep1 依名称排除主目录（生成的压缩文件不会出现不必要的层级）
            //r 修复压缩档案
            //t 测试压缩档案内的文件
            //as 同步压缩档案内容 
            //-p 给压缩文件加密码方式为：-p123456
            #endregion
            //打包文件存放目录
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = winRarPath,//执行的文件名
                    Arguments = pathInfo,//需要执行的命令
                    UseShellExecute = false,//使用Shell执行
                    WindowStyle = ProcessWindowStyle.Normal,//不隐藏窗体
                    WorkingDirectory = rarPath,//rar 存放位置
                    CreateNoWindow = true,//不显示窗体                   
                },
            };
            process.Start();//开始执行
            process.WaitForExit();//等待完成并退出
            process.Close();

            UnityEngine.Debug.Log("openFile  =" + openFile);
            openFile = openFile.Replace("/", "\\");
            System.Diagnostics.Process.Start("explorer.exe", openFile);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log(ex);
        }
    }
    /// <summary>
    /// 验证WinRar是否安装。
    /// </summary>
    /// <returns>true：已安装，false：未安装</returns>
    private static bool ExistsRar(out String winRarPath)
    {
        winRarPath = String.Empty;
        //通过Regedit（注册表）找到WinRar文件
        var registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
        if (registryKey == null) return false;//未安装
        //registryKey = theReg;可以直接返回Registry对象供会面操作
        winRarPath = registryKey.GetValue("").ToString();
        //这里为节约资源，直接返回路径，反正下面也没用到
        registryKey.Close();//关闭注册表
        return !String.IsNullOrEmpty(winRarPath);
    }


    /*
     * 新版方法
     */
    private void CreateDifferenceFilesData()
    {
        Dictionary<string, AbFileInfo> historicalDic = GetHistoricalFilesData();
        Dictionary<string, AbFileInfo> LatelyDic = GetLatelyBuildFilesData();

        string input = "";
        string outPut = "";
        if (historicalDic.Count <= 0)
        {
            foreach (KeyValuePair<string, AbFileInfo> pair in LatelyDic)
            {
                input = NewStreamingAssets + pair.Value.RelativePath;
                outPut = SaveAssetDataPath + pair.Value.RelativePath;
                string outDir = Path.GetDirectoryName(outPut);
                if (!Directory.Exists(outDir)) Directory.CreateDirectory(outDir);
                File.Copy(input, outPut, true);
            }
        }
        else
        {
            foreach (KeyValuePair<string, AbFileInfo> Latelypair in LatelyDic)
            {
                if (!historicalDic.ContainsKey(Latelypair.Key) ||
                    historicalDic[Latelypair.Key].Crc != Latelypair.Value.Crc)
                {
                    input = NewStreamingAssets + Latelypair.Value.RelativePath;
                    outPut = SaveAssetDataPath + Latelypair.Value.RelativePath;
                    string outDir = Path.GetDirectoryName(outPut);
                    if (!Directory.Exists(outDir)) Directory.CreateDirectory(outDir);
                    File.Copy(input, outPut, true);
                }
            }
        }

        //保存历史File文件
        SavaHistorcalFilesData();
        //保存Version 和Files.txt 到SaveAssetData文件加下
        SaveVersionFilesToAssetDataPath();
    }
    private void CreateSeviserFilesData()
    {     
        Dictionary<string, AbFileInfo> fileInfos = GetLatelyBuildFilesData();
        string input = "";
        string outPut = "";
        foreach (KeyValuePair<string, AbFileInfo> pair in fileInfos)
        {
            input = NewStreamingAssets + pair.Value.RelativePath;
            outPut = SaveAssetDataPath + pair.Value.RelativePath;
            string outDir = Path.GetDirectoryName(outPut);
            if (!Directory.Exists(outDir)) Directory.CreateDirectory(outDir);
            File.Copy(input, outPut, true);
        }
        //保存历史File文件
        SavaHistorcalFilesData();
        //保存Version 和Files.txt 到SaveAssetData文件加下
        SaveVersionFilesToAssetDataPath();
    }
    private Dictionary<string, AbFileInfo> GetLatelyBuildFilesData()
    {
        Dictionary<string, AbFileInfo> ProjectDic = new Dictionary<string, AbFileInfo>();
        string projectFilePath = StreamingAssets + "files.txt";
        string latelyFile = File.ReadAllText(projectFilePath);
        ProjectDic = AbFileInfo.DeCode(latelyFile);
        return ProjectDic;
    }
    private Dictionary<string, AbFileInfo> GetHistoricalFilesData()
    {
        Dictionary<string, AbFileInfo> historocalInfoDic = new Dictionary<string, AbFileInfo>();
        string[] Paths = Directory.GetFiles(SaveHistoricalPath);
        if (Paths.Length < 0) return historocalInfoDic;
        //提取FilesNames        
        string TargetFilePath = "";
        long tempLatelyDate = 0;
        foreach (string ps in Paths)
        {
            string whileFileName = Path.GetFileNameWithoutExtension(ps);
            string[] sname = whileFileName.Split('_');
            if (sname.Length < 2) continue;
            long timeSpan = long.Parse(sname[1]);
            if (timeSpan > tempLatelyDate)
            {
                tempLatelyDate = timeSpan;
                TargetFilePath = ps;
            }
        }
        if (string.IsNullOrEmpty(TargetFilePath)) return historocalInfoDic;
        //解析上一次打包的file文件
        string FileContent = File.ReadAllText(TargetFilePath);
        historocalInfoDic = AbFileInfo.DeCode(FileContent);
        return historocalInfoDic;
    }

    //保存历史Files.txt文件   时间由近到远   最多存储50个
    private void SavaHistorcalFilesData()
    {
        string[] Paths = Directory.GetFiles(SaveHistoricalPath);
        if (Paths.Length < 50)
        {
            string input = NewStreamingAssets + "files.txt";
            string outPath = SaveHistoricalPath + GetHistoricalFileName();
            File.Copy(input, outPath, true);
        }
        else
        {
            //提取FilesNames         
            string TargetFilePath = "";
            long tempOldDate = 0;
            foreach (string ps in Paths)
            {
                string whileFileName = Path.GetFileNameWithoutExtension(ps);
                string[] sname = whileFileName.Split('_');
                if (sname.Length < 2) continue;
                long timeSpan = long.Parse(sname[1]);
                if (timeSpan > tempOldDate)
                {
                    tempOldDate = timeSpan;
                    TargetFilePath = ps;
                }
            }
            if (!string.IsNullOrEmpty(TargetFilePath))
            {
                File.Delete(TargetFilePath);
            }
        }
    }
    private void SaveVersionFilesToAssetDataPath()
    {
        string sourceVer = NewStreamingAssets + "version.txt";
        string sourceFile = NewStreamingAssets + "files.txt";
        string outVer = SaveAssetDataPath + "version.txt";
        string outFile = SaveAssetDataPath + "files.txt";

        if (File.Exists(sourceVer)) File.Copy(sourceVer, outVer, true);
        else UnityEngine.Debug.LogError("没有找到 StreamingAssets 下的 Version.txt文件");
        if (File.Exists(sourceFile)) File.Copy(sourceFile, outFile, true);
        else UnityEngine.Debug.LogError("没有找到 StreamingAssets 下的 Files.txt文件");
    }
    private string GetHistoricalFileName()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(DateTime.Now.Year);
        sb.Append(DateTime.Now.Month);
        sb.Append(DateTime.Now.Day);
        sb.Append(DateTime.Now.Hour);
        sb.Append(DateTime.Now.Minute);
        return "files_" + sb.ToString() + ".txt";
    }

}


[System.Serializable]
public class PListConfigInfo
{
    [XmlElement]
    public string ClientVersion { get; set; }
    [XmlElement]
    public string GameVersion { get; set; }
    [XmlElement]
    public string ClientUpdatePackageURL { get; set; }
    [XmlElement]
    public string ApkSize { get; set; }
    [XmlElement]
    public string AssetPackageURL { get; set; }
}
#endif