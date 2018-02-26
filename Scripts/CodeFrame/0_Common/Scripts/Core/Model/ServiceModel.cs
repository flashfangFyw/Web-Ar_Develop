using UnityEngine;
using System.Collections;
using ffDevelopmentSpace;
using System.Collections.Generic;


/* 
    Author:     fyw 
    CreateDate: 2018-02-25 16:29:25 
    Desc:       注释 
*/

namespace ffDevelopmentSpace
{
    public class ServiceModel : ModelBase
    {
        //所有服务器信息
        private Dictionary<int, ServerInfoVO> allSeverInfos;
        //当前选择的服务器
        private ServerInfoVO CurServerInfo = new ServerInfoVO();
        //private string account = "";
        //private long playerID;
        //private string token = "";
        //private long expiryTime;
        public static string SEVER_CHANGE = "sever_change";

        public void UpdateServerInfo(int key)
        {
            CurServerInfo = allSeverInfos[key];
            EventObject eventobj = new EventObject();
            eventobj.obj = key;
            dispatchEvent(SEVER_CHANGE, eventobj);
        }
        public ServerInfoVO GetCurSeverInfo()
        {
            return CurServerInfo;
        }
        public Dictionary<int, ServerInfoVO> GetAllSeverInfos()
        {
            if (allSeverInfos == null)
            {
                allSeverInfos = new Dictionary<int, ServerInfoVO>();
                int count = 0;
                Dictionary<string, DeployConfigInfo> deployinfo = ConfigDataManager.GetDeployConfigInfos();
                if (deployinfo != null)
                {
                    foreach (KeyValuePair<string, DeployConfigInfo> info in deployinfo)
                    {
                        if (info.Value.port != -1)
                        {
                            ServerInfoVO severinfo = new ServerInfoVO();
                            severinfo.Type = info.Value.type;
                            severinfo.IP = info.Value.value;
                            severinfo.Port = info.Value.port;
                            severinfo.Name = info.Value.name;
                            severinfo.ID = info.Value.severId;
                            allSeverInfos[count] = severinfo;
                            count++;
                        }
                    }
                }
            }
            return allSeverInfos;
        }
    }
}
