using System;
using UnityEngine;
using UnityEngine.UI;

// Class returned to the user so it can't call any other functions that
public abstract class TweenCActions
{
    /// <summary>Is playing the tween.</summary>
    public abstract bool isPlaying();

    /// <summary>Destroys the tweening process.</summary>
    public abstract void Stop();

    /// <summary>Code that gets run every time the tween updates.</summary>
    public abstract TweenCActions SetOnUpdate(Action<float> action);

    /// <summary>Code that gets run every time the tween updates.</summary>
    public abstract TweenCActions SetOnUpdate(Action<Color> action);

    /// <summary>Code that gets run every time the tween updates.</summary>
    public abstract TweenCActions SetOnUpdate(Action<Vector3> action);

    /// <summary>Code that gets run when the tweening ends.</summary>
    public abstract TweenCActions SetOnComplete(Action action);

    /// <summary>Set a delay before the tweening starts</summary>
    public abstract TweenCActions SetDelayBeforeStart(float seconds);

    /// <summary>Code that gets run when the tweening starts</summary>
    public abstract TweenCActions SetOnStart(Action action);

    /// <summary>Code that gets run when a loop ends</summary>
    public abstract TweenCActions SetOnLoopEnd(Action action);

    /// <summary>Set how many times it will do the tween. Every other tween is reversed. Leave out for 1.</summary>
    public abstract TweenCActions SetLooping(int loopTimes);

    /// <summary>Set if the tweening continues infinitely or not</summary>
    public abstract TweenCActions SetLooping(bool infinite);

    /// <summary>Set the style that the tween tweens with.</summary>
    /// <param name="type">Choose the tween style that you like.</param>
    public abstract TweenCActions SetEase(TweenC.EaseType type);

    /// <summary>Set the style that the tween tweens with.</summary>
    /// <param name="ease">Create a public AnimationClip that yout can edit in inspector. Then feed it here.</param>
    public abstract TweenCActions SetEase(AnimationCurve ease);

    /// <summary>Does the tweening get stopped on scene load. If not, the object shouldn't be destroyed on load either.</summary>
    public abstract TweenCActions SetDontDestroyOnSceneLoad(bool dontDestroy);
}

[Serializable]
public abstract class TweenTemplate : TweenCActions
{
    public TweenTemplate(Vector4 startValue, Vector4 endValue, float duration, TweenType type) {
        mTweenType = type;
        mEndValue = endValue;
        mStartValue = startValue;
        mTotalDuration = duration;
        mDuration = duration;
    }

    public enum TweenType
    {
        Value, Vector, Color, Timer,
        Move, LocalMove, Scale, GlobalScale, Rotate, LocalRotate,
        RectMove, RectLocalMove, RectScale, RectRotate, RectSizeDelta,
        ImageColor, // Alpha included
        SpriteColor, // Alpha included
        UIAlpha,
        MaterialColor,
        AudioVolume, AudioPitch, AudioStereoPan, AudioSpatialBlend, AudioReverbZoneMix
    }

    public TweenType mTweenType;
    public bool dontStopOnSceneLoad;
    public AnimationCurve mEase;

    public int mLoopTimes;
    public Action mEndAction;
    public Action mStartAction;
    public Action mLoopEndAction;

    public float mTotalDuration;
    public float mDuration;
    public float mTimeUntilStart;

    public Vector4 mEndValue;
    public Vector4 mStartValue;

    public abstract void CallAction(Vector4 value);
    public abstract override TweenCActions SetOnUpdate(Action<float> action);
    public abstract override TweenCActions SetOnUpdate(Action<Color> action);
    public abstract override TweenCActions SetOnUpdate(Action<Vector3> action);

    public override TweenCActions SetOnComplete(Action action) {
        mEndAction = action;
        return this;
    }

    public override TweenCActions SetDelayBeforeStart(float seconds) {
        mTimeUntilStart = seconds;
        return this;
    }

    public override TweenCActions SetOnStart(Action action) {
        mStartAction = action;
        return this;
    }

    public override TweenCActions SetOnLoopEnd(Action action) {
        mLoopEndAction = action;
        return this;
    }

    public override TweenCActions SetLooping(int loopTimes) {
        mLoopTimes = loopTimes-1;
        return this;
    }

    public override TweenCActions SetLooping(bool infinite) {
        mLoopTimes = infinite ? Int32.MaxValue : 1;
        return this;
    }

