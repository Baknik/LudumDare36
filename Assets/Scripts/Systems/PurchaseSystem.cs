using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class PurchaseSystem : MonoBehaviour, ISystem {

	public delegate void FunctionPurchasedDelegate(PlayerInputEventType eventType);
	public static event FunctionPurchasedDelegate FunctionPurchased;

	// Use this for initialization
	void Start () {
	
	}

	void OnEnable()
	{
		
	}

	void OnDisable()
	{
		
	}

	// Update is called once per frame
	void Update () {
	
	}

	public Entity GetMoneyStatEntity()
	{
		Entity[] entities = ComponentPool.Instance.GetEntitiesWithComponents("Stat");

		foreach (Entity e in entities)
		{
			StatComponent stat = (StatComponent)e.Components["Stat"];
			if (stat.Stat == StatType.MONEY)
			{
				return e;
			}
		}

		return null;
	}

	public void HandleButtonInput(PlayerInputEventType eventType, int cost)
	{
		switch (eventType)
		{
			case PlayerInputEventType.NEW_NPC:
				Entity moneyStat = this.GetMoneyStatEntity();
				Assert.IsNotNull(moneyStat);
				StatComponent stat = (StatComponent)moneyStat.Components["Stat"];
				if (stat.Value >= cost)
				{
					stat.Value -= cost;

					FunctionPurchased(eventType);
				}
				break;
		}
	}
}
