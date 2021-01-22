using Assets.Script.Controller.Game.Map;
using Assets.Script.Controller.Game.Quiz;
using Assets.Script.Controller.Game.Search;
using Assets.Script.Controller.Manager;
using Assets.Script.Enum;
using Assets.Script.Model;
using Assets.Script.Utility;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Utage.AdvUiManager;

namespace Assets.Script.Controller.Game
{
    /// <summary>
    /// GameSceneController
    /// </summary>
    public class GameSceneController : MonoBehaviour
    {

        #region SerializeField

        /// <summary>
        /// 探索グループ
        /// </summary>
        [SerializeField]
        private GameObject _searchGroup;
        public GameObject SearchGroup { get { return _searchGroup; } }

        /// <summary>
        /// マップグループ
        /// </summary>
        [SerializeField]
        private GameObject _mapGroup;
        public GameObject MapGroup { get { return _mapGroup; } }

        /// <summary>
        /// クイズグループ
        /// </summary>
        [SerializeField]
        private GameObject _quizGroup;
        public GameObject QuizGroup { get { return _quizGroup; } }

        #endregion


        #region 参照用

        /// <summary>
        /// ADVエンジンのコントローラの参照用
        /// </summary>
        private NazotokiAdvEngineController _nazotokiAdvEngineController;

        /// <summary>
        /// 探索コントローラの参照用
        /// </summary>
        private SearchController _searchController;
        public SearchController SearchController { get { return _searchController; } }

        /// <summary>
        /// マップコントローラの参照用
        /// </summary>
        private MapController _mapController;
        public MapController MapController { get { return _mapController; } }

        /// <summary>
        /// クイズコントローラの参照用
        /// </summary>
        private QuizController _quizController;
        public QuizController QuizController { get { return _quizController; } }

        #endregion


        #region 現在のゲーム情報

        /// <summary>
        /// 現在のステージ情報
        /// </summary>
        private StageInfo _currentStageInfo;
        public StageInfo CurrentStageInfo { get { return _currentStageInfo; } set { _currentStageInfo = value; } }

        /// <summary>
        /// 現在のハウスID
        /// </summary>
        private HouseInfo _currentHouseInfo;
        public HouseInfo CurrentHouseInfo { get { return _currentHouseInfo; } set { _currentHouseInfo = value; } }

        /// <summary>
        /// 現在の装備アイテムID
        /// </summary>
        private int _currentEauipItemId;

        /// <summary>
        /// 現在の部屋のクリア状況
        /// </summary>
        private int _completeHouseCount;
        public int CompleteHouseCount { get { return _completeHouseCount; } set { _completeHouseCount = value; } }

        /// <summary>
        /// 現在のゲームモード
        /// </summary>
        private GameMode _currentGameMode;
        public GameMode CurrentGameMode { get { return _currentGameMode; } }

        /// <summary>
        /// 現在の取得アイテムコレクション (IDを保持)
        /// </summary>
        private List<string> _currentGetItemList;
        public List<string> CurrentGetItemList { get { return _currentGetItemList; } }

        /// <summary>
        /// 現在のシナリオ
        /// </summary>
        private string _currentScenarioLabel;

        #endregion


        #region ロードデータ

        /// <summary>
        /// アイテム情報
        /// </summary>
        private List<ItemInfo> _itemInfoList;
        public List<ItemInfo> ItemInfoList { get { return _itemInfoList; } }

        /// <summary>
        /// クイズ情報
        /// </summary>
        private List<QuizInfo> _quizInfoList;
        public List<QuizInfo> QuizInfoList { get { return _quizInfoList; } }

        /// <summary>
        /// ステージ情報
        /// </summary>
        public List<StageInfo> _stageInfoList;
        public List<StageInfo> StageInfoList { get { return _stageInfoList; } }

        /// <summary>
        /// ハウス情報
        /// </summary>
        public List<HouseInfo> _houseInfoList;
        public List<HouseInfo> HouseInfoList { get { return _houseInfoList; } }

        #endregion

