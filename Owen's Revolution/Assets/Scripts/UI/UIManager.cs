using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HO
{
    public class UIManager : MonoBehaviour
    {

        public void NewTime(int numberOfItems, string nameOfTheItem)
        {
            Debug.Log("+ " + numberOfItems + " de " + nameOfTheItem);
        }
    }
}