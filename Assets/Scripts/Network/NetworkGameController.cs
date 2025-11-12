using Deforestation.Interaction;
using Deforestation.Recolectables;
using Deforestation;
using System;
using UnityEngine;
using Deforestation.Machine;

public class NetworkGameController : GameController
{
    #region Fields

    #endregion

    #region Properties

    #endregion

    #region Unity Callbacks

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Public Methods
    public void InitializePlayer(HealthSystem health, CharacterController player, Inventory inventory, InteractionSystem interaction, Transform playerFollow)
    {
        _playerHealth = health;
        _player = player;
        _inventory = inventory;
        _interactionSystem = interaction;
        _playerFollow = playerFollow;
    }
    public void InitializeMachine(Transform follow, MachineController machine)
    {
        if (_machine != null)
        { 
           _machine.HealthSystem.OnHealthChanged += _uiController.UpdateMachineHealth;
        }
        

        _machineFollow = follow;
        _machine = machine;

        _machine.HealthSystem.OnHealthChanged += _uiController.UpdateMachineHealth;
        _machine.HealthSystem.TakeDamage(0);
    }
    #endregion

    #region Private Methods

    #endregion
}

