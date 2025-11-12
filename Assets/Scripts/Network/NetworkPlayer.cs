using Deforestation.Interaction;
using Deforestation.Recolectables;
using Photon.Pun;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEditor;

namespace Deforestation.Network
{ 

public class NetworkPlayer : MonoBehaviourPun
{
    #region Fields

    [SerializeField] private HealthSystem _health;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private InteractionSystem _interactions;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private FirstPersonController _fps;
    [SerializeField] private StarterAssetsInputs _inputs;
    [SerializeField] private PlayerInput _inputsPlayer;
    [SerializeField] private GameObject _3dAvatar;
    [SerializeField] private Transform _playerfollow;
    
    private NetworkGameController _gameController;
    private Animator _anim;

        #endregion

        #region Properties

        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            _anim = _3dAvatar.GetComponent<Animator>();
        }

        [System.Obsolete]
        private void Start()
    {
            _gameController = FindObjectOfType<NetworkGameController>(true);
            if (photonView.IsMine)
            {
                _gameController = InitializeMe(_health, _controller, _inventory, _interactions, _playerfollow);
                _health.OnHealthChanged += Hit;
                _health.OnDeath += Die;
                _health.enabled = true;
                _inventory.enabled = true;
                _interactions.enabled = true;
                _fps.enabled = true;
                _controller.enabled = true;

                Invoke(nameof(AddInitialCrystals), 1);
            }
            else 
            {

                DisconectPlayer();
                
            }
        }

        private void AddInitialCrystals()

        {
            _inventory.AddRecolectable(RecolectableType.SuperCrystal);
            _inventory.AddRecolectable(RecolectableType.HyperCrystal);
        }

        private void DisconectPlayer()
        {
            Destroy(_health);
            Destroy(_inventory);
            Destroy(_interactions);
            Destroy(_controller);
            Destroy(_fps);
            Destroy(_inputs);
            Destroy(_inputsPlayer);
        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    _anim.SetBool("Run", true);
                }
                else
                {
                    _anim.SetBool("Run", false);
                }
                if (Input.GetKeyUp(KeyCode.Space))
                    _anim.SetTrigger("Jump");
            }
        }
        #endregion
        
        #region Public Methods

        private void Die()
        {
            _anim.SetTrigger("Run");
            DisconectPlayer();
            this.enabled = false;
        }

        private void Hit(float obj)
        {
            _anim.SetTrigger("Hit");
        }
        private NetworkGameController InitializeMe(HealthSystem health, CharacterController controller, Inventory inventory, InteractionSystem interactions, Transform playerfollow)
        {
            throw new NotImplementedException();
        }

        private void Destroy(FirstPersonController fps)
        {
            throw new NotImplementedException();
        }
        
        #endregion

        #region Private Methods

        #endregion
    }

}