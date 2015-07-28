/* 
Make a billboard out of an object in the scene 
The camera will auto-place to get the best view of the object so no need for camera adjustment 

To use - place an object in an empty scene with just camera and any lighting you want. 
Add this script to your scene camera and link to the object you want to render. 
Press play and you will get a snapshot of the object (looking down the +Z-axis at it) saved out to billboard.png in your project folder 
Any pixels colored the same as the camera background color will be transparent 
*/ 

var objectToRender : GameObject; 
var imageWidth : int = 128; 
var imageHeight : int = 128; 

function Start() 
{ 
    if (!objectToRender) return; 

    //grab the main camera and mess with it for rendering the object - make sure orthographic 
    var cam : Camera = Camera.main; 
    cam.orthographic = true; 
     
    //render to screen rect area equal to out image size 
    var rw : float = imageWidth; rw /= Screen.width; 
    var rh : float = imageHeight; rh /= Screen.height; 
    cam.rect = Rect(0,0,rw,rh); 
     
    //grab size of object to render - place/size camera to fit 
    var bb : Bounds = objectToRender.GetComponent(Renderer).bounds; 
     
        //place camera looking at centre of object - and backwards down the z-axis from it 
    cam.transform.position = bb.center; 
    cam.transform.position.z = -1.0 + (bb.min.z * 2.0); 
        //make clip planes fairly optimal and enclose whole mesh 
    cam.nearClipPlane = 0.5; 
    cam.farClipPlane = -cam.transform.position.z + 10.0 + bb.max.z; 
        //set camera size to just cover entire mesh 
    cam.orthographicSize = 1.01 * Mathf.Max( (bb.max.y - bb.min.y)/2.0, (bb.max.x - bb.min.x)/2.0); 
    cam.transform.position.y += cam.orthographicSize * 0.05; 

    //render 
    yield new WaitForEndOfFrame(); 
     
    var tex = new Texture2D( imageWidth, imageHeight, TextureFormat.ARGB32, false ); 
    // Read screen contents into the texture 
    tex.ReadPixels( Rect(0, 0, imageWidth, imageHeight), 0, 0 ); 
    tex.Apply(); 

    //turn all pixels == background-color to transparent 
    var bCol : Color = cam.backgroundColor; 
    var alpha = bCol; 
    alpha.a = 0.0; 
    for(var y : int = 0; y < imageHeight; y++) 
    { 
        for(var x : int = 0; x < imageWidth; x++) 
        { 
            var c : Color = tex.GetPixel(x,y); 
            if (c.r == bCol.r) 
                tex.SetPixel(x,y,alpha); 
        } 
    } 
    tex.Apply(); 

    // Encode texture into PNG 
    var bytes = tex.EncodeToPNG(); 
    Destroy( tex ); 
    System.IO.File.WriteAllBytes(Application.dataPath + "/../billboard.png", bytes); 
}