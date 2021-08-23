using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunTweens : MonoBehaviour
{
    public List<TweenTemplate> tweens = new List<TweenTemplate>();
    void Start() { SceneManager.sceneLoaded += OnSceneLoaded; DontDestroyOnLoad(gameObject); }
    private void OnDisable() { SceneManager.sceneLoaded -= OnSceneLoaded; }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        for(int i = tweens.Count-1; i >= 0; i--) {
            if (!tweens[i].dontStopOnSceneLoad) {
                tweens.RemoveAt(i);
            }
        }
    }

    void Update()
    {
        for (int i = tweens.Count-1; i > -1; i--) {
            var tween = tweens[i];
            if (tween.mTimeUntilStart > 0) {
                tween.mTimeUntilStart -= Time.deltaTime;
                continue;
            }
            else if (tween.mTotalDuration == 0) {
                tweens.RemoveAt(i);
                continue;
            }

            switch (tween.mTweenType) {
                case TweenTemplate.TweenType.Move:
                    (tween as TransformTween).mTrans.position = GetValueInTime(tween);
                    break;
                case TweenTemplate.TweenType.LocalMove:
                    (tween as TransformTween).mTrans.localPosition = GetValueInTime(tween);
                    break;
                case TweenTemplate.TweenType.Scale:
                    (tween as TransformTween).mTrans.localScale = GetValueInTime(tween);
                    break;
                case TweenTemplate.TweenType.Rotate:
                    (tween as TransformTween).mTrans.eulerAngles = GetValueInTime(tween);
                    break;
                case TweenTemplate.TweenType.RectMove:
                    (tween as RectTween).mRectT.position = GetValueInTime(tween);
                    break;
                case TweenTemplate.TweenType.RectLocalMove:
                    (tween as RectTween).mRectT.localPosition = GetValueInTime(tween);
                    break;
                case TweenTemplate.TweenType.RectRotate:
                    (tween as RectTween).mRectT.eulerAngles = GetValueInTime(tween);
                    break;
                case TweenTemplate.TweenType.RectScale:
                    (tween as RectTween).mRectT.localScale = GetValueInTime(tween);
                    break;
                case TweenTemplate.TweenType.RectSizeDelta:
                    (tween as RectTween).mRectT.sizeDelta = GetValueInTime(tween);
                    break;
                case TweenTemplate.TweenType.ImageColor:
                    (tween as ImageTween).mImage.color = GetValueInTime(tween);
                    break;
                case TweenTemplate.TweenType.SpriteColor:
                    (tween as SpriteTween).mSprite.color = GetValueInTime(tween);
                    break;
                case TweenTemplate.TweenType.UIAlpha:
                    (tween as CanvasGroupTween).mGroup.alpha = GetValueInTime(tween).x;
                    break;
                case TweenTemplate.TweenType.MaterialColor:
                    (tween as MaterialTween).mMaterial.color = GetValueInTime(tween);
                    break;
                case TweenTemplate.TweenType.AudioVolume:
                    (tween as AudioTween).mSource.volume = GetValueInTime(tween).x;
                    break;
                case TweenTemplate.TweenType.AudioPitch:
                    (tween as AudioTween).mSource.pitch = GetValueInTime(tween).x;
                    break;
                case TweenTemplate.TweenType.AudioSpatialBlend:
                    (tween as AudioTween).mSource.spatialBlend = GetValueInTime(tween).x;
                    break;
                case TweenTemplate.TweenType.AudioStereoPan:
                    (tween as AudioTween).mSource.panStereo = GetValueInTime(tween).x;
                    break;
                case TweenTemplate.TweenType.AudioReverbZoneMix:
                    (tween as AudioTween).mSource.reverbZoneMix = GetValueInTime(tween).x;
                    break;
            }

            if (tween.mDuration > 0) {
                tween.mDuration -= Time.deltaTime;
                tween.CallAction(GetValueInTime(tween));
            }
            else {
                if (tween.mLoopTimes > 0) {
                    tween.mLoopTimes--;
                    Vector4 tempEnd = tween.mEndValue;
                    tween.mEndValue = tween.mStartValue;
                    tween.mStartValue = tempEnd;
                    tween.mDuration = tween.mTotalDuration;
                }
                else {
                    tweens.RemoveAt(i);
                    tween.mEndAction?.Invoke();
                }
            }
        }
    }

    public static Vector4 GetValueInTime(TweenTemplate tween) {
        var percentDone = 1-Mathf.Max(tween.mDuration / tween.mTotalDuration, 0);
        return tween.mEase == null ? CustomLerp(tween.mStartValue, tween.mEndValue, percentDone) : CustomLerp(tween.mStartValue, tween.mEndValue, tween.mEase.Evaluate(percentDone));
    }

    // Custom lerp because the default has checks that can't go over
    static Vector4 CustomLerp(Vector4 from, Vector4 to, float percent) { return from + (to - from) * percent; }
}
