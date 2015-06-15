using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item : MonoBehaviour {
	
	public string name1;
	public enum Type {equip, consumable, misc};
	public Type type;
	public int amount = 1;
	
	public Sprite sprite;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseEnter(){
		transform.parent.parent.GetComponent<InventoryController> ().selectedItem = this.transform;
	}
	void OnMouseExit(){
		if(!transform.parent.parent.GetComponent<InventoryController> ().canDragItem)
			transform.parent.parent.GetComponent<InventoryController> ().selectedItem = null;
	}
	public void IncreaseAmount(int a){
		amount += a;
		transform.Find("amount.text").GetComponent<Text>().text = amount.ToString();
	}
}
