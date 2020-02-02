using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sock : MonoBehaviour
{
    public Sprite mySprite;
    //for now only one feature per sock
    public SockFeature MultiFeature;
    public SockFeature ZigzagFeature;
    public SockFeature SingleFeature;
    public Color SockColor;
    public SpriteRenderer SockBaseRenderer;
    private SpriteRenderer SR;

    class SockShape
    {
        //here should be a list of pairs: target rectangle mask and feature name?/pointer?
        //this is todo much later
    }

    void MakeSpriteFromTexture()
    {
        
    }

    void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
        if (SockBaseRenderer != null)
        {
            SockBaseRenderer.color = SockColor;
        }
        mySprite = SR.sprite;
        //mySprite = null;
    }

    private void Start()
    {
        if (MultiFeature != null)
        {
            //SpriteRenderer SR = GetComponent<SpriteRenderer>(); // this is in Awake()
            Debug.Log("HEY!!!");
            //Feature.debugz = Feature.GenerateSockTexture((int)mySprite.rect.width, (int)mySprite.rect.height);
            SR.sprite = Sprite.Create(MultiFeature.GenerateSockTexture(mySprite), 
                mySprite.rect, 
                new Vector2(0.5f, 0.5f));
        }
    }

    public bool Equals(Object comparedObject)
    {
        Sock comparedSock = (Sock)comparedObject;
        if (SockColor != comparedSock.SockColor)
        {
            return false;
        }
        if (!SingleFeature?.Equals(comparedSock.SingleFeature) ?? false)
        {
            return false;
        }
        if (!MultiFeature?.Equals(comparedSock.MultiFeature) ?? false)
        {
            return false;
        }
        if (!ZigzagFeature?.Equals(comparedSock.ZigzagFeature) ?? false)
        {
            return false;
        }
        
        return true;
        //TODO
    }
}
