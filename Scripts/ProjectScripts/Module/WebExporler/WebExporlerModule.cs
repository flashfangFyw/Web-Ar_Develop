using UnityEngine;
using System.Collections;
using ffDevelopmentSpace;


/* 
    Author:     fyw 
    CreateDate: 2018-02-27 10:44:32 
    Desc:       内置嵌入浏览器模块
*/  


    public class WebExporlerModule : BaseModule
{

    #region public property
    #endregion
    #region private property
    private RectTransform webExporlerTrans;
    //private RectTransform mainUiTrans;
    private RectTransform topUiTrans;
    private UniWebView uniWebView;
    #endregion

    #region unity function
   

    protected override void InitView()
    {
        webExporlerTrans = rectTrans.Find("WebExporler").gameObject.GetComponent<RectTransform>();
        if (webExporlerTrans) uniWebView= webExporlerTrans.gameObject.AddComponent<UniWebView>();
        topUiTrans = rectTrans.Find("TopPanel").gameObject.GetComponent<RectTransform>();
        AddClick("TopPanel/bg/Btn4");
        AddClick("TopPanel/bg/Btn5");
        InitUniWebView();
    }

    protected override void InitEffect()
    {
        //UITweener twPos = CreateTweener(mainUiTrans, TweenType.BOTTOM_IN);
        //SingletonMB<UITweenManagerController>.GetInstance().AddTweener(twPos);

        //twPos = CreateTweener(topUiTrans, TweenType.TOP_IN);
        //SingletonMB<UITweenManagerController>.GetInstance().AddTweener(twPos);
    }

    protected override void InitEvent()
    {
        base.InitEvent();
    }

    protected override void InitNet()
    {
        //Singleton<DstrikeModel>.GetInstance().RequestDstrikeList();
    }


    public override void OnEnter()
    {
        base.OnEnter();
    }

    override protected void OnClick(GameObject obj)
    {
        switch (obj.name)
        {
            case "Btn4":
                Debug.Log("click Btn4");
                break;
            case "Btn5":
                Debug.Log("click Btn5");
                break;
        }
    }

    public override void OnExit()
    {
        //UITweener twPos = CreateTweener(mainUiTrans, TweenType.BOTTOM_OUT);
        //twPos.onEnd = ExitHandle;
        //SingletonMB<UITweenManagerController>.GetInstance().AddTweener(twPos);

        //twPos = CreateTweener(topUiTrans, TweenType.TOP_OUT);
        //SingletonMB<UITweenManagerController>.GetInstance().AddTweener(twPos);

    }
    #endregion

    #region public function
    #endregion
    #region private function
    private void InitUniWebView()
    {
        if(uniWebView)
        {
            uniWebView.ReferenceRectTransform = webExporlerTrans;
            uniWebView.Load("http://172.29.106.254:3000/");
            uniWebView.Show();
        }
    }
    #endregion

    #region event function
    #endregion
}
