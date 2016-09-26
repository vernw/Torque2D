using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Avatar : MonoBehaviour {
    public GameController gameController;
    public LifeOverlay lifeOverlay;
    public KeyCode up, down, left, right;
    // public MonoBehavior controller;
    public GameObject explosion;
    public GameObject healthCount;
    public Color faded;
    public Color solid;
    public bool controlDisabled;
    public Wormhole transporter;

    private Player player;
    private Rigidbody2D _rb;
    private float invincibilityTime = 5f;

    // Force acting on player avatars; increase for boosts
    private float _thrust;
    public float thrust
    {
        get { return _thrust; }
        set
        {
            _thrust = value;
        }
    }

    // Alive state used to check when player is eliminated
    [SerializeField]
    private bool _alive;
    public bool alive
    {
        get { return _alive; }
        set
        {
            _alive = value;
        }
    }

    // Invincibility state post-hit
    [SerializeField]
    private bool _invincible;
    public bool invincible
    {
        get { return _invincible; }
        set
        {
            _invincible = value;

            StartCoroutine(DoInvincible());

            // Fade avatar for invincibility
            if (value)
                GetComponent<SpriteRenderer>().DOFade(0.5f, 0.1f);
            else
                GetComponent<SpriteRenderer>().DOFade(1f, 0.1f);
        }
    }

	void Start () {
        player = transform.parent.GetComponent<Player>();
        // gameController = GameController.instance;
        // lifeOverlay = GameObject.FindGameObjectWithTag("LifeOverlay").GetComponent<LifeOverlay>();

        _rb = GetComponent<Rigidbody2D>();
        thrust = 30000.0f;
        invincible = false;
        alive = true;
	}

    public void TakeDamage(int damage) {
        if (invincible) {
            return;
        }
        invincible = true;
        //TODO: Scale explosion by damage dealt?
        //TODO: Screen shake on big damage?
        // if (gameObject.tag == "P1" && gameController.livesP1 > 0)
        // {
        //     gameController.livesP1 -= damage;
        // }
        // if (gameObject.tag == "P2" && gameController.livesP2 > 0)
        // {
        //     gameController.livesP2 -= damage;
        // }
        // if (gameObject.tag == "P3" && gameController.livesP3 > 0)
        // {
        //     gameController.livesP3 -= damage;
        // }
        // if (gameObject.tag == "P4" && gameController.livesP4 > 0)
        // {
        //     gameController.livesP4 -= damage;
        // }
        // invi
        player.lives -= damage;
        player.onDamage(player);
        if (player.lives <= 0) {
            player.onDeath(player);
        }
        StartCoroutine(Explode());
    }

    public IEnumerator Explode()
    {
        // Instantiate explosive particle prefabs
        GameObject explode = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(0.7f);
        Destroy(explode);
    }

    // public IEnumerator Destruct()
    // {
    //     // Sequentially destructs all components of a player
    //     yield return new WaitForSeconds(0.2f);
    //     for (int i = transform.parent.transform.childCount - 1; i > 0; i--)
    //     {
    //         transform.parent.transform.GetChild(i).gameObject.SetActive(false);
    //         GameObject explode = Instantiate(explosion, transform.parent.transform.GetChild(i).position, Quaternion.identity) as GameObject;
    //         yield return new WaitForSeconds(0.5f);
    //         Destroy(explode);
    //     }
    //     StartCoroutine(Explode());
    //     gameObject.SetActive(false);
    // }

    IEnumerator DoInvincible()
    {
        //print("DoInvincible");
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }

    // Key Inputs
    void Update () {
        // Moving is only possible post-countdown
        //TODO: stop control during countdown
        if (!Countdown.countingDown)
        {
            bool up = false;
            bool right = false;
            bool down = false;
            bool left = false;
            switch (player.playerType) {
                case Player.PLAYER.ONE:
                    up = Input.GetKey(KeyCode.W);
                    right = Input.GetKey(KeyCode.D);
                    down = Input.GetKey(KeyCode.S);
                    left = Input.GetKey(KeyCode.A);
                break;
                case Player.PLAYER.TWO:
                    up = Input.GetKey(KeyCode.UpArrow);
                    right = Input.GetKey(KeyCode.RightArrow);
                    down = Input.GetKey(KeyCode.DownArrow);
                    left = Input.GetKey(KeyCode.LeftArrow);
                break;
                case Player.PLAYER.THREE:
                    up = Input.GetKey(KeyCode.I);
                    right = Input.GetKey(KeyCode.L);
                    down = Input.GetKey(KeyCode.K);
                    left = Input.GetKey(KeyCode.J);
                break;
                case Player.PLAYER.FOUR:
                    up = Input.GetKey(KeyCode.Keypad8);
                    right = Input.GetKey(KeyCode.Keypad6);
                    down = Input.GetKey(KeyCode.Keypad5);
                    left = Input.GetKey(KeyCode.Keypad4);
                break;
            }
            if (up) {
                 _rb.AddForce(new Vector2(0, thrust) * Time.deltaTime);
            }
            if (right) {
                _rb.AddForce(new Vector2(thrust, 0) * Time.deltaTime);
            }
            if (down) {
                _rb.AddForce(new Vector2(0, -thrust) * Time.deltaTime);
            }
            if (left) {
                 _rb.AddForce(new Vector2(-thrust, 0) * Time.deltaTime);
            }
            // /** P1 Controls **/
            // if (gameObject.tag == "P1")
            // {
            //     if (Input.GetKey(KeyCode.W))
            //     {
            //         /** Force Up **/
            //         _rb.AddForce(new Vector2(0, thrust) * Time.deltaTime);
            //     }
            //     if (Input.GetKey(KeyCode.A))
            //     {
            //         /** Force Left **/
            //         _rb.AddForce(new Vector2(-thrust, 0) * Time.deltaTime);
            //     }
            //     if (Input.GetKey(KeyCode.S))
            //     {
            //         /** Force Down **/
            //         _rb.AddForce(new Vector2(0, -thrust) * Time.deltaTime);
            //     }
            //     if (Input.GetKey(KeyCode.D))
            //     {
            //         /** Force Right **/
            //         _rb.AddForce(new Vector2(thrust, 0) * Time.deltaTime);
            //     }
            // }
            // /** P2 Controls **/
            // else if (gameObject.tag == "P2")
            // {
            //     if (Input.GetKey(KeyCode.UpArrow))
            //     {
            //         /** Force Up **/
            //         _rb.AddForce(new Vector2(0, thrust) * Time.deltaTime);
            //     }
            //     if (Input.GetKey(KeyCode.LeftArrow))
            //     {
            //         /** Force Left **/
            //         _rb.AddForce(new Vector2(-thrust, 0) * Time.deltaTime);
            //     }
            //     if (Input.GetKey(KeyCode.DownArrow))
            //     {
            //         /** Force Down **/
            //         _rb.AddForce(new Vector2(0, -thrust) * Time.deltaTime);
            //     }
            //     if (Input.GetKey(KeyCode.RightArrow))
            //     {
            //         /** Force Right **/
            //         _rb.AddForce(new Vector2(thrust, 0) * Time.deltaTime);
            //     }
            // }
            // /** P3 Controls **/
            // else if (gameObject.tag == "P3")
            // {
            //     if (Input.GetKey(KeyCode.I))
            //     {
            //         /** Force Up **/
            //         _rb.AddForce(new Vector2(0, thrust) * Time.deltaTime);
            //     }
            //     if (Input.GetKey(KeyCode.J))
            //     {
            //         /** Force Left **/
            //         _rb.AddForce(new Vector2(-thrust, 0) * Time.deltaTime);
            //     }
            //     if (Input.GetKey(KeyCode.K))
            //     {
            //         /** Force Down **/
            //         _rb.AddForce(new Vector2(0, -thrust) * Time.deltaTime);
            //     }
            //     if (Input.GetKey(KeyCode.L))
            //     {
            //         /** Force Right **/
            //         _rb.AddForce(new Vector2(thrust, 0) * Time.deltaTime);
            //     }
            // }
            // /** P4 Controls **/
            // else if (gameObject.tag == "P4")
            // {
            //     if (Input.GetKey(KeyCode.Keypad8))
            //     {
            //         /** Force Up **/
            //         _rb.AddForce(new Vector2(0, thrust) * Time.deltaTime);
            //     }
            //     if (Input.GetKey(KeyCode.Keypad4))
            //     {
            //         /** Force Left **/
            //         _rb.AddForce(new Vector2(-thrust, 0) * Time.deltaTime);
            //     }
            //     if (Input.GetKey(KeyCode.Keypad5))
            //     {
            //         /** Force Down **/
            //         _rb.AddForce(new Vector2(0, -thrust) * Time.deltaTime);
            //     }
            //     if (Input.GetKey(KeyCode.Keypad6))
            //     {
            //         /** Force Right **/
            //         _rb.AddForce(new Vector2(thrust, 0) * Time.deltaTime);
            //     }
            // }

        }
    }
}
