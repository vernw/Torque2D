﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Meteor : MonoBehaviour {

    public GameController gameController;

    public float radius = 3.0f;
    public float strength = 1500.0F;
    public float approachDuration = 5f;

    [SerializeField]
    private Vector3 _meteorPos;
    [SerializeField]
    private Collider2D[] _colliders;
    [SerializeField]
    private GameObject[] _playerAvatars;
    
    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Start()
    {
        _meteorPos = transform.position;

        _playerAvatars = new GameObject[gameController.maxPlayers];
        _playerAvatars = GameObject.FindGameObjectsWithTag("Player");
        
        StartCoroutine(Approach());
    }

    // Enlarges meteor sprite
    IEnumerator Approach()
    {
        transform.DOScale(radius, approachDuration);
        yield return new WaitForSeconds(approachDuration);

        Impact();
    }

    // Deals damage and knocks back players
    void Impact()
    {
        _colliders = Physics2D.OverlapCircleAll(_meteorPos, radius);

        foreach (Collider2D hit in _colliders)
        {
            // Only hits Avatars
            if (hit.gameObject.tag == "P1" || hit.gameObject.tag == "P2" || hit.gameObject.tag == "P3" || hit.gameObject.tag == "P4")
            {
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                Vector2 direction = new Vector2(hit.GetComponent<Collider2D>().transform.position.x - _meteorPos.x, hit.GetComponent<Collider2D>().transform.position.y - _meteorPos.y);

                // Adjusts added force depending on distance from explosion center
                /*
                float impactDifferential = 1 - (direction.magnitude / radius);
                if (impactDifferential <= 0)
                {
                    impactDifferential = 0;
                }*/

                // Explosion force
                if (rb != null)
                    rb.AddForce(direction * strength * Time.deltaTime, ForceMode2D.Impulse);

                Avatar avatar = hit.gameObject.GetComponent<Avatar>();
                
                if (!avatar.invincible)
                {
                    switch (hit.gameObject.name)
                    {
                        case "Avatar1":
                            StartCoroutine(avatar.Damage(1));
                            break;
                        case "Avatar2":
                            StartCoroutine(avatar.Damage(2));
                            break;
                        case "Avatar3":
                            StartCoroutine(avatar.Damage(3));
                            break;
                        case "Avatar4":
                            StartCoroutine(avatar.Damage(4));
                            break;
                    }
                }
            }
        }

        Destruct();
    }

    void Destruct()
    {
        Destroy(gameObject);
    }
}
