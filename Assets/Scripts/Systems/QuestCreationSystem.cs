using UnityEngine;
using System.Collections;

public class QuestCreationSystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnEnable()
	{
		PlayerInputSystem.PlayerInputEvent += HandlePlayerInputEvent;
		InputModeStateBehavior.InputModeEntered += HandleInputModeEntered;
		InputModeStateBehavior.InputModeExited += HandleInputModeExited;
	}

	void OnDisable()
	{
		PlayerInputSystem.PlayerInputEvent -= HandlePlayerInputEvent;
		InputModeStateBehavior.InputModeEntered -= HandleInputModeEntered;
		InputModeStateBehavior.InputModeExited -= HandleInputModeExited;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public Entity GetMoneyStatEntity()
	{
		Entity[] entities = ComponentPool.Instance.GetEntitiesWithComponents("Stat");

		foreach (Entity e in entities)
		{
			StatComponent stat = (StatComponent)e.Components["Stat"];
			if (stat.Stat == StatType.MONEY)
			{
				return e;
			}
		}

		return null;
	}

	public Entity GetCursorActionTextEntity()
	{
		Entity[] entities = ComponentPool.Instance.GetEntitiesWithComponents("Cursor Action Text");

		if (entities.Length > 0)
		{
			return entities[0];
		}

		return null;
	}

	public void HandlePlayerInputEvent(PlayerInputEventType eventType, int cost, Vector3 direction)
	{
		
	}

	public void HandleInputModeEntered(InputMode inputMode)
	{
		
	}

	public void HandleInputModeExited(InputMode inputMode)
	{

	}
}
