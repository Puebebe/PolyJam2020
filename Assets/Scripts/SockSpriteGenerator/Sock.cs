using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sock : MonoBehaviour
{
    public Sprite mySprite;
    //for now only one feature per sock
    public SockFeature Feature;

    class SockShape
    {
        //here should be a list of pairs: target rectangle mask and feature name?/pointer?
        //this is todo much later
    }

    void MakeSpriteFromTexture()
    {
        
    }

    void Start()
    {
        SpriteRenderer SR = GetComponent<SpriteRenderer>();
        mySprite = SR.sprite;
        //mySprite = null;
        if (Feature != null)
        {
            Debug.Log("HEY!!!");
            //Feature.debugz = Feature.GenerateSockTexture((int)mySprite.rect.width, (int)mySprite.rect.height);
            SR.sprite = Sprite.Create(Feature.GenerateSockTexture((int)mySprite.rect.width, (int)mySprite.rect.height), mySprite.rect, new Vector2(0.5f,0.5f));
        }
    }
}
