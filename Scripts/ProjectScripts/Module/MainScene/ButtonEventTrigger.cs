using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEventTrigger : EventTrigger
{
	private RectTransform trans;
	private Vector3 initScale;

	private void Awake()
	{
		trans = GetComponent<RectTransform>();
		initScale = trans.localScale;
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		//放大10%
		trans.localScale = initScale * 1.1f;
		//trans.anchoredPosition = new Vector2 (trans.anchoredPosition.x, trans.anchoredPosition.y - 2);
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		//还原
		trans.localScale = initScale;
		//trans.anchoredPosition = new Vector2 (trans.anchoredPosition.x, trans.anchoredPosition.y + 2);
	}
}