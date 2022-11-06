using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderForTower : MonoBehaviour
{
    float startX;
    float endX = 0;
    float startWidth;
    float endWith;
    float fillSpeed = 1f / 5f;
    RectTransform myRect;
    float timer = 0;
    float height;
    Transform tower;
    // Start is called before the first frame update
    void Start()
    {
        myRect = GetComponent<RectTransform>();
        endWith = myRect.rect.width;
        startX = myRect.localPosition.x - endWith/2;
        height = myRect.rect.height;
        myRect.localPosition = new Vector3(startX, myRect.localPosition.y,0);
        myRect.sizeDelta = new Vector2(0, height);
    }
    public void SetTower(Transform t)
    {
        tower = t;
    }
    public void AnchorToTowwer()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; 
        float width = Mathf.Lerp(startWidth, endWith, timer);
        float x = Mathf.Lerp(startX, endX, timer);
        myRect.localPosition = new Vector3(x, myRect.localPosition.y);
        myRect.sizeDelta = new Vector2(width,height);
    }
}
