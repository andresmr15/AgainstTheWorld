
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    private Animator _animator;
    private bool _isAttacking;
    private int _combo;
    public AudioSource audioS;
    public AudioClip[] sound;


    void Awake()
    {
        _animator = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
    }

    void Update()
    {
        Combos();
    }

    public void Combos()
    {
        if (Input.GetKeyDown(KeyCode.C) && !_isAttacking)
        {
            _isAttacking = true;
            _animator.SetTrigger("" + _combo);
            audioS.clip = sound[_combo];
            audioS.Play();
        }

    }
    public void StartCombo()
    {
        _isAttacking = false;
        if (_combo < 3)
        {
            _combo++;
        }
    }

    public void FinishCombo()
    {
        _isAttacking = false;
        _combo = 0;
    }
}



