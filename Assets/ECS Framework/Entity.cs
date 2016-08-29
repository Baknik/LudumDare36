using System;
using UnityEngine;
using System.Collections.Generic;

public class Entity
{
	public int ID;

	public Dictionary<string, IComponent> Components;

	public Entity(int id)
	{
		this.ID = id;
		this.Components = new Dictionary<string, IComponent>();
	}
}