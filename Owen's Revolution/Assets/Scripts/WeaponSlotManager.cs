using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HO
{
    public class WeaponSlotManager : MonoBehaviour
    {
        public WeaponHolderSlot leftHandSlot;
        public WeaponHolderSlot rightHandSlot;

        public DamageCollider leftHandDamageCollider;
        public DamageCollider rightHandDamageCollider;

        public DamageCollider damageCollider;

        private void Awake()
        {
            //DamageCollider[] damageCollider = GetComponentsInChildren<DamageCollider>();
            //foreach(DamageCollider damageCollider1 in damageCollider)
            //{
            //    if(damageCollider1.damageCollider.enabled != true)
            //    {
            //        leftHandDamageCollider = damageCollider1;
            //        rightHandDamageCollider = damageCollider1;
            //        damageCollider0 = damageCollider1;
            //    }
            //}


            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if(weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if(weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
            }
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if(isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
            }
            else
            {
                rightHandSlot.LoadWeaponModel(weaponItem);;
                LoadRightWeaponDamageCollider();
            }
        }
        
        #region Handle Weapon's Damage Collider 

        private void LoadLeftWeaponDamageCollider()
        {
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }
        
        private void LoadRightWeaponDamageCollider()
        {
            print("CACA");
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            //c'erst ici que ça bug? PARCE QU"IL NE RECONNAIT PAS currentWeaponModel
            print("Hello i just assigned bitch");
        }

        public void OpenRightDamageCollider()
        {
            print("SAY MY NAME");
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            print("reassigned");
            rightHandDamageCollider.EnableDamageCollider();
            print("Enabled collider");
            rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>().EnableDamageCollider(); 
            print("Reenabled");
        }

        public void OpenLeftDamageCollider()
        {
            leftHandDamageCollider.EnableDamageCollider();
        }

        public void CloseRightDamageCollider()
        {
            rightHandDamageCollider.DisableDamageCollider();
            print("Closed collider");
        }

        public void CloseLeftDamageCollider()
        {
            leftHandDamageCollider.DisableDamageCollider();
        }
        #endregion
    }
}