using UnityEngine;
using System.Collections;

public class Paralaxing : MonoBehaviour {

    public Transform[] backgrounds;         // Array(List) of all the back an foregrounds to be paralaxed.

    private float[] parallaxScales;         // The proportion of the cameras movment to move the backgrounds by.

    public float smoothing = 1;             // How smooth the paralax is going to be. Make sure to set this above 0 or effect will not work.  

    private Transform cam;                  // Reference to the main camera transform.    
    private Vector3 previousCamPos;         // The position of the camera in the previous frame.

    // Called before start().
    // Used to preload.
    // Great for references.
    void Awake() {
        //Set up the camera reference.
        cam = Camera.main.transform;
    }
	// Use this for initialization
	void Start () {
        // The Previous frame had the cureent frames camera position.
        previousCamPos = cam.position;

        //Assigning coreresponding parallaxScales.
        parallaxScales = new float[backgrounds.Length];

        for(int i = 0; i < backgrounds.Length; i++){
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    for(int i = 0; i < backgrounds.Length; i++){
            //The paralex is the opposit of the camera movment bacause the previous frame multiplied by the scale.
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            //Set a target x position wich is the current position plus the parallax.
            float backgroundTargetPosX = backgrounds[i].position.x - parallax;

            // Create a target position wich is the backgrounds current posittion with its target x postition.
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Fade between the current pos and the target pos using lerp.
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // Set prev cam to the cameras pos at the end of the frame.
        previousCamPos = cam.position;
	}
}