    public override TweenCActions SetDontDestroyOnSceneLoad(bool dontDestroy) {
        dontStopOnSceneLoad = dontDestroy;
        return this;
    }

    public override TweenCActions SetEase(TweenC.EaseType type) {
        Keyframe[] keys;
        switch (type) {
            #region Eases
            case TweenC.EaseType.SineIn:
                keys = new Keyframe[] { new Keyframe(0, 0, 0, 0), new Keyframe(1, 1, 1.45f, 0) };
                keys[0].weightedMode = WeightedMode.None;
                keys[1].weightedMode = WeightedMode.None;
                break;
            case TweenC.EaseType.SineOut:
                keys = new Keyframe[] { new Keyframe(0, 0, 0, 1.62f), new Keyframe(1, 1, 0, 0) };
                keys[0].weightedMode = WeightedMode.None;
                keys[1].weightedMode = WeightedMode.None;
                break;
            case TweenC.EaseType.SineInOut:
                keys = new Keyframe[] { new Keyframe(0, 0, 0, 0), new Keyframe(1, 1, 0, 0) };
                keys[0].weightedMode = WeightedMode.None;
                keys[1].weightedMode = WeightedMode.None;
                break;
            case TweenC.EaseType.CubicIn:
                keys = new Keyframe[] { new Keyframe(0, 0, 0, 3.35f), new Keyframe(1, 1, 0, 0) };
                keys[0].weightedMode = WeightedMode.None;
                keys[1].weightedMode = WeightedMode.None;
                break;
            case TweenC.EaseType.CubicOut:
                keys = new Keyframe[] { new Keyframe(0, 0, 0, 0), new Keyframe(1, 1, 3.35f, 0) };
                keys[0].weightedMode = WeightedMode.None;
                keys[1].weightedMode = WeightedMode.None;
                break;
            case TweenC.EaseType.CubicInOut:
                keys = new Keyframe[] { new Keyframe(0, 0, 0, 0), new Keyframe(0.5f, 0.5f, 3.23f, 3.23f), new Keyframe(1, 1, 0, 0) };
                for (int i = 0; i < keys.Length; i++)
                    keys[i].weightedMode = WeightedMode.None;
                break;
            case TweenC.EaseType.ExpIn:
                keys = new Keyframe[] { new Keyframe(0, 0, 0, 0), new Keyframe(0.6f, 0.025f, 0.169f, 0.169f), new Keyframe(1, 1, 7.41f, 0) };
                for (int i = 0; i < keys.Length; i++)
                    keys[i].weightedMode = WeightedMode.None;
                break;
            case TweenC.EaseType.ExpOut:
                keys = new Keyframe[] { new Keyframe(0, 0, 0, 6.87f), new Keyframe(0.4f, 0.962f, 0.245f, 0.245f), new Keyframe(1, 1, 0, 0) };
                for (int i = 0; i < keys.Length; i++)
                    keys[i].weightedMode = WeightedMode.None;
                break;
            case TweenC.EaseType.ExpInOut:
                keys = new Keyframe[] { new Keyframe(0, 0, 0, 0.33f, 0, 0.075f), new Keyframe(0.42f, 0.13f, 1.16f, 1.16f, 0.16f, 0),
                    new Keyframe(0.5f, 0.5f, 11.62f, 11.62f), new Keyframe(0.58f, 0.87f, 1.16f, 1.16f, 0, 0.16f), new Keyframe(1, 1, 0.33f, 0, 0.075f, 0) };
                for (int i = 1; i < 4; i++)
                    keys[i].weightedMode = WeightedMode.None;
                keys[0].weightedMode = WeightedMode.Both;
                keys[4].weightedMode = WeightedMode.Both;
                break;
            case TweenC.EaseType.BackIn:
                keys = new Keyframe[] { new Keyframe(0, 0, 0, 0), new Keyframe(1, 1, 4.5f, 0) };
                keys[0].weightedMode = WeightedMode.None;
                keys[1].weightedMode = WeightedMode.None;
                break;
            case TweenC.EaseType.BackOut:
                keys = new Keyframe[] { new Keyframe(0, 0, 0, 4.5f), new Keyframe(1, 1, 0, 0) };
                keys[0].weightedMode = WeightedMode.None;
                keys[1].weightedMode = WeightedMode.None;
                break;
            case TweenC.EaseType.BackInOut:
                keys = new Keyframe[] { new Keyframe(0, 0, 0, 0), new Keyframe(0.5f, 0.5f, 5.7f, 5.7f), new Keyframe(1, 1, 0, 0) };
                keys[0].weightedMode = WeightedMode.None;
                keys[1].weightedMode = WeightedMode.None;
                break;
            case TweenC.EaseType.ElasticIn:
                keys = new Keyframe[] { new Keyframe(0, 0, 0, 0, 0, 1), new Keyframe(0.4f, 0, 1.25f, 1.25f, 0.11f, 0.55f),
                    new Keyframe(0.55f, 0, -1.47f, -1.47f, 0.06f, 0.34f), new Keyframe(0.7f, 0, 3.56f, 3.56f, 0.2f, 0.52f), new Keyframe(1, 1, 16, 16, 0.515f, 0) };
                for (int i = 0; i < keys.Length; i++)
                    keys[i].weightedMode = WeightedMode.Both;
                break;
            case TweenC.EaseType.ElasticOut:
                keys = new Keyframe[] { new Keyframe(0, 0, 16, 16, 0, 0.515f), new Keyframe(0.3f, 1, 3.56f, 3.56f, 0.52f, 0.2f),
                    new Keyframe(0.45f, 1, -1.47f, -1.47f, 0.34f, 0.06f), new Keyframe(0.6f, 1, 1.25f, 1.25f, 0.55f, 0.11f), new Keyframe(1, 1, 0, 0, 1, 0) };
                for (int i = 0; i < keys.Length; i++)
                    keys[i].weightedMode = WeightedMode.Both;
                break;
            case TweenC.EaseType.ElasticInOut:
                keys = new Keyframe[] { new Keyframe(0, 0, 0, 0, 0, 1), new Keyframe(0.35f, 0, -1.95f, -1.95f, 0.18f, 0.3f),
                    new Keyframe(0.5f, 0.5f, 12.9f, 12.9f, 0.36f, 0.333f), new Keyframe(0.65f, 1, -1.95f, -1.95f, 0.44f, 0.18f), new Keyframe(1, 1, 0, 0, 0.77f, 0) };
                for (int i = 0; i < keys.Length; i++)
                    keys[i].weightedMode = WeightedMode.Both;
                break;
            case TweenC.EaseType.BounceOut:
                keys = new Keyframe[] { new Keyframe(0, 0, 0, 0.62f, 0, 0.9f), new Keyframe(0.1f, 0, -0.5f, 1, 0.63f, 0.3f),
                    new Keyframe(0.3f, 0, -0.88f, 2.5f, 0.36f, 0.17f), new Keyframe(0.6f, 0, -2.6f, 5.7f, 0.16f, 0.06f), new Keyframe(1, 1, 0, 0, 0.3f, 0) };
                for (int i = 0; i < keys.Length; i++)
                    keys[i].weightedMode = WeightedMode.None;
                break;
            case TweenC.EaseType.BounceIn:
                keys = new Keyframe[] { new Keyframe(0, 0, 0, 0f, 0, 0.3f), new Keyframe(0.4f, 1, 5.73f, -2.6f, 0.06f, 0.16f),
                    new Keyframe(0.7f, 1, 2.5f, -0.88f, 0.17f, 0.36f), new Keyframe(0.9f, 1, 0.97f, -0.51f, 0.29f, 0.63f), new Keyframe(1, 1, 0.62f, 0, 0.9f, 0) };
                for (int i = 0; i < keys.Length; i++)
                    keys[i].weightedMode = WeightedMode.None;
                break;
            case TweenC.EaseType.BounceInOut:
                keys = new Keyframe[] { new Keyframe(0, 0, 0, 0.62f, 0, 0.9f), new Keyframe(0.05f, 0, -0.5f, 1, 0.63f, 0.3f),
                    new Keyframe(0.15f, 0, -0.88f, 2.5f, 0.36f, 0.17f), new Keyframe(0.3f, 0, -2.6f, 5.7f, 0.16f, 0.06f), new Keyframe(0.5f, 0.5f, 0, 0, 0.3f, 0),
                    new Keyframe(0.7f, 1, 5.73f, -2.6f, 0.06f, 0.16f),
                    new Keyframe(0.85f, 1, 2.5f, -0.88f, 0.17f, 0.36f), new Keyframe(0.95f, 1, 0.97f, -0.51f, 0.29f, 0.63f), new Keyframe(1, 1, 0.62f, 0, 0.9f, 0)};
                for (int i = 0; i < keys.Length; i++)
                    keys[i].weightedMode = WeightedMode.None;
                break;
            default:
                return this;
                #endregion
        }
        mEase = new AnimationCurve(keys);
        return this;
    }

