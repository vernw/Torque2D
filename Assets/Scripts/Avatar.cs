using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Avatar : MonoBehaviour {

    private Rigidbody2D rb;

    public GameController gameController;
    public GameObject explosion;
    public GameObject healthCount;
    public Color faded;
    public Color solid;

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
    private bool _alive;
    public bool alive
    {
        get { return _alive; }
        set
        {
            _alive = value;
        }
    }

    private bool _invincible;
    public bool invincible
    {
        get { return _invincible; }
        set
        {
            _invincible = value;
            if (value)
                GetComponent<SpriteRenderer>().DOFade(35, 0.1f);
            else
                GetComponent<SpriteRenderer>().DOFade(100, 0.1f);
        }
    }

    public KeyCode up, down, left, right;

	void Start () {
        gameController = GameController.instance;

        rb = GetComponent<Rigidbody2D>();
        thrust = 30000.0f;
        invincible = false;
        alive = true;

        faded = new Color(gameObject.transform.GetComponent<SpriteRenderer>().color.r, gameObject.transform.GetComponent<SpriteRenderer>().color.g, gameObject.transform.GetComponent<SpriteRenderer>().color.b, gameObject.transform.GetComponent<SpriteRenderer>().color.a / 10);
        solid = gameObject.transform.GetComponent<SpriteRenderer>().color;
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (gameObject.tag == "P1" && (coll.gameObject.tag == "P2Puck" || coll.gameObject.tag == "P3Puck" || coll.gameObject.tag == "P4Puck") && !invincible && gameController.livesP1 > 0)
        {
            gameController.livesP1--;
            StartCoroutine(DisplayHealth(1));
            StartCoroutine(Explode());
        }
        if (gameObject.tag == "P2" && (coll.gameObject.tag == "P1Puck" || coll.gameObject.tag == "P3Puck" || coll.gameObject.tag == "P4Puck") && !invincible && gameController.livesP2 > 0)
        {
            gameController.livesP2--;
            StartCoroutine(DisplayHealth(2));
            StartCoroutine(Explode());
        }
        if (gameObject.tag == "P3" && (coll.gameObject.tag == "P1Puck" || coll.gameObject.tag == "P2Puck" || coll.gameObject.tag == "P4Puck") && !invincible && gameController.livesP3 > 0)
        {
            gameController.livesP3--;
            StartCoroutine(DisplayHealth(3));
            StartCoroutine(Explode());
        }
        if (gameObject.tag == "P4" && (coll.gameObject.tag == "P1Puck" || coll.gameObject.tag == "P2Puck" || coll.gameObject.tag == "P3Puck") && !invincible && gameController.livesP4 > 0)
        {
            gameController.livesP4--;
            StartCoroutine(DisplayHealth(4));
            StartCoroutine(Explode());
        }
    }

    public IEnumerator Explode()
    {
        // Instantiate explosive particle prefabs
        GameObject explode = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(0.7f);
        Destroy(explode);
    }

    public IEnumerator DisplayHealth(int player)
    {
        // Fade avatar for invincibility
        gameObject.transform.GetComponent<SpriteRenderer>().color = faded;
        invincible = true;

        if (player == 1)
        {
            // Fill with P1's health
            healthCount.GetComponent<TextMesh>().text = gameController.livesP1.ToString();
            healthCount.GetComponent<TextMesh>().color = solid;
        }
        if (player == 2)
        {
            // Fill with P2's health
            healthCount.GetComponent<TextMesh>().text = gameController.livesP2.ToString();
            healthCount.GetComponent<TextMesh>().color = solid;
        }
        if (player == 3)
        {
            // Fill with P3's health
            healthCount.GetComponent<TextMesh>().text = gameController.livesP3.ToString();
            healthCount.GetComponent<TextMesh>().color = solid;
        }
        if (player == 4)
        {
            // Fill with P4's health
            healthCount.GetComponent<TextMesh>().text = gameController.livesP4.ToString();
            healthCount.GetComponent<TextMesh>().color = solid;
        }

        // Flash health
        GameObject health = Instantiate(healthCount, (transform.position + new Vector3(0f, 0f, 0f)), Quaternion.identity) as GameObject;
        health.transform.DOJump(transform.position + new Vector3(0f, 2f, 0f), 1.5f, 1, 0.7f, false);

        yield return new WaitForSeconds(1);

        // Reset values
        invincible = false;
        gameObject.transform.GetComponent<SpriteRenderer>().color = solid;
        Destroy(health);
    }

    public IEnumerator Destruct()
    {
        // Sequentially destructs all components of a player
        yield return new WaitForSeconds(0.2f);
        for (int i = transform.parent.transform.childCount - 1; i > 0; i--)
        {
            transform.parent.transform.GetChild(i).gameObject.SetActive(false);
            GameObject explode = Instantiate(explosion, transform.parent.transform.GetChild(i).position, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(0.5f);
            Destroy(explode);
        }
        StartCoroutine(Explode());
        gameObject.SetActive(false);
    }

	void Update () {
        // Moving is only possible post-countdown
        if (!gameController.countdown)
        {
            /** P1 Controls **/
            if (gameObject.tag == "P1")
            {
                if (Input.GetKey(KeyCode.W))
                {
                    /** Force Up **/
                    rb.AddForce(new Vector2(0, thrust) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    /** Force Left **/
                    rb.AddForce(new Vector3(-thrust, 0) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    /** Force Down **/
                    rb.AddForce(new Vector3(0, -thrust) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    /** Force Right **/
                    rb.AddForce(new Vector3(thrust, 0) * Time.deltaTime);
                }
            }
            /** P2 Controls **/
            else if (gameObject.tag == "P2")
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    /** Force Up **/
                    rb.AddForce(new Vector2(0, thrust) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    /** Force Left **/
                    rb.AddForce(new Vector3(-thrust, 0) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    /** Force Down **/
                    rb.AddForce(new Vector3(0, -thrust) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    /** Force Right **/
                    rb.AddForce(new Vector3(thrust, 0) * Time.deltaTime);
                }
            }
            /** P3 Controls **/
            else if (gameObject.tag == "P3")
            {
                if (Input.GetKey(KeyCode.I))
                {
                    /** Force Up **/
                    rb.AddForce(new Vector2(0, thrust) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.J))
                {
                    /** Force Left **/
                    rb.AddForce(new Vector3(-thrust, 0) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.K))
                {
                    /** Force Down **/
                    rb.AddForce(new Vector3(0, -thrust) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.L))
                {
                    /** Force Right **/
                    rb.AddForce(new Vector3(thrust, 0) * Time.deltaTime);
                }
            }
            /** P4 Controls **/
            else if (gameObject.tag == "P4")
            {
                if (Input.GetKey(KeyCode.Keypad8))
                {
                    /** Force Up **/
                    rb.AddForce(new Vector2(0, thrust) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.Keypad4))
                {
                    /** Force Left **/
                    rb.AddForce(new Vector3(-thrust, 0) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.Keypad5))
                {
                    /** Force Down **/
                    rb.AddForce(new Vector3(0, -thrust) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.Keypad6))
                {
                    /** Force Right **/
                    rb.AddForce(new Vector3(thrust, 0) * Time.deltaTime);
                }
            }

        }
    }
}
