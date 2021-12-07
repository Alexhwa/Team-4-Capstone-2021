using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

public class IntroBunny : MonoBehaviour
{
	//	paths and path accessories
	private List<GameObject> paths;
	int iter = 0;
	float initialV = 0f;
	[SerializeField] float elapsedTime = 0f;
	[SerializeField] float timeBetweenPaths = .5f;
	float currentGravity = 0f;
	
	//	NOTE: update this if more/less movement paths used
	[SerializeField] private GameObject path1, path2, path3, path4, path5;
	
	//	current path bunny is moving on
	//	starts as 0 initially
	static int currentPath;
	
	//	if true, stop and wait for player to get close
	//	if false, move around path to next stop location
	private bool waitForPlayer = true;
	[SerializeField] GameObject interactButton;
	[SerializeField] GameObject canvas;
	
	//	vars for storing current locations/speeds
	private Vector3 prevLocation;
	private Vector3 nextLocation;

	public Animator anim;
	private SpriteRenderer sprtRnd;
	
    void Start(){
		//	init paths
		paths = new List<GameObject>();
		paths.Add(path1);
		paths.Add(path2);
		paths.Add(path3);
		paths.Add(path4);
		paths.Add(path5);
		
		GetComponent<NPC>().talkToNode = "Rabbit" + currentPath;
		
		//	set starting position based on currentPath
		if(currentPath != 0) {
			int lastPoint = paths[currentPath - 1].GetComponent<Path>().getGravs().Length;
			transform.position = paths[currentPath - 1].GetComponent<Path>().getPointFromChildren(lastPoint);
		}
		
		print("BUNNY INITIALIZED WITH PATH " + currentPath);
		nextLocation = transform.position;
		sprtRnd = GetComponent<SpriteRenderer>();
    }
    
    
	//	Somebody move this to some method that only runs when !waitForPlayer
	void Update() {
		if(!waitForPlayer) {
			// move to nextLocation if not there yet
			if(elapsedTime < timeBetweenPaths)
			{
				float convertedTime = (1 / timeBetweenPaths) * elapsedTime;
				float newX = prevLocation.x + (convertedTime * (nextLocation.x - prevLocation.x));
				float newY = prevLocation.y + (initialV * convertedTime) + (currentGravity * convertedTime * convertedTime/2);
				transform.position = new Vector3(newX, newY, 0);
				
				elapsedTime += Time.deltaTime;
			}
			
			//	if we ARE there, then reset vars and choose new nextLocation
			else {
				iter++;
				anim.Play("IdleGetUp");
				//	if we're all out of checkpoints for this path, stop
				if(paths[currentPath].GetComponent<Path>().getPoints().Length <= iter) {
					print("Bunny out of locations, waiting for player");
					waitForPlayer = true;
					currentPath++;
						interactButton.SetActive(true);
						canvas.SetActive(false);
					if (currentPath < paths.Count)
                    {
						//interactButton.transform.GetChild(0);
                    }
				}
				//	else select next point to move towards
				else {
					setVarsForNewPoint(iter);
				}
			}
		}
	}
	
	//	Only works if player enters area while out of checkpoints
	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.name == "Player")
		{
			print(collider.name + " triggered bunny");
			if(waitForPlayer) {
				/*
				//	move to next path
				anim.Play("Jump");

				//	if all out of paths, do nothing
				if(paths[currentPath] == null) {return;}
				
				setVarsForNewPoint(0);
				
				//	ideally there will always be a nextLocation, add troubleshooting case here if needed
				waitForPlayer = false;
				iter = 0;
				*/
			}
		}
		else if (collider.gameObject.tag.Equals("Cymbal"))
		{
			AudioManager.Instance.PlaySfx("single cymbal");
			gameObject.SetActive(false);
		}
	}

	//	set vars for movement to first point in path
	void setVarsForNewPoint(int iter) {
		elapsedTime = 0f;
		prevLocation = nextLocation;
		nextLocation = paths[currentPath].GetComponent<Path>().getPoint(iter);
		TryFipSprite(nextLocation.x - transform.position.x > 0);
		currentGravity = paths[currentPath].GetComponent<Path>().getGravity(iter);
		initialV = nextLocation.y - prevLocation.y - (currentGravity/2);
		//print("Bunny moving from point " + (iter - 1) + " " + prevLocation + " to point " + iter + " " + nextLocation);
	}
	
	private void TryFipSprite(bool isMovingRight)
	{
		if (isMovingRight && sprtRnd.flipX)
		{
			sprtRnd.flipX = false;
		}
		else if(!isMovingRight && !sprtRnd.flipX)
		{
			sprtRnd.flipX = true;
		}
	}

	[YarnCommand("ContinuePath")]
	public void ContinuePath()
    {
		//	move to next path
		anim.Play("Jump");

		//	if all out of paths, do nothing
		if (paths.Count <= currentPath) {
			interactButton.SetActive(false);
		}

		setVarsForNewPoint(0);

		//	ideally there will always be a nextLocation, add troubleshooting case here if needed
		waitForPlayer = false;
		iter = 0;

		interactButton.SetActive(false);
	}
}
