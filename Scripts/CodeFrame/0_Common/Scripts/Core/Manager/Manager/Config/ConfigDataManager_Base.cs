using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

//using SimpleJSON;

    /// <summary>
    /// 
    /// </summary>
namespace ffDevelopmentSpace
{
    public class ConfigDataManager_Base
    {
        //	private const string SPLIT_SYMBOL:String = "-";
        private static char[] SPLIT_SYMBOL_EXTEND = new char[] { '|' };

        private static char[] SPLIT_SYMBOL_LIST = new char[] { ',' };
        private static Dictionary<string, Dictionary<int, BaseConfigInfo>> configs = new Dictionary<string, Dictionary<int, BaseConfigInfo>>();
        private static Dictionary<sy.CopyType, List<int>> CopyTypeId = new Dictionary<sy.CopyType, List<int>>();         //副本类型对应的id

        //	private static Dictionary<string, JsonData> configs = new Dictionary<string, JsonData>();
        private static BaseConfigInfo[] tempList;

        //filename  为配置文件名 除去后缀和路径  比如 equip.json  这里的filename 为 equip
        public static Dictionary<int, BaseConfigInfo> GetConfigInfos(string filename)
        {
            if (configs.ContainsKey(filename))
            {
                return configs[filename];
            }

            /*string path ="Assets/data/"+filename + ".json";
            string jsonstr = File.ReadAllText(path, System.Text.Encoding.UTF8);*/

            string jsonstr = SingletonMB<ResourceManagerController>.GetInstance().LoadData(AssetConst.JsonData, filename);
            Util.onTimeStart();
            //		JSONNode jn = JSON.Parse (jsonstr);
            JsonData jd = JsonMapper.ToObject(jsonstr);

            JsonData items = jd["items"];
            //		JSONArray items = jn["items"].AsArray;
            int count = items.Count;
            if (0 == count)
                return null;
            //		Util.onTimeStart ();
            Dictionary<int, BaseConfigInfo> cinfos = new Dictionary<int, BaseConfigInfo>(count);
            tempList = new BaseConfigInfo[count];

            //		try
            {
                Type t = GetConfigInfoType(filename, count);
                FieldInfo[] fields = t.GetFields();
                FieldInfo f;
                string fieldName = string.Empty;
                string value;
                int len = fields.Length;
                for (int i = 0; i < count; i++)
                {
                    JsonData item = items[i];
                    //				JSONNode item = items[i];
                    //				string json = "";
                    //				json = JsonMapper.ToJson(item);
                    //				BaseConfigInfo cinfo = GetConfigInfo(filename, json);
                    BaseConfigInfo cinfo = tempList[i];
                    for (int m = 0; m < len; m++)
                    {
                        f = fields[m];
                        fieldName = f.Name;
                        value = item[f.Name].ToString();
                        //					value = item[f.Name].Value;
                        f.SetValue(cinfo, Convert.ChangeType(value, f.FieldType));
                    }
                    cinfos.Add(cinfo.id, cinfo);
                }
            }
            //		catch
            //		{
            //			Debuger.Log("解析数据出错 filename：" + filename + "   出错数据内容：" + json);
            //		}
            tempList = null;
            configs.Add(filename, cinfos);
            Util.onTimeEnd(filename);
            return cinfos;
        }

        private static Type GetConfigInfoType(string filename, int count)
        {
            Type t = null;
            //		BaseConfigInfo info = null;
            switch (filename)
            {
                //每一个文件需要添加对应的解析目标文件 如下：
                //case "copy_chapter":
                //    for (int i = 0; i < count; i++)
                //    {
                //        tempList[i] = new Copy_chapterConfigInfo();
                //    }
                //    t = typeof(Copy_chapterConfigInfo);
                //    break;

                default:
                    Debuger.Log("没有添加对应的解析 filename:" + filename);
                    break;
            }

            return t;
        }

        protected  static BaseConfigInfo GetConfigInfo(string filename, string jsonstr)
        {
            BaseConfigInfo cinfo = null;
            //这个对应关系可以优化为一个配置表来完成，在生成json的工具里生成这个配置表
            //		try
            //		{
            switch (filename)
            {
                //每一个文件需要添加对应的解析目标文件 如下：
                //case "guide_info":
                //    cinfo = JsonMapper.ToObject<Guide_infoConfigInfo>(jsonstr);
                //    break;
                default:
                    Debuger.Log("没有添加对应的解析 filename:" + filename);
                    break;
            }
            //		}
            //		catch
            //		{
            //			Debuger.Log("解析数据出错 filename：" + filename + "   出错数据内容：" + jsonstr);
            //		}

            return cinfo;
        }

