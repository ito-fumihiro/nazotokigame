using System;
using System.Collections;
using UnityEngine;
using Utage;

namespace Assets.Script.Controller.Manager
{
    /// <summary>
    /// ADVパートのエンジンのコントローラー
    /// </summary>
    public class NazotokiAdvEngineController : MonoBehaviour
    {

        /// <summary>
        /// Advエンジン
        /// </summary>
        public AdvEngine AdvEngine { get { return _advEngine; } }
        [SerializeField]
        protected AdvEngine _advEngine;

        /// <summary>
        /// シナリオが再生中かどうか
        /// </summary>
        public bool IsPlayingScenario { get; set; }

        /// <summary>
        /// デフォルトバリュー
        /// </summary>
        private float _defaultMessageSpeed = -1;
        private float _defaultBgm = -1;
        private float _defaultSe = -1;
        private float _defaultVoice = -1;
        private float _defaultAlpha = -1;

        #region シナリオ

        /// <summary>
        /// 指定のラベルのシナリオを再生する (終了時アクションの指定なし)
        /// </summary>
        /// <param name="label">シナリオラベル</param>
        public void JumpScenario(string label)
        {
            StartCoroutine(JumpScenarioAsync(label, null));
        }

        /// <summary>
        /// 指定のラベルのシナリオを再生する (終了時アクションの指定あり)
        /// </summary>
        /// <param name="label">シナリオラベル</param>
        /// <param name="onComplete">終了時のアクションを指定</param>
        public void JumpScenario(string label, Action onComplete)
        {
            StartCoroutine(JumpScenarioAsync(label, onComplete));
        }

        /// <summary>
        /// 指定のラベルのシナリオを再生する (開始、終了、失敗時アクションの指定あり)
        /// </summary>
        /// <param name="label">シナリオラベル</param>
        /// <param name="onStart">開始時のアクションを指定</param>
        /// <param name="onComplete">終了時のアクションを指定</param>
        /// <param name="onFailed">失敗時のアクションを指定</param>
        public void JumpScenario(string label, Action onStart, Action onComplete, Action onFailed)
        {
            if (string.IsNullOrEmpty(label))
            {
                if (onFailed != null) onFailed();
                Debug.LogErrorFormat("シナリオラベルが空です");
                return;
            }
            if (label[0] == '*')
            {
                label = label.Substring(1);
            }
            if (AdvEngine.DataManager.FindScenarioData(label) == null)
            {
                if (onFailed != null) onFailed();
                Debug.LogErrorFormat("{0}はまだロードされていないか、存在しないシナリオです", label);
                return;
            }

            if (onStart != null) onStart();
            JumpScenario(
                label,
                onComplete);
        }

        IEnumerator JumpScenarioAsync(string label, Action onComplete)
        {
            AdvEngine.JumpScenario(label);
            IsPlayingScenario = true;
            while (!AdvEngine.IsEndOrPauseScenario)
            {
                yield return null;
            }
            IsPlayingScenario = false;
            if (onComplete != null) onComplete();
        }

        #endregion

        #region コンフィグ

        /// <summary>
        /// メッセージウィンドのテキスト表示の速度を設定
        /// </summary>
        /// <param name="speed">設定値</param>
        public void SetMessageSpeed(float speed)
        {
            if (_defaultMessageSpeed < 0)
            {
                _defaultMessageSpeed = AdvEngine.Config.MessageSpeed;
            }
            AdvEngine.Config.MessageSpeed = speed;
        }

        /// <summary>
        /// メッセージウィンドのテキスト表示の速度を元に戻す
        /// </summary>
        public void ResetMessageSpeed()
        {
            if (_defaultMessageSpeed >= 0)
            {
                AdvEngine.Config.MessageSpeed = _defaultMessageSpeed;
            }
        }

        /// <summary>
        /// メッセージウィンドの透過度を設定
        /// </summary>
        /// <param name="alpha">設定値</param>
        public void SetMessageWindowAlpha(float alpha)
        {
            if (_defaultAlpha < 0)
            {
                _defaultAlpha = AdvEngine.Config.MessageWindowAlpha;
            }
            AdvEngine.Config.MessageSpeed = alpha;
        }

        /// <summary>
        /// メッセージウィンドの透過度を元に戻す
        /// </summary>
        public void ResetMessageWindowAlpha()
        {
            if (_defaultAlpha >= 0)
            {
                AdvEngine.Config.MessageSpeed = _defaultAlpha;
            }
        }

        /// <summary>
        /// BGMの音量を設定
        /// </summary>
        /// <param name="bgmVol">設定値</param>
        public void SetBgmVolume(float bgmVol)
        {
            if (_defaultBgm < 0)
            {
                _defaultBgm = AdvEngine.Config.BgmVolume;
            }
            AdvEngine.Config.BgmVolume = bgmVol;
        }

        /// <summary>
        /// BGMの音量を元に戻す
        /// </summary>
        public void ResetBgmVolume()
        {
            if (_defaultBgm >= 0)
            {
                AdvEngine.Config.BgmVolume = _defaultBgm;
            }
        }


        /// <summary>
        /// SEの音量を設定
        /// </summary>
        /// <param name="seVol">設定値</param>
        public void SetSeVolume(float seVol)
        {
            if (_defaultSe < 0)
            {
                _defaultSe = AdvEngine.Config.SeVolume;
            }
            AdvEngine.Config.SeVolume = seVol;
        }

        /// <summary>
        /// SEの音量を元に戻す
        /// </summary>
        public void ResetSeVolume()
        {
            if (_defaultSe >= 0)
            {
                AdvEngine.Config.SeVolume = _defaultSe;
            }
        }

        /// <summary>
        /// VOICEの音量を設定
        /// </summary>
        /// <param name="voiceVol">設定値</param>
        public void SetVoiceVolume(float voiceVol)
        {
            if (_defaultVoice < 0)
            {
                _defaultVoice = AdvEngine.Config.VoiceVolume;
            }
            AdvEngine.Config.VoiceVolume = voiceVol;
        }

        /// <summary>
        /// VOICEの音量を元に戻す
        /// </summary>
        public void ResetVoiceVolume()
        {
            if (_defaultVoice >= 0)
            {
                AdvEngine.Config.VoiceVolume = _defaultVoice;
            }
        }

        #endregion

    }
}