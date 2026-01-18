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
            if (sisterMesh.GetComponent<RoomEntity>().overrideMaterial)
            {
                mat = sisterMesh.GetComponent<MeshRenderer>().materials[sisterMesh.GetComponent<RoomEntity>().overrideMaterialIndex];
            }
            else
            {
                mat = sisterMesh.GetComponent<MeshRenderer>().material;
            }
            
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
        base.Choice(isCorrectChoice);
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
                SoundManager.Instance.sfxSource.PlayOneShot(SoundManager.Instance.explosion);
                explodeMesh.SetActive(true);
                sisterMesh.SetActive(false);
                regularMesh.SetActive(false) ;
            }
        }
    }

}
