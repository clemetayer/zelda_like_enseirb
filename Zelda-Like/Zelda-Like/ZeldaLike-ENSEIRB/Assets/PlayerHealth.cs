using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {  
     
	public string SceneName;
	public GameObject player;
    public int maxHealth = 100; 
    public static int curHealth = 100; 

    public float healthBarLength; 
     
	public void LoadGameLevel(string SceneName)
    { 
		SceneManager.LoadScene(SceneName);     
	} 
	
    // Use this for initialization 
    void Start () 
	{ 
       healthBarLength = Screen.width / 2; 
    } 
     
    // Update is called once per frame 
    void Update () 
	{ 
       AdjustCurrentHealth(0); 
	   if(curHealth == 0)
	   {
		   Destroy(player);
		   LoadGameLevel(SceneName);
	   }
    } 
     
    void OnGUI() 
	{     
       GUI.Box (new Rect (10, 10, healthBarLength, 20), curHealth + "/" + maxHealth);      
    } 
     
     
    public void AdjustCurrentHealth(int adj) 
	{ 
       curHealth -= adj; 
 
       if(curHealth < 0) 
              curHealth = 0; 
 
          if (curHealth > maxHealth) 
            curHealth  = maxHealth; 
        
       if (maxHealth < 1) 
          maxHealth = 1;    
        
       healthBarLength = (Screen.width / 2) * (curHealth / (float)maxHealth); 
             
    }
}
