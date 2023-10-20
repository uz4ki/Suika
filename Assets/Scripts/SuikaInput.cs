using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuikaInput : MonoBehaviour
{
    public static bool DropButtonDown => Input.GetKeyDown(KeyCode.Space);
}
