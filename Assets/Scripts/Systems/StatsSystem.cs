using UnityEngine;
using System.Collections;

public class StatsSystem : MonoBehaviour, ISystem {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Entity[] entities = this.GetMyEntityList();

		foreach (Entity e in entities)
		{
			StatComponent stat = (StatComponent)e.Components["Stat"];
			stat.MyText.text = stat.Value.ToString().Trim();
		}
	}

	public Entity[] GetMyEntityList()
	{
		return ComponentPool.Instance.GetEntitiesWithComponents("Stat");
	}
}
