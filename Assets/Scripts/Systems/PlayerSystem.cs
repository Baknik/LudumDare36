using UnityEngine;
using System.Collections;

public class PlayerSystem : MonoBehaviour, ISystem {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Entity[] playerEntities = this.GetPlayerEntities();
		foreach (Entity e in playerEntities)
		{
			PlayerComponent player = (PlayerComponent)e.Components["Player"];
			if (player.TargetNPC == null && player.TargetArtifact == null)
			{
				player.TargetNPC = this.GetRandomNPC();
				if (player.TargetNPC == null && Vector3.Distance(player.transform.position, player.TargetPosition) < 10.0f)
				{
					player.TargetPosition = new Vector3(Random.Range(5, 45), 0.0f, Random.Range(5, 45));
					this.WritePlayerThought("Time to explore!");
				}
				else if (player.TargetNPC != null)
				{
					player.TargetPosition = player.TargetNPC.position;
					this.WritePlayerThought("Oh look a quest!");
				}
			}
			else if (player.TargetNPC != null && Vector3.Distance(player.transform.position, player.TargetPosition) < 2.0f)
			{
				player.TargetNPC = null;
				player.ArrivedAtNPC = false;
				player.ArrivedAtArtifact = false;
				player.TargetArtifact = this.GetRandomArtifact();
				if (player.TargetArtifact != null)
				{
					player.TargetPosition = player.TargetArtifact.position;
					this.WritePlayerThought("Get a new quest, have to find an ancient artifact...");
				}
			}
			else if (player.TargetArtifact != null && Vector3.Distance(player.transform.position, player.TargetPosition) < 2.0f)
			{
				player.TargetArtifact = null;
				player.ArrivedAtNPC = false;
				player.ArrivedAtArtifact = false;
				player.TargetNPC = this.GetRandomNPC();
				if (player.TargetNPC != null)
				{
					player.TargetPosition = player.TargetNPC.position;
					this.WritePlayerThought("Found the artifact. On to the next quest!");
				}
			}

			player.MyNavMeshAgent.SetDestination(player.TargetPosition);
		}
	}

	public Entity[] GetPlayerEntities()
	{
		return ComponentPool.Instance.GetEntitiesWithComponents("Player");
	}

	private Transform GetRandomArtifact()
	{
		GameObject[] artifactObjects = GameObject.FindGameObjectsWithTag("Artifact");
		if (artifactObjects.Length > 0)
		{
			return artifactObjects[Random.Range(0, artifactObjects.Length)].transform;
		}
		return null;
	}

	private Transform GetRandomNPC()
	{
		GameObject[] artifactObjects = GameObject.FindGameObjectsWithTag("NPC");
		if (artifactObjects.Length > 0)
		{
			return artifactObjects[Random.Range(0, artifactObjects.Length)].transform;
		}
		return null;
	}

	private Entity GetPlayerThoughtsEntity()
	{
		Entity[] entities = ComponentPool.Instance.GetEntitiesWithComponents("Player Thoughts");

		if (entities.Length > 0)
		{
			return entities[0];
		}

		return null;
	}

	private void WritePlayerThought(string thought)
	{
		Entity playerThoughtsEntity = this.GetPlayerThoughtsEntity();
		if (playerThoughtsEntity != null)
		{
			PlayerThoughtsComponent thoughts = (PlayerThoughtsComponent)playerThoughtsEntity.Components["Player Thoughts"];
			thoughts.MyText.text = "\"" + thought + "\"\n" + thoughts.MyText.text;
			if (thoughts.MyText.text.Length > 1000)
			{
				thoughts.MyText.text = thoughts.MyText.text.Substring(0, 1000);
			}
		}
	}
}
