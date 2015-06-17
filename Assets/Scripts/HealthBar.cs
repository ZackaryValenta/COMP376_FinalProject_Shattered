using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {
	[SerializeField]
	Player player;
	// Use this for initialization
	void Start () {
	
		//Image image = GetComponent<Image>();
		//image.fillAmount = Mathf.Lerp(image.fillAmount, 1f, Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () {

		Image image = GetComponent<Image>();
		image.fillAmount = player.getCurrentHealth() / player.getMaxHealth();
	}
}
