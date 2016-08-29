using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CursorActionTextComponent : MonoBehaviour, IComponent {

	// Unity-specific components are linked at runtime
	[Header("Linked Unity Components")]
	public Text MyText;

	// Config fields are setup in the inspector and are the default values of state fields.
	// These are not changed during runtime
	[Header("Configuration Fields")]
	public string SelectNPCText;
	public string SelectArtifactText;

	// State fields are defaulted on startup, and then are read and changed by systems during runtime
	[Header("State Fields")]
	public bool StateField0;

	// Use awake to link unity-specific components
	void Awake()
	{
		this.MyText = this.GetComponent<Text>();
	}

	void Start()
	{
		
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
		return "Cursor Action Text";
	}

}

