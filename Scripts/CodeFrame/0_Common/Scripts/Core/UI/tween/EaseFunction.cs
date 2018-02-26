using UnityEngine;
using System.Collections;

public class EaseFunction
{
	private EaseFunction() { }
	public static readonly EaseFunction Instance = new EaseFunction();
	public float GetEaseProgress(UITweener.Method ease_type, float linear_progress)
	{
		switch (ease_type)
		{
		case UITweener.Method.Linear:
			return linear_progress;
		case UITweener.Method.BackEaseIn:
			return BackEaseIn(linear_progress, 0, 1, 1);
			
		case UITweener.Method.BackEaseInOut:
			return BackEaseInOut(linear_progress, 0, 1, 1);
		case UITweener.Method.BackEaseOut:
			return BackEaseOut(linear_progress, 0, 1, 1);
		case UITweener.Method.BackEaseOutIn:
			return BackEaseOutIn(linear_progress, 0, 1, 1);
		case UITweener.Method.BounceEaseIn:
			return BounceEaseIn(linear_progress, 0, 1, 1);
		case UITweener.Method.BounceEaseInOut:
			return BounceEaseInOut(linear_progress, 0, 1, 1);
		case UITweener.Method.BounceEaseOut:
			return BounceEaseOut(linear_progress, 0, 1, 1);
		case UITweener.Method.BounceEaseOutIn:
			return BounceEaseOutIn(linear_progress, 0, 1, 1);
		case UITweener.Method.CircEaseIn:
			return CircEaseIn(linear_progress, 0, 1, 1);
		case UITweener.Method.CircEaseInOut:
			return CircEaseInOut(linear_progress, 0, 1, 1);
		case UITweener.Method.CircEaseOut:
			return CircEaseOut(linear_progress, 0, 1, 1);
		case UITweener.Method.CircEaseOutIn:
			return CircEaseOutIn(linear_progress, 0, 1, 1);
		case UITweener.Method.CubicEaseIn:
			return CubicEaseIn(linear_progress, 0, 1, 1);
		case UITweener.Method.CubicEaseInOut:
			return CubicEaseInOut(linear_progress, 0, 1, 1);
		case UITweener.Method.CubicEaseOut:
			return CubicEaseOut(linear_progress, 0, 1, 1);
		case UITweener.Method.CubicEaseOutIn:
			return CubicEaseOutIn(linear_progress, 0, 1, 1);
		case UITweener.Method.ElasticEaseIn:
			return ElasticEaseIn(linear_progress, 0, 1, 1);
			
		case UITweener.Method.ElasticEaseInOut:
			return ElasticEaseInOut(linear_progress, 0, 1, 1);
		case UITweener.Method.ElasticEaseOut:
			return ElasticEaseOut(linear_progress, 0, 1, 1);
		case UITweener.Method.ElasticEaseOutIn:
			return ElasticEaseOutIn(linear_progress, 0, 1, 1);
		case UITweener.Method.ExpoEaseIn:
			return ExpoEaseIn(linear_progress, 0, 1, 1);
		case UITweener.Method.ExpoEaseInOut:
			return ExpoEaseInOut(linear_progress, 0, 1, 1);
		case UITweener.Method.ExpoEaseOut:
			return ExpoEaseOut(linear_progress, 0, 1, 1);
		case UITweener.Method.ExpoEaseOutIn:
			return ExpoEaseOutIn(linear_progress, 0, 1, 1);
		case UITweener.Method.QuadEaseIn:
			return QuadEaseIn(linear_progress, 0, 1, 1);
		case UITweener.Method.QuadEaseInOut:
			return QuadEaseInOut(linear_progress, 0, 1, 1);
		case UITweener.Method.QuadEaseOut:
			return QuadEaseOut(linear_progress, 0, 1, 1);
		case UITweener.Method.QuadEaseOutIn:
			return QuadEaseOutIn(linear_progress, 0, 1, 1);
		case UITweener.Method.QuartEaseIn:
			return QuartEaseIn(linear_progress, 0, 1, 1);
		case UITweener.Method.QuartEaseInOut:
			return QuartEaseInOut(linear_progress, 0, 1, 1);
		case UITweener.Method.QuartEaseOut:
			return QuartEaseOut(linear_progress, 0, 1, 1);
		case UITweener.Method.QuartEaseOutIn:
			return QuartEaseOutIn(linear_progress, 0, 1, 1);
		case UITweener.Method.QuintEaseIn:
			return QuintEaseIn(linear_progress, 0, 1, 1);
		case UITweener.Method.QuintEaseInOut:
			return QuintEaseInOut(linear_progress, 0, 1, 1);
		case UITweener.Method.QuintEaseOut:
			return QuintEaseOut(linear_progress, 0, 1, 1);
		case UITweener.Method.QuintEaseOutIn:
			return QuintEaseOutIn(linear_progress, 0, 1, 1);
			
		case UITweener.Method.SineEaseIn:
			return SineEaseIn(linear_progress, 0, 1, 1);
		case UITweener.Method.SineEaseInOut:
			return SineEaseInOut(linear_progress, 0, 1, 1);
		case UITweener.Method.SineEaseOut:
			return SineEaseOut(linear_progress, 0, 1, 1);
		case UITweener.Method.SineEaseOutIn:
			return SineEaseOutIn(linear_progress, 0, 1, 1);
			
		default:
			return linear_progress;
		}
	}
	
