using Deforestation.UI;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Deforestation.Network

{

    public class UINetwork : MonoBehaviour

    {
       
        #region Fields

        [SerializeField] private GameObject _connectingPanel;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _retryButton;

        #endregion

        #region Properties
        public GameObject EndGamePanel;
        #endregion


        #region Unity Callsbacks
        private void Awake()

        {
            _exitButton.onClick.AddListener(Exit);
            _retryButton.onClick.AddListener(Retry);
        }

        #endregion

        #region Public Methods

       
        public void Retry()

        {
            SceneManager.LoadScene(0);
        }

        public void Exit()

        {
            Application.Quit();
        }

        internal void LoadingComplete()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        #endregion
    }
}

