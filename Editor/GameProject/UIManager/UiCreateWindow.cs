using UnityEngine;
using UnityEditor;
using System.Linq;
//using System.Enum;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;


public class UiCreateWindow : EditorWindow {
    string myComponentName = "Hello World";
    //bool groupEnabled = false;
    //bool myBool = true;
    //float myFloat = 1.23f;

    private const string kComponentPath = "Resources/Sprite";
    private int m_ComponentIndex;
    private string[] m_ComponentNames;
    private string m_ComponentType = "";
    private string[] typeArr;
    private int typeIndex = 0;
    public int TypeIndex//= 0;
    {
        get
        {
            return typeIndex;
        }
        set
        {
            typeIndex = value;
            UpdateComponentNamesAndComponent();
        }
    }
    private string[] componentArr;
    private int componentIndex = 0;
    public int ComponentIndex//= 0;
    {
        get
        {
            return componentIndex;
        }
        set
        {
            componentIndex = value;
            //UpdateComponentNamesAndComponent();
        }
    }
    private GameObject m_GameObjectToAttachTo;
    private DirectoryInfo componentDirInfo;
    private GameObject selectedComponent;
    class Styles
    {
        public GUIContent m_WarningContent = new GUIContent(string.Empty);
        public GUIStyle m_PreviewBox = new GUIStyle("OL Box");
        public GUIStyle m_PreviewTitle = new GUIStyle("OL Title");
        public GUIStyle m_LoweredBox = new GUIStyle("TextField");
        public GUIStyle m_HelpBox = new GUIStyle("helpbox");
        public Styles()
        {
            m_LoweredBox.padding = new RectOffset(1, 1, 1, 1);
        }
    }
    private static Styles m_Styles;
    private Vector2 m_PreviewScroll;
    //private ScriptPrescription m_ScriptPrescription;
	// Add menu item to the Window menu
    [MenuItem("GameProject/UI管理/Ui Create Window")]
	static void Init () {
		// Get existing open window or if none, make a new one:
        //EditorWindow.GetWindowWithRect<UiCreateWindow> (new Rect(0, 0, 500, 500),false);
        UiCreateWindow window = EditorWindow.GetWindow<UiCreateWindow>(false);
        window.ShowTab();
    }
	
