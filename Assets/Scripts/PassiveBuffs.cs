using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveBuffs : MonoBehaviour
{
    public int powerID;
    public float speedIncrease;
    public float dashSpeedIncrease;
    public float dashTimeIncrease;
    public float dashCooldownDecrease;
    private PlayerMovement powerUp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            powerUp = player.GetComponent<PlayerMovement>();

            switch (powerID) 
            {
                case 1:
                    powerUp.speed += speedIncrease;
                    powerUp.dash += dashSpeedIncrease;
                    Destroy(this.gameObject);
                    break;
                case 2:
                    powerUp.timeDashing += dashTimeIncrease;
                    Destroy(this.gameObject);
                    break;
                case 3:
                    powerUp.dashcoolsec -= dashCooldownDecrease;
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}
