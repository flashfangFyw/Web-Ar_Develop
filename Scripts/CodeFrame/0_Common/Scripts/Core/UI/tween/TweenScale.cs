using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class TweenScale : UITweener
{
    public Vector3 from = Vector3.one;
    public Vector3 to = Vector3.one;
    public RectTransform RectTransform;
    public Transform Transform;
    public bool IsUI;
    public Action OnFinished;
    protected Vector2 CacheTemp;

    protected override void OnUpdate(float factor, bool isFinished)
    {
        if (IsUI)
        {
            if (RectTransform) RectTransform.localScale = from*(1f - factor) + to*factor;
            else endFlag = true;
        }
        if (Transform) Transform.localScale = from*(1f - factor) + to*factor;
        else endFlag = true;

        if (isFinished)
        {
            if (OnFinished != null) OnFinished.Invoke();
        }
    }


}
