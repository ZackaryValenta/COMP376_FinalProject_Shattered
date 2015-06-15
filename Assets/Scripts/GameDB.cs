using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDB : MonoBehaviour {
	
	public Sprite[] sprites;
	
	public static List<Item> allItems = new List<Item>();
	public static List<Item> sortedItems = new List<Item>();
	
	// Use this for initialization
	void Awake () {
		
		//ITEM CREATION
		Item i0 = gameObject.AddComponent<Item>();
		i0.name = "Golden Sword";
		i0.type = Item.Type.equip;
		i0.sprite = sprites[0];
		allItems.Add(i0);
		
		//ITEM CREATION
		Item i1 = gameObject.AddComponent<Item>();
		i1.name = "Health Potion";
		i1.type = Item.Type.consumable;
		i1.sprite = sprites[1];
		allItems.Add(i1);
		
		//ITEM CREATION
		Item i2 = gameObject.AddComponent<Item>();
		i2.name = "Rejuvenation Potion";
		i2.type = Item.Type.consumable;
		i2.sprite = sprites[2];
		allItems.Add(i2);
		
		//ITEM CREATION
		Item i3 = gameObject.AddComponent<Item>();
		i3.name = "Bow";
		i3.type = Item.Type.equip;
		i3.sprite = sprites[3];
		allItems.Add(i3);
		
		//ITEM CREATION
		Item i4 = gameObject.AddComponent<Item>();
		i4.name = "Iron Sword";
		i4.type = Item.Type.equip;
		i4.sprite = sprites[4];
		allItems.Add(i4);
		
		SortAllItems();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void SortAllItems(){
		sortedItems.Clear();
		foreach(Item i in allItems){
			sortedItems.Add(i);
		}
	}
	
	public void SortItemsByType(string type){
		sortedItems.Clear();
		foreach(Item i in allItems){
			if(i.type.ToString() == type)
				sortedItems.Add(i);
		}
	}
}
