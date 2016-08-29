using UnityEngine;
using System.Collections;

public class ObjectPlacementSystem : MonoBehaviour, ISystem {

	[Header("Prefab Links")]
	public CursorTemplateComponent NPCCursorTemplatePrefab;
	public Transform NPCPrefab;
	public CursorTemplateComponent ArtifactCursorTemplatePrefab;
	public Transform ArtifactPrefab;
	public CursorTemplateComponent SelectorCursorTemplatePrefab;

	private Transform selectedQuestNPC;
	private Transform selectedQuestArtifact;

	// Use this for initialization
	void Start () {
		
	}

	void OnEnable()
	{
		PlayerInputSystem.PlayerInputEvent += HandlePlayerInputEvent;
		InputModeStateBehavior.InputModeEntered += HandleInputModeEntered;
		InputModeStateBehavior.InputModeExited += HandleInputModeExited;
		InputModeStateBehavior.InputModeStay += HandleInputModeStay;
	}

	void OnDisable()
	{
		PlayerInputSystem.PlayerInputEvent -= HandlePlayerInputEvent;
		InputModeStateBehavior.InputModeEntered -= HandleInputModeEntered;
		InputModeStateBehavior.InputModeExited -= HandleInputModeExited;
		InputModeStateBehavior.InputModeStay -= HandleInputModeStay;
	}

	// Update is called once per frame
	void Update () {
		Entity cursorTemplateEntity = this.GetCursorTemplateEntity();
		Entity terrainEntity = this.GetTerrainEntity();

		if (cursorTemplateEntity != null && terrainEntity != null)
		{
			TerrainComponent terrain = (TerrainComponent)terrainEntity.Components["Terrain"];
			CursorTemplateComponent cursorTemplate = (CursorTemplateComponent)cursorTemplateEntity.Components["Cursor Template"];

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll(ray, 1000, terrain.TerrainRaycastLayer);
			if (hits.Length > 0 && hits[0].point.x > 5.0f && hits[0].point.x < 45.0f && hits[0].point.z > 5.0f && hits[0].point.z < 45.0f && hits[0].point.y >= 4.0f)
			{
				cursorTemplate.AtValidLocation = true;
				cursorTemplate.transform.position = hits[0].point;
			}
			else
			{
				cursorTemplate.AtValidLocation = false;
			}

			if (cursorTemplate.CollidingWithWorld)
			{
				if (!cursorTemplate.OnlyCollisionsValid)
				{
					cursorTemplate.AtValidLocation = false;
				}
			}

			Entity moneyStat = this.GetMoneyStatEntity();
			if (moneyStat != null)
			{
				StatComponent stat = (StatComponent)moneyStat.Components["Stat"];
				if (stat.Value < cursorTemplate.Cost)
				{
					cursorTemplate.AtValidLocation = false;
				}
			}

			cursorTemplate.MyMeshRenderer.material.color =
				(cursorTemplate.AtValidLocation ? cursorTemplate.ValidColor : cursorTemplate.InvalidColor);
		}
	}

	public Entity GetTerrainEntity()
	{
		Entity[] entities = ComponentPool.Instance.GetEntitiesWithComponents("Terrain");

		if (entities.Length > 0)
		{
			return entities[0];
		}

		return null;
	}

