using UnityEngine;
using System.Collections;

public class Crocodile : MonoBehaviour
{
    private bool animFinished = false;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animFinished = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
             Debug.Log("trigger crocodile");
             StartCoroutine(WaitAndStartAnimation());
                
            
        }
    }
    public void AnimStart()
    {
        anim.SetBool("finished", false);
    }
    public void AnimFinished()
    {
        animFinished = true;
        anim.SetBool("finished", true);
        
    }

    private IEnumerator WaitAndStartAnimation()
    {
        yield return new WaitForSeconds(3f);

        AnimStart();

        
        
    }
}
