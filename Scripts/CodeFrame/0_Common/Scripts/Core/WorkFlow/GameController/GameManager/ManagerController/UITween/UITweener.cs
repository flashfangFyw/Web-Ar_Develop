using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class UITweener
{
	public enum Method
	{
		Linear,
		EaseIn, EaseOut, EaseInOut, BounceIn, BounceOut,
		QuadEaseOut, QuadEaseIn, QuadEaseInOut, QuadEaseOutIn,
		ExpoEaseOut, ExpoEaseIn, ExpoEaseInOut, ExpoEaseOutIn,
		CubicEaseOut, CubicEaseIn, CubicEaseInOut, CubicEaseOutIn,
		QuartEaseOut, QuartEaseIn, QuartEaseInOut, QuartEaseOutIn,
		QuintEaseOut, QuintEaseIn, QuintEaseInOut, QuintEaseOutIn,
		CircEaseOut, CircEaseIn, CircEaseInOut, CircEaseOutIn,
		SineEaseOut, SineEaseIn, SineEaseInOut, SineEaseOutIn,
		ElasticEaseOut, ElasticEaseIn, ElasticEaseInOut, ElasticEaseOutIn,
		BounceEaseOut, BounceEaseIn, BounceEaseInOut, BounceEaseOutIn,
		BackEaseOut, BackEaseIn, BackEaseInOut, BackEaseOutIn
	}

//	[HideInInspector]
	public delegate void handle();
	public Method method = Method.Linear;
//	public bool ignoreTimeScale = true;
	public bool endFlag = false;
	public float delay = 0f;
	public float duration = 1f;

	public handle onEnd;

	bool steeperCurves = false;
	bool mStarted = false;
	float mStartTime = 0f;
	float mDuration = 0f;
	float mAmountPerDelta = 1000f;
	float mFactor = 0f;
//	AnimationCurve animationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

	public float amountPerDelta
	{
		get
		{
			if (mDuration != duration)
			{
				mDuration = duration;
				mAmountPerDelta = Mathf.Abs((duration > 0f) ? 1f / duration : 1000f) * Mathf.Sign(mAmountPerDelta);
			}
			return mAmountPerDelta;
		}
	}

	public void Update ()
	{
		if (endFlag)
			return;
		float delta = Time.deltaTime;
		float time = Time.time;

		if (!mStarted)
		{
			mStarted = true;
			mStartTime = time + delay;
		}

		if (time < mStartTime) return;

		// Advance the sampling factor
		mFactor += amountPerDelta * delta;

		// If the factor goes out of range and this is a one-time tweening operation, disable the script
		if (duration == 0f || mFactor > 1f || mFactor < 0f)
		{
			mFactor = Mathf.Clamp01(mFactor);
			endFlag = true;
			if(null != onEnd) onEnd();
		}
		Sample(mFactor, endFlag);
	}

	public void OnDisable () { mStarted = false; }

	public void Sample (float factor, bool isFinished)
	{
		// Calculate the sampling value
		float val = Mathf.Clamp01(factor);
		if(method == Method.Linear) {
//			val = Mathf.Clamp01(factor);
		} else if (method == Method.EaseIn) {
			val = 1f - Mathf.Sin (0.5f * Mathf.PI * (1f - val));
			if (steeperCurves)
				val *= val;
		} else if (method == Method.EaseOut) {
			val = Mathf.Sin (0.5f * Mathf.PI * val);

			if (steeperCurves) {
				val = 1f - val;
				val = 1f - val * val;
			}
		} else if (method == Method.EaseInOut) {
			const float pi2 = Mathf.PI * 2f;
			val = val - Mathf.Sin (val * pi2) / pi2;

			if (steeperCurves) {
				val = val * 2f - 1f;
				float sign = Mathf.Sign (val);
				val = 1f - Mathf.Abs (val);
				val = 1f - val * val;
				val = sign * val * 0.5f + 0.5f;
			}
		} else if (method == Method.BounceIn) {
			val = BounceLogic (val);
		} else if (method == Method.BounceOut) {
			val = 1f - BounceLogic (1f - val);
		} else {
			val = EaseFunction.Instance.GetEaseProgress(method, val);
		}
		// Call the virtual update
		OnUpdate(val, isFinished);
//		OnUpdate((animationCurve != null) ? animationCurve.Evaluate(val) : val, isFinished);
	}

	/// <summary>
	/// Main Bounce logic to simplify the Sample function
	/// </summary>
	
	float BounceLogic (float val)
	{
		if (val < 0.363636f) // 0.363636 = (1/ 2.75)
		{
			val = 7.5685f * val * val;
		}
		else if (val < 0.727272f) // 0.727272 = (2 / 2.75)
		{
			val = 7.5625f * (val -= 0.545454f) * val + 0.75f; // 0.545454f = (1.5 / 2.75) 
		}
		else if (val < 0.909090f) // 0.909090 = (2.5 / 2.75) 
		{
			val = 7.5625f * (val -= 0.818181f) * val + 0.9375f; // 0.818181 = (2.25 / 2.75) 
		}
		else
		{
			val = 7.5625f * (val -= 0.9545454f) * val + 0.984375f; // 0.9545454 = (2.625 / 2.75) 
		}
		return val;
	}

	public void ResetToBeginning ()
	{
		mStarted = false;
		endFlag = false;
		mFactor = (amountPerDelta < 0f) ? 1f : 0f;
//		Sample(mFactor, false);
	}

	abstract protected void OnUpdate (float factor, bool isFinished);
}
