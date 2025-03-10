using UnityEngine;
using System.Collections;

public class Crocodile : MonoBehaviour
{
    private bool animFinished = false;
    private Animator anim;
    public BoxCollider2D colliderBox;
    public BoxCollider2D triggerBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.IsTouching(triggerBox))
        {
            
             Debug.Log("trigger crocodile");
             StartCoroutine(WaitAndStartAnimation());
                
            
        }
    }
    public void AnimStart()
    {
        anim.SetBool("start", true);

    }
    public void AnimFinished()
    {
        animFinished = true;
        anim.SetBool("start", false);
        anim.SetBool("finished", true);
        StartCoroutine(FreezeAnimationAtEnd());
        Destroy(colliderBox.gameObject);
    }

    private IEnumerator FreezeAnimationAtEnd()
    {
        yield return new WaitForEndOfFrame(); 

        anim.speed = 0; 
        anim.enabled = false; 
    }
    private IEnumerator WaitAndStartAnimation()
    {
        yield return new WaitForSeconds(3f);

        AnimStart();

        
        
    }
}
