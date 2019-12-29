using UnityEngine;
using UnityEngine.TextCore;
using System.Collections.Generic;
using Common.Data;

namespace BallBlast
{
    // --------------------------------------------------
    // LevelController.cs
    // --------------------------------------------------

    public class LevelController : MonoBehaviour
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("Data")]
        public TextAsset JSON;
        public JSONData dataJSON;
        public PlayerData playerData;
        public LevelData levelData;

        [Header("Events")]
        public SimpleEvent onGameAwake;
        public SimpleEvent onGameStart;
        public SimpleEvent onLevelStart;
        public SimpleEvent onLevelRestart;
        public SimpleEvent onGameOver;

        [Header("Info")]
        public bool isPause;

        // --------------------------------------------------
        // FUNDAMENTAL
        // --------------------------------------------------

        void Awake()
        {
            isPause = true;
            levelData.level = 1;
            onGameAwake.TRIGGER();
        }

        void Start()
        {
            ParseJSON();
            ResetPlayer();
            SetInitialLevelParameters();

            onGameStart.TRIGGER();
        }

        // --------------------------------------------------
        // METHODS
        // --------------------------------------------------

        public void START_LEVEL()
        {
            if (isPause)
            {
                Time.timeScale = 1;

                isPause = false;

                onLevelStart.TRIGGER();
            }
        }

        public void RESTART_LEVEL()
        {
            Time.timeScale = 1;

            ResetPlayer();

            onLevelRestart.TRIGGER();
        }

        public void NEXT_LEVEL()
        {
            isPause = true;

            levelData.level++;

            ResetPlayer();
        }

        public void GAME_OVER()
        {
            Time.timeScale = 0;

            onGameOver.TRIGGER();
        }

        // --------------------------------------------------
        // FUNCTIONS
        // --------------------------------------------------

        private void ParseJSON()
        {
            dataJSON.gameDataJSON = new GameDataJSON();
            dataJSON.gameDataJSON = JsonUtility.FromJson<GameDataJSON>(JSON.text);
        }

        private void ResetPlayer()
        {
            playerData.position = new Vector2(0, playerData.position.y);
            playerData.positionTarget = new Vector2(0, playerData.position.y);
            playerData.speed = new Vector2(0, 0);
        }

        private void SetInitialLevelParameters()
        {
            Physics.gravity = new Vector3(0, dataJSON.gameDataJSON.gravity, 0);
        }
    }
}
