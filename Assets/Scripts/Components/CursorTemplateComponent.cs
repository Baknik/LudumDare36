using UnityEngine;
using System.Collections;
using System;

public class CursorTemplateComponent : MonoBehaviour, IComponent {

	// Unity-specific components are linked at runtime
	[Header("Linked Unity Components")]
	public MeshRenderer MyMeshRenderer;

	// Config fields are setup in the inspector and are the default values of state fields.
	// These are not changed during runtime
	[Header("Configuration Fields")]
	public Color ValidColor;
	public Color InvalidColor;
	public bool OnlyCollisionsValid;
	public string[] CollideWithTags;

	// State fields are defaulted on startup, and then are read and changed by systems during runtime
	[Header("State Fields")]
	public bool AtValidLocation;
	public bool CollidingWithWorld;
	public int Cost;
	public Transform CollisionObject;

	// Use awake to link unity-specific components
	void Awake()
	{
		this.MyMeshRenderer = this.GetComponentInChildren<MeshRenderer>();
	}

	void Start()
	{
		this.CollidingWithWorld = false;
	}

	void OnEnable()
	{
		// register this component for this entity
		ComponentPool.Instance.RegisterComponent(GetInstanceID(), this);
	}

	void OnDisable()
	{
		// unregister this component for this entity
		ComponentPool.Instance.UnregisterComponent(GetInstanceID(), this);
	}

	public string GetName()
	{
		return "Cursor Template";
	}

	void OnTriggerStay(Collider collider)
	{
		if (Array.Exists(this.CollideWithTags, t => t.Equals(collider.tag)))
		{
			this.CollisionObject = collider.transform;

			this.CollidingWithWorld = true;
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (Array.Exists(this.CollideWithTags, t => t.Equals(collider.tag)))
		{
			this.CollidingWithWorld = false;

			this.CollisionObject = null;
		}
	}

}
