using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float maxOrthographicSize = 20; //max zoom out
    public float minOrthographicSize = 1;

    private bool singleTouchMoved = false;
    private float lastDistance;

    public Camera camera;
    public Text PlayerUpgradePointsUI;
    public Text PlayerHealthLevelUI;
    public Text PlayerAttackLevelUI;
    public GameObject PlayerHealthBarUI;
    public Menu menu;
    
    private List<TapBlock> blocks;
    private Player player;

    //returns the block at location Vector2 pos
    //Vector 2 pos is in Screen space coords
    private TapBlock getBlockAtLocation(Vector2 pos)
    {
        foreach(TapBlock b in blocks)
        {
            if(b.pointIsInside(camera.ScreenToWorldPoint(pos)))
            {
                return b;
                break;
            }
        }
        return null;
    }

    
    private float pixelUnitToWorldUnitScaler()
    {
        return (camera.orthographicSize*2)/(Screen.height);
    }
    /*
    public Vector2 pixelUnitToWorldUnit(Vector2 point)
    {
        Vector2 pixelToWorldScalar;
        pixelToWorldScalar.y = camera.orthographicSize*2;
        pixelToWorldScalar.x = pixelToWorldScalar.y * (Screen.width/Screen.height);

        pixelToWorldScalar.x /= Screen.width ;// / pixelToWorldScalar.x;
        pixelToWorldScalar.y /= Screen.height;// / pixelToWorldScalar.y;

        return new Vector2(pixelToWorldScalar.x * point.x, pixelToWorldScalar.y * point.y); 
    }
    */

    public Player getPlayer()
    {
        return player;
    }

    private void updateInput()
    {
        if(!menu.isOpen())
        {
            if(Input.touchCount == 1 )
            {
                Touch touch = Input.GetTouch(0);
                switch(touch.phase)
                {
                    case TouchPhase.Began:
                        singleTouchMoved = false;
                    break;
    
                    case TouchPhase.Moved:
                        singleTouchMoved = true;
                        Vector2 newChange = new Vector2(-touch.deltaPosition.x,-touch.deltaPosition.y)*(pixelUnitToWorldUnitScaler());
                        camera.transform.Translate(newChange.x, newChange.y,0);
                    break;
    
                    case TouchPhase.Ended:
                        if(singleTouchMoved == false)
                        {
                            TapBlock block = getBlockAtLocation(touch.position);
                            if(block)
                            {
                                block.Tap(this);
                            }
                        }
                    break;
                }
    
            }
            if(Input.touchCount == 2)
            {
                singleTouchMoved = false;
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);
                
                float Distance = Vector2.Distance(touch1.position, touch2.position);
                Vector2 middlePoint = Vector2.Lerp(touch1.position, touch2.position, 0.5f);
                Vector2 middlePointDelta = Vector2.Lerp(touch1.deltaPosition, touch2.deltaPosition, 0.5f);
                
                if(touch2.phase == TouchPhase.Began)
                {
                    lastDistance = Distance;
                }
    
                if(touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
                {
                    float distanceDelta = Distance - lastDistance;
                    camera.orthographicSize -= (distanceDelta/256)*camera.orthographicSize;
                    camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minOrthographicSize, maxOrthographicSize);
                    lastDistance = Distance;
    
                    Vector2 change = middlePointDelta * pixelUnitToWorldUnitScaler();
                    camera.transform.Translate(-change, 0);
                }
                
            }
        }
    }

    public void Start()
    {
        camera = Camera.main;
        blocks = new List<TapBlock>(FindObjectsOfType<TapBlock>());
        player = new Player();
    }

    public void Update()
    {
        updateInput();
        player.update();
        PlayerHealthLevelUI.text = player.Health + "/" + player.maxHealth;
        PlayerAttackLevelUI.text = player.AttackPower + "/" + player.maxAttackPower;
        PlayerHealthBarUI.transform.localScale = new Vector3(player.currentHealth/player.Health, PlayerHealthBarUI.transform.localScale.y, PlayerHealthBarUI.transform.localScale.z);
        PlayerUpgradePointsUI.text = "Upgrade Points:  " + player.upgradePoints;
    }

    public void UIfunc_upgradePlayerHealth(){getPlayer().upgradeHealth();}
    public void UIfunc_upgradePlayerAttackPower(){getPlayer().upgradeAttackPower();}
}