        /// <summary>
        /// Start
        /// </summary>
        void Start()
        {
            // よく使用するため、コントローラの参照をあらかじめ取得しておく
            _nazotokiAdvEngineController = UtageUtil.GetNazotokiAdvEngineController();
            _searchController = SearchGroup.GetComponent<SearchController>();
            _mapController = _mapGroup.GetComponent<MapController>();
            _quizController = _quizGroup.GetComponent<QuizController>();

            // 初期化
            _currentGetItemList = new List<string>();
            _completeHouseCount = 1;

            // TODO: 外部ファイルから読み込み
            _itemInfoList = new List<ItemInfo>();
            _quizInfoList = new List<QuizInfo>();
            _stageInfoList = new List<StageInfo>();
            _houseInfoList = new List<HouseInfo>();

            // アイテム情報を読み込む
            var item1 = new ItemInfo();
            item1.Id = "item_sample_1";
            item1.Type = ItemType.Equipment;
            item1.Name = "ほげ";
            item1.Description = "アイテム説明１";
            item1.ThmFilePath = "Utage/Dialog/Inventory/ItemThumbnail/thm_sample_001";
            item1.OrgFilePath = "Utage/Dialog/ItemDetail/ItemOrg/org_sample_001";
            item1.SpThm = Resources.Load<Sprite>(item1.ThmFilePath);
            item1.SpOrg = Resources.Load<Sprite>(item1.OrgFilePath);
            ItemInfoList.Add(item1);

            var item2 = new ItemInfo();
            item2.Id = "item_sample_2";
            item2.Type = ItemType.Equipment;
            item2.Name = "ふが";
            item2.Description = "アイテム説明２";
            item2.ThmFilePath = "Utage/Dialog/Inventory/ItemThumbnail/thm_sample_002";
            item2.OrgFilePath = "Utage/Dialog/ItemDetail/ItemOrg/org_sample_002";
            item2.SpThm = Resources.Load<Sprite>(item2.ThmFilePath);
            item2.SpOrg = Resources.Load<Sprite>(item2.OrgFilePath);
            ItemInfoList.Add(item2);

            var item3 = new ItemInfo();
            item3.Id = "item_sample_3";
            item3.Type = ItemType.UseRooms;
            item3.Name = "ハゲ";
            item3.Description = "アイテム説明３";
            item3.ThmFilePath = "Utage/Dialog/Inventory/ItemThumbnail/thm_sample_003";
            item3.OrgFilePath = "Utage/Dialog/ItemDetail/ItemOrg/org_sample_003";
            item3.SpThm = Resources.Load<Sprite>(item3.ThmFilePath);
            item3.SpOrg = Resources.Load<Sprite>(item3.OrgFilePath);
            ItemInfoList.Add(item3);

            var item4 = new ItemInfo();
            item4.Id = "item_sample_4";
            item4.Type = ItemType.UseBossRoom;
            item4.Name = "手紙";
            item4.Description = "直接届けられない想いを伝えるために……";
            item4.ThmFilePath = "Utage/Dialog/Inventory/ItemThumbnail/thm_sample_004";
            item4.OrgFilePath = "Utage/Dialog/ItemDetail/ItemOrg/org_sample_004";
            item4.SpThm = Resources.Load<Sprite>(item4.ThmFilePath);
            item4.SpOrg = Resources.Load<Sprite>(item4.OrgFilePath);
            ItemInfoList.Add(item4);

            CurrentGetItemList.Add(item1.Id);
            CurrentGetItemList.Add(item2.Id);

            var quizInfo1 = new QuizInfo()
            {
                Id = "quiz_sample_1",
                CorrectAnswer = "興味",
                GetItemId = "item_sample_4",
                BeforeScenarioLabel = "Test1",
                EndScenarioLabel = "Test2",
                HintScenarioLabelList = new List<string>(),
                QuestionObjectFilePath = "Game/Prefab/Quiz/Question/SampleQuestion"
            };
            _quizInfoList.Add(quizInfo1);

            var quizInfo2 = new QuizInfo()
            {
                Id = "quiz_sample_2",
                CorrectAnswer = "兵",
                GetItemId = "item_sample_4",
                BeforeScenarioLabel = "Test3",
                EndScenarioLabel = "Test4",
                HintScenarioLabelList = new List<string>(),
                QuestionObjectFilePath = "Game/Prefab/Quiz/Question/Sample2Question"
            };
            _quizInfoList.Add(quizInfo2);

            var houseInfo1 = new HouseInfo()
            {
                Id = "house_sample_1",
                BeforeScenarioLabel = "Test1",
                EndScenarioLabel = "Test2",
                QuizId = "quiz_sample_1",
                HouseObjectFilePath = "Game/Prefab/Search/House/SampleHouse"
            };
            _houseInfoList.Add(houseInfo1);

            var houseInfo2 = new HouseInfo()
            {
                Id = "house_sample_2",
                BeforeScenarioLabel = "Test4",
                EndScenarioLabel = "Test3",
                QuizId = "quiz_sample_2",
                HouseObjectFilePath = "Game/Prefab/Search/House/Sample2House"
            };
            _houseInfoList.Add(houseInfo2);

            var stageInfo1 = new StageInfo()
            {
                Id = "stage_sample_1",
                StageObjectFilePath = "Game/Prefab/Map/Stage/SampleStage",
                EpilogueScenarioLabel = "Test1",
                PrologueScenarioLabel = "プロローグ"
            };
            _stageInfoList.Add(stageInfo1);
            _currentStageInfo = stageInfo1;

            // 最初はシナリオシーン
            _currentScenarioLabel = stageInfo1.PrologueScenarioLabel;
            ChangeGameMode(GameMode.Senario);
        }


