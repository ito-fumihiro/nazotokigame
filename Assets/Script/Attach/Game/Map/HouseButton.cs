using Assets.Script.Enum;
using Assets.Script.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Attach.Game.Map
{
    /// <summary>
    /// ハウスボタン
    /// </summary>
    public class HouseButton : MonoBehaviour
    {
        #region SerializeField

        /// <summary>
        /// 押したときに移動するハウスID
        /// </summary>
        [SerializeField]
        private string _searchHouseId;

        /// <summary>
        /// 選択不可の時のスプライト
        /// </summary>
        [SerializeField]
        private Sprite _spDisabled;

        /// <summary>
        /// 選択可の時のスプライト
        /// </summary>
        [SerializeField]
        private Sprite _spEnabled;

        /// <summary>
        /// クリア後のスプライト
        /// </summary>
        [SerializeField]
        private Sprite _spComplete;

        /// <summary>
        /// ステージの解放される順番
        /// </summary>
        [SerializeField]
        private int _releaseOrder;
        public int ReleaseOrder { get { return _releaseOrder; } }

        #endregion

        /// <summary>
        /// ハウスボタンの現在のステータス
        /// </summary>
        private HouseButtonStatus _status;

        /// <summary>
        /// ハウスボタンのステータスを設定
        /// </summary>
        /// <param name="status"></param>
        public void SetStatus(HouseButtonStatus status)
        {
            _status = status;
            var image = this.GetComponent<Image>();
            switch (_status)
            {
                case HouseButtonStatus.Disabled:
                    image.sprite = _spDisabled;
                    image.raycastTarget = false;
                    break;
                case HouseButtonStatus.Enabled:
                    image.sprite = _spEnabled;
                    image.raycastTarget = true;
                    break;
                case HouseButtonStatus.Complete:
                    image.sprite = _spComplete;
                    image.raycastTarget = false;
                    break;
                default:
                    break;
            }
        }

        #region アクション定義

        /// <summary>
        /// ハウスボタンの押下
        /// </summary>
        public void Clicked()
        {
            GameUtil.GetGameSceneController().ChangeGameMode(GameMode.Seach);

            // クリックしたハウスIDのハウスオブジェクトを配置、セットアップする
            GameUtil.GetSearchController().SetupSerchGroup(_searchHouseId);
        }

        #endregion

    }
}
