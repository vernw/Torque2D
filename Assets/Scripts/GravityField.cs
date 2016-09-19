using UnityEngine;
using System.Collections;

public class GravityField : MonoBehaviour {

    public GameController gameController;

    public float strength = 1500.0F;
    public float duration = 7f;

    [SerializeField]
    private Vector2 _randVector;
    [SerializeField]
    private GameObject[] _playerAvatars;

    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Start()
    {
        _playerAvatars = new GameObject[gameController.maxPlayers];
        _playerAvatars = GameObject.FindGameObjectsWithTag("Player");

        _randVector = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));

        Invoke("Destruct", duration);
    }
    
    void Update()
    {
        foreach (GameObject obj in _playerAvatars)
        {
            Rigidbody2D rb = obj.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
                rb.AddForce(_randVector * strength * Time.deltaTime);
        }
    }

    void Destruct()
    {
        Destroy(gameObject);
    }
}
