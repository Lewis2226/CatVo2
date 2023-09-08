using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public int tier, good_Yarn, bad_Yarn;

    bool isDragged, hasDestination;

    Vector3 destination, offset;

    public Yarn yarn;
    // Start is called before the first frame update
    void Start()
    {
        Set_Cat();

        InvokeRepeating("Take_Yarn",1,3);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDragged)
        {
         if(hasDestination)
         {
           if(Vector3.Distance(transform.position,destination)> .5f)
           {
            transform.position = Vector3.MoveTowards(transform.position,destination, 1 * Time.deltaTime);
           }
           else
           {
            hasDestination= false;
           }
         }
         else
         {
            destination = new Vector3(Random.Range(GameManager.gameManager.Fence.bounds.extents.x -0.15f, (GameManager.gameManager.Fence.bounds.extents.x * -1) + 0.15f), Random.Range(GameManager.gameManager.Fence.bounds.extents.y -0.5f,(GameManager.gameManager.Fence.bounds.extents.y * -1) + 0.5f),0);
            hasDestination = true;
         }
        }
    }

    public void Set_Cat()
    {
        GetComponent<SpriteRenderer>().sprite = GameManager.gameManager.Cat_Sprites[tier];

        bad_Yarn = tier / 5;
        if(tier != 0)
        good_Yarn = bad_Yarn + 1;
        else 
        good_Yarn = bad_Yarn;
    }

    public void Evolve()
    {
        tier ++;
         Set_Cat();
    }

    public void Take_Yarn()
    {
      Yarn new_Yarn = Instantiate(yarn, transform.position, Quaternion.identity, null);
    }





    private void OnMouseDown()
    {
       offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,10));
    }

    private void OnMouseDrag()
    {
        isDragged = true;

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,10)) + offset;
    }

    private void OnMouseUp()
    {
        isDragged = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(isDragged)
       {
        if(collision.tag == "Cat")
        {
            if(collision.GetComponent<Cat>().tier == tier)
            {
                Evolve();

                Destroy(collision.gameObject);
            }
        }
       }

     }

     private void OnTriggerStay2D(Collider2D collision)
     {
        if(isDragged)
        {
            if(collision.tag == "Cat")
            {
                if (collision.GetComponent<Cat>().tier == tier)
                {
                    Evolve();

                    Destroy(collision.gameObject);
                }
            }
        }
     }
}
