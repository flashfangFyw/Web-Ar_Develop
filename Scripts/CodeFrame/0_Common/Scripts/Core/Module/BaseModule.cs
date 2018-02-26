using sy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//#pragma strict	
//在脚本顶部，之后Unity将禁用脚本的动态类型，强制你使用静态类型。
//public class BaseModule : MonoBehaviour
namespace ffDevelopmentSpace
{
    public class BaseModule : UIBehaviour
    {
        [HideInInspector]
        public string moduleName;

        protected RectTransform rectTrans;
        protected MODULE_STATE moduleState;

        void Awake()
        {
            //		useGUILayout = false;
        }

        void Start()
        {
            Init();
            InitView();
            InitEvent();
            InitEffect();
            InitNet();
        }

        public MODULE_STATE GetState()
        {
            return moduleState;
        }

        private void Init()
        {
            moduleState = MODULE_STATE.RUN;
            //SoundManager.GetInstance().PlaySound("open");
            //		gameObject.SetActive (true);
            rectTrans = transform as RectTransform;
            Transform go = rectTrans.Find("Close");
            if (null != go)
            {
                go.gameObject.GetComponent<Button>().onClick.AddListener(
                delegate ()
                {
                    OnQuit();
                }
                );
            }
        }

        protected virtual void InitView()
        {
        }

        protected virtual void InitEvent()
        {
        }

        protected virtual void InitEffect()
        {
        }

        protected virtual void InitNet()
        {
        }

        public virtual void OnEnter()
        {
            SoundManager.GetInstance().PlaySound("open");
            moduleState = MODULE_STATE.RUN;
            gameObject.SetActive(true);
            InitEffect();
        }

        protected void OnQuit()
        {
            SoundManager.GetInstance().PlaySound("click3");
            SoundManager.GetInstance().PlaySound("close");
            ModuleManager.GetInstance().CloseModule(moduleName);
            //OnExit ();
        }

        public virtual void OnExit()
        {
            ExitHandle();
            //Singleton<GuideModel>.GetInstance().PopStack();
        }

        public virtual void ExitHandle()
        {
            moduleState = MODULE_STATE.STOP;
            gameObject.SetActive(false);
        }

        protected void AddHandle(MSG_CS msgId, NetworkManagerController.SocketHandle handle)
        {
            NetworkManagerController.GetInstance().AddHandle((int)msgId, handle);
        }

        /// 添加单击事件
        protected void AddClick(string button)
        {
            Transform to = rectTrans.Find(button);
            if (to == null)
            {
                Debuger.Log("没有找到按钮：" + button);
                return;
            }
            GameObject go = to.gameObject;
            AddClick(go);
        }

        protected void AddClick(GameObject obj)
        {
            Button btn = obj.GetComponent<Button>();
            if (null != btn)
            {
                btn.onClick.AddListener(
                    delegate ()
                    {
                        ClickHandle(obj);
                    }
                );
            }
        }

        private void ClickHandle(GameObject go)
        {
            SoundManager.GetInstance().PlaySound("click3");
            OnClick(go);
        }

        protected virtual void OnClick(GameObject go)
        {
            Debuger.Log("没有重载点击处理：" + go.name);
        }

        protected void SetBack(bool isShowBackBtn = true)
        {
            GameObject prefab = SingletonMB<ResourceManagerController>.GetInstance().LoadAsset(PathUtil.getUiCommonPath(), "PopPanel");
            if (prefab == null)
            {
                return;
            }
            GameObject backPanel = GameObject.Instantiate(prefab) as GameObject;
            Transform objTrans = backPanel.transform;
            objTrans.SetParent(rectTrans);
            objTrans.localScale = Vector3.one;
            objTrans.localPosition = Vector3.zero;

            RectTransform objRectTrans = objTrans as RectTransform;
            objRectTrans.anchorMin = new Vector2(0, 0);
            objRectTrans.anchorMax = new Vector2(1, 1);
            objRectTrans.pivot = new Vector2(0.5f, 0.5f);
            objRectTrans.anchoredPosition = new Vector2(0, 0);
            objRectTrans.sizeDelta = new Vector2(0, 0);
            objRectTrans.offsetMin = new Vector2(0, 0);
            objRectTrans.offsetMax = new Vector2(0, 0);

            objTrans.SetAsFirstSibling();

            Transform go = objTrans.Find("BackBtn");
            if (null != go)
            {
                go.gameObject.SetActive(isShowBackBtn);
            }
        }

