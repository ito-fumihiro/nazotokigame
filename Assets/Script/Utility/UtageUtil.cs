using Assets.Script.Controller.Manager;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utage;

namespace Assets.Script.Utility
{
    /// <summary>
    /// 宴のユーティリティクラス
    /// </summary>
    public sealed class UtageUtil:MonoBehaviour
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		private UtageUtil() {}

        /// <summary>
        /// 宴の設定変更を行うためのコントローラクラスを取得する
        /// </summary>
        /// <returns>NazotokiAdvEngineController</returns>
        static public NazotokiAdvEngineController GetNazotokiAdvEngineController()
        {
            Scene scene = SceneManager.GetSceneByName("Manager");
            foreach (var rootGameObject in scene.GetRootGameObjects())
            {
                var nazotokiAdvEngineController = rootGameObject.GetComponent<NazotokiAdvEngineController>();
                if (nazotokiAdvEngineController != null)
                {
                    return nazotokiAdvEngineController;
                }
            }
            return null;
        }

        /// <summary>
        /// 宴のEngineを取得する
        /// </summary>
        /// <returns>NazotokiAdvEngineController</returns>
        static public AdvEngine GetNazotokiAdvEngine()
        {
            Scene scene = SceneManager.GetSceneByName("Manager");
            foreach (var rootGameObject in scene.GetRootGameObjects())
            {
                var nazotokiAdvEngine = rootGameObject.GetComponent<AdvEngine>();
                if (nazotokiAdvEngine != null)
                {
                    return nazotokiAdvEngine;
                }
            }
            return null;
        }

        /// <summary>
        /// 宴のUIをデフォルト(何も表示されていない)状態にする
        /// </summary>
        static public void InitGetNazotokiAdvUi()
        {
            var engine = GetNazotokiAdvEngine();
            ChangeNazotokiAdvUiStatus(AdvUiManager.UiStatus.Default);
            engine.UiManager.Open();
        }

        /// <summary>
        /// ステータス変更
        /// </summary>
        /// <param name="status">ステータス</param>
        static public void ChangeNazotokiAdvUiStatus(AdvUiManager.UiStatus status)
        {
            var engine = GetNazotokiAdvEngine();
            engine.UiManager.Status = status;
        }

        /// <summary>
        /// 宴でBGMを鳴らす
        /// </summary>
        /// <param name="bgmLabel">BGMのラベル</param>
        /// <returns></returns>
        static public IEnumerator CoUtagePlayBgm(string bgmLabel)
        {
            // TODO:ほんとにこれでよい？
            var engine = GetNazotokiAdvEngine();
            var path = engine.DataManager.SettingDataManager.SoundSetting.LabelToFilePath(bgmLabel, SoundType.Bgm);

            AssetFile file = AssetFileManager.Load(path, null);

            while (!file.IsLoadEnd) yield return null;
            engine.SoundManager.PlayBgm(file);
        }
        
    }
}