using System;
using UnityEngine;
using System.Collections.Generic;

public class ComponentPool
{
	private static ComponentPool instance;

	public static ComponentPool Instance
	{
		get
		{
			if (ComponentPool.instance == null)
			{
				ComponentPool.instance = new ComponentPool();
			}
			return ComponentPool.instance;
		}

		private set
		{
			ComponentPool.instance = value;
		}
	}

	private ComponentPool ()
	{
		this.Pool = new Dictionary<int, Dictionary<string, IComponent>>();
	}

	private Dictionary<int, Dictionary<string, IComponent>> Pool;

	public void RegisterComponent(int entityID, IComponent component)
	{
		string componentName = component.GetName();

		// If no pool exists for this entity, create it
		if (!this.Pool.ContainsKey(entityID))
		{
			this.Pool.Add(entityID, new Dictionary<string, IComponent>());
		}

		// Enforce only one component of each type on any one entity
		if (!this.Pool[entityID].ContainsKey(componentName))
		{
			this.Pool[entityID].Add(componentName, component);
		}
	}

	public void UnregisterComponent(int entityID, IComponent component)
	{
		string componentName = component.GetName();

		if (this.Pool.ContainsKey(entityID) && this.Pool[entityID].ContainsKey(componentName))
		{
			this.Pool[entityID].Remove(componentName);
		}
	}

	public Entity[] GetEntitiesWithComponents(params string[] componentNames)
	{
		List<Entity> matchingEntities = new List<Entity>();

		foreach (int entityID in this.Pool.Keys)
		{
			Entity entity = new Entity(entityID);
			bool entityHasAllComponents = true;

			for (int i=0; i<componentNames.Length; i++)
			{
				string componentName = componentNames[0];

				if (this.Pool[entityID].ContainsKey(componentName))
				{
					entity.Components.Add(componentName, this.Pool[entityID][componentName]);
				}
				else
				{
					entityHasAllComponents = false;
				}
			}

			if (entityHasAllComponents)
			{
				matchingEntities.Add(entity);
			}
		}

		return matchingEntities.ToArray();
	}

}