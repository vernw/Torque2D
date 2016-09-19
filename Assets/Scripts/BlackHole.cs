using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlackHole : MonoBehaviour {
    private float strength = 500f;
    private float rotateSpeed = 50f;
    private float radius;
    private List<GameObject> _targets;

    void Start () {
        _targets = new List<GameObject>();
        radius = GetComponent<CircleCollider2D>().radius;
    }

    void Update()
    {
        // suck
        Vector2 center = transform.position;
        foreach (GameObject target in _targets) {
            float delta = Vector2.Distance(target.transform.position, center);
            Vector2 dir = center - (Vector2)target.transform.position;
            dir.Normalize();
            target.GetComponent<Rigidbody2D>().AddForce(dir * strength * (1 - Mathf.Min((delta / radius), 1)));
        }
        // rotate
        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * -rotateSpeed);
    }

    void OnTriggerEnter2D(Collider2D coll) {
        Avatar avatar = coll.GetComponent<Avatar>();
        if (avatar) {
            _targets.Add(avatar.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        Avatar avatar = coll.GetComponent<Avatar>();
        if (avatar) {
            if (_targets.Contains(avatar.gameObject)) {
                _targets.Remove(avatar.gameObject);
            }
        }
    }

    void Destruct()
    {
        Destroy(gameObject);
    }
}
