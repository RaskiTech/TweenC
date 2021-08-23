using UnityEngine;
using UnityEngine.UI;

public static class TweenC
{
    // TweenC 1.9

    static RunTweens tweenRunner;
    static bool throwNullWarnings = true;
    #region User methods
    /// <summary>Does TweenC throw warnings if a field is null. It's recommended to keep at true.</summary>
    public static void ThrowNullFieldWarnings(bool isTrue) { throwNullWarnings = isTrue; }

    /// <summary>Tween a value between two points. Use .SetOnUpdate((float v) => {}) to get access to the value.</summary>
    public static TweenCActions Value(float from, float to, float duration) {
        return SetupTween(new FloatTween(Vector4(from), Vector4(to), duration, TweenTemplate.TweenType.Value));
    }
    /// <summary>Tween a value between two vectors. Use .SetOnUpdate((Vector3 v) => {}) to get access to the value.</summary>
    public static TweenCActions Vector3(Vector3 from, Vector3 to, float duration) {
        return SetupTween(new VectorTween(from, to, duration, TweenTemplate.TweenType.Vector));
    }
    /// <summary>Tween a value between two colors. Use .SetOnUpdate((Color c) => {}) to get access to the value.</summary>
    public static TweenCActions Color(Color from, Color to, float duration) {
        return SetupTween(new ColorTween(Vector4(from), Vector4(to), duration, TweenTemplate.TweenType.Color));
    }
    public static TweenCActions MoveX(Transform transform, float position, float duration) {
        return SetupTween(transform != null ? new TransformTween(transform, transform.localPosition,
            new Vector4(position, transform.localPosition.y, transform.localPosition.z), duration, TweenTemplate.TweenType.Move) : null);
    }
    public static TweenCActions MoveY(Transform transform, float position, float duration) {
        return SetupTween(transform != null ? new TransformTween(transform, transform.localPosition,
            new Vector4(transform.localPosition.x, position, transform.localPosition.z), duration, TweenTemplate.TweenType.Move) : null);
    }
    public static TweenCActions MoveZ(Transform transform, float position, float duration) {
        return SetupTween(transform != null ? new TransformTween(transform, transform.localPosition,
            new Vector4(transform.localPosition.x, transform.localPosition.y, position), duration, TweenTemplate.TweenType.Move) : null);
    }
    public static TweenCActions Move(Transform transform, Vector3 position, float duration) {
        return SetupTween(transform != null ? new TransformTween(transform, transform.position, position, duration, TweenTemplate.TweenType.Move) : null);
    }
    public static TweenCActions LocalMove(Transform transform, Vector3 position, float duration) {
        return SetupTween(transform != null ? new TransformTween(transform, transform.localPosition, position, duration, TweenTemplate.TweenType.LocalMove) : null);
    }
    public static TweenCActions Scale(Transform transform, Vector3 scale, float duration) {
        return SetupTween(transform != null ? new TransformTween(transform, transform.localScale, scale, duration, TweenTemplate.TweenType.Scale) : null);
    }
    public static TweenCActions Rotate(Transform trans, Vector3 rotation, float duration) {
        return SetupTween(trans != null ? new TransformTween(trans, trans.rotation.eulerAngles, rotation, duration, TweenTemplate.TweenType.Rotate) : null);
    }
    public static TweenCActions RectMove(RectTransform rect, Vector3 position, float duration) {
        return SetupTween(rect != null ? new RectTween(rect, rect.position, position, duration, TweenTemplate.TweenType.RectMove) : null);
    }
    public static TweenCActions RectLocalMove(RectTransform rect, Vector3 localPosition, float duration) {
        return SetupTween(rect != null ? new RectTween(rect, rect.localPosition, localPosition, duration, TweenTemplate.TweenType.RectLocalMove) : null);
    }
    public static TweenCActions RectScale(RectTransform rect, Vector3 localScale, float duration) {
        return SetupTween(rect != null ? new RectTween(rect, rect.localScale, localScale, duration, TweenTemplate.TweenType.RectScale) : null);
    }
    public static TweenCActions RectRotate(RectTransform rect, Vector3 localRotation, float duration) {
        return SetupTween(rect != null ? new RectTween(rect, rect.rotation.eulerAngles, localRotation, duration, TweenTemplate.TweenType.RectRotate) : null);
    }
    public static TweenCActions RectSizeDelta(RectTransform rect, Vector3 sizeDelta, float duration) {
        return SetupTween(rect != null ? new RectTween(rect, rect.sizeDelta, sizeDelta, duration, TweenTemplate.TweenType.RectSizeDelta) : null);
    }
    public static TweenCActions ImageColor(Image image, Color color, float duration) {
        return SetupTween(image != null ? new ImageTween(image, image.color, color, duration, TweenTemplate.TweenType.ImageColor) : null);
    }
    public static TweenCActions ImageAlpha(Image image, float alpha, float duration) {
        return SetupTween(image != null ? new ImageTween(image, image.color,
            new Vector4(image.color.r, image.color.g, image.color.b, alpha), duration, TweenTemplate.TweenType.ImageColor) : null);
    }
    public static TweenCActions SpriteColor(SpriteRenderer renderer, Color color, float duration) {
        return SetupTween(renderer != null ? new SpriteTween(renderer, renderer.color, color, duration, TweenTemplate.TweenType.SpriteColor) : null);
    }
    public static TweenCActions SpriteAlpha(SpriteRenderer renderer, float alpha, float duration) {
        return SetupTween(renderer != null ? new SpriteTween(renderer, renderer.color,
            new Vector4(renderer.color.r, renderer.color.g, renderer.color.b, alpha), duration, TweenTemplate.TweenType.SpriteColor) : null);
    }
    public static TweenCActions UIAlpha(CanvasGroup group, float alpha, float duration) {
        return SetupTween(group != null ? new CanvasGroupTween(group, Vector4(group.alpha), Vector4(alpha), duration, TweenTemplate.TweenType.UIAlpha) : null);
    }
    public static TweenCActions MaterialColor(Material material, Color color, float duration) {
        return SetupTween(material != null ? new MaterialTween(material, material.color, color, duration, TweenTemplate.TweenType.MaterialColor) : null);
    }
    public static TweenCActions AudioVolume(AudioSource source, float volume, float duration) {
        return SetupTween(source != null ? new AudioTween(source, 
            Vector4(source.volume), Vector4(volume), duration, TweenTemplate.TweenType.AudioVolume) : null);
    }
    public static TweenCActions AudioPitch(AudioSource source, float pitch, float duration) {
        return SetupTween(source != null ? new AudioTween(source, 
            Vector4(source.pitch), Vector4(pitch), duration, TweenTemplate.TweenType.AudioPitch) : null);
    }
    public static TweenCActions AudioStereoPan(AudioSource source, float pan, float duration) {
        return SetupTween(source != null ? new AudioTween(source, 
            Vector4(source.panStereo), Vector4(pan), duration, TweenTemplate.TweenType.AudioStereoPan) : null);
    }
    public static TweenCActions AudioSpatialBlend(AudioSource source, float blend, float duration) {
        return SetupTween(source != null ? new AudioTween(source, 
            Vector4(source.spatialBlend), Vector4(blend), duration, TweenTemplate.TweenType.AudioSpatialBlend) : null);
    }
    public static TweenCActions AudioReverbZoneMix(AudioSource source, float reverb, float duration) {
        return SetupTween(source != null ? new AudioTween(source, 
            Vector4(source.reverbZoneMix), Vector4(reverb), duration, TweenTemplate.TweenType.AudioReverbZoneMix) : null);
    }
    #endregion

    static TweenCActions SetupTween(TweenTemplate addedTween) {
        if (addedTween == null) {
            if (throwNullWarnings)
                Debug.LogWarning("TweenC: Field was null");
            return null;
        }

        if (tweenRunner == null) {
            GameObject obj = new GameObject("Tween");
            tweenRunner = obj.AddComponent<RunTweens>();
        }
        tweenRunner.tweens.Add(addedTween);
        return addedTween;
    }
    static Vector4 Vector4(float amount) { return new Vector4(amount, 0, 0, 0); }
    static Vector4 Vector4(Color c) { return new Vector4(c.r, c.g, c.b, c.a); }

    public enum EaseType
    {
        SineIn, SineOut, SineInOut,
        CubicIn, CubicOut, CubicInOut,
        ExpIn, ExpOut, ExpInOut,
        BackIn, BackOut, BackInOut,
        ElasticIn, ElasticOut, ElasticInOut,
        BounceIn, BounceOut, BounceInOut
    }
}