    public override TweenCActions SetEase(AnimationCurve ease) {
        // Normalize the curve. Need to get the first one to 0 and the last one to 1
        mEase = NormalizeCurve(ease);
        return this;
    }
    AnimationCurve NormalizeCurve(AnimationCurve curve) {
        // Sets the starting point at 0 and ending point at 1 and all other points respectively
        if (curve.length > 1) {
            float subtractValue = curve.keys[0].value;
            float devideValue = curve.keys[curve.keys.Length - 1].value - subtractValue;
            float subtractTime = curve.keys[0].time;
            float devideTime = curve.keys[curve.keys.Length - 1].time - subtractTime;
            if (devideValue != 0) {
                Keyframe[] keys = new Keyframe[curve.length];

                for (int i = 0; i < curve.length; i++) {
                    float newTime = (curve.keys[i].time - subtractTime) / devideTime;
                    float newValue = (curve.keys[i].value - subtractValue) / devideValue;

                    keys[i] = new Keyframe(newTime, newValue, curve.keys[i].inTangent / devideValue,
                        curve.keys[i].outTangent / devideValue, curve.keys[i].inWeight, curve.keys[i].outWeight);
                    keys[i].weightedMode = curve.keys[i].weightedMode;
                }

                return new AnimationCurve(keys);
            }
            else Debug.LogError("TweenCEaseError: The AnimationCurve cannot have the start and end on the same height.");
        }
        else Debug.LogError("TweenCEaseError: The AnimationCurve needs at least two keys.");

        return null;
    }

