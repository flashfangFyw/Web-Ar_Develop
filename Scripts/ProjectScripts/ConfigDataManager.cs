using LitJson;
using UnityEngine;
using System.Collections;
using ffDevelopmentSpace;
using System.Collections.Generic;


/* 
    Author:     fyw 
    CreateDate: 2018-02-25 16:37:38 
    Desc:       注释 
*/


public class ConfigDataManager : ConfigDataManager_Base
{

    private static Dictionary<string, DeployConfigInfo> deployConfigs;  //服务器列表
    public static Dictionary<string, DeployConfigInfo> GetDeployConfigInfos()
    {
        if (deployConfigs == null)
        {
            string jsonstr = "";// SingletonMB<ResourceManagerController>.GetInstance().LoadData(AssetConst.JsonData, "deploy");
            JsonData jd = JsonMapper.ToObject(jsonstr);
            JsonData items = jd["items"];

            Dictionary<string, DeployConfigInfo> cinfos = new Dictionary<string, DeployConfigInfo>();
            int count = items.Count;
            for (int i = 0; i < count; i++)
            {
                JsonData item = items[i];
                string json = JsonMapper.ToJson(item);

                DeployConfigInfo cinfo = GetConfigInfo("deploy", json) as DeployConfigInfo;
                if (cinfo != null)
                {
                    if (!cinfos.ContainsKey(cinfo.param))
                    {
                        cinfos.Add(cinfo.param, cinfo);
                    }
                }
            }
            deployConfigs = cinfos;
        }
        return deployConfigs;
    }
}
