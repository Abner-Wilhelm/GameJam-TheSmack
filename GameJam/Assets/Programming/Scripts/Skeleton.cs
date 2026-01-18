using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : RoomEntity
{
    public GameObject explodeMesh;
    public GameObject regularMesh;

    public GameObject sisterMesh;
    public bool isLiving = false;



    public override void isBeingLookedAt(bool isBeingLookedAt)
    {
        base.isBeingLookedAt(isBeingLookedAt);
        if (isBeingLookedAt && !hasBeenInteractedWith && isLiving)
        {
            Material mat;
            
            
                mat = sisterMesh.GetComponent<MeshRenderer>().material;
            
            mat.SetColor("_OutlineColor", highlightColor);
        }
        else
        {
            if (isLiving)
            {
                sisterMesh.GetComponent<RoomEntity>().ChangeMaterialOutline();
            }
        }
    }
    public override void Choice(bool isCorrectChoice)
    {
        if (isLiving)
        {
                sisterMesh.GetComponent<RoomEntity>().hasBeenInteractedWith = true;
            sisterMesh.GetComponent<RoomEntity>().ChangeMaterialOutline();
            var mat = sisterMesh.GetComponent<MeshRenderer>().material;
            mat.SetColor("_OutlineColor", normalOutline);
        }
        if (isCorrectChoice)
        {

        }
        else
        {
            if (!isLiving)
            {
                explodeMesh.SetActive(true);
                regularMesh.SetActive(false);
            }
            else
            {
                explodeMesh.SetActive(true);
                sisterMesh.SetActive(false);
                regularMesh.SetActive(false) ;
            }
        }
    }

}
