using UnityEngine;
using System.Collections;

public class InputModeStateMachine : MonoBehaviour {

	private Animator myAnimator;

	void Awake()
	{
		this.myAnimator = this.GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
		
	}

	void OnEnable()
	{
		PlayerInputSystem.PlayerInputEvent += HandlePlayerInputEvent;
	}

	void OnDisable()
	{
		PlayerInputSystem.PlayerInputEvent -= HandlePlayerInputEvent;
	}

	// Update is called once per frame
	void Update () {
		this.myAnimator.ResetTrigger(PlayerInputEventType.NEW_NPC.ToString());
		this.myAnimator.ResetTrigger(PlayerInputEventType.NEW_ARTIFACT.ToString());
		this.myAnimator.ResetTrigger(PlayerInputEventType.NEW_QUEST.ToString());
		this.myAnimator.ResetTrigger(PlayerInputEventType.CANCEL.ToString());
		this.myAnimator.ResetTrigger(PlayerInputEventType.SUBMIT.ToString());
	}

	public void HandlePlayerInputEvent(PlayerInputEventType eventType, int cost, Vector3 direction)
	{
		switch (eventType)
		{
			case PlayerInputEventType.NEW_NPC:
				this.myAnimator.SetTrigger(eventType.ToString());
				break;
			case PlayerInputEventType.NEW_ARTIFACT:
				this.myAnimator.SetTrigger(eventType.ToString());
				break;
			case PlayerInputEventType.NEW_QUEST:
				this.myAnimator.SetTrigger(eventType.ToString());
				break;
			case PlayerInputEventType.CANCEL:
				this.myAnimator.SetTrigger(eventType.ToString());
				break;
			case PlayerInputEventType.SUBMIT:
				this.myAnimator.SetTrigger(eventType.ToString());
				break;
		}
	}
}