        /// <summary>
        /// アイテムを取得する
        /// </summary>
        /// <param name="itemId"></param>
        public void GetItem(string itemId)
        {
            CurrentGetItemList.Add(itemId);
        }

        /// <summary>
        /// アイテムを装備する
        /// </summary>
        /// <param name="itemId"></param>
        public void EquipItem(string itemId)
        {
            var itemInfo = ItemInfoList.Where(x => x.Id == itemId).FirstOrDefault();
            var equipImage = _searchController.SearchEquipItemImage.GetComponent<Image>();
            equipImage.sprite = itemInfo.SpThm;

            UtageUtil.ChangeNazotokiAdvUiStatus(UiStatus.Default);
        }

        /// <summary>
        /// アイテムを使用する
        /// </summary>
        /// <param name="itemId"></param>
        public void UseItem(string itemId)
        {
            _quizController.QuestionContorller.ReleaseQuestionObstacles(itemId);
            _currentGetItemList.Remove(itemId);

            UtageUtil.ChangeNazotokiAdvUiStatus(UiStatus.Default);
        }

        /// <summary>
        /// ゲームモードを変更する
        /// </summary>
        public void ChangeGameMode(GameMode gameMode)
        {
            _currentGameMode = gameMode;

            bool isActiveSearch = (_currentGameMode == GameMode.Seach);
            bool isActiveMap = (_currentGameMode == GameMode.Map);
            bool isActiveQuiz = (_currentGameMode == GameMode.Quiz);

            _searchGroup.SetActive(isActiveSearch);
            _mapGroup.SetActive(isActiveMap);
            _quizGroup.SetActive(isActiveQuiz);

            switch (_currentGameMode)
            {
                case GameMode.Senario:
                    _searchController.SetSearchCanvasesEnabled(false);
                    _nazotokiAdvEngineController.JumpScenario(_currentScenarioLabel, OnCompleteSenaril);
                    break;

                    // シナリオが終了した際の処理
                    void OnCompleteSenaril()
                    {
                        if (_currentScenarioLabel == _currentStageInfo.PrologueScenarioLabel)
                        {
                            GameUtil.GetMapController().SetupMapObject(_currentStageInfo.Id);
                        }
                        else if (_currentScenarioLabel == _currentStageInfo.EpilogueScenarioLabel)
                        {
                            // TODO: 一旦タイトル画面に戻しちゃう
                            StartCoroutine(GameUtil.CoUnloadCurrentSceneAndLoadNextScene("Game", "Title"));
                        }
                    }

                case GameMode.Seach:
                    _searchController.SetSearchCanvasesEnabled(true);
                    break;

                case GameMode.Map:
                    _searchController.SetSearchCanvasesEnabled(false);
                    break;

                case GameMode.Quiz:
                    _searchController.SetSearchCanvasesEnabled(false);
                    break;

                default:
                    break;
            }
        }
    }

}
