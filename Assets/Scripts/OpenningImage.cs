using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpenningImage : MonoBehaviour
{
	private Image image;
	private float textureWidthHeightRatio;

	// Use this for initialization
	void Start ()
	{
		image = gameObject.GetComponent<Image> ();
		textureWidthHeightRatio = image.mainTexture.width / image.mainTexture.height;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float screenWidth = Screen.width;
		float screenHeight = Screen.height;
		float newWidth;
		float newHeight;

		if ((screenWidth / textureWidthHeightRatio) >= screenHeight)
		{
			newWidth = screenWidth;
			newHeight = screenWidth / textureWidthHeightRatio;
		}
		else
		{
			newHeight = screenHeight;
			newWidth = screenHeight * textureWidthHeightRatio;
		}
		image.rectTransform.sizeDelta = new Vector2 (newWidth, newHeight);
	}
}
