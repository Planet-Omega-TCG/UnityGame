using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;

    public static int instantsAvailable;

    private void Awake() {
        instance = this;
        instantsAvailable = Random.Range(1, 6) + Random.Range(1,6) + Random.Range(1, 6) + Random.Range(1, 6);
    }
}
