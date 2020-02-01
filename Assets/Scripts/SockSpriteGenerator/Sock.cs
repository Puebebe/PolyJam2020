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
        SpriteRenderer SR = GetComponent<SpriteRenderer>();
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
            SpriteRenderer SR = GetComponent<SpriteRenderer>();
            Debug.Log("HEY!!!");
            //Feature.debugz = Feature.GenerateSockTexture((int)mySprite.rect.width, (int)mySprite.rect.height);
            SR.sprite = Sprite.Create(MultiFeature.GenerateSockTexture((int)mySprite.rect.width, (int)mySprite.rect.height), mySprite.rect, new Vector2(0.5f, 0.5f));
        }
    }

    public bool Equals(Object comparedObject)
    {
        Sock comparedSock = (Sock)comparedObject;
        if (SingleFeature != comparedSock.SingleFeature)
        {
            return false;
        }
        if (MultiFeature != comparedSock.MultiFeature)
        {
            return false;
        }
        if (ZigzagFeature != comparedSock.ZigzagFeature)
        {
            return false;
        }
        if (SockColor != comparedSock.SockColor)
        {
            return false;
        }
        return true;
        //TODO
    }
}
