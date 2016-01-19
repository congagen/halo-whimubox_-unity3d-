using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Track_Ctrl : MonoBehaviour
{
	public bool init = false;
	public int max_pattern_length = 256;
	public int cur_song_length = 256;
	public int cur_loop_length = 32;
	public int prv_loop_length;

	public float speed_multi;
	public int bpm = 120;
	public int quant = 16;

	public int play_mode;
	public bool play = true;
	public bool stop = false;

	void Update ()
	{
		play = true;
	}
}
