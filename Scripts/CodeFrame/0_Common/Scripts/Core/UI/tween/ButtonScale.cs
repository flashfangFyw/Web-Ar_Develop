using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using ffDevelopmentSpace;

public class ButtonScale : UIBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Transform tweenTarget; 
    public Vector3 Normal = new Vector3(1, 1, 1);    
    [NonSerialized]
    public Vector3 Pressed = new Vector3(1.1f, 1.1f, 1.1f);
    public float duration = 0.1f;
    private TweenScale ts;

    protected override void OnEnable()
    {
        if (!tweenTarget) tweenTarget = transform;
    }
    protected override void OnDisable()
    {
        tweenTarget.localScale = Normal;        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (ts == null) ts = new TweenScale();
        ts.from = Normal;
        ts.to = Pressed;
        ts.duration = duration;
        ts.Transform = tweenTarget;
        SingletonMB<UITweenManagerController>.GetInstance().AddTweener(ts);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (ts == null) ts = new TweenScale();
        ts.from = Pressed;
        ts.to = Normal;
        ts.duration = duration;
        ts.Transform = tweenTarget;
        UITweenManagerController.GetInstance().AddTweener(ts);
    }

}