    public override void Stop() {
        mTotalDuration = 0;
    }

    public override bool isPlaying() {
        return mTotalDuration <= 0.00001f;
    }
}

public class ColorTween : TweenTemplate
{
    public ColorTween(Vector4 startValue, Vector4 endValue, float duration, TweenType type)
        : base(startValue, endValue, duration, type) { }

    public Action<Color> mUpdateAction;
    public override TweenCActions SetOnUpdate(Action<float> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a float, but ColorTween can only give a color");
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Vector3> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a vectro3, but ColorTween can only give a color");
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Color> action) {
        mUpdateAction = action;
        return this;
    }
    public override void CallAction(Vector4 value) { mUpdateAction?.Invoke(value); }
}
public class VectorTween : TweenTemplate
{
    public VectorTween(Vector4 startValue, Vector4 endValue, float duration, TweenType type)
        : base(startValue, endValue, duration, type) { }

    public Action<Vector3> mUpdateAction;
    public override TweenCActions SetOnUpdate(Action<float> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a float, but VectorTween can only give a vector3");
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Vector3> action) {
        mUpdateAction = action;
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Color> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a color, but VectorTween can only give a vector3");
        return this;
    }
    public override void CallAction(Vector4 value) { mUpdateAction?.Invoke(value); }
}
public class FloatTween : TweenTemplate
{
    public FloatTween(Vector4 startValue, Vector4 endValue, float duration, TweenType type)
        : base(startValue, endValue, duration, type) { }

    public Action<float> mUpdateAction;
    public override TweenCActions SetOnUpdate(Action<float> action) {
        mUpdateAction = action;
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Vector3> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a vector3, but FloatTween can only give a float");
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Color> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a color, but FloatTween can only give a float");
        return this;
    }
    public override void CallAction(Vector4 value) { mUpdateAction?.Invoke(value.x); }
}
public class TimerTween : TweenTemplate
{
    public Action<float> mUpdateAction;
    public TimerTween(float duration, TweenType type) : base(Vector4.zero, Vector4.zero, duration, type) { }

    public override TweenCActions SetOnUpdate(Action<float> action) {
        mUpdateAction = action;
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Vector3> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a vector3, but Timer can only give a float");
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Color> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a color, but Timer can only give a float");
        return this;
    }
    public override void CallAction(Vector4 value) { mUpdateAction?.Invoke(value.x); }
}
public class CanvasGroupTween : TweenTemplate
{
    // UIAlpha
    public CanvasGroup mGroup;
    public CanvasGroupTween(CanvasGroup group, Vector4 startValue, Vector4 endValue, float duration, TweenType type)
        : base(startValue, endValue, duration, type) {
        mGroup = group;
    }

