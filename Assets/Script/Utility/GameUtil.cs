using Assets.Script.Controller.Game;
using Assets.Script.Controller.Game.Map;
using Assets.Script.Controller.Game.Quiz;
using Assets.Script.Controller.Game.Search;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Script.Utility
{
    /// <summary>
    /// 宴のユーティリティクラス
    /// </summary>
    public sealed class GameUtil
    {
		/// <summary>
		/// コンストラクタ
		/// </summary>
		private GameUtil() {}

        /// <summary>
        /// GameSceneController取得する
        /// </summary>
        /// <returns>GameSceneController</returns>
        static public GameSceneController GetGameSceneController()
        {
            var gameSceneController = GameObject.Find("GameSceneController").GetComponent<GameSceneController>();
            return gameSceneController;
        }

        /// <summary>
        /// SeachControllerを取得する
        /// </summary>
        /// <returns>SearchController</returns>
        static public SearchController GetSearchController()
        {
            return GetGameSceneController().SearchController;
        }

        /// <summary>
        /// MapControllerを取得する
        /// </summary>
        /// <returns>MapController</returns>
        static public MapController GetMapController()
        {
            return GetGameSceneController().MapController;
        }

        /// <summary>
        /// QuizControllerを取得する
        /// </summary>
        /// <returns>QuizController</returns>
        static public QuizController GetQuizController()
        {
            return GetGameSceneController().QuizController;
        }

        /// <summary>
        /// 現在のシーンを破棄して次のシーンを読み込む
        /// </summary>
        /// <param name="currentSceneName">現在のシーン名</param>
        /// <param name="nextSceneName">次のシーン名</param>
        /// <returns></returns>
        public static IEnumerator CoUnloadCurrentSceneAndLoadNextScene(string currentSceneName, string nextSceneName)
        {
            //TitleSceneをアンロード
            SceneManager.UnloadSceneAsync(currentSceneName);

            //BgSceneをロード
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);

            // 宴のADVを初期化
            UtageUtil.GetNazotokiAdvEngine().ClearOnEnd();

            //必要に応じて不使用アセットをアンロードしてメモリを解放する
            yield return Resources.UnloadUnusedAssets();
        }
    }
}