using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent (typeof(AudioSource))]
public class Note_Trg : MonoBehaviour
{
	public GameObject track_ctrl;
	public int noteval;
	private float cur_clr;
	private bool locked;
	private AudioSource plyr;

	public float curamp;
	private float[] spectrum = new float[256];

	private Vector3 startpos;
	private Vector3 endpos;

	void Start ()
	{
		locked = true;
		plyr = transform.GetComponent<AudioSource> ();
	}

	void play_note ()
	{
		track_ctrl = transform.parent.parent.gameObject;
		float aplitude = Mathf.Clamp ((0.4F / (float)noteval), 0.15f, 0.4f);
		float pitch = (float)noteval / 12f;
		plyr.pitch = pitch;
		plyr.PlayOneShot (plyr.clip, aplitude);
	}

	void Update ()
	{
		float xval = transform.position.x;
		float zval = transform.position.z;


		if (transform.localPosition.x < 0.0F) {
			if (locked == false) {
				cur_clr = 1f;
				startpos = transform.position;
				play_note ();
				locked = true;
			}
		}
		if (transform.localPosition.x > 0.0F) {
			locked = false;
			cur_clr = 0.5f;
		}

		transform.GetComponent<MeshRenderer> ().material.color = new Color (cur_clr, cur_clr, cur_clr, 1f);
		 
	}
}