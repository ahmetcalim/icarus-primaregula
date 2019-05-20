using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    public Image image;
    public Sprite unSelected;
    public Sprite selected;
    public InputManager.HandType handType;
    private void Start()
    {
        ChangeSprite(true);
        if (handType == InputManager.hand)
        {
            ChangeSprite(false);
        }
    }
    public void ChangeSprite(bool isSelected)
    {
       
        switch (handType)
        {
            case InputManager.HandType.Left:
                if (isSelected)
                {
                    image.sprite = unSelected;
                }
                else
                {
                    InputManager.hand = handType;
                    image.sprite = selected;
                }
                break;
            case InputManager.HandType.Right:
                if (isSelected)
                {
                    image.sprite = unSelected;
                }
                else
                {
                    InputManager.hand = handType;
                    image.sprite = selected;
                }
                break;
            default:
                break;
        }
        Debug.Log(InputManager.hand);

    }
}
