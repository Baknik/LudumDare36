using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonComponent : MonoBehaviour, IComponent {

	// Unity-specific components are linked at runtime
	[Header("Linked Unity Components")]
	public Button MyButton;

	// Config fields are setup in the inspector and are the default values of state fields.
	// These are not changed during runtime
	[Header("Configuration Fields")]
	public PlayerInputEventType InputEvent;
	public int StartCost;

	// State fields are defaulted on startup, and then are read and changed by systems during runtime
	[Header("State Fields")]
	public bool PlayerClicked;
	public int Cost;

	// Use awake to link unity-specific components
	void Awake()
	{
		this.MyButton = this.GetComponent<Button>();
	}

	void Start()
	{
		this.PlayerClicked = false;
		this.Cost = this.StartCost;
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
		return "UI Button";
	}

	public void OnClick()
	{
		this.PlayerClicked = true;
	}
}
