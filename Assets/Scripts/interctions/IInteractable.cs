using UnityEngine;

namespace Deforestation.Interaction
{



}
    public interface IInteractable
{
        public void Interact();
        public InteractableInfo GetInfo();
    void Interact(Transform _target);
}

    //MOver a otro archivo
    [System.Serializable]
    public class InteractableInfo
{
        public string Action;
        public string Type;
}