	public Entity GetCursorTemplateEntity()
	{
		Entity[] entities = ComponentPool.Instance.GetEntitiesWithComponents("Cursor Template");

		if (entities.Length > 0)
		{
			return entities[0];
		}

		return null;
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

	public void HandlePlayerInputEvent(PlayerInputEventType eventType, int cost, Vector3 direction)
	{
		switch (eventType)
		{
			case PlayerInputEventType.NEW_NPC:
				CursorTemplateComponent template = (CursorTemplateComponent)Instantiate(this.NPCCursorTemplatePrefab, Vector3.zero, Quaternion.identity);
				template.Cost = cost;
				break;
			case PlayerInputEventType.NEW_ARTIFACT:
				template = (CursorTemplateComponent)Instantiate(this.ArtifactCursorTemplatePrefab, Vector3.zero, Quaternion.identity);
				template.Cost = cost;
				break;
			case PlayerInputEventType.NEW_QUEST:
				template = (CursorTemplateComponent)Instantiate(this.SelectorCursorTemplatePrefab, Vector3.zero, Quaternion.identity);
				template.Cost = cost;
				break;
		}
	}

	public void HandleInputModeEntered(InputMode inputMode)
	{
		switch (inputMode)
		{
			case InputMode.PLACED_NPC:
				this.Place(this.NPCPrefab);
				break;
			case InputMode.PLACED_ARTIFACT:
				this.Place(this.ArtifactPrefab);
				break;
			case InputMode.SELECTED_QUEST_NPC:
				Entity cursorTemplateEntity = this.GetCursorTemplateEntity();
				if (cursorTemplateEntity != null)
				{
					CursorTemplateComponent cursorTemplate = (CursorTemplateComponent)cursorTemplateEntity.Components["Cursor Template"];
					if (cursorTemplate.AtValidLocation)
					{
						this.selectedQuestNPC = cursorTemplate.CollisionObject;
						cursorTemplate.CollideWithTags[0] = "Artifact";
					}
				}
				break;
			case InputMode.SELECTED_QUEST_ARTIFACT:
				cursorTemplateEntity = this.GetCursorTemplateEntity();
				if (cursorTemplateEntity != null)
				{
					CursorTemplateComponent cursorTemplate = (CursorTemplateComponent)cursorTemplateEntity.Components["Cursor Template"];
					if (cursorTemplate.AtValidLocation)
					{
						this.selectedQuestArtifact = cursorTemplate.CollisionObject;
						GameObject.Destroy(cursorTemplate.gameObject);
					}
				}
				break;
			case InputMode.QUEST_SELECTED:
				if (this.selectedQuestNPC != null && this.selectedQuestArtifact != null)
				{
					// instantiate quest prefab
					Debug.Log("Quest Created " + this.selectedQuestNPC.ToString() + "," + this.selectedQuestArtifact.ToString());

					this.selectedQuestNPC = null;
					this.selectedQuestArtifact = null;
				}
				break;
			case InputMode.IDLE:
				this.DestroyAllCursorTemplates();
				this.selectedQuestNPC = null;
				this.selectedQuestArtifact = null;
				break;
		}
	}

	public void HandleInputModeExited(InputMode inputMode)
	{
		
	}

	public void HandleInputModeStay(InputMode inputMode)
	{
//		switch (inputMode)
//		{
//			case InputMode.IDLE:
//				this.DestroyAllCursorTemplates();
//				break;
//		}
	}

	private void DestroyAllCursorTemplates()
	{
		Entity[] entities = ComponentPool.Instance.GetEntitiesWithComponents("Cursor Template");
		foreach (Entity e in entities)
		{
			CursorTemplateComponent cursorTemplate = (CursorTemplateComponent)e.Components["Cursor Template"];
			GameObject.Destroy(cursorTemplate.gameObject);
		}
	}

	private void Place(Transform placePrefab)
	{
		Entity cursorTemplateEntity = this.GetCursorTemplateEntity();
		if (cursorTemplateEntity != null)
		{
			CursorTemplateComponent cursorTemplate = (CursorTemplateComponent)cursorTemplateEntity.Components["Cursor Template"];
			if (cursorTemplate.AtValidLocation)
			{
				Transform placed = (Transform)Instantiate(placePrefab, cursorTemplate.transform.position, Quaternion.identity);
				Entity moneyStat = this.GetMoneyStatEntity();
				if (moneyStat != null)
				{
					StatComponent stat = (StatComponent)moneyStat.Components["Stat"];
					stat.Value -= cursorTemplate.Cost;
				}
			}
			GameObject.Destroy(cursorTemplate.gameObject);
		}
	}
}
