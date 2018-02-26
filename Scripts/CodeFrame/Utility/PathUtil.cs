using System.Collections;
using UnityEngine;

public class PathUtil
{
    public static string getPlaneTypeIcon()
    {
        return "icon/planetypeicon" + Const.ExtName;
    }

    public static string getPlaneIcon()
    {
		return "icon/planeicon" + Const.ExtName;
    }

    public static string getShipImagePath(string name)
    {
		return "image/ship/" + name + Const.ExtName;
    }

	public static string getPassImagePath(string name)
	{
		return "image/pass/" + name + Const.ExtName;
	}

    public static string GetDialyCopyImagePath()
    {
		return "image/dailycopy" + Const.ExtName;
    }

    public static string GetDialyCopyDetailImagePath()
    {
		return "image/dailycopydetail" + Const.ExtName;
    }

    public static string getShipModelPath(string name)
    {
		return "model/ship/" + name + Const.ExtName;
    }

    public static string getCarrierModelPath(string name)
    {
		return "model/carrier/" + name + Const.ExtName;
    }

    public static string getUiSoundPath(string name)
    {
		return "sound/ui/" + name + Const.ExtName;
    }

    public static string getSceneSoundPath(string name)
    {
		return "sound/scene/" + name + Const.ExtName;
    }

    public static string getIconShipPath()
    {
		return "icon/shipicon" + Const.ExtName;
    }

    public static string getIconItemPath()
    {
		return "icon/itemicon" + Const.ExtName;
    }

    public static string getIconSkillPath()
    {
		return "icon/skillicon" + Const.ExtName;
    }

    public static string getIconArmyPath()
    {
		return "icon/armyicon" + Const.ExtName;
    }

    public static string getShipTypeIconPath()
    {
		return "icon/shiptypeicon" + Const.ExtName;
    }

    public static string getShipItemIconPath()
    {
		return "icon/shipitemicon" + Const.ExtName;
    }

    public static string getShipFlagIconPath()
    {
		return "icon/flagicon" + Const.ExtName;
    }

    public static string getUiPanelPath(string name)
    {
		return "ui/" + name + Const.ExtName;
    }

    public static string getUiAssetPath(string name)
    {
		return "ui/" + name + Const.ExtName;
    }

    public static string getTerrainPath(string name)
    {
		return "terrain/" + name + Const.ExtName;
    }

    public static string getUiCommonPath()
    {
		return "ui/commonui" + Const.ExtName;
    }

    public static string getMaterialPath(string name)
    {
		return "material/" + name + Const.ExtName;
    }
}