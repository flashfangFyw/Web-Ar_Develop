using UnityEngine;
using System.Collections;

public class TweenPosition : UITweener {

	public RectTransform trans;
	public Vector2 from;
	public Vector2 to;

	protected override void OnUpdate (float factor, bool isFinished) 
	{
	    if(trans)	trans.anchoredPosition = from * (1f - factor) + to * factor;
	}
}
