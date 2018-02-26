using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ffDevelopmentSpace
{


public enum DialogType
{
	OK,         //只有确认按钮
	Cancel,     //确认和取消按钮同时存在
	checkOk,
	checkCancel,
	Tips         //tips
}

public enum DialogSubType
{
	SUB_TYPE_BAG,
	SUB_TYPE_SELLCHIP, //回收零件
}

public enum moduleType
{
    eActive,
    eTask,
    eChat,
    eCopy,
	eHangMu,
    eEnd
}
public class ModuleEventDispatcher : EventDispatcher
{
	public static string PROMPT_TEXT = "prompt_text";
	public static string PROMPT_WND = "prompt_wnd";
	public static string PROMPT_CHECK_WND = "prompt_checkwnd";

	public static string CAMERA_MOVE = "camera_move";

	public static string TOPINFO_TYPE = "topinfo_type";

    public static string ENTER_MOVIE = "enter_moive";
	public static string PLAYER_NEED_CREATE = "player_need_create";

	public static string DEBUG_LOG = "debug_log";
	public static string PROMPT_ITEMS_WND = "Prompt_items_wnd";
	public static string GET_ITEMS_WND = "get_items_wnd";
	public static string LOADDING_SCENE_START = "loading_start";
	public static string LOADDING_SCENE_END = "loading_end";

	public static string GAME_LOAD_START = "game_load_start";
	public static string GAME_LOAD_COMPLETE = "game_load_complete";
	public static string GAME_LOAD_INFO = "game_load_info";

    public static string GAME_ENTER_MAINSCENE = "game_enter_mainscene";

	public Dictionary<DialogSubType, bool> ModuleType = new Dictionary<DialogSubType, bool>();

	public static string[] MODULE_TYPE = { "eActive", "eTask", "eChat", "eCopy" ,"eHangMu"};
    //信息条
    public void dispatchTopInfoEvent()
	{
		EventObject eventobj = new EventObject();
		dispatchEvent(TOPINFO_TYPE, eventobj);
	}

	//场景镜头
	public void dispatchCameraEvent(int moveType)
	{
		EventObject eventobj = new EventObject();
		eventobj.obj = moveType;
		dispatchEvent(CAMERA_MOVE, eventobj);
	}

	//浮动文字提示
	public void dispatchPromptText(string str)
	{
		EventObject eventobj = new EventObject();
		eventobj.obj = str;
		dispatchEvent(PROMPT_TEXT, eventobj);
	}

	//窗口提示
	public void dispatchPromptWnd(DialogType type, string str, Action okHandle = null, Action cancelHandle = null)
	{
		WndShowInfo info = new WndShowInfo();
		info.type = type;
		info.str = str;
		info.okHandle = okHandle;
		info.cancelHandle = cancelHandle;
		EventObject eventobj = new EventObject();
		eventobj.obj = info;
		dispatchEvent(PROMPT_WND, eventobj);
	}

	//check窗口提示
	public void dispatchPromptCheckWnd(DialogSubType moduletype, DialogType type, string content, Action okHandle = null, Action cancelHandle = null)
	{
		WndShowInfo info = new WndShowInfo();
		info.moduletype = moduletype;
		info.type = type;
		info.str = content;
		info.okHandle = okHandle;
		info.cancelHandle = cancelHandle;
		EventObject eventobj = new EventObject();
		eventobj.obj = info;
		dispatchEvent(PROMPT_CHECK_WND, eventobj);
	}
    //進入劇情
    public void dispatchEnterMovieEvent()
    {
        EventObject eventobj = new EventObject();
        dispatchEvent(ENTER_MOVIE, eventobj);
    }
	//需要创建角色
	public void dispatchPlayerNeedCreateEvent()
	{
		EventObject eventobj = new EventObject();
		dispatchEvent(PLAYER_NEED_CREATE, eventobj);
	}

	//打印log
	public void dispatchDebugLogEvent(string str)
	{
		EventObject eventobj = new EventObject();
		eventobj.obj = str;
		dispatchEvent(DEBUG_LOG, eventobj);
	}

	//物品窗口提示 自动关闭（标准）
	public void dispatchPromptItemsWnd(List<sy.Item> iteminfo)
	{
		EventObject eventobj = new EventObject();
		eventobj.obj = iteminfo;
		dispatchEvent(PROMPT_ITEMS_WND, eventobj);
	}
	//物品窗口提示  需要点击关闭
	public void dispatchGetItemsWnd(List<sy.Item> iteminfo)
	{
		EventObject eventobj = new EventObject();
		eventobj.obj = iteminfo;
		dispatchEvent(GET_ITEMS_WND, eventobj);
	}
	//场景start
	public void dispatchLoadingSceneStart(string sceneName)
	{
		EventObject eventobj = new EventObject();
		eventobj.obj = sceneName;
		dispatchEvent(LOADDING_SCENE_START, eventobj);
	}

	//场景end
	public void dispatchLoadingSceneEnd(string sceneName)
	{
		EventObject eventobj = new EventObject();
		eventobj.obj = sceneName;
		dispatchEvent(LOADDING_SCENE_END, eventobj);
	}

    //模块红点
    public void dispatchModuleRedEvent(moduleType type)
    {
        EventObject eventobj = new EventObject();
        eventobj.obj = type;
        dispatchEvent(MODULE_TYPE[(int)type], eventobj);
    }

    //进入主场景
    public void dispatchEnterMainscene()
    {
        EventObject eventobj = new EventObject();
        dispatchEvent(GAME_ENTER_MAINSCENE, eventobj);
    }
}

public class WndShowInfo
{
	public string str = "";
	public DialogSubType moduletype;
	public DialogType type = DialogType.OK;
	public Action okHandle = null;
	public Action cancelHandle = null;
}
}