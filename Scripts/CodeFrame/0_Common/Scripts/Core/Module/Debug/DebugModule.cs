using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using System.IO;
using ffDevelopmentSpace;


public class DebugModule : BaseModule
{
    public Text RightText;
    public Text fpsText;
    public Text severText;
    public Text versionText;
    public ScrollRect scrollView;
    private string m_pLogShowInfo = ""; //用于显示
    private string m_pLogFileInfo = ""; //用于存储
    private string m_pLeftInfo = "";

    private int frames = 0; // Frames over current interval
    private int fps;
    private float updateInterval = 0.5f;
    private float lastInterval;
	
	void Update ()
	{                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
        OnShowFPS();
	}

    protected override void InitView()
    {
        //ShowVersionInfo();
    }

    protected override void InitEvent()
    {
        Singleton<ModuleEventDispatcher>.GetInstance().addEvent(ModuleEventDispatcher.DEBUG_LOG, RefreshLogInfo);
        //Singleton<GameModel>.GetInstance().addEvent(GameModel.SEVER_CHANGE, showSeverInfo);
		//OnExit ();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (RightText != null)
        {
            RightText.text = m_pLogShowInfo;
        }
        scrollView.verticalNormalizedPosition = 0;
        ShowVersionInfo();
    }

    public void ClearText()
    {
        if (m_pLogShowInfo.Length > 10000)
        {
            m_pLogShowInfo = "";
        }
    }
    //刷新显示内容
    public void RefreshLogInfo(EventObject obj)
    {
        string str = obj.obj.ToString();
        ClearText();
		m_pLogShowInfo = m_pLogShowInfo + str + "\n";
		m_pLogFileInfo = m_pLogFileInfo + CommUtils.htmltotext("\r\n" + str);
        if (RightText != null)
        {
            RightText.text = m_pLogShowInfo;
        }
        scrollView.verticalNormalizedPosition = 0;
    }

    public void ClickGMButtonEvent()
    {
        ModuleManager.GetInstance().CreateModule("gm", ModuleManager.GetInstance().getOtherPanel());
    }

    //保存到文件中
    public void ClickSaveFileEvent()
    {
        string directoryPath;
        string fileName = "GameLog_" + DateTime.Now.ToString("yyyy_MM_dd_H_m_s") + ".txt";
        if (Application.isMobilePlatform)
        {
            directoryPath = Util.AppContentPath() + "/DebugLog";
        }
        else
        {
            directoryPath = "E:/DebugLog";
        }
        UpdateLeftInfo();
        SaveFile(m_pLeftInfo + m_pLogFileInfo, directoryPath, fileName);

		string path = Path.Combine(directoryPath, "GameLog_" + DateTime.Now.ToString("yyyy_MM_dd_H_m_s") + ".png");
		captureScreenshot (path);

		Singleton<ModuleEventDispatcher>.GetInstance ().dispatchPromptText ("--Debug log ---保存成功到" + directoryPath + "/" + fileName);
    }

	void captureScreenshot(string path)
	{
		ScreenCapture.CaptureScreenshot (path);
/*
		Texture2D myTexture2D = new Texture2D(Screen.width, Screen.height);
		myTexture2D.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0); 
		myTexture2D.Apply(); 
		File.WriteAllBytes (path, myTexture2D.EncodeToPNG ()); 
*/		
	}

    void SaveFile(string p_Text, string p_Path ,string fileName)
    {
        string directoryPath = @p_Path;         //定义一个路径变量
        string filePath = fileName;             //定义一个文件路径变量
        if (!Directory.Exists(directoryPath))   //如果路径不存在
        {
            Directory.CreateDirectory(directoryPath);//创建一个路径的文件夹
        }
        StreamWriter sw = new StreamWriter(Path.Combine(directoryPath, filePath));
        sw.Write(p_Text);
        sw.Flush();
        sw.Close();
    }

    public void UpdateLeftInfo()
    {
        ServerInfoVO curInfo = Singleton<ServiceModel>.GetInstance().GetCurSeverInfo();
        m_pLeftInfo = versionText.text + "\r\n" + "账号："// + Singleton<GameModel>.GetInstance().Account + "\r\n" + fpsText.text 
            + "\r\n" + severText.text + "\r\n" + "服务器IP：" 
            + curInfo.IP + "\r\n" + "服务器端口号：" + curInfo.Port;
    }

    void OnShowFPS()
    {
        ++frames;
        var timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval)
        {
            fps = (int)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;
        }
        fpsText.text = "帧率：" + fps.ToString(); ;
    }

    public void ShowSeverInfo(EventObject obj)
    {
        ServerInfoVO curInfo = Singleton<ServiceModel>.GetInstance().GetCurSeverInfo();
        severText.text = "服务器：" + curInfo.Name;
    }

    public void ShowVersionInfo()
    {
        //    versionText.text = "版本号：" + Singleton<GameModel>.GetInstance().Version_s;
    }
}
