using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryName : MonoBehaviour
{
    public void Destroy()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.RemoveName(this.gameObject);
    }
}
