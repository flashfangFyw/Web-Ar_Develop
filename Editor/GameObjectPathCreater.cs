using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using ffDevelopmentSpace;

public class GameObjectPathCreater : EditorWindow
{
	private string moduleName;
	private string path;

	[MenuItem("GameProject/获取模块名称和路径")]
	private static void AddWindow()
	{
		//创建窗口
		Rect wr = new Rect(0, 0, 400, 500);
		GameObjectPathCreater window = (GameObjectPathCreater)EditorWindow.GetWindowWithRect(typeof(GameObjectPathCreater), wr, true, "widow name");
		window.Show();
	}

	private Transform parentT;
	private StringBuilder sb;

	private void OnEnable()
	{
		sb = new StringBuilder();

		foreach (var goItem in Selection.gameObjects)
		{
			//var selectObj = Selection.gameObjects[0];
			var selectObj = goItem;
			parentT = selectObj.transform;
			List<string> nameList = new List<string>();
			bool isBreak = true;
			int i = 0;
			do
			{
				if (parentT.GetComponent<BaseModule>() != null)
				{
					isBreak = false;
				}
				else
				{
					parentT = parentT.parent;
					nameList.Add(parentT.name);
				}

				i++;
			} while (isBreak && i < 10);

			if (parentT.GetComponent<BaseModule>() != null)
			{
				moduleName = parentT.GetComponent<BaseModule>().moduleName;
				if (goItem.layer == 5)
				{
					sb.Append(2 + "    ");
				}
				else
				{
					sb.Append(3 + "    ");
				}

				sb.Append(moduleName + "    ");
				nameList.Reverse();
				foreach (var item in nameList)
				{
					sb.Append(item + "/");
				}
				sb.Append(selectObj.name);
			}
			else
			{
				Debug.LogError("没找到模块名称");
			}
			sb.Append(Environment.NewLine);
		}
	}

	private void OnGUI()
	{
		if (moduleName != null)
		{
			//moduleName = EditorGUILayout.TextField("模块名称", moduleName);
			path = EditorGUILayout.TextArea(sb.ToString());
		}
	}
}