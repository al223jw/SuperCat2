using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

    public int offsetX = 2; // The offset so we wont get errors.

    // Theese are used to check if we need a buddy or not.
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;

    public bool reverseScale = false; // Used if the objekt is not titable.

    private float spriteWidth = 0f; // the width of our element.
    private Camera cam;
    private Transform myTransform;

    void Awake() {
        cam = Camera.main;
        myTransform = transform;
    }

    // Use this for initialization
    void Start () {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
        // Dose it still need buddys? 
	    if(hasALeftBuddy == false || hasARightBuddy == false)
        {
            // Calculate the cameras exent (half the width) of what the camera can see in the world.
            float camHorizontalExtent = cam.orthographicSize * Screen.width / Screen.height;

            //Calculate the x postition where the camera can see the edge of the sprite.
            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtent;
            float edgeVisiblePositionLeft = (myTransform.position.x + spriteWidth / 2) + camHorizontalExtent;

            // Checking if we can see the edge.
            if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
            {
                MakeNewBuddy(1);
                hasARightBuddy = true;
            }
            else if(cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false)
            {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }
        }
	}

    // Function makes a Buddy on the side requierd.
    void MakeNewBuddy(int rightOrLeft)
    {
        // Calculating the new pos for our new buddy.
        Vector3 newPos = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        //Instantiation our new buffy and storing him in a new varible.
        Transform newBuddy = Instantiate(myTransform, newPos, myTransform.rotation) as Transform;

        if(reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent;
        if(rightOrLeft > 0)
        {
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
        }
        else
        {
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
        }
    }
}
