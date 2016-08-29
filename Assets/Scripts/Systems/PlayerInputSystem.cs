using UnityEngine;
using System.Collections;

public class PlayerInputSystem : MonoBehaviour, ISystem {

	public delegate void PlayerInputEventDelegate(PlayerInputEventType eventType, int cost, Vector3 direction);
	public static event PlayerInputEventDelegate PlayerInputEvent;

	private bool cancelSubmitAxisInUse;

	// Use this for initialization
	void Start () {
		this.cancelSubmitAxisInUse = false;
	}
	
	// Update is called once per frame
	void Update () {
		float scrollDirection = Input.GetAxis("Mouse ScrollWheel");
		if (scrollDirection > 0.0f)
		{
			// scroll up
			PlayerInputEvent(PlayerInputEventType.ZOOM_IN, 0, Vector3.zero);
		}
		else if (scrollDirection < 0.0f)
		{
			// scroll down
			PlayerInputEvent(PlayerInputEventType.ZOOM_OUT, 0, Vector3.zero);
		}

		Vector3 panDirection = Vector3.zero;

		float horizontalDirection = Input.GetAxisRaw("Horizontal");
		if (horizontalDirection > 0.0f)
		{
			panDirection.x += 1.0f;
		}
		else if (horizontalDirection < 0.0f)
		{
			panDirection.x -= 1.0f;
		}

		float verticalDirection = Input.GetAxisRaw("Vertical");
		if (verticalDirection > 0.0f)
		{
			panDirection.z += 1.0f;
		}
		else if (verticalDirection < 0.0f)
		{
			panDirection.z -= 1.0f;
		}

		if (panDirection.magnitude > 0.0f)
		{
			PlayerInputEvent(PlayerInputEventType.CAMERA_PAN, 0, panDirection.normalized);
		}

		Entity[] uiButtonEntities = this.GetUIButtonEntities();
		foreach (Entity e in uiButtonEntities)
		{
			UIButtonComponent uiButton = (UIButtonComponent)e.Components["UI Button"];
			if (uiButton.PlayerClicked)
			{
				PlayerInputEvent(uiButton.InputEvent, uiButton.Cost, Vector3.zero);

				uiButton.PlayerClicked = false;

				this.cancelSubmitAxisInUse = true;
			}
		}

		float cancelAxis = Input.GetAxisRaw("Cancel");
		if (cancelAxis > 0.0f && !this.cancelSubmitAxisInUse)
		{
			PlayerInputEvent(PlayerInputEventType.CANCEL, 0, Vector3.zero);

			this.cancelSubmitAxisInUse = true;
		}
		else
		{
			this.cancelSubmitAxisInUse = false;
		}

		float submitAxis = Input.GetAxisRaw("Submit");
		if (submitAxis > 0.0f && !this.cancelSubmitAxisInUse)
		{
			PlayerInputEvent(PlayerInputEventType.SUBMIT, 0, Vector3.zero);

			this.cancelSubmitAxisInUse = true;
		}
		else
		{
			this.cancelSubmitAxisInUse = false;
		}
	}

	public Entity[] GetUIButtonEntities()
	{
		return ComponentPool.Instance.GetEntitiesWithComponents("UI Button");
	}
}
