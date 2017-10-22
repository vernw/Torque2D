using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCard : MonoBehaviour {
	Camera cam;

    public void Initialize(Dictionary<Util.PLAYER, Util.COLOR> playerDefs, Util.COLOR winningColor)
    {
        cam = Camera.main;
    }
}
