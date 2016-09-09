using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

    public float radius = 5.0F;
    public float strength = 10.0F;
    public float duration = 5f;

    private Vector3 _vacuumPos;
    private Collider2D[] _colliders;

    void Start () {
        _vacuumPos = transform.position;
        Invoke("Destruct", duration);
    }

    /*** THIS IS BROKEN, BUT IT'S FUNNY TO WATCH ***/
    void Update()
    {
        _colliders = Physics2D.OverlapCircleAll(_vacuumPos, radius);

        foreach (Collider2D hit in _colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

            if (rb != null)
                rb.AddForce((hit.GetComponent<Collider2D>().transform.position - _vacuumPos) * -strength);
        }
    }

    void Destruct()
    {
        Destroy(gameObject);
    }
}
