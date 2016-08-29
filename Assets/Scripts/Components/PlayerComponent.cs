using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerComponent : MonoBehaviour, IComponent {

	// Unity-specific components are linked at runtime
	[Header("Linked Unity Components")]
	public NavMeshAgent MyNavMeshAgent;

	// Config fields are setup in the inspector and are the default values of state fields.
	// These are not changed during runtime
	[Header("Configuration Fields")]
	public string ConfigField0;

	// State fields are defaulted on startup, and then are read and changed by systems during runtime
	[Header("State Fields")]
	public Transform TargetArtifact;
	public Transform TargetNPC;
	public bool ArrivedAtNPC;
	public bool ArrivedAtArtifact;
	public Vector3 TargetPosition;

	// Use awake to link unity-specific components
	void Awake()
	{
		this.MyNavMeshAgent = this.GetComponent<NavMeshAgent>();
	}

	void Start()
	{
		this.ArrivedAtNPC = false;
		this.ArrivedAtNPC = false;

		this.TargetPosition = new Vector3(Random.Range(15, 35), 0.0f, Random.Range(15, 35));
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
		return "Player";
	}

	void OnTriggerEnter(Collider collider)
	{
//		if (collider.tag.Equals("NPC") && collider.transform == this.TargetNPC)
//		{
//			this.ArrivedAtNPC = true;
//		}
//		else if (collider.tag.Equals("Artifact") && collider.transform == this.TargetArtifact)
//		{
//			this.ArrivedAtArtifact = true;
//		}
	}

}
