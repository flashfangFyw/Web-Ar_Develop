using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ffDevelopmentSpace
{


public class TimerManager 
{
	private List<Timer> timerList = new List<Timer>();

	private static TimerManager instance;
	public static TimerManager GetInstance()
	{
		if (null == instance) {
			instance = new TimerManager ();
		}
		return instance;
	}

	public void addTimer(Timer timer)
	{
		if (timerList.Contains (timer))
			return;
		timerList.Add (timer);
	}

	public void Update(float timeElapsed)
	{
		int len = timerList.Count;
		Timer timer;
		for(int i = len - 1; i >= 0; i--) 
		{
			timer = timerList[i];
			if(!timer.isEnd())
			{
				timer.onTime(timeElapsed);
			}
			else 
			{
				timerList.RemoveAt(i);
			}
		}
	}
    }
}
