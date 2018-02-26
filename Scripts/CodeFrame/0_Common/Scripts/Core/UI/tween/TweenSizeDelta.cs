using UnityEngine;
using System.Collections;

public class TweenSizeDelta : UITweener
{
    public RectTransform TarRectTransform;
    public Vector2 From;
    public Vector2 To;

    private bool isLoop;
    public bool IsLoop
    {
        get { return isLoop; }
        set
        {
            isLoop = value;
            if (isLoop) onEnd = ResetToBeginning;
            else onEnd = null;
        }
    }

    private bool isPingPang;
    public bool IsPingPang
    {
        get { return isPingPang; }
        set
        {
            isPingPang = value;
            if (isPingPang) onEnd = OnPingPang;
            else onEnd = null;
        }
    }

    protected Vector2 CacheTemp;

    protected override void OnUpdate(float factor, bool isFinished)
    {
        if (TarRectTransform != null)
        {
            TarRectTransform.sizeDelta = From * (1f - factor) + To * factor;
        }
        else
        {
            endFlag = true;
        }     
    }

    protected void OnPingPang()
    {
        CacheTemp = From;
        From = To;
        To = CacheTemp;

        ResetToBeginning();
    }
}
