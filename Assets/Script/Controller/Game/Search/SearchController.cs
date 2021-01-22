using Assets.Script.Utility;
using System.Linq;
using UnityEngine;
using static Utage.AdvUiManager;

namespace Assets.Script.Controller.Game.Search
{
    public class SearchController : MonoBehaviour
    {
        #region const

        /// <summary>
        /// 表示されている部屋の定数
        /// </summary>
        private const int ROOM_FRONT = 1;
        private const int ROOM_RIGHT = 2;
        private const int ROOM_BACK = 3;
        private const int ROOM_LEFT = 4;

        #endregion

        #region SerializeField

        /// <summary>
        /// 表示されるメッセージウィンドウ(ボタン)
        /// </summary>
        [SerializeField]
        private GameObject _messageButton;
        public GameObject MessageButton { get { return _messageButton; } }

        /// <summary>
        /// 表示されるメッセージ
        /// </summary>
        [SerializeField]
        private GameObject _messageButtonText;
        public GameObject MessageButtonText { get { return _messageButtonText; } }

        /// <summary>
        /// 探索グループ
        /// </summary>
        [SerializeField]
        private GameObject _searchGroup;

        /// <summary>
        /// 探索UIキャンバス
        /// </summary>
        [SerializeField]
        private GameObject _uiCanvas;

        /// <summary>
        /// 探索部屋キャンバス
        /// </summary>
        [SerializeField]
        private GameObject _houseCanvas;

        /// <summary>
        /// 装備しているアイテムのイメージ
        /// </summary>
        [SerializeField]
        private GameObject _searchEquipItemImage;
        public GameObject SearchEquipItemImage { get { return _searchEquipItemImage; } }

        #endregion

        #region 参照用

        /// <summary>
        /// 探索部屋のオブジェクト (Prefabから配置される)
        /// </summary>
        private GameObject _searchRoomObject;

        #endregion

        /// <summary>
        /// 現在表示されている部屋
        /// </summary>
        private int _displayRoom;


        /// <summary>
        /// ハウスオブジェクトを配置し、セットアップする
        /// </summary>
        /// <param name="searchRoomId">配置するハウスID</param>
        public void SetupSerchGroup(string houseId)
        {
            // HouseCanvasにHouseオブジェクトを読み込む
            DestroyRoomObject();

            // 現在のハウス情報確定
            var houseInfo = GameUtil.GetGameSceneController().HouseInfoList.Where(x => x.Id == houseId).FirstOrDefault();
            GameUtil.GetGameSceneController().CurrentHouseInfo = houseInfo;

            // ハウスを配置
            GameObject house = (GameObject)Resources.Load(houseInfo.HouseObjectFilePath);
            GameObject prefab = (GameObject)Instantiate(house);
            prefab.transform.SetParent(_houseCanvas.transform, false);
            _searchRoomObject = prefab;

            // 表示する部屋番号
            _displayRoom = ROOM_FRONT;

            _searchGroup.SetActive(true);
        }

        /// <summary>
        /// 番号に対応した部屋を表示する
        /// </summary>
        public void DisplayRoom()
        {
            switch (_displayRoom)
            {
                case ROOM_FRONT:
                    _searchRoomObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    break;
                case ROOM_RIGHT:
                    _searchRoomObject.transform.localPosition = new Vector3(-750.0f, 0.0f, 0.0f);
                    break;
                case ROOM_BACK:
                    _searchRoomObject.transform.localPosition = new Vector3(-1500.0f, 0.0f, 0.0f);
                    break;
                case ROOM_LEFT:
                    _searchRoomObject.transform.localPosition = new Vector3(-2250.0f, 0.0f, 0.0f);
                    break;
            }
        }

        /// <summary>
        /// 探索キャンバスのクリック可能かどうかを設定
        /// </summary>
        /// <param name="isEnabled">クリック可能な場合はtrue</param>
        public void SetSearchCanvasesEnabled(bool isEnabled)
        {
            // UIキャンバスは表示、非表示を切り替える
            _uiCanvas.SetActive(isEnabled);

            // クリック可否を切り替える
            var canvasGroupRooms = _houseCanvas.GetComponent<CanvasGroup>();
            canvasGroupRooms.interactable = isEnabled;
            canvasGroupRooms.blocksRaycasts = isEnabled;
        }

        /// <summary>
        /// 配置した部屋オブジェクトを破棄する
        /// </summary>
        private void DestroyRoomObject()
        {
            if (_searchRoomObject != null)
            {
                Destroy(_searchRoomObject);
            }
        }

        #region アクション定義

        /// <summary>
        /// 左に移動ボタン押下
        /// </summary>
        public void ClickedLeftButton()
        {
            _displayRoom--;
            if (_displayRoom < ROOM_FRONT)
            {
                _displayRoom = ROOM_LEFT;
            }
            DisplayRoom();
        }

        /// <summary>
        /// 右に移動ボタン押下
        /// </summary>
        public void ClickedRightButton()
        {
            _displayRoom++;
            if (_displayRoom > ROOM_LEFT)
            {
                _displayRoom = ROOM_FRONT;
            }

            DisplayRoom();

        }

        /// <summary>
        /// メッセージボタン押下
        /// </summary>
        public void ClickedMessageButton()
        {
            _messageButton.SetActive(false);
        }

        /// <summary>
        /// メニューボタン押下
        /// </summary>
        public void ClickedMenuButton()
        {
            UtageUtil.InitGetNazotokiAdvUi();
            UtageUtil.ChangeNazotokiAdvUiStatus(UiStatus.Menu);
        }

        /// <summary>
        /// インベントリボタン押下
        /// </summary>
        public void ClickedInventoryButton()
        {
            UtageUtil.InitGetNazotokiAdvUi();
            UtageUtil.ChangeNazotokiAdvUiStatus(UiStatus.Inventory);
        }

        #endregion

    }
}
