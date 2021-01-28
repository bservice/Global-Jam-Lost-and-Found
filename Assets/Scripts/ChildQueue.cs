using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildQueue : MonoBehaviour
{
    public Child activeChild;
    private PickUp activeItem;
    public List<Child> possibleChildren;

    private float prevChildX;
    private float prevChildY;

    // Start is called before the first frame update
    void Start()
    {
        prevChildX = 0.0f;
        prevChildY = 0.0f;
        CreateNewChild(prevChildX, prevChildY);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateChild();
    }

    public bool CheckChildItem()
    {
        return activeChild.CheckItem(activeItem);
    }

    public void AddActiveItem(PickUp item)
    {
        activeItem = item;
    }

    //Checks if the child recieved the right item and removes child from queue
    //TO-DO: 
    // -Update the score based on if it was right or not
    public void UpdateChild()
    {
        if (activeItem != null)
        {
            bool test = CheckChildItem();
            activeItem = null;
            prevChildX = activeChild.transform.position.x;
            prevChildY = activeChild.transform.position.y;
            activeChild.transform.position = new Vector2(200.0f, 200.0f);
            activeChild = null;
            CreateNewChild(prevChildX, prevChildY);
        }
    }

    //Creates new child
    public void CreateNewChild(float x, float y)
    {
        int rand = Random.Range(0, possibleChildren.Count);
        activeChild = possibleChildren[rand];
        possibleChildren.RemoveAt(rand);

        activeChild.transform.position = new Vector2(x, y);
    }
}
