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

	protected override void CustomInitialize ()
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
		StartCoroutine (buildChains ()); // TODO: build based on colors
	}

	private void buildChain(Player a, Player b) {
		GameObject chainParent = new GameObject ();
		chainParent.name = "chain";
		Vector3 startPoint = a.transform.position;
		Vector3 direction = b.transform.position - a.transform.position;
		Rigidbody2D toConnect = a.avatar.GetComponent<Rigidbody2D>();
		TDDUPLink previousLink = null;
		for (float traveled = linkDiameter + 0.2f; traveled < direction.magnitude - 0.2f; traveled += linkDiameter) {
			Vector3 actionPoint = startPoint + direction.normalized * traveled;
			GameObject linkGO = (GameObject)Instantiate (linkPrefab, actionPoint, Quaternion.identity);
			SpringJoint2D linkSJ = linkGO.AddComponent<SpringJoint2D> ();
			linkSJ.autoConfigureDistance = false;
			linkSJ.connectedBody = toConnect;
			if (previousLink) {
				linkSJ.distance = springLength;
			} else {
				linkSJ.distance = avatarSpringLength;
			}
			linkSJ.frequency = springFrequency;
			linkSJ.dampingRatio = springDampening;
			TDDUPLink linkScript = linkGO.GetComponent<TDDUPLink> ();
			linkScript.backNeighbor (toConnect.gameObject);
			if (previousLink) {
				previousLink.frontNeighbor (linkGO);
			}
			previousLink = linkScript;
			toConnect = linkGO.GetComponent<Rigidbody2D> ();
			linkGO.transform.parent = chainParent.transform;
		}
		SpringJoint2D springJoint = b.avatar.gameObject.AddComponent<SpringJoint2D> ();
		springJoint.autoConfigureDistance = false;
		springJoint.connectedBody = toConnect;
		springJoint.distance = avatarSpringLength;
		springJoint.frequency = springFrequency;
		springJoint.dampingRatio = springDampening;
		previousLink.frontNeighbor (b.avatar.gameObject);
	}

	IEnumerator buildChains() {
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
		foreach (Player a in players) {
			foreach (Player b in players) {
				if (a == b) {
					print ("same");
				}
				if (a != b && a.color == b.color) {
					buildChain (a, b);
				}
			}
		}
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
