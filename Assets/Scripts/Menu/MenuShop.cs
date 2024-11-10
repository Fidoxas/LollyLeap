using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShop : MonoBehaviour
{
   [SerializeField] Animator anim;
   private bool expanded;

   private void Start()
   {
      expanded = false;
   }

   public void ExpandShop()
   {
      if (expanded == false)
      {
         expanded = true;
         anim.SetBool("Expanded", true);
      }
      else if (expanded)
      {
         expanded = false;
         anim.SetBool("Expanded", false);
      }
   }
}
