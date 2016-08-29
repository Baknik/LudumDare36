using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class PlaceableComponent : MonoBehaviour, IComponent {

	// Unity-specific components are linked at runtime
	[Header("Linked Unity Components")]
	public MeshRenderer MyMeshRenderer;

	// Config fields are setup in the inspector and are the default values of state fields.
	// These are not changed during runtime
	[Header("Configuration Fields")]
	public int ConfigField0;

	// State fields are defaulted on startup, and then are read and changed by systems during runtime
	[Header("State Fields")]
	public bool AtValidLocation;

	// Use awake to link unity-specific components
	void Awake()
	{
		this.MyMeshRenderer = this.GetComponent<MeshRenderer>();
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
		return "Placeable";
	}

}