    public Action<float> mUpdateAction;
    public override TweenCActions SetOnUpdate(Action<float> action) {
        mUpdateAction = action;
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Vector3> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a vector3, but CanvasGroup can only give a float");
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Color> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a color, but CanvasGroup can only give a float");
        return this;
    }
    public override void CallAction(Vector4 value) { mUpdateAction?.Invoke(value.x); }
}
public class TransformTween : TweenTemplate
{
    // Move, Scale, Rotate
    public Transform mTrans;
    public TransformTween(Transform trans, Vector4 startValue, Vector4 endValue, float duration, TweenType type)
        : base(startValue, endValue, duration, type) {
        mTrans = trans;
    }

    public Action<Vector3> mUpdateAction;
    public override TweenCActions SetOnUpdate(Action<float> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a float, but TransformTween can only give a vector3");
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Vector3> action) {
        mUpdateAction = action;
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Color> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a color, but TransformTween can only give a vector3");
        return this;
    }
    public override void CallAction(Vector4 value) { mUpdateAction?.Invoke(value); }
}
public class RectTween : TweenTemplate
{
    // RectMove, RectScale, RectRotate,
    public RectTransform mRectT;

    public RectTween(RectTransform reactT, Vector4 startValue, Vector4 endValue, float duration, TweenType type)
        : base(startValue, endValue, duration, type) {
        mRectT = reactT;
    }

    public Action<Vector3> mUpdateAction;
    public override TweenCActions SetOnUpdate(Action<float> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a float, but RectTween can only give a vector3");
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Vector3> action) {
        mUpdateAction = action;
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Color> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a color, but RectTween can only give a vector3");
        return this;
    }
    public override void CallAction(Vector4 value) { mUpdateAction?.Invoke(value); }
}
public class ImageTween : TweenTemplate
{
    // ImageColor (Color), Alpha (Color)
    public Image mImage;

    public ImageTween(Image image, Vector4 startValue, Vector4 endValue, float duration, TweenType type)
        : base(startValue, endValue, duration, type) {
        mImage = image;
    }
    public Action<Color> mUpdateAction;
    public override TweenCActions SetOnUpdate(Action<float> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a float, but ImageTween can only give a color");
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Vector3> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a vector3, but ImageTween can only give a color");
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Color> action) {
        mUpdateAction = action;
        return this;
    }
    public override void CallAction(Vector4 value) { mUpdateAction?.Invoke(value); }
}
public class SpriteTween : TweenTemplate
{
    // SpriteColor (Color), Alpha (Color)
    public SpriteRenderer mSprite;

    public SpriteTween(SpriteRenderer sprite, Vector4 startValue, Vector4 endValue, float duration, TweenType type)
        : base(startValue, endValue, duration, type) {
        mSprite = sprite;
    }

    public Action<Color> mUpdateAction;
    public override TweenCActions SetOnUpdate(Action<float> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a float, but SpriteTween can only give a color");
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Vector3> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a vector3, but SpriteTween can only give a color");
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Color> action) {
        mUpdateAction = action;
        return this;
    }
    public override void CallAction(Vector4 value) { mUpdateAction?.Invoke(value); }
}
public class MaterialTween : TweenTemplate
{
    // MaterialColor
    public Material mMaterial;

    public MaterialTween(Material material, Vector4 startValue, Vector4 endValue, float duration, TweenType type)
        : base(startValue, endValue, duration, type) {
        mMaterial = material;
    }

    public Action<Color> mUpdateAction;
    public override TweenCActions SetOnUpdate(Action<float> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a float, but MaterialTween can only give a color");
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Vector3> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a vector3, but MaterialTween can only give a color");
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Color> action) {
        mUpdateAction = action;
        return this;
    }
    public override void CallAction(Vector4 value) { mUpdateAction?.Invoke(value); }
}
public class AudioTween : TweenTemplate
{
    // AudioVolume, AudioPitch, AudioStereoPan, AudioSpatialBlend, AudioReverbZoneMix
    public AudioSource mSource;

    public AudioTween(AudioSource source, Vector4 startValue, Vector4 endValue, float duration, TweenType type)
        : base(startValue, endValue, duration, type) {
        mSource = source;
    }

    public Action<float> mUpdateAction;
    public override TweenCActions SetOnUpdate(Action<float> action) {
        mUpdateAction = action;
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Vector3> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a vector3, but AudioTween can only give a float");
        return this;
    }
    public override TweenCActions SetOnUpdate(Action<Color> action) {
        Debug.LogWarning("TweenC: Tried to call SetOnUpdate with a color, but AudioTween can only give a float");
        return this;
    }
    public override void CallAction(Vector4 value) { mUpdateAction?.Invoke(value.x); }
}