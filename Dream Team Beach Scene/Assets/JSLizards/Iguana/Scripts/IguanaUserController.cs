using UnityEngine;
using System.Collections;

public class IguanaUserController : MonoBehaviour {
	IguanaCharacter iguanaCharacter;


    [SerializeField]
    private float speed = 5f;

    void Start () {
		iguanaCharacter = GetComponent < IguanaCharacter> ();
	}
	
	void Update () {	
		if (Input.GetButtonDown ("Fire1")) {
			iguanaCharacter.Attack();
		}
		
		if (Input.GetKeyDown (KeyCode.H)) {
			iguanaCharacter.Hit();
		}
		
		if (Input.GetKeyDown (KeyCode.E)) {
			iguanaCharacter.Eat();
		}

		if (Input.GetKeyDown (KeyCode.K)) {
			iguanaCharacter.Death();
		}
		
		if (Input.GetKeyDown (KeyCode.R)) {
			iguanaCharacter.Rebirth();
		}		



	}
	
	private void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
        v = v * speed;
        h *= speed;
		iguanaCharacter.Move (v,-h);
	}
}
