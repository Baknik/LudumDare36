using UnityEngine;
using System.Collections;

public class CameraControlSystem : MonoBehaviour, ISystem {

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
		Entity cameraEntity = this.GetCameraEntity();
		if (cameraEntity != null)
		{
			WorldViewCameraComponent camera = (WorldViewCameraComponent) cameraEntity.Components["World View Camera"];
			camera.LookAtPosition = Vector3.Lerp(camera.LookAtPosition, camera.TargetLookAtPosition, camera.PanSpeed * Time.deltaTime);
			camera.ZoomDistance = Mathf.Lerp(camera.ZoomDistance, camera.TargetZoomDistance, camera.ZoomSpeed * Time.deltaTime);
			camera.transform.position = camera.LookAtPosition + (camera.OffsetDirection.normalized * camera.ZoomDistance);
		}
	}

	public Entity GetCameraEntity()
	{
		Entity[] entities = ComponentPool.Instance.GetEntitiesWithComponents("World View Camera");

		if (entities.Length > 0)
		{
			return entities[0];
		}

		return null;
	}

	public void HandlePlayerInputEvent(PlayerInputEventType eventType, int cost, Vector3 direction)
	{
		switch (eventType)
		{
			case PlayerInputEventType.ZOOM_IN:
				Entity cameraEntity = this.GetCameraEntity();
				if (cameraEntity != null)
				{
					WorldViewCameraComponent camera = (WorldViewCameraComponent) cameraEntity.Components["World View Camera"];
					camera.TargetZoomDistance = Mathf.Clamp(camera.ZoomDistance - camera.ZoomIncrement, camera.MinZoomDistance, camera.MaxZoomDistance);
				}
				break;
			case PlayerInputEventType.ZOOM_OUT:
				cameraEntity = this.GetCameraEntity();
				if (cameraEntity != null)
				{
					WorldViewCameraComponent camera = (WorldViewCameraComponent) cameraEntity.Components["World View Camera"];
					camera.TargetZoomDistance = Mathf.Clamp(camera.ZoomDistance + camera.ZoomIncrement, camera.MinZoomDistance, camera.MaxZoomDistance);
				}
				break;
			case PlayerInputEventType.CAMERA_PAN:
				cameraEntity = this.GetCameraEntity();
				if (cameraEntity != null)
				{
					WorldViewCameraComponent camera = (WorldViewCameraComponent) cameraEntity.Components["World View Camera"];
					camera.TargetLookAtPosition = new Vector3(
						Mathf.Clamp(camera.LookAtPosition.x + (direction.x * camera.PanIncrement * (camera.ZoomDistance / camera.MaxZoomDistance)), camera.MinLookAtPosition.x, camera.MaxLookAtPosition.x),
						0.0f,
						Mathf.Clamp(camera.LookAtPosition.z + (direction.z * camera.PanIncrement * (camera.ZoomDistance / camera.MaxZoomDistance)), camera.MinLookAtPosition.z, camera.MaxLookAtPosition.z)
					);
				}
				break;
		}
	}
}
