using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralPresence : Error
{
    public override void Fix()
    {
        // Destroys the game object to which this script is attached, effectively "fixing" the error
        Destroy(gameObject);
    }
}
