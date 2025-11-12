using UnityEngine;
using System;
using System.Collections.Generic;
using static Deforestation.Recolectables.Recolectable;

namespace Deforestation.Recolectables

{ 

public class Inventory : MonoBehaviour

{
        #region Properties
        public Dictionary<RecolectableType,int> InventoryStack = new Dictionary<RecolectableType, int>();
        public Action OnInventoryUpdated;
        #endregion

        #region Fields

        #endregion

        #region Unity Callbacks

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag.Equals("Crystal"))
            {
                Recolectable item = collision.gameObject.GetComponent<Recolectable>();
                AddRecolectable(item.Type, item.Count);
                
                Debug.Log("Recolected!");
            }
        }
        public void AddRecolectable(RecolectableType type, int count)
        {
            InventoryStack.Add(type, count);
            OnInventoryUpdated?.Invoke();
        
        }

        internal bool HasResource(RecolectableType hyperCrystal)
        {
            throw new NotImplementedException();
        }

        internal void UseResource(RecolectableType hyperCrystal)
        {
            throw new NotImplementedException();
        }

        internal void AddRecolectable(RecolectableType superCrystal)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods


        #endregion

    }
}
     


