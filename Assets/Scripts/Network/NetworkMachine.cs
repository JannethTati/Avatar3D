using Deforestation.Machine;
using UnityEngine;
using Photon.Pun;
using System;
using static UnityEngine.Rendering.DebugUI;

namespace Deforestation.Network
{ 

public class NetworkMachine : MonoBehaviourPun
{
    #region Fields

    [SerializeField] private MachineController _machine;
    public Transform _machineFollow;
    private NetworkGameController _gameController;

        #endregion

        #region Properties

        #endregion

        #region Unity Callbacks

        [System.Obsolete]
        private void Start()

        {
            _gameController = FindObjectOfType<NetworkGameController>(true);
            if (photonView.IsMine)
            {
                _gameController = InitializeMachine(_machineFollow, _machine);
                _gameController.gameObject.SetActive(true);
                _machine.enabled = true;
                _machine.WeaponController.enabled = true;
                _machine.GetComponent<MachineMovement>().enabled = true;
                _machine.HealthSystem.OnHealthChanged += SyncHealth;
                _machine.WeaponController.OnMachineShoot += SyncShoot;
            }
            else 
            {
                //---
            }
        }

        

        private NetworkGameController InitializeMachine(Transform machineFollow, MachineController machine)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Public Methods
        private void SyncShoot()
        {

            RaycastHit hit;
            Ray ray = _gameController.MainCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            photonView.RPC("OtherShoot", RpcTarget.Others);

        }

        [PunRPC]
        private void OtherShoot(Vector3 shootDirection)
        {
            _machine.WeaponController.Shoot(shootDirection);
        }

        private void SyncHealth(float value)
        {
            photonView.RPC("RefreshHealth", RpcTarget.Others, value);
        }

        [PunRPC]

        private void RefreshHealth(float value)
        {
            _machine.HealthSystem.SetHealth(value);
        }
        #endregion

        #region Private Methods

        #endregion
    }

}