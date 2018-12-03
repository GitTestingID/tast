using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class FontMaker : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var images = Resources.LoadAll<Sprite>(@"Font\text_01");
        //Debug.Log(images.Length);
        for(int i = 0; i < images.Length; i++)
        {
            Sprite spr = images[i];
            Texture2D tex = new Texture2D((int)spr.rect.width, (int)spr.rect.height, TextureFormat.ARGB32, false);
            tex.SetPixels(0, 0, tex.width, tex.height, spr.texture.GetPixels((int)spr.rect.x, (int)spr.rect.y, tex.width, tex.height));
            var image = tex.EncodeToPNG();
            string path = string.Format("{0}{1}{2:00}{3}", Application.dataPath + @"\10_Data\Font\", "image_", i, ".png");
            Debug.Log(path);
            File.WriteAllBytes(path, image);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
