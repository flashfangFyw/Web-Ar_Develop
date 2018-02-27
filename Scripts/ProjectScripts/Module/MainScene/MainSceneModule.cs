//using SuperSDKForUnity3D;
//using SuperSDKForUnity3D.Class;
using sy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ffDevelopmentSpace;
using UnityEngine.SceneManagement;

public class MainSceneModule : BaseModule
{
	private RectTransform mainUiTrans;
	private RectTransform topUiTrans;
	//private GameObject rightPanel;
	//private RectTransform leftUiTrans;
	//public Transform ShopPanl;
	//public Transform RewardBoxForm;

	//private Transform activeRed;
	//private Transform TaskRed;
	//private Transform ChatRed;
	//private Transform CopyRed;

	//	private GameObject leftPanel;

	protected override void InitView()
	{
		mainUiTrans = rectTrans.Find("MainUiPanel").gameObject.GetComponent<RectTransform>();
        //topUiTrans = rectTrans.Find("TopPanel").gameObject.GetComponent<RectTransform>();

        AddClick("MainUiPanel/bg/Btn1");
		AddClick("MainUiPanel/bg/Btn2");
		AddClick("MainUiPanel/bg/Btn3");

		//AddClick("TopPanel/bg/Btn4");
		//AddClick("TopPanel/bg/Btn5");
	}

	protected override void InitEffect()
	{
		UITweener twPos = CreateTweener(mainUiTrans, TweenType.BOTTOM_IN);
		SingletonMB<UITweenManagerController>.GetInstance().AddTweener(twPos);

        //twPos = CreateTweener(topUiTrans, TweenType.TOP_IN);
        //SingletonMB<UITweenManagerController>.GetInstance().AddTweener(twPos);

        //twPos = CreateTweener(leftUiTrans, TweenType.LEFT_IN);
        //      SingletonMB<UITweenManagerController>.GetInstance().AddTweener(twPos);
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
			case "Btn1":
                //if (ModuleManager.GetInstance().IsOpenModule("copy",true)) {
                //	ModuleManager.GetInstance().CloseAllModuleWithOutLogic();
                //	Singleton<ModuleEventDispatcher>.GetInstance().dispatchLoadingSceneStart("copy");
                //}
                Debuger.Log("click Btn1");
                //SceneManager.LoadScene("Ar_MapBox");
                break;

			case "Btn2":
                //if (ModuleManager.GetInstance().IsOpenModule("switchMode",true)) {
                //	ModuleManager.GetInstance().CloseAllModule();
                //	ModuleManager.GetInstance().CreateModule("SwitchMode");
                //}
                Debuger.Log("click Btn1");
                ModuleManager.GetInstance().CloseModule(StringConst.Module_WebExporler);
                ModuleManager.GetInstance().CreateModule(StringConst.Module_Projective);
                SceneManager.LoadScene(StringConst.Scene1);
                break;

			case "Btn3":
                //if (ModuleManager.GetInstance().IsOpenModule("WarShipList",true)) {
                //	ModuleManager.GetInstance().CreateModule("WarShipList");	
                //}
                Debug.Log("click Btn3");
                break;
            case "Btn4":
                //if (ModuleManager.GetInstance().IsOpenModule("WarShipList",true)) {
                //	ModuleManager.GetInstance().CreateModule("WarShipList");	
                //}
                Debug.Log("click Btn4");
                break;
            case "Btn5":
                //if (ModuleManager.GetInstance().IsOpenModule("WarShipList",true)) {
                //	ModuleManager.GetInstance().CreateModule("WarShipList");	
                //}
                Debug.Log("click Btn5");
                break;
        }
	}

	public override void OnExit()
	{
		UITweener twPos = CreateTweener(mainUiTrans, TweenType.BOTTOM_OUT);
		twPos.onEnd = ExitHandle;
        SingletonMB<UITweenManagerController>.GetInstance().AddTweener(twPos);

		//twPos = CreateTweener(topUiTrans, TweenType.TOP_OUT);
  //      SingletonMB<UITweenManagerController>.GetInstance().AddTweener(twPos);

	}
	
}