using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Cursor : MonoBehaviour
{
	public GameObject Audio_Eng;
	public GameObject Track_Ctrl;
	public bool init = false;
	private float sin_val;
	private float cos_val;

	//-------------------------------------------------

	public GameObject spr_obj;
	public List<GameObject> fibo_spr_objs;

	public int octaves = 3;
	public float scale_interval = 0.5F;

	private int cur_song_length;
	private int max_pattern_length;
	private int cur_loop_length;
	private int prv_loop_length;

	private float speed_multi;
	private int bpm;
	private int quant;
	private bool play;

	public float spr_offset = 10f;
	public List<int> notes_to_play;
	public List<float> m_scale;
	public int cur_n_;

	public AudioSource Au_Src;

	bool busy = false;
	private float qtimer;
	private float qtm;


	//-------------------------------------------------

	void init_prj ()
	{
		max_pattern_length = Track_Ctrl.GetComponent<Track_Ctrl> ().max_pattern_length;
		cur_song_length = Track_Ctrl.GetComponent<Track_Ctrl> ().cur_song_length;
		cur_loop_length = Track_Ctrl.GetComponent<Track_Ctrl> ().cur_loop_length;
		prv_loop_length = cur_loop_length;
		speed_multi = Track_Ctrl.GetComponent<Track_Ctrl> ().speed_multi;

		bpm = Track_Ctrl.GetComponent<Track_Ctrl> ().bpm;
		quant = Track_Ctrl.GetComponent<Track_Ctrl> ().quant;
		play = Track_Ctrl.GetComponent<Track_Ctrl> ().play;

		for (int i = 1; i < max_pattern_length + 1; i++) {
			GameObject st_obj = Instantiate (spr_obj, transform.position, Quaternion.identity) as GameObject;
			fibo_spr_objs.Add (st_obj);
			st_obj.GetComponent<Note_Trg> ().noteval = i;
			st_obj.name = i.ToString ();
			st_obj.transform.SetParent (transform);
			st_obj.SetActive (false);
		}

		build_scale (scale_interval, octaves);
		init = true;
	}

	void build_scale (float interv, int octaves)
	{
		play = false;
		m_scale.Clear ();
		for (int i = 1; i < octaves; i++) {
			for (int o = 1; o < 12; o++) {
				m_scale.Add ((float)o * (interv * i));
			}
		}
	}

	void edit_pattern (int opt)
	{
		if (opt == 1) {
			for (int i = 0; i < cur_loop_length; i++) {
				GameObject go = fibo_spr_objs [i];

				if (i <= cur_loop_length - 1) {
					go.SetActive (true);
				} else {
					go.SetActive (false);
				}
			}
			//Camera.main.orthographicSize = cur_loop_length * 3f;
		}
	}


	void fibo_spir_anim_a ()
	{
		for (int i = 0; i < cur_loop_length + 1; i++) {
			GameObject gob = fibo_spr_objs [i];
			sin_val = Mathf.Sin (Time.time * ((i + spr_offset) * 0.02F));
			cos_val = Mathf.Cos (Time.time * ((i + spr_offset) * 0.02F));
	
			float x_pos = transform.position.x + ((i + spr_offset)) * (sin_val);
			float y_pos = 0f;
			float z_pos = transform.position.z + ((i + spr_offset)) * (cos_val);
			
	
			gob.transform.localPosition = new Vector3 (x_pos, y_pos, z_pos);
		}
	}


	void fibo_spir_anim_b ()
	{
		for (int i = 0; i < cur_loop_length + 1; i++) {
			GameObject gob = fibo_spr_objs [i];
			sin_val = Mathf.Sin (Time.time * ((i + spr_offset) * 0.02F));
			cos_val = Mathf.Cos (Time.time * ((i + spr_offset) * 0.02F));

			float x_pos = transform.position.x + ((i + spr_offset)) * ((sin_val) * (Time.time * (i * 0.0002f)));
			float y_pos = 0f;
			float z_pos = transform.position.z + ((i + spr_offset)) * ((cos_val) * (Time.time * (i * 0.0002f)));


			gob.transform.localPosition = new Vector3 (x_pos, y_pos, z_pos);
		}
	}

	void fibo_spir_anim_c ()
	{
		for (int i = 0; i < cur_loop_length + 1; i++) {
			GameObject gob = fibo_spr_objs [i];

			float ml_sin = Mathf.Sin (Time.time * 0.1f) * 10f;
			float ml_cos = Mathf.Cos (Time.time * 0.1f) * 10f;

			sin_val = Mathf.Sin (Time.time * ((i + spr_offset) * 0.02F));
			cos_val = Mathf.Cos (Time.time * ((i + spr_offset) * 0.02F));

			float x_pos = transform.localPosition.x + ((i + spr_offset)) * (sin_val) * (ml_sin);
			//float y_pos = transform.localPosition.y + ((i + (spr_offset * 0.2F)) * 2F);
			float y_pos = 0f;
			float z_pos = transform.localPosition.z + ((i + spr_offset)) * (cos_val) * (ml_sin);


			gob.transform.localPosition = new Vector3 (x_pos, y_pos, z_pos);
		}
	}





	void Update ()
	{
		if (init == false) {
			init_prj ();
			edit_pattern (1);
			play = true;
		} else {
			cur_loop_length = Track_Ctrl.GetComponent<Track_Ctrl> ().cur_loop_length;

			if (play == true) {
				fibo_spir_anim_b ();
			}

			if (cur_loop_length != prv_loop_length) {
				edit_pattern (1);
			}


		}
	}
}
