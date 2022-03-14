using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{

	public int healthRestoration = 1; //Esta variable almacena el daño que se le hará al jugador

	private SpriteRenderer _rederer; //Se declara una variable para guardar el componente Renderer del objeto
	private Collider2D _collider; //Se declara una variable para guardar el componente Collider del objeto

	private void Awake()
	{
		_rederer = GetComponent<SpriteRenderer>();//Se obtiene el componente Renderer del objeto
		_collider = GetComponent<Collider2D>(); //Se obtiene el componente Collider del objeto
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Hurt")) //Si el jugador entra en el collider
		{
			collision.SendMessageUpwards("AddHealth", healthRestoration); //Le dice al jugador que se añada un corazón
			gameObject.SetActive(false);//Se desactiva el objeto recolectablew
		}
	}
}
