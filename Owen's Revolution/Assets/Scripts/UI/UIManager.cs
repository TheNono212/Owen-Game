using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HO
{
    public class UIManager : MonoBehaviour
    {
        public GameObject FloatingItemTextPrefab;
        public GameObject AllUI;

        private void Start()
        {

        }

        public void NewItem(int numberOfItems, string nameOfTheItem)
        {
            if(FloatingItemTextPrefab != null)
            {
            var go = Instantiate(FloatingItemTextPrefab, transform.position, Quaternion.identity, AllUI.transform);

            go.GetComponent<TMP_Text>().text = "+" + numberOfItems.ToString() + " de " + nameOfTheItem.ToString();
            }

            Debug.Log("+ " + numberOfItems + " de " + nameOfTheItem);
        }
    }
}