	// Implement your own editor GUI here.
	void OnGUI () {
        if (m_Styles == null)
            m_Styles = new Styles();

        EditorGUIUtility.LookLikeControls(85f, 85f);

        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Space(10);
            EditorGUILayout.BeginVertical();
            {
                OptionsGUI();
                GUILayout.Space(10);

                CreateAndCancelButtonsGUI();
            } EditorGUILayout.EndVertical();

            GUILayout.Space(10);

            PreviewGUI();

            GUILayout.Space(10);
        } EditorGUILayout.EndHorizontal();
        
	}
    private void OptionsGUI()
    {
        EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
        {
            //======================
            GUILayout.Label("创建UI组件", EditorStyles.boldLabel);
            updateSelectedUI();
            AttachToUI();
            //string selectName = "";
            //if (Selection.activeTransform) selectName = Selection.activeTransform.name;
            //EditorGUILayout.LabelField("当前选择的物体:", selectName);
            UpdateCompentFileType();
            myComponentName = EditorGUILayout.TextField("组件名称", myComponentName);
        } EditorGUILayout.EndVertical();
    }

    private void AttachToUI()
    {
        GUILayout.BeginHorizontal();
        {
            m_GameObjectToAttachTo = EditorGUILayout.ObjectField("目标父物体：", m_GameObjectToAttachTo, typeof(GameObject), true) as GameObject;

            //if (ClearButton())
            //    m_GameObjectToAttachTo = null;
        } GUILayout.EndHorizontal();

        HelpField("选择将要创建的UI组件的父物体，默认为当前所选物体");
    }
    private void updateSelectedUI()
    {
        m_GameObjectToAttachTo = Selection.activeGameObject;
        
    }
   
    private void UpdateCompentFileType()
    {
        DirectoryInfo rootDirInfo = new DirectoryInfo(Application.dataPath + "/Resources");
        ArrayList strList = new ArrayList();
        foreach (DirectoryInfo dirInfo in rootDirInfo.GetDirectories())
        {
            strList.Add(dirInfo.Name.ToString());
        }
        typeArr = (string[])strList.ToArray(typeof(string));
        TypeIndex = EditorGUILayout.Popup("要创建的类型", TypeIndex, typeArr, GUILayout.Width(200));
    }
  
    private void UpdateComponentNamesAndComponent()
    {
        //return;
        //componentDirInfo = new DirectoryInfo(Application.dataPath + "/Resources/" + typeArr[TypeIndex]);
        ////this.ShowNotification(new GUIContent(Application.dataPath + "/Resources/" + typeArr[TypeIndex]));
        //ArrayList componentList = new ArrayList();
        //foreach (FileInfo fileInfo in componentDirInfo.GetFiles("*.prefab", SearchOption.AllDirectories))
        //{
        //    componentList.Add(Path.GetFileNameWithoutExtension(fileInfo.Name));
        //}
        ////this.ShowNotification(new GUIContent(componentList.Count.ToString()));
        //componentArr = (string[])componentList.ToArray(typeof(string));
        //ComponentIndex = EditorGUILayout.Popup("要创建的组件", ComponentIndex, componentArr, GUILayout.Width(200));
    }
    private string[] GetComponentNames()
    {
        List<string> components = new List<string>();

        // Get all file names of custom templates
        if (Directory.Exists(GetAbsoluteCustomComponentPath()))
            components.AddRange(Directory.GetFiles(GetAbsoluteCustomComponentPath()));

        //// Get all file names of built-in templates
        //if (Directory.Exists(GetAbsoluteBuiltinComponentPath()))
        //    components.AddRange(Directory.GetFiles(GetAbsoluteBuiltinComponentPath()));

        if (components.Count == 0)
            return new string[0];

        // Filter and clean up list
        components = components
            .Distinct()
            //.Where(f => (f.EndsWith("." + extension + ".txt")))
            //.Select(f => Path.GetFileNameWithoutExtension(f.Substring(0, f.Length - 4)))
            .ToList();

        // Determine which scripts have editor class base class
        //for (int i = 0; i < components.Count; i++)
        //{
        //    string templateContent = GetTemplate(components[i]);
        //    if (IsEditorClass(GetBaseClass(templateContent)))
        //        components[i] = kTempEditorClassPrefix + components[i];
        //}

        // Order list
        components = components
            //.OrderBy(f => f, new TemplateNameComparer())
            .ToList();

        // Insert separator before first editor script template
        //bool inserted = false;
        //for (int i = 0; i < components.Count; i++)
        //{
        //    if (components[i].StartsWith(kTempEditorClassPrefix))
        //    {
        //        components[i] = components[i].Substring(kTempEditorClassPrefix.Length);
        //        if (!inserted)
        //        {
        //            components.Insert(i, string.Empty);
        //            inserted = true;
        //        }
        //    }
        //}

        // Return list
        return components.ToArray();
    }
    private string GetAbsoluteCustomComponentPath()
    {
        string path = Path.Combine(kComponentPath, m_ComponentType);
        return Path.Combine(Application.dataPath, path);
    }
    private string GetAbsoluteBuiltinComponentPath()
    {
        return Path.Combine(EditorApplication.applicationContentsPath, kComponentPath);
    }
    private string GetComponent(string nameWithoutExtension)
    {
        //string path = Path.Combine(GetAbsoluteCustomTemplatePath(), nameWithoutExtension + "." + extension + ".txt");
        //if (File.Exists(path))
        //    return File.ReadAllText(path);

        //path = Path.Combine(GetAbsoluteBuiltinTemplatePath(), nameWithoutExtension + "." + extension + ".txt");
        //if (File.Exists(path))
        //    return File.ReadAllText(path);

        //return kNoTemplateString;
        return "btn01";
    }

    private void CreateAndCancelButtonsGUI()
    {
        GUI.enabled = CanCreate();
        if (GUILayout.Button("创建", GUILayout.Width(80)))
        {
            Create();
        }
    }
    private void Create()
    {
        Debug.Log("create");
        GameObject uiComponent =(GameObject) Instantiate(selectedComponent, new Vector3(), Quaternion.identity);
        uiComponent.transform.parent = Selection.activeTransform;
		uiComponent.name = myComponentName;
        //Selection.activeGameObject.in
    }
    private bool CanCreate()
    {

        return SelectedObjectExist()
              && SelectedComponentExist();
    }
    private GameObject selectedGameObject;
    private bool SelectedObjectExist()
    {
        if (!Selection.activeGameObject) return false;
        
        Canvas canvas = Selection.activeGameObject.GetComponentInParent<Canvas>();
        return canvas;
    }
    private bool SelectedComponentExist()
    {
        if (componentArr == null || ComponentIndex + 1 > componentArr.Length ) return false;
        selectedComponent = GameObject.Find(componentArr[ComponentIndex]);
        return selectedComponent;
    }
    private void PreviewGUI()
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(Mathf.Max(position.width * 0.4f, position.width - 380f)));
        {
            // Reserve room for preview title
            Rect previewHeaderRect = GUILayoutUtility.GetRect(new GUIContent("预览"), m_Styles.m_PreviewTitle);

            // Secret! Toggle curly braces on new line when double clicking the script preview title
            //Event evt = Event.current;
            //if (evt.type == EventType.MouseDown && evt.clickCount == 2 && previewHeaderRect.Contains(evt.mousePosition))
            //{
            //    EditorPrefs.SetBool("CurlyBracesOnNewLine", !EditorPrefs.GetBool("CurlyBracesOnNewLine"));
            //    Repaint();
            //}

            // Preview scroll view
            m_PreviewScroll = EditorGUILayout.BeginScrollView(m_PreviewScroll, m_Styles.m_PreviewBox);
            {
                EditorGUILayout.BeginHorizontal();
                {
                    // Tiny space since style has no padding in right side
                    GUILayout.Space(5);

                    // Preview text itself
                    //string previewStr = new NewScriptGenerator(m_ScriptPrescription).ToString();
                    //Rect r = GUILayoutUtility.GetRect(
                    //    new GUIContent(previewStr),
                    //    EditorStyles.miniLabel,
                    //    GUILayout.ExpandWidth(true),
                    //    GUILayout.ExpandHeight(true));
                    //EditorGUI.SelectableLabel(r, previewStr, EditorStyles.miniLabel);
                } EditorGUILayout.EndHorizontal();
            } EditorGUILayout.EndScrollView();

            // Draw preview title after box itself because otherwise the top row
            // of pixels of the slider will overlap with the title
            GUI.Label(previewHeaderRect, new GUIContent("预览"), m_Styles.m_PreviewTitle);

            GUILayout.Space(4);
        } EditorGUILayout.EndVertical();
    }
    private void HelpField(string helpText)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(string.Empty, GUILayout.Width(85));
        GUILayout.Label(helpText, m_Styles.m_HelpBox);
        GUILayout.EndHorizontal();
    }
}