        //根据id获取对应的配置信息
        public static BaseConfigInfo GetConfigInfoById(string filename, int id)
        {
            Dictionary<int, BaseConfigInfo> cinfos = GetConfigInfos(filename);
            //		JsonData cinfos = GetConfigInfos(filename);
            //		return cinfos[0];

            if (cinfos != null)
            {
                if (cinfos.ContainsKey(id))
                {
                    return cinfos[id];
                }
                else
                {
                    string tipsStr = "没有找到对应的配置 filename：" + filename + "    id:" + id;
                    Singleton<ModuleEventDispatcher>.GetInstance().dispatchPromptWnd(DialogType.Cancel, tipsStr);
                    Singleton<ModuleEventDispatcher>.GetInstance().dispatchDebugLogEvent(tipsStr);
                    Debuger.Log("没有找到对应的配置 filename：" + filename + "    id:" + id);
                }
            }
            else
            {
                string tipsStr = "没有找到对应的配置 filename：" + filename + "    id:" + id;
                Singleton<ModuleEventDispatcher>.GetInstance().dispatchPromptWnd(DialogType.Cancel, tipsStr);
                Singleton<ModuleEventDispatcher>.GetInstance().dispatchDebugLogEvent(tipsStr);
                Debuger.Log("没有找到对应的配置文件 filename:" + filename);
            }
            return null;
        }

        public static T GetConfig<T>(string filename, int id) where T : BaseConfigInfo
        {
            return GetConfigInfoById(filename, id) as T;
        }

        //public static int GetSettingValueByName(string name)
        //{
        //    int nValue = 0;
        //    SettingConfigInfo info = GetSettingByName(name);
        //    if (info != null)
        //    {
        //        nValue = info.value;
        //    }
        //    return nValue;
        //}

        //public static SettingConfigInfo GetSettingByName(string name)
        //{
        //    if (settingConfigs == null)
        //    {
        //        string jsonstr = SingletonMB<ResourceManagerController>.GetInstance().LoadData(AssetConst.JsonData, "setting");
        //        JsonData jd = JsonMapper.ToObject(jsonstr);
        //        JsonData items = jd["items"];

        //        Dictionary<string, SettingConfigInfo> cinfos = new Dictionary<string, SettingConfigInfo>();
        //        int count = items.Count;
        //        for (int i = 0; i < count; i++)
        //        {
        //            JsonData item = items[i];
        //            string json = JsonMapper.ToJson(item);

        //            SettingConfigInfo cinfo = GetConfigInfo("setting", json) as SettingConfigInfo;
        //            if (cinfo != null)
        //            {
        //                if (!cinfos.ContainsKey(cinfo.param))
        //                {
        //                    cinfos.Add(cinfo.param, cinfo);
        //                }
        //            }
        //        }
        //        settingConfigs = cinfos;
        //    }

        //    if (settingConfigs.ContainsKey(name))
        //    {
        //        return settingConfigs[name];
        //    }
        //    else
        //    {
        //        Debuger.Log("没有找到对应的数据 name:" + name);
        //        return null;
        //    }
        //}



        //public static string GetSettingArrayValueByName(string name)
        //{
        //    string strValue = "";
        //    Setting2ConfigInfo info = GetSetting2ByName(name);
        //    if (info != null)
        //    {
        //        strValue = info.value;
        //    }
        //    return strValue;
        //}


        public static List<KeyValuePair<int, int>> GetAttributeList(string str)
        {
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
            if (str == "")
            {
                return list;
            }
            string[] tmp = str.Split(SPLIT_SYMBOL_LIST);
            int len = tmp.Length;
            for (int i = 0; i < len; i++)
            {
                string strTmp = tmp[i];
                string[] tmp1 = strTmp.Split(SPLIT_SYMBOL_EXTEND);
                KeyValuePair<int, int> pair = new KeyValuePair<int, int>(int.Parse(tmp1[0]), int.Parse(tmp1[1]));
                list.Add(pair);
            }
            return list;
        }

        public static KeyValuePair<int, int> GetValue(string str)
        {
            KeyValuePair<int, int> pair = new KeyValuePair<int, int>();
            if (null != str && str != "")
            {
                string[] strTmp = str.Split(SPLIT_SYMBOL_EXTEND);
                pair = new KeyValuePair<int, int>(int.Parse(strTmp[0]), int.Parse(strTmp[1]));
            }
            return pair;
        }

        public static string[] GetValueList(string str)
        {
            string[] list = new string[] { };
            if (null != str && str != "") list = str.Split(SPLIT_SYMBOL_EXTEND);
            return list;
        }

        public static string[] GetValueList2(string str)
        {
            string[] list = new string[] { };
            if (null != str && str != "") list = str.Split(SPLIT_SYMBOL_LIST);
            return list;
        }

        public static string[][] GetValueArray(string str)
        {
            string[][] arrStr = null;
            if (str != "")
            {
                string[] tmp = str.Split(SPLIT_SYMBOL_LIST);
                int row = tmp.Length;
                if (row > 0)
                {
                    arrStr = new string[row][];
                    for (int i = 0; i < row; i++)
                    {
                        string strTmp = tmp[i];
                        arrStr[i] = strTmp.Split(SPLIT_SYMBOL_EXTEND);
                    }
                }
            }

            return arrStr;
        }

    }
}