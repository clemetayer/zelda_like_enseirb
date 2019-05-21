using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowTarget : MonoBehaviour {
	
	public GameObject m_target;
	public float Speed; 
	
    //This moves the player with a slope of 1 or undefined. Very limited
     
     
    void Update()
    {
    	//Go towards Player's x 
    	if(m_target.transform.position.x > transform.position.x)
    	{
    		//Go right 
    		transform.position += new Vector3 (Speed*Time.deltaTime,0,0);
    	}else{
    		//Go left 
    		transform.position -= new Vector3 (Speed*Time.deltaTime,0,0);
    	}
    	//Go towards Player's y 
    	if(m_target.transform.position.y > transform.position.y)
    	{
    		//Go up 
    		transform.position += new Vector3 (0,Speed*Time.deltaTime,0);
    	}else{
    		//Go down 
    		transform.position -= new Vector3 (0,Speed*Time.deltaTime,0);
    	}
    } 
}
