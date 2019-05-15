/* Author : Raphaël Marczak - 2018, for ENSEIRB-MATMECA
 * 
 * This work is licensed under the CC0 License. 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents the cardinal directions (South, North, West, East)
public enum CardinalDirections { CARDINAL_S, CARDINAL_N, CARDINAL_W, CARDINAL_E };
public enum HeroColor {NONE, RED, GREEN, BLUE}

public class PlayerBehavior : MonoBehaviour
{
    public float m_speed = 1f; // Speed of the player when he moves
    private CardinalDirections m_direction; // Current facing direction of the player
	public static HeroColor h_color = HeroColor.NONE; // Sets the base hero color to NONE

    public Sprite m_frontSprite = null;
    public Sprite m_leftSprite = null;
    public Sprite m_rightSprite = null;
    public Sprite m_backSprite = null;

	//red player
	public Sprite m_REDfrontSprite = null; 
    public Sprite m_REDleftSprite = null;
    public Sprite m_REDrightSprite = null;
    public Sprite m_REDbackSprite = null;
	
	//green player
	public Sprite m_GREENfrontSprite = null;
    public Sprite m_GREENleftSprite = null;
    public Sprite m_GREENrightSprite = null;
    public Sprite m_GREENbackSprite = null;
	
	//blue player
	public Sprite m_BLUEfrontSprite = null;
    public Sprite m_BLUEleftSprite = null;
    public Sprite m_BLUErightSprite = null;
    public Sprite m_BLUEbackSprite = null;
	
    public GameObject m_fireBall = null; // Object the player can shoot
	
	public GameObject m_REDfrontFireBall = null; // Object the player can shoot (red fireball)
	public GameObject m_REDleftFireBall = null; // Object the player can shoot (red fireball)
	public GameObject m_REDrightFireBall = null; // Object the player can shoot (red fireball)
	public GameObject m_REDbackFireBall = null; // Object the player can shoot (red fireball)
	
	public GameObject m_GREENfrontFireBall = null; // Object the player can shoot (green fireball)
	public GameObject m_GREENleftFireBall = null; // Object the player can shoot (green fireball)
	public GameObject m_GREENrightFireBall = null; // Object the player can shoot (green fireball)
	public GameObject m_GREENbackFireBall = null; // Object the player can shoot (green fireball)
	
	public GameObject m_BLUEfrontFireBall = null; // Object the player can shoot (blue fireball)
	public GameObject m_BLUEleftFireBall = null; // Object the player can shoot (blue fireball)
	public GameObject m_BLUErightFireBall = null; // Object the player can shoot (blue fireball)
	public GameObject m_BLUEbackFireBall = null; // Object the player can shoot (blue fireball)

    public GameObject m_map = null;
    public DialogManager m_dialogDisplayer;

    private Dialog m_closestNPCDialog;
	
    Rigidbody2D m_rb2D;
    SpriteRenderer m_renderer;

    void Awake()
    {
        m_rb2D = gameObject.GetComponent<Rigidbody2D>();
        m_renderer = gameObject.GetComponent<SpriteRenderer>();

        m_closestNPCDialog = null;
    }

    // This update is called at a very precise and constant FPS, and
    // must be used for physics modification
    // (i.e. anything related with a RigidBody)
    void FixedUpdate()
    {
        // If a dialog is on screen, the player should not be updated
        // If the map is displayed, the player should not be updated
        if (m_dialogDisplayer.IsOnScreen() || m_map.activeSelf)
        {
            return;
        }

        // Moves the player regarding the inputs
        Move();
    }

    private void Move()
    {
        float horizontalOffset = Input.GetAxis("Horizontal");
        float verticalOffset = Input.GetAxis("Vertical");

        // Translates the player to a new position, at a given speed.
        Vector2 newPos = new Vector2(transform.position.x + horizontalOffset,
                                     transform.position.y + verticalOffset)
                                     * m_speed;
        m_rb2D.MovePosition(newPos);

        // Computes the player main direction (North, Sound, East, West)
        if (Mathf.Abs(horizontalOffset) > Mathf.Abs(verticalOffset))
        {
            if (horizontalOffset > 0)
            {
                m_direction = CardinalDirections.CARDINAL_E;
            }
            else
            {
                m_direction = CardinalDirections.CARDINAL_W;
            }
        }
        else if (Mathf.Abs(horizontalOffset) < Mathf.Abs(verticalOffset))
        {
            if (verticalOffset > 0)
            {
                m_direction = CardinalDirections.CARDINAL_N;
            }
            else
            {
                m_direction = CardinalDirections.CARDINAL_S;
            }
        }
    }


    // This update is called at the FPS which can be fluctuating
    // and should be called for any regular actions not based on
    // physics (i.e. everything not related to RigidBody)
    private void Update()
    {
        // If the player presses M, the map will be activated if not on screen
        // or desactivated if already on screen
        if (Input.GetKeyDown(KeyCode.M))
        {
            m_map.SetActive(!m_map.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // If a dialog is on screen, the player should not be updated
        // If the map is displayed, the player should not be updated
        if (m_dialogDisplayer.IsOnScreen() || m_map.activeSelf)
        {
            return;
        }
        ChangeSpriteToMatchDirection();

        // If the player presses SPACE, then two solution
        // - If there is a dialog ready to be displayed (i.e. the player is closed to a NPC)
        //   then a dialog is set to the dialogManager
        // - If not, then the player will shoot a fireball
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_closestNPCDialog != null)
            {
                m_dialogDisplayer.SetDialog(m_closestNPCDialog.GetDialog());
            }
			/*
            else 
            {
                ShootFireball();
            }
			*/
        }
		if (Input.GetKeyDown(KeyCode.Z))
        {
            ShootFireball('z');
        }
		if (Input.GetKeyDown(KeyCode.Q))
        {
            ShootFireball('q');
        }
		if (Input.GetKeyDown(KeyCode.S))
        {
            ShootFireball('s');
        }
		if (Input.GetKeyDown(KeyCode.D))
        {
            ShootFireball('d');
        }
		if ((Input.GetKeyDown(KeyCode.Alpha1)) && (h_color == HeroColor.RED))
        {
            h_color = HeroColor.NONE;
        }
		else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            h_color = HeroColor.RED;
        }
		if ((Input.GetKeyDown(KeyCode.Alpha2)) && (h_color == HeroColor.GREEN))
        {
            h_color = HeroColor.NONE;
        }
		else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            h_color = HeroColor.GREEN;
        }
		if ((Input.GetKeyDown(KeyCode.Alpha3)) && (h_color == HeroColor.BLUE))
        {
            h_color = HeroColor.NONE;
        }
		else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            h_color = HeroColor.BLUE;
        }
    }

    // Changes the player sprite regarding it position
    // (back when going North, front when going south, right when going east, left when going west)
    private void ChangeSpriteToMatchDirection()
    {
		if(h_color == HeroColor.NONE)
		{
			if (m_direction == CardinalDirections.CARDINAL_N)
			{
				m_renderer.sprite = m_backSprite;
			}
			else if (m_direction == CardinalDirections.CARDINAL_S)
			{
				m_renderer.sprite = m_frontSprite;
			}
			else if (m_direction == CardinalDirections.CARDINAL_E)
			{
				m_renderer.sprite = m_rightSprite;
			}
			else if (m_direction == CardinalDirections.CARDINAL_W)
			{
				m_renderer.sprite = m_leftSprite;
			}
		}
		if(h_color == HeroColor.RED)
		{
			if (m_direction == CardinalDirections.CARDINAL_N)
			{
				m_renderer.sprite = m_REDbackSprite;
			}
			else if (m_direction == CardinalDirections.CARDINAL_S)
			{
				m_renderer.sprite = m_REDfrontSprite;
			}
			else if (m_direction == CardinalDirections.CARDINAL_E)
			{
				m_renderer.sprite = m_REDrightSprite;
			}
			else if (m_direction == CardinalDirections.CARDINAL_W)
			{
				m_renderer.sprite = m_REDleftSprite;
			}
		}
		if(h_color == HeroColor.GREEN)
		{
			if (m_direction == CardinalDirections.CARDINAL_N)
			{
				m_renderer.sprite = m_GREENbackSprite;
			}
			else if (m_direction == CardinalDirections.CARDINAL_S)
			{
				m_renderer.sprite = m_GREENfrontSprite;
			}
			else if (m_direction == CardinalDirections.CARDINAL_E)
			{
				m_renderer.sprite = m_GREENrightSprite;
			}
			else if (m_direction == CardinalDirections.CARDINAL_W)
			{
				m_renderer.sprite = m_GREENleftSprite;
			}
		}
		if(h_color == HeroColor.BLUE)
		{
			if (m_direction == CardinalDirections.CARDINAL_N)
			{
				m_renderer.sprite = m_BLUEbackSprite;
			}
			else if (m_direction == CardinalDirections.CARDINAL_S)
			{
				m_renderer.sprite = m_BLUEfrontSprite;
			}
			else if (m_direction == CardinalDirections.CARDINAL_E)
			{
				m_renderer.sprite = m_BLUErightSprite;
			}
			else if (m_direction == CardinalDirections.CARDINAL_W)
			{
				m_renderer.sprite = m_BLUEleftSprite;
			}
		}
    }

    // Creates a fireball, and launches it
    private void ShootFireball(char key)
    {
		if(h_color == HeroColor.NONE)
		{
			GameObject newFireball = Instantiate(m_fireBall, this.transform) as GameObject;

			FireBehavior fireBallBehavior = newFireball.GetComponent<FireBehavior>();

			if (fireBallBehavior != null)
			{
				if (key == 'z')
				{
					fireBallBehavior.Launch(new Vector2(0f, 1f));
				}
				if (key == 's')
				{
					fireBallBehavior.Launch(new Vector2(0f, -1f));
				}
				if (key == 'd')
				{
					fireBallBehavior.Launch(new Vector2(1f, 0f));
				}
				if (key == 'q')
				{
					fireBallBehavior.Launch(new Vector2(-1f, 0f));
				}
			}
		}
		else if(h_color == HeroColor.RED)
		{
			if (key == 'z')
			{
				GameObject newFireball = Instantiate(m_REDfrontFireBall, this.transform) as GameObject;
			
				FireBehavior fireBallBehavior = newFireball.GetComponent<FireBehavior>();

				if (fireBallBehavior != null)
				{
					fireBallBehavior.Launch(new Vector2(0f, 1f));
				}
			}
			if (key == 's')
			{
				GameObject newFireball = Instantiate(m_REDbackFireBall, this.transform) as GameObject;
			
				FireBehavior fireBallBehavior = newFireball.GetComponent<FireBehavior>();

				if (fireBallBehavior != null)
				{
					fireBallBehavior.Launch(new Vector2(0f, -1f));
				}
			}
			if (key == 'd')
			{
				GameObject newFireball = Instantiate(m_REDrightFireBall, this.transform) as GameObject;
			
				FireBehavior fireBallBehavior = newFireball.GetComponent<FireBehavior>();

				if (fireBallBehavior != null)
				{
					fireBallBehavior.Launch(new Vector2(1f, 0f));
				}
			}
			if (key == 'q')
			{
				GameObject newFireball = Instantiate(m_REDleftFireBall, this.transform) as GameObject;
			
				FireBehavior fireBallBehavior = newFireball.GetComponent<FireBehavior>();

				if (fireBallBehavior != null)
				{
					fireBallBehavior.Launch(new Vector2(-1f, 0f));
				}
			}
		}
		if(h_color == HeroColor.GREEN)
		{
			if (key == 'z')
			{
				GameObject newFireball = Instantiate(m_GREENfrontFireBall, this.transform) as GameObject;
			
				FireBehavior fireBallBehavior = newFireball.GetComponent<FireBehavior>();

				if (fireBallBehavior != null)
				{
					fireBallBehavior.Launch(new Vector2(0f, 1f));
				}
			}
			if (key == 's')
			{
				GameObject newFireball = Instantiate(m_GREENbackFireBall, this.transform) as GameObject;
			
				FireBehavior fireBallBehavior = newFireball.GetComponent<FireBehavior>();

				if (fireBallBehavior != null)
				{
					fireBallBehavior.Launch(new Vector2(0f, -1f));
				}
			}
			if (key == 'd')
			{
				GameObject newFireball = Instantiate(m_GREENrightFireBall, this.transform) as GameObject;
			
				FireBehavior fireBallBehavior = newFireball.GetComponent<FireBehavior>();

				if (fireBallBehavior != null)
				{
					fireBallBehavior.Launch(new Vector2(1f, 0f));
				}
			}
			if (key == 'q')
			{
				GameObject newFireball = Instantiate(m_GREENleftFireBall, this.transform) as GameObject;
			
				FireBehavior fireBallBehavior = newFireball.GetComponent<FireBehavior>();

				if (fireBallBehavior != null)
				{
					fireBallBehavior.Launch(new Vector2(-1f, 0f));
				}
			}
		}
		if(h_color == HeroColor.BLUE)
		{
			if (key == 'z')
			{
				GameObject newFireball = Instantiate(m_BLUEfrontFireBall, this.transform) as GameObject;
			
				FireBehavior fireBallBehavior = newFireball.GetComponent<FireBehavior>();

				if (fireBallBehavior != null)
				{
					fireBallBehavior.Launch(new Vector2(0f, 1f));
				}
			}
			if (key == 's')
			{
				GameObject newFireball = Instantiate(m_BLUEbackFireBall, this.transform) as GameObject;
			
				FireBehavior fireBallBehavior = newFireball.GetComponent<FireBehavior>();

				if (fireBallBehavior != null)
				{
					fireBallBehavior.Launch(new Vector2(0f, -1f));
				}			
			}
			if (key == 'd')
			{
				GameObject newFireball = Instantiate(m_BLUErightFireBall, this.transform) as GameObject;
			
				FireBehavior fireBallBehavior = newFireball.GetComponent<FireBehavior>();

				if (fireBallBehavior != null)
				{
					fireBallBehavior.Launch(new Vector2(1f, 0f));
				}			
			}
			if (key == 'q')
			{
				GameObject newFireball = Instantiate(m_BLUEleftFireBall, this.transform) as GameObject;
			
				FireBehavior fireBallBehavior = newFireball.GetComponent<FireBehavior>();

				if (fireBallBehavior != null)
				{
					fireBallBehavior.Launch(new Vector2(-1f, 0f));
				}
			}
		}
    }


    // This is automatically called by Unity when the gameObject (here the player)
    // enters a trigger zone. Here, two solutions
    // - the player is in an NPC zone, then he grabs the dialog information ready to be
    //   displayed when SPACE will be pressed
    // - the player is in an instantDialog zone, then he grabs the dialog information and
    //   displays it instantaneously
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            m_closestNPCDialog = collision.GetComponent<Dialog>();
        }
        else if (collision.tag == "InstantDialog")
        {
            Dialog instantDialog = collision.GetComponent<Dialog>();
            if (instantDialog != null)
            {
                m_dialogDisplayer.SetDialog(instantDialog.GetDialog());
            }
        }
		else if (collision.tag == "Ennemy")
		{
			PlayerHealth.curHealth -= 5;
		}
    }

    // This is automatically called by Unity when the gameObject (here the player)
    // leaves a trigger zone. Here, two solutions
    // - the player was in an NPC zone, then the dialog information is removed
    // - the player was in an instantDialog zone, then the instantDialog is destroyed
    //   (as it has been displayed, and must only be displayed once)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            m_closestNPCDialog = null;
        }
        else if (collision.tag == "InstantDialog")
        {
            Destroy(collision.gameObject);
        }
    }
}
