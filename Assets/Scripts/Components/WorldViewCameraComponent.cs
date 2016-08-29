using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class WorldViewCameraComponent : MonoBehaviour, IComponent {

	// Unity-specific components are linked at runtime
	[Header("Linked Unity Components")]
	public Camera MyCamera;

	// Config fields are setup in the inspector and are the default values of state fields.
	// These are not changed during runtime
	[Header("Configuration Fields")]
	public float ZoomIncrement;
	public float StartZoomDistance;
	public float MinZoomDistance;
	public float MaxZoomDistance;
	public float PanIncrement;
	public float PanSpeed;
	public float ZoomSpeed;
	public Vector3 MinLookAtPosition;
	public Vector3 MaxLookAtPosition; 
	public Vector3 StartLookAtPosition;
	public Vector3 OffsetDirection;

	// State fields are defaulted on startup, and then are read and changed by systems during runtime
	[Header("State Fields")]
	public float ZoomDistance;
	public float TargetZoomDistance;
	public Vector3 LookAtPosition;
	public Vector3 TargetLookAtPosition;

	// Use awake to link unity-specific components
	void Awake()
	{
		this.MyCamera = this.GetComponent<Camera>();
	}

	void Start()
	{
		this.ZoomDistance = this.StartZoomDistance;
		this.TargetZoomDistance = this.StartZoomDistance;
		this.LookAtPosition = this.StartLookAtPosition;
		this.TargetLookAtPosition = this.StartLookAtPosition;
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
		return "World View Camera";
	}

}

