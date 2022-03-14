using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlayer : MonoBehaviour
{
    private GameObject _player;
    private Transform _transform;

    public void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Instantiate()
    {
        Instantiate(gameObject, _transform.position, Quaternion.identity );
    }
}
