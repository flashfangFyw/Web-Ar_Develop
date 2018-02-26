using sy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ffDevelopmentSpace
{
    public class ModuleManager:Manager_Base<ModuleManager>
    {
        //private static ModuleManager instance;
        private Dictionary<string, BaseModule> moduleList = new Dictionary<string, BaseModule>();
        private List<BaseModule> showModuleList = new List<BaseModule>();
        private Transform parent;
        private Transform otherParent;
        private string preModuleName;

        //public static ModuleManager GetInstance()
        //{
        //    if (null == instance)
        //    {
        //        instance = new ModuleManager();
        //        //instance.init();
        //    }
        //    return instance;
        //}

        protected override void Init()
        {
            GameObject go = GameObject.Find("UIPanel");
            if (go != null) parent = go.transform;
            go = GameObject.Find("OtherPanel");
            if (go != null) otherParent = go.transform;
        }

        public Transform getOtherPanel()
        {
            return otherParent;
        }

        public BaseModule CreateModule(string name, Transform parentPanel = null)
        {
            name = name.ToLower();
            if (!LoadModuleScene(name))
                return null;

            BaseModule module;
            string assetName = name + "Panel";
            //Debuger.Log("create panel=" + name);
            if (moduleList.ContainsKey(name))
            {
                module = moduleList[name];
                module.transform.SetAsLastSibling();
                module.OnEnter();
                module.moduleName = name;
                OpenOrCloseWithModule(name, true, parentPanel);
                return module;
            }
            string path = PathUtil.getUiPanelPath(name);
            GameObject prefab = ResourceManagerController.GetInstance().LoadAsset(path, assetName);
            if (prefab == null)
            {
                return null;
            }

            GameObject go = GameObject.Instantiate(prefab) as GameObject;

            RectTransform rectTrans = go.GetComponent<RectTransform>();
            Vector2 pos = rectTrans.anchoredPosition;

            if (parent == null)
                Init();
            Transform trans = go.transform;
            if (null == parentPanel)
                trans.SetParent(parent);
            else
                trans.SetParent(parentPanel);

            trans.localScale = Vector3.one;
            rectTrans.anchorMin = new Vector2(0, 0);
            rectTrans.anchorMax = new Vector2(1, 1);
            rectTrans.pivot = new Vector2(0.5f, 0.5f);
            rectTrans.anchoredPosition = new Vector2(0, 0);
            rectTrans.sizeDelta = new Vector2(0, 0);
            rectTrans.offsetMin = new Vector2(0, 0);
            rectTrans.offsetMax = new Vector2(0, 0);

            module = go.GetComponent<BaseModule>();
            module.moduleName = name;
            moduleList.Add(name, module);
            //OpenOrCloseWithModule(name, true, parentPanel);
            return module;
        }

        //打开界面前判断是否达到开启等级和场景是否匹配
        public bool LoadModuleScene(string moduleName)
        {
            //Sys_moduleConfigInfo info = ConfigDataManager.GetSys_moduleConfigInfo(moduleName.ToLower());
            //if (info != null)
            //{
            //    PlayerInfo playerinfo = Singleton<PlayerInfoModel>.GetInstance().GetPlayerInfo();
            //    if (playerinfo.level < info.open_lv)
            //    {
            //        Singleton<ModuleEventDispatcher>.GetInstance().dispatchPromptText("未达到开启等级!");
            //        return false;
            //    }
            //    string sceneName = info.scenename;
            //    if (sceneName != null && sceneName.Length > 0)
            //    {
            //        string curSceneName = Application.loadedLevelName;
            //        if (!string.Equals(sceneName, curSceneName))
            //        {
            //            ModuleManager.GetInstance().CloseAllModule();
            //            Singleton<GameModel>.GetInstance().setSceneCameraPos(info.scenepostion);
            //            Application.LoadLevel(sceneName);
            //        }
            //    }
            //}
            return true;
        }

        //同时打开或关闭界面
        public void OpenOrCloseWithModule(string name, bool IsOpen, Transform parentPanel = null)
        {
            //Sys_moduleConfigInfo info = ConfigDataManager.GetSys_moduleConfigInfo(name.ToLower());
            //if (info != null)
            //{
            //    string openStr = info.keepopenmodule;
            //    if (openStr != null && openStr.Length > 0)
            //    {
            //        string[] str = openStr.Split(',');
            //        for (int i = 0; i < str.Length; i++)
            //        {
            //            if (IsOpen)
            //            {
            //                CreateModule(str[i]);
            //                if (String.Equals(str[i], "topinfo", StringComparison.CurrentCultureIgnoreCase))
            //                {
            //                    if (info.topicontype.Length > 0)
            //                    {
            //                        TopInfoModule topInfo = ModuleManager.GetInstance().GetModule("topinfo") as TopInfoModule;
            //                        topInfo.setIconImage(info.topicontype);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                CloseModule(str[i], false);
            //            }
            //        }
            //    }
            //}
        }

        //打开上一次打开的界面
        public void OpenPreModule(string name)
        {
            //Sys_moduleConfigInfo info = ConfigDataManager.GetSys_moduleConfigInfo(name.ToLower());
            //if (info != null)
            //{
            //    int isShowlast = info.isshowlastmodule;
            //    if (isShowlast > 0 && preModuleName != null)
            //    {
            //        CreateModule(preModuleName);
            //    }
            //}
        }

        public BaseModule GetModule(string name)
        {
            name = name.ToLower();
            BaseModule module;
            if (moduleList.ContainsKey(name))
            {
                module = moduleList[name];
                return module;
            }
            return null;
        }

        public void CloseModule(string name, bool isMainModule = true)
        {
            name = name.ToLower();
            BaseModule module;
            if (moduleList.ContainsKey(name))
            {
                module = moduleList[name];
                module.OnExit();
                OpenOrCloseWithModule(name, false);
                OpenPreModule(name);
                if (isMainModule)
                {
                    preModuleName = name;
                }
            }
        }

        public void DestoryModule(string name)
        {
            name = name.ToLower();
            BaseModule module;
            if (!moduleList.ContainsKey(name))
                return;
            module = moduleList[name];
            module.OnExit();
            GameObject.DestroyObject(module.gameObject);
            moduleList.Remove(name);

            SingletonMB<ResourceManagerController>.GetInstance().UnloadAssetBundle(name, true);
        }

        public void CloseAllModule(bool inOnlyUiModule = true)
        {
            foreach (BaseModule module in moduleList.Values)
            {
                if (inOnlyUiModule)
                {
                    if (module.transform.parent == otherParent) continue;
                }
                if (module.GetState() == MODULE_STATE.RUN) module.OnExit();
            }
        }

        //关闭面板 不会执行OnExit内的逻辑  执行exitHandle最基本的关闭
        public void CloseModuleWithOutLogic(string name, bool isMainModule = true)
        {
            name = name.ToLower();
            BaseModule module;
            if (moduleList.ContainsKey(name))
            {
                module = moduleList[name];
                module.ExitHandle();
                OpenOrCloseWithModule(name, false);
                OpenPreModule(name);
                if (isMainModule)
                {
                    preModuleName = name;
                }
            }
        }

        public void CloseAllModuleWithOutLogic(bool inOnlyUiModule = true)
        {
            foreach (BaseModule module in moduleList.Values)
            {
                if (inOnlyUiModule)
                {
                    if (module.transform.parent == otherParent) continue;
                }
                if (module.GetState() == MODULE_STATE.RUN) module.ExitHandle();
            }
        }

        //public bool IsOpenModule(string moduleName, string kidName = "")
        //{
        //    Sys_moduleConfigInfo moduleConfigInfo = ConfigDataManager.GetSys_moduleConfigInfo(moduleName.ToLower() + kidName);
        //    if (moduleConfigInfo != null)
        //    {
        //        int curLevel = Singleton<PlayerInfoModel>.GetInstance().GetLevel();
        //        return moduleConfigInfo.open_lv <= curLevel;
        //    }
        //    return true;
        //}
        //public bool IsOpenModule(string moduleName, bool isPrompt)
        //{
        //    Sys_moduleConfigInfo moduleConfigInfo = ConfigDataManager.GetSys_moduleConfigInfo(moduleName.ToLower());
        //    if (moduleConfigInfo != null)
        //    {
        //        int curLevel = Singleton<PlayerInfoModel>.GetInstance().GetLevel();
        //        if (moduleConfigInfo.open_lv > curLevel && isPrompt)
        //        {
        //            Singleton<ModuleEventDispatcher>.GetInstance().dispatchPromptText(moduleConfigInfo.open_lv + "级开放");
        //        }
        //        return moduleConfigInfo.open_lv <= curLevel;
        //    }
        //    return true;
        //}
        //public bool IsOpenModule(int moduleId)
        //{
        //    Sys_moduleConfigInfo moduleConfigInfo = ConfigDataManager.getConfigInfoById("sys_module", moduleId) as Sys_moduleConfigInfo;
        //    if (moduleConfigInfo != null)
        //    {
        //        int curLevel = Singleton<PlayerInfoModel>.GetInstance().GetLevel();
        //        return moduleConfigInfo.open_lv <= curLevel;
        //    }
        //    return true;
        //}
        //public int getOpenModuleLevel(string moduleName, string kidName = "")
        //{
        //    Sys_moduleConfigInfo moduleConfigInfo = ConfigDataManager.GetSys_moduleConfigInfo(moduleName.ToLower() + kidName);
        //    if (moduleConfigInfo != null)
        //    {
        //        int curLevel = Singleton<PlayerInfoModel>.GetInstance().GetLevel();
        //        return moduleConfigInfo.open_lv; ;
        //    }
        //    return 0;
        //}
        //public int getOpenModuleLevel(int moduleId)
        //{
        //    Sys_moduleConfigInfo moduleConfigInfo = ConfigDataManager.getConfigInfoById("sys_module", moduleId) as Sys_moduleConfigInfo;
        //    if (moduleConfigInfo != null)
        //    {
        //        int curLevel = Singleton<PlayerInfoModel>.GetInstance().GetLevel();
        //        return moduleConfigInfo.open_lv;
        //    }
        //    return 0;
        //}
    }

    public enum MODULE_STATE
    {
        RUN,
        STOP
    }
}