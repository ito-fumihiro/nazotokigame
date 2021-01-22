﻿// UTAGE: Unity Text Adventure Game Engine (c) Ryohei Tokimura
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UtageExtensions;
using System.Collections;
using System.Collections.Generic;


namespace Utage
{

	/// <summary>
	/// イベントを受け取ってSEを鳴らす
	/// </summary>
	[AddComponentMenu("Utage/Lib/UI/UguiPointerDownSe")]
	public class UguiPointerDownSe : MonoBehaviour, IPointerDownHandler
	{
		//ポインターダウン時のSE
		public AudioClip se;
		
		//同じSEが鳴っていたら鳴らさないとか前のを止めるとか
		public SoundPlayMode playMode = SoundPlayMode.Add;

		// ポインターダウンでイベントでSEを鳴らす
		public void OnPointerDown(PointerEventData data)
		{
			//シングルクリックと左クリックのみに反応
			switch (data.pointerId)
			{
				case -1:
				case 0:
					PlaySe(playMode, se);
					break;
				default:
					break;

			}
		}

		void PlaySe(SoundPlayMode mode, AudioClip clip)
		{
			if (clip != null)
			{
				SoundManager soundManager = SoundManager.GetInstance();

				if (soundManager)
				{
					soundManager.PlaySe(clip, clip.name, mode);
				}
				else
				{
					AudioSource.PlayClipAtPoint(clip, Vector3.zero);
				}
			}
		}
	}
}