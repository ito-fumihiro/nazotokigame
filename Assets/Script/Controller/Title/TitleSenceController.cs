using Assets.Script.Utility;
using UnityEngine;
using Utage;

namespace Assets.Script.Controller.Title
{
    /// <summary>
    /// タイトルコントローラー
    /// </summary>
    public class TitleSenceController : MonoBehaviour
    {
        /// <summary>
        /// Start
        /// </summary>
        void Start()
        {
            UtageUtil.InitGetNazotokiAdvUi();
        }

        #region アクション定義

        /// <summary>
        /// スタートボタン押下
        /// </summary>
        public void ClickedStartButton()
        {

            StartCoroutine(GameUtil.CoUnloadCurrentSceneAndLoadNextScene("Title", "Game"));
        }

        /// <summary>
        /// コンフィグボタン押下
        /// </summary>
        public void ClickedConfigButton()
        {
            UtageUtil.ChangeNazotokiAdvUiStatus(AdvUiManager.UiStatus.Config);
        }

        /// <summary>
        /// ギャラリーボタン押下
        /// </summary>
        public void ClickedGalleryButton()
        {

        }

        #endregion
    }
}