	/* EASING FUNCTIONS */
	
	#region Linear
	
	/// <summary>
	/// Easing equation function for a simple linear tweening, with no easing.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float Linear(float t, float b, float c, float d)
	{
		return c * t / d + b;
	}
	
	#endregion
	
	#region Expo
	
	/// <summary>
	/// Easing equation function for an exponential (2^t) easing out: 
	/// decelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float ExpoEaseOut(float t, float b, float c, float d)
	{
		return (t == d) ? b + c : c * (-Mathf.Pow(2, -10 * t / d) + 1) + b;
	}
	
	/// <summary>
	/// Easing equation function for an exponential (2^t) easing in: 
	/// accelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float ExpoEaseIn(float t, float b, float c, float d)
	{
		return (t == 0) ? b : c * Mathf.Pow(2, 10 * (t / d - 1)) + b;
	}
	
	/// <summary>
	/// Easing equation function for an exponential (2^t) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float ExpoEaseInOut(float t, float b, float c, float d)
	{
		if (t == 0)
			return b;
		
		if (t == d)
			return b + c;
		
		if ((t /= d / 2) < 1)
			return c / 2 * Mathf.Pow(2, 10 * (t - 1)) + b;
		
		return c / 2 * (-Mathf.Pow(2, -10 * --t) + 2) + b;
	}
	
	/// <summary>
	/// Easing equation function for an exponential (2^t) easing out/in: 
	/// deceleration until halfway, then acceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float ExpoEaseOutIn(float t, float b, float c, float d)
	{
		if (t < d / 2)
			return ExpoEaseOut(t * 2, b, c / 2, d);
		
		return ExpoEaseIn((t * 2) - d, b + c / 2, c / 2, d);
	}
	
	#endregion
	
	#region Circular
	
	/// <summary>
	/// Easing equation function for a circular (sqrt(1-t^2)) easing out: 
	/// decelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float CircEaseOut(float t, float b, float c, float d)
	{
		return c * Mathf.Sqrt(1 - (t = t / d - 1) * t) + b;
	}
	
	/// <summary>
	/// Easing equation function for a circular (sqrt(1-t^2)) easing in: 
	/// accelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float CircEaseIn(float t, float b, float c, float d)
	{
		return -c * (Mathf.Sqrt(1 - (t /= d) * t) - 1) + b;
	}
	
	/// <summary>
	/// Easing equation function for a circular (sqrt(1-t^2)) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float CircEaseInOut(float t, float b, float c, float d)
	{
		if ((t /= d / 2) < 1)
			return -c / 2 * (Mathf.Sqrt(1 - t * t) - 1) + b;
		
		return c / 2 * (Mathf.Sqrt(1 - (t -= 2) * t) + 1) + b;
	}
	
	/// <summary>
	/// Easing equation function for a circular (sqrt(1-t^2)) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float CircEaseOutIn(float t, float b, float c, float d)
	{
		if (t < d / 2)
			return CircEaseOut(t * 2, b, c / 2, d);
		
		return CircEaseIn((t * 2) - d, b + c / 2, c / 2, d);
	}
	
	#endregion
	
	#region Quad
	
	/// <summary>
	/// Easing equation function for a quadratic (t^2) easing out: 
	/// decelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float QuadEaseOut(float t, float b, float c, float d)
	{
		return -c * (t /= d) * (t - 2) + b;
	}
	
	/// <summary>
	/// Easing equation function for a quadratic (t^2) easing in: 
	/// accelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float QuadEaseIn(float t, float b, float c, float d)
	{
		return c * (t /= d) * t + b;
	}
	
	/// <summary>
	/// Easing equation function for a quadratic (t^2) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float QuadEaseInOut(float t, float b, float c, float d)
	{
		if ((t /= d / 2) < 1)
			return c / 2 * t * t + b;
		
		return -c / 2 * ((--t) * (t - 2) - 1) + b;
	}
	
