using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script is for the  shell
public class SCT_Shell : MonoBehaviour {
	public float lifeTime = 2.0f;
	public int shellDamage = 10;
	public ParticleSystem m_ExplosionParticles;         // Reference to the particles that will play on explosion.
	public AudioSource m_ExplosionAudio;             
	void OnTriggerEnter(Collider col){


		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Collider>().enabled = false;
		GetComponentInChildren<Renderer>().enabled = false;
		m_ExplosionParticles.Play();
		m_ExplosionAudio.Play();
	 	Destroy (gameObject, 2);	 

 
 
	}
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(0.05f);
		GetComponent<BoxCollider> ().enabled = true;
		GetComponent<SphereCollider> ().enabled = true;
		yield return new WaitForSeconds(lifeTime);
		  Destroy(gameObject);
	}
}
