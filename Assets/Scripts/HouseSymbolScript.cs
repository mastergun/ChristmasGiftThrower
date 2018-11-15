using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseSymbolScript : MonoBehaviour {
    public float minRange = 3f;
    public float maxRange = 7f;
    public GameObject houseRef;
    public GameObject playerRef;
    float yPos = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //update house alert pos
        Vector3 newPos = Vector3.zero;
        SetVisibility();
        if (houseRef != null) newPos = Camera.main.WorldToScreenPoint(houseRef.transform.position);
        else Debug.Log("failed to load house ref");
        newPos.y = yPos - Screen.currentResolution.height/5;
        transform.position = newPos;
    }

    public void SetVisibility()
    {
        if (houseRef == null) GetComponent<AutoDestroy>().SelfDestruction();
        else
        {
            float distance = playerRef.transform.position.y - houseRef.transform.position.y;
            float lerpAmt = Mathf.Clamp01((distance - minRange) / (maxRange - minRange));
            // update material's alpha
            Color color = GetComponent<Image>().color;
            color.a = lerpAmt;
            GetComponent<Image>().color = color;
        }
        
        // output current values
    }

    public void SetAltitude(float altitude)
    {
        yPos = altitude;
    }
}
