﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Avatar : MonoBehaviour {

    private Rigidbody2D _rb;
    private float invincibilityTime = 5f;

    public GameController gameController;
    public LifeOverlay lifeOverlay;

    public GameObject explosion;
    public GameObject healthCount;
    public Color faded;
    public Color solid;
    public bool controlDisabled;
    public Wormhole transporter;

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

            StartCoroutine(DoInvincible());

            // Fade avatar for invincibility
            if (value)
            {
                GetComponent<SpriteRenderer>().DOFade(0.5f, 0.1f);
            }
            else
                GetComponent<SpriteRenderer>().DOFade(1f, 0.1f);
        }
    }

    public KeyCode up, down, left, right;

	void Start () {
        gameController = GameController.instance;
        lifeOverlay = GameObject.FindGameObjectWithTag("LifeOverlay").GetComponent<LifeOverlay>();

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
        if (gameObject.tag == "P1" && gameController.livesP1 > 0)
        {
            gameController.livesP1 -= damage;
        }
        if (gameObject.tag == "P2" && gameController.livesP2 > 0)
        {
            gameController.livesP2 -= damage;
        }
        if (gameObject.tag == "P3" && gameController.livesP3 > 0)
        {
            gameController.livesP3 -= damage;
        }
        if (gameObject.tag == "P4" && gameController.livesP4 > 0)
        {
            gameController.livesP4 -= damage;
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
        if (!gameController.countdown && !controlDisabled)
        {
            /** P1 Controls **/
            if (gameObject.tag == "P1")
            {
                if (Input.GetKey(KeyCode.W))
                {
                    /** Force Up **/
                    _rb.AddForce(new Vector2(0, thrust) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    /** Force Left **/
                    _rb.AddForce(new Vector2(-thrust, 0) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    /** Force Down **/
                    _rb.AddForce(new Vector2(0, -thrust) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    /** Force Right **/
                    _rb.AddForce(new Vector2(thrust, 0) * Time.deltaTime);
                }
            }
            /** P2 Controls **/
            else if (gameObject.tag == "P2")
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    /** Force Up **/
                    _rb.AddForce(new Vector2(0, thrust) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    /** Force Left **/
                    _rb.AddForce(new Vector2(-thrust, 0) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    /** Force Down **/
                    _rb.AddForce(new Vector2(0, -thrust) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    /** Force Right **/
                    _rb.AddForce(new Vector2(thrust, 0) * Time.deltaTime);
                }
            }
            /** P3 Controls **/
            else if (gameObject.tag == "P3")
            {
                if (Input.GetKey(KeyCode.I))
                {
                    /** Force Up **/
                    _rb.AddForce(new Vector2(0, thrust) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.J))
                {
                    /** Force Left **/
                    _rb.AddForce(new Vector2(-thrust, 0) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.K))
                {
                    /** Force Down **/
                    _rb.AddForce(new Vector2(0, -thrust) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.L))
                {
                    /** Force Right **/
                    _rb.AddForce(new Vector2(thrust, 0) * Time.deltaTime);
                }
            }
            /** P4 Controls **/
            else if (gameObject.tag == "P4")
            {
                if (Input.GetKey(KeyCode.Keypad8))
                {
                    /** Force Up **/
                    _rb.AddForce(new Vector2(0, thrust) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.Keypad4))
                {
                    /** Force Left **/
                    _rb.AddForce(new Vector2(-thrust, 0) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.Keypad5))
                {
                    /** Force Down **/
                    _rb.AddForce(new Vector2(0, -thrust) * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.Keypad6))
                {
                    /** Force Right **/
                    _rb.AddForce(new Vector2(thrust, 0) * Time.deltaTime);
                }
            }

        }
    }

    IEnumerator DoInvincible() {
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }
}
