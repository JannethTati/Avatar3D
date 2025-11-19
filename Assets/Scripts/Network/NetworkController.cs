using Photon.Realtime;
using Photon.Pun;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UIElements;
using Deforestation.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using Random = UnityEngine.Random;

namespace Deforestation.Network
{
    public class NetworkController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private UINetwork _ui;
        [SerializeField] private UIGameController uIGameController;

        //Master
        [SerializeField] private List<Transform> _spawnPoints;
        private int _indexSpawn = 0;

        private bool _waitingForSpawn = false;

        private GameObject _machine;
        private GameObject _player;

        void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinOrCreateRoom("DeforestationRoom", new RoomOptions { MaxPlayers = 10 }, null);
        }

      
        public override void OnJoinedRoom()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                
                _indexSpawn = 0;
            }
            else
            {
                _waitingForSpawn = true;
                photonView.RPC("RPC_SpawnPoint", RpcTarget.MasterClient);
            }

            _ui.LoadingComplete();
        }

        [Obsolete]
        private void SpawnMe(Vector3 spawnPoint)
        {
            _player = PhotonNetwork.Instantiate("PlayerMultiplayer", spawnPoint, Quaternion.identity);
            _machine = PhotonNetwork.Instantiate("TheMachine", spawnPoint + Vector3.back * 7, Quaternion.identity);

            //dead Controll
            _player.GetComponent<HealthSystem>().OnDeath += PlayerDie;
            _machine.GetComponent<HealthSystem>().OnDeath += MachineDie;

            uIGameController.enabled = true;
        }

        [PunRPC]
        void RPC_SpawnPoint()
        {
            _indexSpawn++;
            if (_indexSpawn >= _spawnPoints.Count)
                _indexSpawn = 0;
            photonView.RPC("RPC_RecivePont", RpcTarget.Others, _spawnPoints[0].position);
        }

        [PunRPC]
        [Obsolete]
        void RPC_RecivePont(Vector3 spawnPos)
        {
            if (_waitingForSpawn)
            {
                _waitingForSpawn = false;
                SpawnMe(spawnPos);
            }
        }

        [Obsolete]
        private void MachineDie()
        {
            if (GameController.Instance.MachineModeOn)
            {
                GameController.Instance.MachineMode(false);
                _player.GetComponent<HealthSystem>().TakeDamage(1000);
            }

            SpawnExplosion(_machine.transform.position + Vector3.up * 4, 5, 5);
            DestroyImmediate(_machine);
        }

        public void SpawnExplosion(Vector3 centerPoint, int numberOfExplosions = 4, float maxdistance = 5f)
        {
            for (int i = 0; i < numberOfExplosions; i++)
            {
                Vector3 randomDirection = Random.insideUnitSphere;
                Vector3 spawnPosition = centerPoint + randomDirection.normalized * Random.Range(0f, maxdistance);
                Instantiate(_explosionPrefab, spawnPosition, Quaternion.identity);
            }
        }

        private void PlayerDie()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
            _ui.EndGamePanel.SetActive(true);


        }
    }
}