	/// <summary>
	/// Easing equation function for a quadratic (t^2) easing out/in: 
	/// deceleration until halfway, then acceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float QuadEaseOutIn(float t, float b, float c, float d)
	{
		if (t < d / 2)
			return QuadEaseOut(t * 2, b, c / 2, d);
		
		return QuadEaseIn((t * 2) - d, b + c / 2, c / 2, d);
	}
	
	#endregion
	
	#region Sine
	
	/// <summary>
	/// Easing equation function for a sinusoidal (sin(t)) easing out: 
	/// decelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float SineEaseOut(float t, float b, float c, float d)
	{
		return c * Mathf.Sin(t / d * (Mathf.PI / 2)) + b;
	}
	
	/// <summary>
	/// Easing equation function for a sinusoidal (sin(t)) easing in: 
	/// accelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float SineEaseIn(float t, float b, float c, float d)
	{
		return -c * Mathf.Cos(t / d * (Mathf.PI / 2)) + c + b;
	}
	
	/// <summary>
	/// Easing equation function for a sinusoidal (sin(t)) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float SineEaseInOut(float t, float b, float c, float d)
	{
		if ((t /= d / 2) < 1)
			return c / 2 * (Mathf.Sin(Mathf.PI * t / 2)) + b;
		
		return -c / 2 * (Mathf.Cos(Mathf.PI * --t / 2) - 2) + b;
	}
	
	/// <summary>
	/// Easing equation function for a sinusoidal (sin(t)) easing in/out: 
	/// deceleration until halfway, then acceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float SineEaseOutIn(float t, float b, float c, float d)
	{
		if (t < d / 2)
			return SineEaseOut(t * 2, b, c / 2, d);
		
		return SineEaseIn((t * 2) - d, b + c / 2, c / 2, d);
	}
	
	#endregion
	
	#region Cubic
	
	/// <summary>
	/// Easing equation function for a cubic (t^3) easing out: 
	/// decelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float CubicEaseOut(float t, float b, float c, float d)
	{
		return c * ((t = t / d - 1) * t * t + 1) + b;
	}
	
	/// <summary>
	/// Easing equation function for a cubic (t^3) easing in: 
	/// accelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float CubicEaseIn(float t, float b, float c, float d)
	{
		return c * (t /= d) * t * t + b;
	}
	
	/// <summary>
	/// Easing equation function for a cubic (t^3) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float CubicEaseInOut(float t, float b, float c, float d)
	{
		if ((t /= d / 2) < 1)
			return c / 2 * t * t * t + b;
		
		return c / 2 * ((t -= 2) * t * t + 2) + b;
	}
	
	/// <summary>
	/// Easing equation function for a cubic (t^3) easing out/in: 
	/// deceleration until halfway, then acceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float CubicEaseOutIn(float t, float b, float c, float d)
	{
		if (t < d / 2)
			return CubicEaseOut(t * 2, b, c / 2, d);
		
		return CubicEaseIn((t * 2) - d, b + c / 2, c / 2, d);
	}
	
	#endregion
	
	#region Quartic
	
	/// <summary>
	/// Easing equation function for a quartic (t^4) easing out: 
	/// decelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float QuartEaseOut(float t, float b, float c, float d)
	{
		return -c * ((t = t / d - 1) * t * t * t - 1) + b;
	}
	
	/// <summary>
	/// Easing equation function for a quartic (t^4) easing in: 
	/// accelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float QuartEaseIn(float t, float b, float c, float d)
	{
		return c * (t /= d) * t * t * t + b;
	}
	
	/// <summary>
	/// Easing equation function for a quartic (t^4) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float QuartEaseInOut(float t, float b, float c, float d)
	{
		if ((t /= d / 2) < 1)
			return c / 2 * t * t * t * t + b;
		
		return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
	}
	
	/// <summary>
	/// Easing equation function for a quartic (t^4) easing out/in: 
	/// deceleration until halfway, then acceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float QuartEaseOutIn(float t, float b, float c, float d)
	{
		if (t < d / 2)
			return QuartEaseOut(t * 2, b, c / 2, d);
		
		return QuartEaseIn((t * 2) - d, b + c / 2, c / 2, d);
	}
	
	#endregion
	
	#region Quintic
	
	/// <summary>
	/// Easing equation function for a quintic (t^5) easing out: 
	/// decelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float QuintEaseOut(float t, float b, float c, float d)
	{
		return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
	}
	
	/// <summary>
	/// Easing equation function for a quintic (t^5) easing in: 
	/// accelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float QuintEaseIn(float t, float b, float c, float d)
	{
		return c * (t /= d) * t * t * t * t + b;
	}
	
	/// <summary>
	/// Easing equation function for a quintic (t^5) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float QuintEaseInOut(float t, float b, float c, float d)
	{
		if ((t /= d / 2) < 1)
			return c / 2 * t * t * t * t * t + b;
		return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
	}
	
