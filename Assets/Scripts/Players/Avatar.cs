using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Avatar : MonoBehaviour {
    public GameController gameController;
    public LifeOverlay lifeOverlay;
    public KeyCode up, down, left, right;
    public GameObject explosion;
    public GameObject healthCount;
    public Color faded;
    public Color solid;
    public bool controlDisabled;
    public Wormhole transporter;
    public Player player;

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
        thrust = 600000.0f;
        invincible = false;
        alive = true;
	}

    public void TakeDamage(int damage) {
        if (invincible) {
            return;
        }
        invincible = true;
        player.lives -= damage;
        player.onDamage(player);
		if (player.lives <= 0) {
			player.onDeath (player);
		} else {
			StartCoroutine (Explode ());
		}
    }

    public IEnumerator Explode()
    {
        // Instantiate explosive particle prefabs
        GameObject explode = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(0.7f);
        Destroy(explode);
    }

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
        }
    }
}
