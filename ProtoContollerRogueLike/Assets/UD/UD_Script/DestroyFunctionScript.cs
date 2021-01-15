using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFunctionScript : MonoBehaviour
{
    public void DestroyItself()
    {
        Destroy(gameObject);
    }
}
