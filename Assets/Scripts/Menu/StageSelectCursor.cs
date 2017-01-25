using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StageSelectCursor : MonoBehaviour
{
    public KeyCode up, down, left, right;
    public bool controlDisabled;

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

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        thrust = 10000000.0f;
    }

    // Key Inputs
    void Update()
    {
        bool up = false;
        bool right = false;
        bool down = false;
        bool left = false;
        switch (tag)
        {
            case "P1":
                up = Input.GetKey(KeyCode.W);
                right = Input.GetKey(KeyCode.D);
                down = Input.GetKey(KeyCode.S);
                left = Input.GetKey(KeyCode.A);
                break;
            case "P2":
                up = Input.GetKey(KeyCode.UpArrow);
                right = Input.GetKey(KeyCode.RightArrow);
                down = Input.GetKey(KeyCode.DownArrow);
                left = Input.GetKey(KeyCode.LeftArrow);
                break;
            case "P3":
                up = Input.GetKey(KeyCode.I);
                right = Input.GetKey(KeyCode.L);
                down = Input.GetKey(KeyCode.K);
                left = Input.GetKey(KeyCode.J);
                break;
            case "P4":
                up = Input.GetKey(KeyCode.Keypad8);
                right = Input.GetKey(KeyCode.Keypad6);
                down = Input.GetKey(KeyCode.Keypad5);
                left = Input.GetKey(KeyCode.Keypad4);
                break;
        }

        if (up)
        {
            _rb.AddForce(new Vector2(0, thrust) * Time.deltaTime);
        }
        if (right)
        {
            _rb.AddForce(new Vector2(thrust, 0) * Time.deltaTime);
        }
        if (down)
        {
            _rb.AddForce(new Vector2(0, -thrust) * Time.deltaTime);
        }
        if (left)
        {
            _rb.AddForce(new Vector2(-thrust, 0) * Time.deltaTime);
        }
    }
}
