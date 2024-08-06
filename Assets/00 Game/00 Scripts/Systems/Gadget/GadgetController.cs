using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetController : ItemWorld
{
    public PlayerController playerController;

    private void Update()
    {
        FollowPlayer();
    }

    public virtual void GadgetActive()
    {

    }

    public void FollowPlayer()
    {
        if (playerController != null && this.transform.position != playerController.transform.position)
        {
            Vector3 gadgetPoint = new Vector3(1, 2, 0);
            this.transform.position = Vector3.Lerp(this.transform.position, playerController.transform.position + gadgetPoint, 10f * Time.deltaTime);
            this.transform.LookAt(playerController.transform.forward + this.transform.position);
            //this.transform.LookAt(playerController.transform);
        }
    }
}
