using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {
	
	public Transform selectedItem, selectedSlot, originalSlot; 
	
	public GameObject slotPrefab, itemPrefab;
	public Vector2 invetorySize = new Vector2(4,6);
	public float slotSize;
	public Vector2 windowSize;
	
	public bool canDragItem = false;
	
	// Use this for initialization
	void Start () {
		CreateInventory();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//*************//
		// Click Down  //
		//*************//
		if(Input.GetMouseButtonDown(0) && selectedItem != null){
			canDragItem = true;
			originalSlot = selectedItem.parent;
			selectedItem.GetComponent<Collider>().enabled = false;
			SetItemsColliders(false);
		}
		
		//****************//
		//  Click Pressed //
		//****************//
		if (Input.GetMouseButton (0) && selectedItem != null && canDragItem) {
			selectedItem.position = Input.mousePosition;
		}
		
		//****************//
		// Click Released //
		//****************//
		else if(Input.GetMouseButtonUp(0) && selectedItem != null){
			canDragItem = false;
			SetItemsColliders(true);
			
			if(selectedSlot == null) selectedItem.SetParent(originalSlot);
			
			else{
				if(selectedSlot.childCount > 0){
					
					//STACK ITEMS
					if(selectedItem.name == selectedSlot.GetChild(0).name &&
					   (selectedItem.GetComponent<Item>().type == Item.Type.consumable 
					 || selectedItem.GetComponent<Item>().type == Item.Type.misc)){
						Debug.Log("WE STACKED 2 ITEMS");
						selectedItem.GetComponent<Item>().IncreaseAmount(selectedSlot.GetChild(0).GetComponent<Item>().amount);
						Destroy(selectedSlot.GetChild(0).gameObject);
					}
					//SWAP ITEMS
					else{
						selectedSlot.GetChild(0).SetParent(originalSlot);
						foreach(Transform t in originalSlot) t.localPosition = Vector3.zero;
					}
				} 
				selectedItem.SetParent(selectedSlot);
			}
			selectedItem.GetComponent<Collider>().enabled = true;
			selectedItem.localPosition = Vector3.zero;
		}
	}
	
	void SetItemsColliders(bool b){
		foreach(GameObject item in GameObject.FindGameObjectsWithTag("Item")){
			item.GetComponent<Collider>().enabled = b;
		}
	}
	public void CreateInventory()
	{
		foreach(Transform t in this.transform){
			Destroy(t.gameObject);
		}
		for (int x = 1; x <= invetorySize.x; x++) {
			for(int y = 1; y <= invetorySize.y; y ++){
				GameObject slot = Instantiate(slotPrefab) as GameObject;
				slot.transform.SetParent(this.transform);
				slot.name = "slot_"+x+"_"+y;
				slot.GetComponent<RectTransform>().anchoredPosition = new Vector3(windowSize.x/(invetorySize.x+1)*x, windowSize.y / (invetorySize.y+1) * -y);
				//print ("hello "+GameDB.sortedItems.Count);
				if((x + (y - 1)*4) <= GameDB.sortedItems.Count){
					
					GameObject item = Instantiate(itemPrefab) as GameObject;
					item.transform.SetParent(slot.transform);
					item.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
					Item i = item.GetComponent<Item>();
					
					//ITEM COMPONENT VARIABLES
					i.name = GameDB.sortedItems[(x + (y - 1)*4) - 1].name;
					i.type = GameDB.sortedItems[(x + (y - 1)*4) - 1].type;
					i.sprite = GameDB.sortedItems[(x + (y - 1)*4) - 1].sprite;
					
					item.name = i.name;
					item.GetComponent<Image>().sprite = i.sprite;
					
					
				}
				
			}
		}
		
	}
}