	/// <summary>
	/// Easing equation function for a quintic (t^5) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float QuintEaseOutIn(float t, float b, float c, float d)
	{
		if (t < d / 2)
			return QuintEaseOut(t * 2, b, c / 2, d);
		return QuintEaseIn((t * 2) - d, b + c / 2, c / 2, d);
	}
	
	#endregion
	
	#region Elastic
	
	/// <summary>
	/// Easing equation function for an elastic (exponentially decaying sine wave) easing out: 
	/// decelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float ElasticEaseOut(float t, float b, float c, float d)
	{
		if ((t /= d) == 1)
			return b + c;
		
		float p = d * 0.3f;
		float s = p / 4;
		
		return (c * Mathf.Pow(2, -10 * t) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) + c + b);
	}
	
	/// <summary>
	/// Easing equation function for an elastic (exponentially decaying sine wave) easing in: 
	/// accelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float ElasticEaseIn(float t, float b, float c, float d)
	{
		if ((t /= d) == 1)
			return b + c;
		
		float p = d * 0.3f;
		float s = p / 4;
		
		return -(c * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;
	}
	
	/// <summary>
	/// Easing equation function for an elastic (exponentially decaying sine wave) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float ElasticEaseInOut(float t, float b, float c, float d)
	{
		if ((t /= d / 2f) == 2)
			return b + c;
		
		float p = d * (0.3f * 1.5f);
		float s = p / 4;
		
		if (t < 1)
			return -0.5f * (c * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;
		return c * Mathf.Pow(2, -10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) * 0.5f + c + b;
	}
	
	/// <summary>
	/// Easing equation function for an elastic (exponentially decaying sine wave) easing out/in: 
	/// deceleration until halfway, then acceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float ElasticEaseOutIn(float t, float b, float c, float d)
	{
		if (t < d / 2)
			return ElasticEaseOut(t * 2, b, c / 2, d);
		return ElasticEaseIn((t * 2) - d, b + c / 2, c / 2, d);
	}
	
	#endregion
	
	#region Bounce
	
	/// <summary>
	/// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out: 
	/// decelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float BounceEaseOut(float t, float b, float c, float d)
	{
		if ((t /= d) < (1 / 2.75f))
			return c * (7.5625f * t * t) + b;
		else if (t < (2 / 2.75f))
			return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f) + b;
		else if (t < (2.5f / 2.75f))
			return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f) + b;
		else
			return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
	}
	
	/// <summary>
	/// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in: 
	/// accelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float BounceEaseIn(float t, float b, float c, float d)
	{
		return c - BounceEaseOut(d - t, 0, c, d) + b;
	}
	
	/// <summary>
	/// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float BounceEaseInOut(float t, float b, float c, float d)
	{
		if (t < d / 2)
			return BounceEaseIn(t * 2, 0, c, d) * 0.5f + b;
		else
			return BounceEaseOut(t * 2 - d, 0, c, d) * 0.5f + c * 0.5f + b;
	}
	
	/// <summary>
	/// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out/in: 
	/// deceleration until halfway, then acceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float BounceEaseOutIn(float t, float b, float c, float d)
	{
		if (t < d / 2)
			return BounceEaseOut(t * 2, b, c / 2, d);
		return BounceEaseIn((t * 2) - d, b + c / 2, c / 2, d);
	}
	
	#endregion
	
	#region Back
	
	/// <summary>
	/// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out: 
	/// decelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float BackEaseOut(float t, float b, float c, float d)
	{
		return c * ((t = t / d - 1) * t * ((1.70158f + 1) * t + 1.70158f) + 1) + b;
	}
	
	/// <summary>
	/// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in: 
	/// accelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float BackEaseIn(float t, float b, float c, float d)
	{
		return c * (t /= d) * t * ((1.70158f + 1) * t - 1.70158f) + b;
	}
	
	/// <summary>
	/// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float BackEaseInOut(float t, float b, float c, float d)
	{
		float s = 1.70158f;
		if ((t /= d / 2) < 1)
			return c / 2 * (t * t * (((s *= (1.525f)) + 1) * t - s)) + b;
		return c / 2 * ((t -= 2) * t * (((s *= (1.525f)) + 1) * t + s) + 2) + b;
	}
	
	/// <summary>
	/// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out/in: 
	/// deceleration until halfway, then acceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public float BackEaseOutIn(float t, float b, float c, float d)
	{
		if (t < d / 2)
			return BackEaseOut(t * 2, b, c / 2, d);
		return BackEaseIn((t * 2) - d, b + c / 2, c / 2, d);
	}
	
	#endregion
}
