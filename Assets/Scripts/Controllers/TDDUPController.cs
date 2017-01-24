using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TDDUPController : GameTypeController {
//	public Player currentOddball;

//	int maxLives = 1;
	int scoreToWin = 30;
	LifeOverlay lifeOverlay;
	Dictionary<Player, int> scores;
	float timeToScore = 1f; // 1 second per point
	float respawnTime = 4f;
	float linkDiameter = 0.4f;
	float springLength = 0.4f;
	float avatarSpringLength = 0.55f;
	float springFrequency = 8f;
	float springDampening = 1f;
	GameObject linkPrefab;

	// Use this for initialization
	void Start () {
//		initialize(new Team(Player.PLAYER.ONE, Player.PLAYER.THREE),
//			new Team(Player.PLAYER.TWO, Player.PLAYER.FOUR));
	}

	// Update is called once per frame
	void Update () {

	}

//	public void initialize(Team t1, Team t2) {
//		superInitialize(maxLives);
//		linkPrefab = (GameObject)Resources.Load ("Prefabs/Game Mode Controllers and Assets/TDDUPLink");
//
//		scores = new Dictionary<Player, int>();
//		foreach(Player player in players) {
//			scores.Add(player, 0);
//			player.onDamage = delegate(Player _player) {
//				_player.lives = 0;
//				_player.doDestruct();
//			};
//			player.onDeath = delegate(Player _player) {
//				_player.lives = 0;
//				_player.doDestruct();
//			};
//		}
//
//		StartCoroutine (buildChains (t1, t2));
//	}

	protected override void CustomInitialize (MenuController.gameModeSelection curMode)
	{
		linkPrefab = (GameObject)Resources.Load ("Prefabs/Game Mode Controllers and Assets/TDDUPLink");
		scores = new Dictionary<Player, int>();
		foreach(Player player in players) {
			scores.Add(player, 0);
			player.onDamage = delegate(Player _player) {
				_player.lives = 0;
				_player.doDestruct();
			};
			player.onDeath = delegate(Player _player) {
				_player.lives = 0;
				_player.doDestruct();
			};
		}
		StartCoroutine (buildChains (new Team(Player.PLAYER.ONE, Player.PLAYER.THREE), new Team(Player.PLAYER.TWO, Player.PLAYER.FOUR))); // TODO: build based on colors
	}

	private void buildChain(Team team) {
		GameObject chainParent = new GameObject ();
		chainParent.name = "chain";
		Player pa = null;
		Player pb = null;
		foreach (Player player in players) {
			if (player.playerType == team.a) {
				pa = player;
			}
			if (player.playerType == team.b) {
				pb = player;
			}
		}
		Vector3 startPoint = pa.transform.position;
		Vector3 direction = pb.transform.position - pa.transform.position;
//		float traveled = 0f;
//		float diameter = linkPrefab.GetComponent<CircleCollider2D> ().radius * 2f;
//		float diameter = 
//		print(pa.avatar);
		Rigidbody2D toConnect = pa.avatar.GetComponent<Rigidbody2D>();
		TDDUPLink previousLink = null;
//		bool first = true;
		for (float traveled = linkDiameter + 0.2f; traveled < direction.magnitude - 0.2f; traveled += linkDiameter) {
			Vector3 actionPoint = startPoint + direction.normalized * traveled;
//			print (actionPoint);
			GameObject linkGO = (GameObject)Instantiate (linkPrefab, actionPoint, Quaternion.identity);
//			HingeJoint2D linkHJ = linkGO.AddComponent<HingeJoint2D> ();
			SpringJoint2D linkSJ = linkGO.AddComponent<SpringJoint2D> ();
			linkSJ.autoConfigureDistance = false;
			linkSJ.connectedBody = toConnect;
			if (previousLink) {
				linkSJ.distance = springLength;
			} else {
				linkSJ.distance = avatarSpringLength;
			}
//			linkSJ.frequency = 10f;
			linkSJ.frequency = springFrequency;
			linkSJ.dampingRatio = springDampening;
			TDDUPLink linkScript = linkGO.GetComponent<TDDUPLink> ();
			linkScript.backNeighbor (toConnect.gameObject);
			if (previousLink) {
//				linkScript.backNeighbor = previousLink;
				previousLink.frontNeighbor (linkGO);
			}
			previousLink = linkScript;
//			if (first) {
//				linkScript.backNeighbor ();
//			}
//			print (linkGO.GetComponent<CircleCollider2D> ().radius * 2f);
			toConnect = linkGO.GetComponent<Rigidbody2D> ();
			linkGO.transform.parent = chainParent.transform;
		}
//		pb.avatar.gameObject.AddComponent<HingeJoint2D> ().connectedBody = toConnect;
//		.connectedBody = toConnect;
		SpringJoint2D springJoint = pb.avatar.gameObject.AddComponent<SpringJoint2D> ();
		springJoint.autoConfigureDistance = false;
		springJoint.connectedBody = toConnect;
		springJoint.distance = avatarSpringLength;
		springJoint.frequency = springFrequency;
		springJoint.dampingRatio = springDampening;
		previousLink.frontNeighbor (pb.avatar.gameObject);
	}

	IEnumerator buildChains(Team t1, Team t2) {
		bool flag = true;
		while (flag) {
			flag = false;
			foreach (Player player in players) {
				if (!player.finishedInitializing) {
					flag = true;
					break;
				}
			}
			yield return new WaitForEndOfFrame ();
		}
		buildChain (t1);
		buildChain (t2);
	}

//	IEnumerator CalculateScore() {
//		while (true) {
//			yield return new WaitForSeconds(timeToScore);
//			if (currentOddball) {
//				scores[currentOddball] += 1;
//				if (scores[currentOddball] > scoreToWin) {
//					// TODO: winning
//				}
//			}
//		}
//	}

//	IEnumerator ManageRespawn(Player player) {
//		player.DoDestruct ();
//		yield return new WaitForSeconds (respawnTime);
//		Respawn.RevivePlayer (player);
//	}

	public struct Team {
		public Player.PLAYER a;
		public Player.PLAYER b;

		public Team(Player.PLAYER _a, Player.PLAYER _b) {
			a = _a;
			b = _b;
		}
	}
}