        protected UITweener CreateTweener(RectTransform recTra, TweenType type)
        {
            TweenPosition twPos = new TweenPosition();
            switch (type)
            {
                case TweenType.LEFT_IN:
                    recTra.anchoredPosition = new Vector2(-recTra.sizeDelta.x, recTra.anchoredPosition.y);
                    twPos.from = recTra.anchoredPosition;
                    twPos.to = new Vector2(0, recTra.anchoredPosition.y);
                    //			twPos.method = UITweener.Method.BackEaseInOut;
                    twPos.method = UITweener.Method.QuintEaseInOut;
                    twPos.duration = .5f;
                    break;

                case TweenType.LEFT_OUT:
                    twPos.from = recTra.anchoredPosition;
                    twPos.to = new Vector2(-recTra.sizeDelta.x, recTra.anchoredPosition.y);
                    //			twPos.method = UITweener.Method.BackEaseIn;
                    twPos.method = UITweener.Method.QuintEaseInOut;
                    twPos.duration = .3f;
                    break;

                case TweenType.RIGHT_IN:
                    recTra.anchoredPosition = new Vector2(recTra.sizeDelta.x, recTra.anchoredPosition.y);
                    twPos.from = recTra.anchoredPosition;
                    twPos.to = new Vector2(0, recTra.anchoredPosition.y);
                    twPos.method = UITweener.Method.QuintEaseInOut;
                    twPos.duration = .5f;
                    break;

                case TweenType.RIGHT_OUT:
                    twPos.from = recTra.anchoredPosition;
                    twPos.to = new Vector2(recTra.sizeDelta.x, recTra.anchoredPosition.y);
                    twPos.method = UITweener.Method.QuintEaseInOut;
                    twPos.duration = .3f;
                    break;

                case TweenType.BOTTOM_IN:
                    recTra.anchoredPosition = new Vector2(0, -recTra.sizeDelta.y);
                    twPos.from = recTra.anchoredPosition;
                    twPos.to = Vector2.zero;
                    twPos.method = UITweener.Method.QuintEaseInOut;
                    twPos.duration = .5f;
                    break;

                case TweenType.BOTTOM_OUT:
                    twPos.from = recTra.anchoredPosition;
                    twPos.to = new Vector2(0, -recTra.sizeDelta.y);
                    twPos.method = UITweener.Method.QuintEaseInOut;
                    twPos.duration = .3f;
                    break;

                case TweenType.TOP_IN:
                    recTra.anchoredPosition = new Vector2(recTra.anchoredPosition.x, recTra.sizeDelta.y);
                    twPos.from = recTra.anchoredPosition;
                    twPos.to = new Vector2(recTra.anchoredPosition.x, 0);
                    twPos.method = UITweener.Method.QuintEaseInOut;
                    twPos.duration = .5f;
                    break;

                case TweenType.TOP_OUT:
                    twPos.from = recTra.anchoredPosition;
                    twPos.to = new Vector2(recTra.anchoredPosition.x, recTra.sizeDelta.y);
                    twPos.method = UITweener.Method.QuintEaseInOut;
                    twPos.duration = .3f;
                    break;
            }
            twPos.trans = recTra;
            return twPos;
        }

        protected void InitUIOver()
        {
            //foreach (var item in guideGOList)
            //{
            //	guideGOMap.Add(item.tag.ToUpper(), item);
            //}
            //Singleton<GuideModel>.GetInstance().PushToStack(moduleName);
        }
    }

    public enum TweenType
    {
        LEFT_IN,
        LEFT_OUT,
        RIGHT_IN,
        RIGHT_OUT,
        BOTTOM_IN,
        BOTTOM_OUT,
        TOP_IN,
        TOP_OUT,
    }
}