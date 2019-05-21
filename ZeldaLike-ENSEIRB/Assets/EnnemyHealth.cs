using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyHealth : MonoBehaviour {

	public HeroColor e_color;
	public int maxHealth = 100; 
    public static int curHealth = 1000; 
	public int min_damage;
	public int max_damage;
	public GameObject ennemy;
     
    public float healthBarLength; 
     
    // Use this for initialization 
    void Start () 
	{ 
       healthBarLength = Screen.width / 2; 
    } 
     
    // Update is called once per frame 
    void Update () 
	{ 
       AdjustCurrentHealth(0); 
    } 
     
    void OnGUI()
	{       
       GUI.Box (new Rect (10, 40, healthBarLength, 20), curHealth + "/" + maxHealth);  
    } 
     
     
    public void AdjustCurrentHealth(int adj) 
	{ 
       curHealth += adj; 
 
       if(curHealth < 0) 
              curHealth = 0; 
 
          if (curHealth > maxHealth) 
            curHealth  = maxHealth; 
        
       if (maxHealth < 1) 
          maxHealth = 1;    
        
       healthBarLength = (Screen.width / 2) * (curHealth / (float)maxHealth);        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Fireball")
		{
			if(PlayerBehavior.h_color == HeroColor.NONE)
			{
				curHealth -= min_damage;
			}
			else if(PlayerBehavior.h_color == e_color)
			{
				curHealth -= max_damage;
			}
			if(curHealth <= 0)
			{
				Destroy(ennemy);
			}
		}
	}
}
