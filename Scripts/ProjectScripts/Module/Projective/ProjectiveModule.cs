using UnityEngine;
using System.Collections;
using ffDevelopmentSpace;
using UnityEngine.SceneManagement;


/* 
    Author:     fyw 
    CreateDate: 2018-02-27 17:37:14 
    Desc:       注释 
*/


public class ProjectiveModule : BaseModule
{

    #region public property
    #endregion
    #region private property
    private RectTransform topUiTrans;
    #endregion

    #region unity function
    protected override void InitView()
    {
        topUiTrans = rectTrans.Find("TopPanel").gameObject.GetComponent<RectTransform>();

        AddClick("TopPanel/bg/Home");
        AddClick("TopPanel/bg/Back");
    }
    protected override void InitEffect()
    {
        UITweener twPos = CreateTweener(topUiTrans, TweenType.TOP_IN);
        SingletonMB<UITweenManagerController>.GetInstance().AddTweener(twPos);
    }
    override protected void OnClick(GameObject obj)
    {
        switch (obj.name)
        {

            case "Home":
                //if (ModuleManager.GetInstance().IsOpenModule("switchMode",true)) {
                //	ModuleManager.GetInstance().CloseAllModule();
                //	ModuleManager.GetInstance().CreateModule("SwitchMode");
                //}
                Debuger.Log("click Home");
                ModuleManager.GetInstance().CloseModule(StringConst.Module_Projective);
                ModuleManager.GetInstance().CreateModule(StringConst.Module_WebExporler);
                //SceneManager.LoadScene(StringConst.Scene0);
                break;

            case "Back":
                //if (ModuleManager.GetInstance().IsOpenModule("WarShipList",true)) {
                //	ModuleManager.GetInstance().CreateModule("WarShipList");	
                //}
                Debug.Log("click Btn3");
                break;
          
        }
    }

    public override void OnExit()
    {
        UITweener twPos = CreateTweener(topUiTrans, TweenType.TOP_OUT);
        SingletonMB<UITweenManagerController>.GetInstance().AddTweener(twPos);
    }
    #endregion

    #region public function
    #endregion
    #region private function
    #endregion

    #region event function
    #endregion
}
