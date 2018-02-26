using UnityEngine;  
using System.Collections;  
using UnityEditor;



public class CustomFontCreater : EditorWindow 
{
 
    [MenuItem ("GameProject/创建字库")]
    static void AddWindow ()
	{       
		//创建窗口
		Rect  wr = new Rect (0, 0, 300, 150);
        CustomFontCreater window = (CustomFontCreater)EditorWindow.GetWindowWithRect(typeof(CustomFontCreater), wr, true, "widow name");	
		window.Show();
 
    }
 
	//输入文字的内容
    private Font bmFont;
	//选择贴图的对象
    private TextAsset textFnt;
 
	public void Awake () 
	{
		//在资源中读取一张贴图
        //texture = Resources.Load("1") as Texture;
	}
 
	//绘制窗口时调用
    void OnGUI () 
	{
        ////输入框控件
        //text = EditorGUILayout.TextField("输入文字:",text);
 
        //if(GUILayout.Button("打开通知",GUILayout.Width(200)))
        //{
        //    //打开一个通知栏
        //    this.ShowNotification(new GUIContent("This is a Notification"));
        //}
 
        //if(GUILayout.Button("关闭通知",GUILayout.Width(200)))
        //{
        //    //关闭通知栏
        //    this.RemoveNotification();
        //}
 
        ////文本框显示鼠标在窗口的位置
        //EditorGUILayout.LabelField ("鼠标在窗口的位置", Event.current.mousePosition.ToString ());
 
		//选择贴图
        bmFont = EditorGUILayout.ObjectField("选择 Custom Font", bmFont, typeof(Font), true) as Font;
 
        textFnt = EditorGUILayout.ObjectField("选择字体信息文件", textFnt, typeof(TextAsset), true) as TextAsset;

        if (GUILayout.Button("生成 Custom Font 属性", GUILayout.Width(200)))
        {
            //关闭窗口
            if (textFnt != null && bmFont != null)
            {
                CreateFont();
            }
            else 
            {
                Debug.LogError("请先选择");
            }
        }

		if(GUILayout.Button("关闭窗口",GUILayout.Width(200)))
		{
			//关闭窗口
			this.Close();
		}
    }
 
	//更新
	void Update()
	{
 
	}
 
	void OnFocus()
	{
        //Debug.Log("当窗口获得焦点时调用一次");
	}
 
	void OnLostFocus()
	{
        //Debug.Log("当窗口丢失焦点时调用一次");
	}
 
	void OnHierarchyChange()
	{
        //Debug.Log("当Hierarchy视图中的任何对象发生改变时调用一次");
	}
 
	void OnProjectChange()
	{
        //Debug.Log("当Project视图中的资源发生改变时调用一次");
	}
 
	void OnInspectorUpdate()
	{
	   //Debug.Log("窗口面板的更新");
	   //这里开启窗口的重绘，不然窗口信息不会刷新
	   this.Repaint();
	}
 
	void OnSelectionChange()
	{
		//当窗口出去开启状态，并且在Hierarchy视图中选择某游戏对象时调用
		foreach(Transform t in Selection.transforms)
		{
			//有可能是多选，这里开启一个循环打印选中游戏对象的名称
			Debug.Log("OnSelectionChange" + t.name);
		}
	}
 
	void OnDestroy()
	{
        //Debug.Log("当窗口关闭时调用");
	}

    private BMFont mbFont = new BMFont();  

    void CreateFont()
    {

		BMFontReader.Load(mbFont, textFnt.name, textFnt.bytes);  // 借用NGUI封装的读取类
        CharacterInfo[] characterInfo = new CharacterInfo[mbFont.glyphs.Count];
        for (int i = 0; i < mbFont.glyphs.Count; i++)
        {
            BMGlyph bmInfo = mbFont.glyphs[i];
            CharacterInfo info = new CharacterInfo();
            info.index = bmInfo.index;
            info.uv.x = (float)bmInfo.x / (float)mbFont.texWidth;
            info.uv.y = 1 - (float)bmInfo.y / (float)mbFont.texHeight;
            info.uv.width = (float)bmInfo.width / (float)mbFont.texWidth;
            info.uv.height = -1f * (float)bmInfo.height / (float)mbFont.texHeight;
            info.vert.x = (float)bmInfo.offsetX;
            //info.vert.y = (float)bmInfo.offsetY;  
            info.vert.y = 0f;//自定义字库UV从下往上，所以这里不需要偏移，填0即可。 
            info.vert.width = (float)bmInfo.width;
            info.vert.height = (float)bmInfo.height;
            info.width = (float)bmInfo.advance;
            characterInfo[i] = info;
        }
        bmFont.characterInfo = characterInfo;
        //AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(bmFont));
        EditorUtility.SetDirty(bmFont);
        AssetDatabase.SaveAssets();
        Debug.Log(AssetDatabase.GetAssetPath(bmFont));
        //AssetDatabase.WriteImportSettingsIfDirty(AssetDatabase.GetAssetPath(bmFont));
        AssetDatabase.Refresh();
    }
} 
