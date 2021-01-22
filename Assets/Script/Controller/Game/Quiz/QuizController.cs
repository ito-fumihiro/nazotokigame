using Assets.Script.Enum;
using Assets.Script.Model;
using Assets.Script.Utility;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Controller.Game.Quiz
{
    /// <summary>
    /// QuizController
    /// </summary>
    public class QuizController : MonoBehaviour
    {
        #region SerializeField

        /// <summary>
        /// クイズグループ
        /// </summary>
        [SerializeField]
        private GameObject _quizGroup;

        /// <summary>
        /// 質問キャンバス
        /// </summary>
        [SerializeField]
        private GameObject _questionCanvas;

        /// <summary>
        /// 結果キャンバス
        /// </summary>
        [SerializeField]
        private GameObject _resultCanvas;

        /// <summary>
        /// 結果キャライメージ
        /// </summary>
        [SerializeField]
        private GameObject _resultCharaImage;

        /// <summary>
        /// 結果テキストイメージ
        /// </summary>
        [SerializeField]
        private GameObject _resultTextImage;

        /// <summary>
        /// アイテムゲットキャンバス
        /// </summary>
        [SerializeField]
        private GameObject _getItemCanvas;

        /// <summary>
        /// ヒントキャンバス
        /// </summary>
        [SerializeField]
        private GameObject _hintCanvas;

        /// <summary>
        /// UIキャンバス
        /// </summary>
        [SerializeField]
        private GameObject _uiCanvas;

        #endregion

        #region Sprite

        /// <summary>
        /// 正解時の表示キャラのスプライト
        /// </summary>
        private Sprite _spCorrectChara;

        /// <summary>
        /// 正解時の表示文字のスプライト
        /// </summary>
        private Sprite _spCorrectText;

        /// <summary>
        /// 間違い時の表示キャラのスプライト
        /// </summary>
        private Sprite _spWrongChara;

        /// <summary>
        /// 間違い時の表示文字のスプライト
        /// </summary>
        private Sprite _spWrongText;

        #endregion Sprite

        /// <summary>
        /// クイズを正解したかどうか
        /// </summary>
        private bool _isCorrectQuiz;

        /// <summary>
        /// アニメーションの時間
        /// </summary>
        private float _animTime;

        /// <summary>
        /// 現在のシナリオラベル
        /// </summary>
        private string _currentSenarioLabel;

        /// <summary>
        /// 現在のクイズモード
        /// </summary>
        private QuizMode _currentQuizMode;
        public QuizMode CurrentQuizMode { get { return _currentQuizMode; } }

        #region 参照

        /// <summary>
        /// 現在のクイズ情報の参照
        /// </summary>
        private QuizInfo _currentQuizInfo;
        public QuizInfo CurrentQuizInfo { get { return _currentQuizInfo; } }
        
        /// <summary>
        /// クイズオブジェクトの参照
        /// </summary>
        private GameObject _questionObject;
        public GameObject QuestionObject { get { return _questionObject; } }

        /// <summary>
        /// QuestionContorllerの参照
        /// </summary>
        private QuestionController _questionController;
        public QuestionController QuestionContorller { get { return _questionController; } }

        #endregion

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            // TODO: ここで読み込むのやめたい
            _spCorrectChara = Resources.Load<Sprite>("Game/Group/Quiz/Result/Character/Sogo/sogo_correct");
            _spCorrectText = Resources.Load<Sprite>("Game/Group/Quiz/Result/Text/correct");
            _spWrongChara = Resources.Load<Sprite>("Game/Group/Quiz/Result/Character/Sogo/sogo_wrong");
            _spWrongText = Resources.Load<Sprite>("Game/Group/Quiz/Result/Text/wrong");

        }

        /// <summary>
        /// Update
        /// </summary>
        void Update()
        {
            _animTime += Time.deltaTime;

            // TODO: 最適化したい
            switch (_currentQuizMode)
            {
                case QuizMode.Question:
                    break;
                case QuizMode.Result:

                    if (_animTime > 3.0f)
                    {
                        if (_isCorrectQuiz)
                        {
                            ChangeQuizMode(QuizMode.GetItem);
                        }
                        else
                        {
                            ChangeQuizMode(QuizMode.Question);
                        }

                    }
                    break;
                case QuizMode.GetItem:
                    if (_animTime > 3.0f)
                    {
                        ChangeQuizMode(QuizMode.Scenario);
                        _currentSenarioLabel = "Test6";
                        UtageUtil.GetNazotokiAdvEngineController().JumpScenario(_currentSenarioLabel);
                    }
                    break;

                case QuizMode.Hint:

                    if (!UtageUtil.GetNazotokiAdvEngineController().IsPlayingScenario)
                    {
                        ChangeQuizMode(QuizMode.Question);
                    }

                    break;

                case QuizMode.Scenario:

                    if (!UtageUtil.GetNazotokiAdvEngineController().IsPlayingScenario)
                    {
                        if (_isCorrectQuiz)
                        {
                            ChangeQuizMode(QuizMode.End);
                        }
                        else
                        {
                            ChangeQuizMode(QuizMode.Question);
                        }
                    }

                    break;

                case QuizMode.End:
                    var gameController = GameUtil.GetGameSceneController();
                    gameController.GetItem(_currentQuizInfo.GetItemId);
                    gameController.CompleteHouseCount++;
                    GameUtil.GetMapController().ReleaseHouseButton(gameController.CompleteHouseCount);
                    gameController.ChangeGameMode(GameMode.Map);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// クイズモードを変更する
        /// </summary>
        /// <param name="quizMode"></param>
        private void ChangeQuizMode(QuizMode quizMode)
        {
            _animTime = 0.0f;
            _currentQuizMode = quizMode;
            bool isQuestion = (_currentQuizMode == QuizMode.Question);
            bool isResult = (_currentQuizMode == QuizMode.Result);
            bool isGetItem = (_currentQuizMode == QuizMode.GetItem);
            bool isHint = (_currentQuizMode == QuizMode.Hint);

            _questionCanvas.SetActive(isQuestion);
            _resultCanvas.SetActive(isResult);
            _getItemCanvas.SetActive(isGetItem);
            _hintCanvas.SetActive(isHint);

            SetEnabledUICanvas();
        }

        /// <summary>
        /// クイズオブジェクトを配置し、セットアップする
        /// </summary>
        /// <param name="quizId"></param>
        public void SetupQuizGroup(string quizId)
        {
            DestroyQuizObject();

            // Setupに必要なクイズ情報を取得
            var quizInfo = GameUtil.GetGameSceneController().QuizInfoList.Where(x => x.Id == quizId).FirstOrDefault();
            var filePath = quizInfo.QuestionObjectFilePath;
            _currentQuizInfo = quizInfo;

            // QuizQuestionCanvasに質問オブジェクトを読み込む
            GameObject questionObj = (GameObject)Resources.Load(filePath);
            GameObject prefab = (GameObject)Instantiate(questionObj);
            prefab.transform.SetParent(_questionCanvas.transform, false);
            _questionObject = prefab;
            _questionController = prefab.GetComponent<QuestionController>();

            // 質問の初期化
            var question = _questionObject.GetComponent<QuestionController>();
            question.SetQuestionParam(quizInfo);

            _quizGroup.SetActive(true);

            // 最初はシナリオ画面
            _currentSenarioLabel = "Test5";
            ChangeQuizMode(QuizMode.Scenario);
            UtageUtil.GetNazotokiAdvEngineController().JumpScenario("Test5");
        }

        /// <summary>
        /// クイズオブジェクトの破棄
        /// </summary>
        private void DestroyQuizObject()
        {
            if (_questionObject != null)
            {
                Destroy(_questionObject);
            }
            _animTime = 0.0f;
            _isCorrectQuiz = false;

        }

        /// <summary>
        /// クイズグループのUICanvasの有効・無効設定
        /// </summary>
        private void SetEnabledUICanvas()
        {
            if (_currentQuizMode == QuizMode.Hint ||
                _currentQuizMode == QuizMode.Scenario ||
                _currentQuizMode == QuizMode.GetItem ||
                _currentQuizMode == QuizMode.Result)
            {
                _uiCanvas.SetActive(false);
            }
            else
            {
                _uiCanvas.SetActive(true);
            }

        }

        /// <summary>
        /// ResultCanvasを設定
        /// </summary>
        private void SetResultCanvas()
        {
            if (_isCorrectQuiz)
            {
                _resultCharaImage.GetComponent<Image>().sprite = _spCorrectChara;
                _resultTextImage.GetComponent<Image>().sprite = _spCorrectText;
            }
            else
            {
                _resultCharaImage.GetComponent<Image>().sprite = _spWrongChara;
                _resultTextImage.GetComponent<Image>().sprite = _spWrongText;
            }
        }

        #region アクション定義

        /// <summary>
        /// 回答ボタン押下
        /// </summary>
        public void ClickedAnswerButton()
        {
            var question = _questionObject.GetComponent<QuestionController>();
            _isCorrectQuiz = question.CheckAnswer();
            ChangeQuizMode(QuizMode.Result);
            SetResultCanvas();
        }

        /// <summary>
        /// ヒントボタン押下
        /// </summary>
        public void ClickedHintButton()
        {

            ChangeQuizMode(QuizMode.Hint);
            SetResultCanvas();
            // TODO: 一旦決め打ち
            UtageUtil.GetNazotokiAdvEngineController().JumpScenario("Test4");
        }

        /// <summary>
        /// バックボタン押下
        /// </summary>
        public void ClickedBackButton()
        {
            GameUtil.GetGameSceneController().ChangeGameMode(GameMode.Seach);
        }

        #endregion
    }
}
