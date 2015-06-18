using UnityEngine;
using System.Collections;

public class PlayVoiceHint : MonoBehaviour
{
	[SerializeField] float initialDelay;
	[SerializeField] float delayBetweenRepeats;

	private AudioSource audioClip;
	private float initialPlayWait;
	private bool initialPlayOccured;
	private float timeToNextPlay;
	private bool playerInHintArea;

	// Use this for initialization
	void Start ()
	{
		audioClip          = gameObject.GetComponent<AudioSource> ();
		initialPlayWait    = initialDelay;
		timeToNextPlay     = delayBetweenRepeats;
		initialPlayOccured = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (playerInHintArea)
		{
			if (!initialPlayOccured)
			{
				if (initialPlayWait < 0.0f || Mathf.Approximately(initialPlayWait, 0.0f))
				{
					audioClip.Play();
					initialPlayOccured = true;
				}
				else
				{
					initialPlayWait -= Time.deltaTime;
				}
			}
			else
			{
				if (timeToNextPlay < 0.0f || Mathf.Approximately(initialPlayWait, 0.0f))
				{
					audioClip.Play();
					timeToNextPlay = delayBetweenRepeats;
				}
				else
				{
					timeToNextPlay -= Time.deltaTime;
				}
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			playerInHintArea = true;
		}
	}
	
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			playerInHintArea = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			playerInHintArea = false;
		}
	}
}
