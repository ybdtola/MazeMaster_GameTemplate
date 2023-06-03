//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;
//using TMPro;


//public class Globals : MonoBehaviour
//{
//    public static int Episode = 0;
//    public static int Success = 0;
//    public static int Fail = 0;
//    public TextMeshProUGUI txtDebug = null;

//    public static void ScreenText()
//    {
//        if (txtDebug == null)
//        {
//            GameObject.Find("txtDebug").gameObject.GetComponent<TextMeshProUGUI>();
//        }
//        float SuccessPercent = (Success / (float)(Success + Fail)) * 100;
//        if (txtDebug != null)
//        {
//            txtDebug.text = $"Episode= {Episode}, Success= {Success}, Fail= {Fail} %{SuccessPercent.ToString("0")}";
//        }
//    }
//}
