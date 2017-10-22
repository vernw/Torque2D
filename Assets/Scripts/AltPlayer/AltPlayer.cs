using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltPlayer : MonoBehaviour {
    public bool testInstantiate = false;

    AltAvatar avatar;
    List<AltLink> chain;
    AltPuck puck;
    Util.PLAYER type;
    float avatarDistance = .3f;
    float linkDistance = .2f;
    float puckDistance = .6f;
    float avatarMass = 1f;
    float linkMass = 1f;
    float puckMass = 1f;
    int chainLength;
    int maxPasses = 10;
    bool initialized = false;

    public void Initialize(Util.PLAYER _type, Util.COLOR color, int _chainLength)
    {
        chain = new List<AltLink>();
        type = _type;
        chainLength = _chainLength;
        avatar = (Instantiate(Resources.Load("Prefabs/AltPlayer/AltAvatar") as GameObject) as GameObject).GetComponent<AltAvatar>();
        avatar.Initialize(avatarMass, color);
        avatar.transform.parent = transform;
        avatar.transform.localPosition = Vector2.zero;
        for (int i = 0; i < chainLength; i++)
        {
            chain.Add((Instantiate(Resources.Load("Prefabs/AltPlayer/AltLink") as GameObject) as GameObject).GetComponent<AltLink>());
            AltActor temp = avatar;
            if (i > 0)
            {
                temp = chain[i - 1];
            }
            chain[i].Initialize(linkMass, color, temp, (i == 0 ? avatarDistance + linkDistance : linkDistance * 2f));
            chain[i].transform.parent = transform;
            chain[i].transform.localPosition = Vector2.left * (linkDistance * 2f * i + linkDistance + avatarDistance);
        }
        puck = (Instantiate(Resources.Load("Prefabs/AltPlayer/AltPuck") as GameObject) as GameObject).GetComponent<AltPuck>();
        puck.Initialize(puckMass, color, chain[chainLength - 1], linkDistance + puckDistance);
        puck.transform.parent = transform;
        puck.transform.localPosition = Vector2.left * (avatarDistance + puckDistance + linkDistance * 2f * chainLength);
        avatar.puck = puck;
        initialized = true;
    }

    void Start ()
    {
        if (testInstantiate)
        {
            Initialize(Util.PLAYER.ONE, Util.COLOR.RED, 3);
        }
    }

    // Update is called once per frame
	void Update () {
        if (!initialized)
        {
            return;
        }
        avatar.ApplyVelocity();
        foreach (AltLink link in chain)
        {
            link.ApplyVelocity();
        }
        puck.ApplyVelocity();
        for (int i = 0; i < maxPasses; i++)
        {
            foreach(AltLink link in chain)
            {
                link.ApplyConstraint();
            }
            puck.ApplyConstraint();
        }
	}
}
