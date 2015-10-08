using UnityEngine;
using System.Collections;

public class Avatar : MonoBehaviour {

    private Rigidbody2D rb;

    public GameController gameController;
    public GameObject explosion;
    public GameObject healthCount;

    public float thrust;
    public KeyCode up, down, left, right;
    public bool invincible;

    public Color faded;
    public Color solid;

	void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        thrust = 30000.0f;
        invincible = false;

        faded = new Color(0, 0, 0, 0);
        solid = gameObject.transform.GetComponent<SpriteRenderer>().color;
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (gameObject.tag == "P1" && coll.gameObject.tag == "P2Puck" && !invincible)
        {
            gameController.livesP1--;
            StartCoroutine(DisplayHealth(1));
            StartCoroutine(Explode());
        }
        if (gameObject.tag == "P2" && coll.gameObject.tag == "P1Puck" && !invincible)
        {
            gameController.livesP2--;
            StartCoroutine(DisplayHealth(2));
            StartCoroutine(Explode());
        }
    }

    public IEnumerator Explode()
    {
        // Instantiate explosive particle prefabs
        print("BOOM!");
        GameObject explode = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(1);
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

        // Flash health
        GameObject health = Instantiate(healthCount, (transform.position + new Vector3(0f, 3f, 0f)), Quaternion.identity) as GameObject;

        yield return new WaitForSeconds(2);

        // Reset values
        invincible = false;
        gameObject.transform.GetComponent<SpriteRenderer>().color = solid;
        Destroy(health);
    }

    public IEnumerator Destruct()
    {
        yield return new WaitForSeconds(1);
        for (int i = transform.parent.transform.childCount - 1; i > 0; i--)
        {
            transform.parent.transform.GetChild(i).gameObject.SetActive(false);
            GameObject explode = Instantiate(explosion, transform.parent.transform.GetChild(i).position, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(0.7f);
            Destroy(explode);
        }
        StartCoroutine(Explode());
        gameObject.SetActive(false);

    }

	void Update () {
        /** P1 Controls **/
        if (gameObject.tag == "P1")
        {
            if (Input.GetKey(KeyCode.W))
            {
                /** Force Up **/
                //rb.AddForce(new Vector3(0, 0, thrust) * Time.deltaTime);
                rb.AddForce(new Vector2(0, thrust) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                /** Force Left **/
                //rb.AddForce(new Vector3(-thrust, 0, 0) * Time.deltaTime);
                rb.AddForce(new Vector3(-thrust, 0) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                /** Force Down **/
                //rb.AddForce(new Vector3(0, 0, -thrust) * Time.deltaTime);
                rb.AddForce(new Vector3(0, -thrust) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                /** Force Right **/
                //rb.AddForce(new Vector3(thrust, 0, 0) * Time.deltaTime);
                rb.AddForce(new Vector3(thrust, 0) * Time.deltaTime);
            }
        }

        /** P2 Controls **/
        else if(gameObject.tag == "P2")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                /** Force Up **/
                //rb.AddForce(new Vector3(0, 0, thrust) * Time.deltaTime);
                rb.AddForce(new Vector2(0, thrust) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                /** Force Left **/
                //rb.AddForce(new Vector3(-thrust, 0, 0) * Time.deltaTime);
                rb.AddForce(new Vector3(-thrust, 0) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                /** Force Down **/
                //rb.AddForce(new Vector3(0, 0, -thrust) * Time.deltaTime);
                rb.AddForce(new Vector3(0, -thrust) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                /** Force Right **/
                //rb.AddForce(new Vector3(thrust, 0, 0) * Time.deltaTime);
                rb.AddForce(new Vector3(thrust, 0) * Time.deltaTime);
            }
        }
	}
}
