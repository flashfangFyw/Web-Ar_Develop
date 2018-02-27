using UnityEngine;
using System.Collections;
using ffDevelopmentSpace;
using System;


/* 
    Author:     fyw 
    CreateDate: 2018-02-27 16:12:58 
    Desc:       注释 
*/

namespace ffDevelopmentSpace
{


    public class ModuleEventDispatcher_Base : EventDispatcher
    {
        #region Debug
        public static string DEBUG_LOG = "debug_log";
        //打印log
        public void DispatchDebugLogEvent(string str)
        {
            EventObject eventobj = new EventObject();
            eventobj.obj = str;
            dispatchEvent(DEBUG_LOG, eventobj);
        }
        #endregion

        #region Game
        public static string GAME_LOAD_START = "game_load_start";
        public static string GAME_LOAD_COMPLETE = "game_load_complete";
        public static string GAME_LOAD_INFO = "game_load_info";
        #endregion
        #region LoadingScene
        public static string LOADDING_SCENE_START = "loading_start";
        public static string LOADDING_SCENE_END = "loading_end";
        //场景start
        public void DispatchLoadingSceneStart(string sceneName)
        {
            EventObject eventobj = new EventObject();
            eventobj.obj = sceneName;
            dispatchEvent(LOADDING_SCENE_START, eventobj);
        }

        //场景end
        public void DispatchLoadingSceneEnd(string sceneName)
        {
            EventObject eventobj = new EventObject();
            eventobj.obj = sceneName;
            dispatchEvent(LOADDING_SCENE_END, eventobj);
        }
        #endregion
        #region Scene
        public static string GAME_ENTER_MAINSCENE = "game_enter_mainscene";
        //进入主场景
        public void dispatchEnterMainscene()
        {
            EventObject eventobj = new EventObject();
            dispatchEvent(GAME_ENTER_MAINSCENE, eventobj);
        }
        #endregion

        #region Prompt
        public static string PROMPT_TEXT = "prompt_text";
        public static string PROMPT_WND = "prompt_wnd";
        public static string PROMPT_CHECK_WND = "prompt_checkwnd";
        //浮动文字提示
        public void dispatchPromptText(string str)
        {
            EventObject eventobj = new EventObject();
            eventobj.obj = str;
            dispatchEvent(PROMPT_TEXT, eventobj);
        }
        //窗口提示
        //public void dispatchPromptWnd(DialogType type, string str, Action okHandle = null, Action cancelHandle = null)
        //{
        //    WndShowInfo info = new WndShowInfo();
        //    info.type = type;
        //    info.str = str;
        //    info.okHandle = okHandle;
        //    info.cancelHandle = cancelHandle;
        //    EventObject eventobj = new EventObject();
        //    eventobj.obj = info;
        //    dispatchEvent(PROMPT_WND, eventobj);
        //}

        ////check窗口提示
        //public void dispatchPromptCheckWnd(DialogSubType moduletype, DialogType type, string content, Action okHandle = null, Action cancelHandle = null)
        //{
        //    WndShowInfo info = new WndShowInfo();
        //    info.moduletype = moduletype;
        //    info.type = type;
        //    info.str = content;
        //    info.okHandle = okHandle;
        //    info.cancelHandle = cancelHandle;
        //    EventObject eventobj = new EventObject();
        //    eventobj.obj = info;
        //    dispatchEvent(PROMPT_CHECK_WND, eventobj);
        //}

        #endregion
    }

}