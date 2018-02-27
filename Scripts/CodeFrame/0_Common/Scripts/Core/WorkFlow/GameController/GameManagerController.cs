using UnityEngine;
using System.Collections;
using ffDevelopmentSpace;


/* 
    Author:     fyw 
    CreateDate: 2018-02-25 14:08:07 
    Desc:      游戏控制器挂靠在对象 GameController下
               整个项目的单例不被摧毁
*/

namespace ffDevelopmentSpace
{


    public class GameManagerController : SingletonMB<GameManagerController>
    {

        #region public property
        public bool OpenDebugPanel = true;
        public bool AddResourceManagerController = true;
        public bool AddNetworkManagerController = true;
        public bool OpenTimerManager = true;
        public bool AddUITweenManagerController = true;
        #endregion
        #region private property
        #endregion

        #region unity function
        void Awake()
        {
            Init();
        }
        void OnEnable()
        {
        }
        void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

        }
       
        void Update()
        {
            if (OpenDebugPanel)
            {
                if (Input.GetKeyDown(KeyCode.D) || Input.touchCount == 3)
                {
                    ModuleManager.GetInstance().CreateModule("debug", ModuleManager.GetInstance().getOtherPanel());
                }
            }
        }
        void OnDisable()
        {
        }
        void OnDestroy()
        {
        }
        #endregion

        #region public function
        #endregion
        #region private function
        private void OnApplicationFocus(bool val)
        {
            if (!val) Screen.sleepTimeout = SleepTimeout.SystemSetting;
            else Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void OnApplicationQuit()
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }
        private void Init()
        {
            DontDestroyOnLoad(gameObject);  //防止销毁自己
            if (AddResourceManagerController)  this.gameObject.AddComponent<ResourceManagerController>();
            if (AddNetworkManagerController) this.gameObject.AddComponent<NetworkManagerController>();
            if(AddUITweenManagerController)
            {
                GameObject obj = GameObject.Find("ScreenUICanvas");
                if (obj) obj.AddComponent<UITweenManagerController>();
            }
            //if(OpenDebugPanel)
            //{
            //    ModuleManager.GetInstance().CreateModule("debug", ModuleManager.GetInstance().getOtherPanel());
            //}
            ModuleManager.GetInstance().CreateModule("MainScene");
            ModuleManager.GetInstance().CreateModule("WebExporler");
            //timerManager = TimerManager.GetInstance();
        }
        #endregion

        #region event function
        #endregion
    }
}
