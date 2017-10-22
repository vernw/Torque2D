using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    protected Dictionary<Util.PLAYER, Corner> corners;

    public void Initialize (Dictionary<Util.PLAYER, Util.COLOR> playerColors, int maxLives) {
        corners = new Dictionary<Util.PLAYER, Corner>();
        foreach (KeyValuePair<Util.PLAYER, Util.COLOR> p in playerColors) {
			corners [p.Key] = new Corner (p.Key, p.Value, maxLives);
		}
	}

	public void Update() {
		foreach (KeyValuePair<Util.PLAYER, Corner> p in corners) {
			(p.Value).Update ();
		}
	}

    public void UpdateCount(Util.PLAYER player, int count)
    {
        if (count > corners[player].count)
        {
            for (int i = 0; i < corners[player].count - count; i++)
            {
                (corners[player]).AddLife();
            }
        } else if (count < corners[player].count)
        {
            for (int i = 0; i < corners[player].count - count; i++)
            {
                (corners[player]).RemoveLife();
            }
        }
        corners[player].count = count;
    }

    public class Corner : MonoBehaviour
    {
        protected enum Edge { TOPLEFT, TOPRIGHT, BOTLEFT, BOTRIGHT }
        protected Edge edge;
        protected Util.COLOR color;
        protected Vector3 position;
        public int count;
        protected Camera cam;
        Vector3 camPos;
        float camHeight;
        protected float camWidth;
        float horizontalBuffer = 1f;
        float verticalBuffer = 1f;
        float expectedWidth = 30;
        protected float scale;
        List<GameObject> lifeGOs;
        LifeUI parent;
        float lifeBuffer = .1f;

        public Corner(Util.PLAYER player, Util.COLOR _color, int _count)
        {
            edge = PlayerToEdge(player);
            color = _color;
            count = _count;
            lifeGOs = new List<GameObject>();
            parent = GameObject.FindObjectOfType<LifeUI>();
            for (int i = 0; i < count; i++)
            {
                AddLife();
            }
            Update();
        }

        public void Update()
        {
            cam = Camera.main;
            camHeight = 2f * cam.orthographicSize;
            camWidth = camHeight * cam.aspect;
            camPos = cam.transform.position;
            position = new Vector3(
                camPos.x + (camWidth / 2f - horizontalBuffer) *
                ((edge == Edge.TOPRIGHT || edge == Edge.BOTRIGHT) ? 1f : -1f),
                camPos.y + (camHeight / 2f - verticalBuffer) *
                ((edge == Edge.TOPLEFT || edge == Edge.TOPRIGHT) ? 1f : -1f), -10f
            );
            scale = camWidth / expectedWidth;

            for (int i = 0; i < count; i++)
            {
                GameObject go = lifeGOs[i];
                go.transform.localScale = new Vector3(scale, scale, 1f);
                float seperation = go.GetComponent<SpriteRenderer>().bounds.extents.x * 2f + lifeBuffer * scale;
                go.transform.position = new Vector3(
                    position.x + (seperation * i) * ((edge == Edge.TOPLEFT || edge == Edge.BOTLEFT) ? 1f : -1f),
                    position.y, 0f
                );
            }
        }

        protected Edge PlayerToEdge(Util.PLAYER player)
        {
            switch (player)
            {
                case Util.PLAYER.ONE:
                    return Edge.TOPLEFT;
                case Util.PLAYER.TWO:
                    return Edge.TOPRIGHT;
                case Util.PLAYER.THREE:
                    return Edge.BOTLEFT;
                case Util.PLAYER.FOUR:
                    return Edge.BOTRIGHT;
            }
            return Edge.TOPRIGHT;
        }

        //public LifeCorner(Util.PLAYER player, Util.COLOR color, int count) : base (player, color, count) {
        //    lifeGOs = new List<GameObject>();
        //    parent = GameObject.FindObjectOfType<LifeUI>();
        //    for (int i = 0; i < count; i++)
        //    {
        //        AddLife();
        //    }
        //}

        //public new void Update()
        //{
        //    base.Update();
        //    for (int i = 0; i < count; i++)
        //    {
        //        GameObject go = lifeGOs[i];
        //        go.transform.localScale = new Vector3(scale, scale, 1f);
        //        float seperation = go.GetComponent<SpriteRenderer>().bounds.extents.x * 2f + lifeBuffer * scale;
        //        go.transform.position = new Vector3(
        //            position.x + (seperation * i) * ((edge == Edge.TOPLEFT || edge == Edge.BOTLEFT) ? 1f : -1f),
        //            position.y, 0f
        //        );
        //    }
        //}

        public void AddLife()
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/GameModes/LifePrefab") as GameObject) as GameObject;
            go.transform.parent = parent.transform;
            lifeGOs.Add(go);
            go.GetComponent<SpriteRenderer>().color = Util.ConvertColor(color);
        }

        public void RemoveLife()
        {
            GameObject go = lifeGOs[lifeGOs.Count];
            lifeGOs.Remove(go);
            Destroy(go);
        }
    }

 //   class LifeCorner : Corner {
	//	List<GameObject> lifeGOs;
	//	LifeUI parent;
	//	float lifeBuffer = .1f;

	//	public LifeCorner(Util.PLAYER player, Util.COLOR color, int count) : base (player, color, count) {
	//		lifeGOs = new List<GameObject>();
	//		parent = GameObject.FindObjectOfType<LifeUI>();
	//		for (int i = 0; i < count; i++) {
	//			AddLife();
	//		}
	//	}

	//	public new void Update() {
	//		base.Update();
	//		for (int i = 0; i < count; i++) {
	//			GameObject go = lifeGOs [i];
	//			go.transform.localScale = new Vector3 (scale, scale, 1f);
	//			float seperation = go.GetComponent<SpriteRenderer> ().bounds.extents.x * 2f + lifeBuffer * scale;
	//			go.transform.position = new Vector3 (
	//				position.x + (seperation * i) * ((edge == Edge.TOPLEFT || edge == Edge.BOTLEFT) ? 1f : -1f),
	//				position.y, 0f
	//			);
	//		}
	//	}

	//	public void AddLife() {
	//		GameObject go = Instantiate(Resources.Load("Prefabs/GameModes/LifePrefab") as GameObject) as GameObject;
	//		go.transform.parent = parent.transform;
	//		lifeGOs.Add(go);
 //           go.GetComponent<SpriteRenderer>().color = Util.ConvertColor(color);
	//	}

 //       public void RemoveLife()
 //       {
 //           GameObject go = lifeGOs[lifeGOs.Count];
 //           lifeGOs.Remove(go);
 //           Destroy(go);
 //       }
	//}
}
