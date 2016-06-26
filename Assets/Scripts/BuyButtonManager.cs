using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BuyButtonManager : MonoBehaviour,IPointerClickHandler
{
	/// <summary>
	/// 道具价格
	/// </summary>
	public float price;
	/// <summary>
	/// 道具名称
	/// </summary>
	public string goodsName;

	#region IPointerClickHandler 成员

	public void OnPointerClick(PointerEventData eventData)
	{
		BmobPayManager.Instance.OnBuyButtonClick(price,goodsName);
	}

	#endregion
}
