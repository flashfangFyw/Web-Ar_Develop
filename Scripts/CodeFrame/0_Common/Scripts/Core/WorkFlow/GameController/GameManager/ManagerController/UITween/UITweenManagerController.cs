using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ffDevelopmentSpace
{

    public class UITweenManagerController :SingletonMB<UITweenManagerController>
    {
        List<UITweener> list = new List<UITweener>();
        //	int time = 0;
         void Update()
        {
            //		if (time == 1) 
            //		{
            //			time = 0;
            //			return;
            //		}
            //		time++;
            int len = list.Count;
            if (len == 0) return;
            for (int i = len - 1; i >= 0; i--)
            {
                UITweener tweener = list[i];
                if (!tweener.endFlag) tweener.Update();
                else list.Remove(tweener);
            }
        }

        public void AddTweener(UITweener tweener)
        {
            if (null != tweener)
                tweener.ResetToBeginning();
            list.Add(tweener);
        }

    }
}
