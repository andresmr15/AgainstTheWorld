using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int totalHealth = 3;
    public RectTransform hearthUI;
    public Transform respawnPoint;

    public RectTransform gameOverMenu;
    public GameObject enemies;
    [SerializeField] private GameObject _hurtBox;

    private int _health;
    private float _hearthSize = 19.5f;

    private SpriteRenderer _renderer;
    private Animator _animator;
    private PlayerController _controller;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _controller = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _health = totalHealth;
    }

    public void AddDamage(int amount)
    {
        _health = _health - amount;

        //Visual Feedback
        StartCoroutine("VisualFeedBack");

       //GameOver
        if (_health <= 0)
        {
            _health = 0;
            StartCoroutine("SetActiveFalse");
        }

        hearthUI.sizeDelta = new Vector2(_hearthSize * _health, _hearthSize - 1.5f );

        Debug.Log("Player got some damage. His current helth is: " + _health);
    }

    public void AddHealth(int amount)
    {
        _health = _health + amount;

        //MaxHealth
        if (_health > totalHealth)
        {
            _health = totalHealth;
        }

        hearthUI.sizeDelta = new Vector2(_hearthSize * _health, _hearthSize - 1.5f);

        Debug.Log("Player got some health. His current hellth is: " + _health);
    }

    private IEnumerator VisualFeedBack()
    {
        _renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _renderer.color = Color.white;
    }

    public void VisualFeedBackReturn()
    {
        _renderer.color = Color.white;
    }

    private IEnumerator SetActiveFalse()
    {
        _controller.enabled = false;
        _hurtBox.SetActive(false);
        _animator.SetBool("Death", true);
        _controller._movement = Vector2.zero;
        yield return new WaitForSeconds(2.3f);
        _animator.enabled = false;
        gameObject.SetActive(false);
        gameObject.transform.SetPositionAndRotation(respawnPoint.position, transform.rotation);
    }

    private void OnEnable()
    {
        _health = totalHealth;
        _hurtBox.SetActive(true);
        hearthUI.sizeDelta = new Vector2(_hearthSize*3, _hearthSize - 1.5f);
    }

    private void OnDisable()
    {
        gameOverMenu.gameObject.SetActive(true);
        _controller._isAttacking = false;
        _controller._isGrounded = true;
        _controller._combo = 0;
        gameObject.GetComponent<AudioSource>().clip = null;
    }

    public int getHealth()
    {
        return _health;
    }
}
