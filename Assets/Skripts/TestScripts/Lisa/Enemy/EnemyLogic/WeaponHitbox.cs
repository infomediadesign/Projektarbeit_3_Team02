using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;


public class CounterUI : EnemyBase
{
    protected PlayerHealth playerHealth;
    public Image timerImage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Attack()
    {
    }
    public override void StopAttack()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("trigger");
            playerHealth = other.GetComponent<PlayerHealth>();

           
                playerHealth.TakeDamage(stats.damage);
                Debug.Log("taking damage: " + stats.damage);
            
            
        }
    }
    public void UpdateTimer(float time, float maxTime)
    {
        if (timerImage != null)
        {
            timerImage.fillAmount = time / maxTime; 
        }
    }

    public void ResetTimer()
    {
        if (timerImage != null)
        {
            timerImage.fillAmount = 1f;
        }
    }
}
