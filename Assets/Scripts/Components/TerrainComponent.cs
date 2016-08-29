using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Terrain))]
public class TerrainComponent : MonoBehaviour, IComponent {

	// Unity-specific components are linked at runtime
	[Header("Linked Unity Components")]
	public Terrain MyTerrain;
	public TerrainCollider MyTerrainCollider;

	// Config fields are setup in the inspector and are the default values of state fields.
	// These are not changed during runtime
	[Header("Configuration Fields")]
	public LayerMask TerrainRaycastLayer;

	// State fields are defaulted on startup, and then are read and changed by systems during runtime
	[Header("State Fields")]
	public int StateField0;

	// Use awake to link unity-specific components
	void Awake()
	{
		this.MyTerrain = this.GetComponent<Terrain>();
		this.MyTerrainCollider = this.GetComponent<TerrainCollider>();
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
		return "Terrain";
	}

}
