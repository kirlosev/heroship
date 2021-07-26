using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LosevHelper {
    public static float Sign(float value, float accuracy = 0) {
        if (value == 0) return 0;
        else if (accuracy != 0 && Mathf.Abs(value) > accuracy)
            return Mathf.Sign(value);
        else return Mathf.Sign(value);
    }

    public static bool CompareSign(float a, float b) {
        return Sign(a) == Sign(b);
    }

    public static float VectorAngle(Vector3 vector) {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }

    public static int ChooseNumber(params int[] numbers) {
        var rand = Random.value;
        var delta = 1f / numbers.Length;
        for (var i = 0; i < numbers.Length; ++i) {
            if (rand < delta * (i + 1)) {
                return numbers[i];
            }
        }
        return numbers[0];
    }

    public static Vector3 GetReflectedDirection(Vector3 inDirection,
                                                Vector3 normal,
                                                float bounceValue) {
        return (inDirection - 2 * (Vector3.Dot(inDirection, normal) * normal)) * bounceValue;
    }

#if UNITY_EDITOR
    [MenuItem("PlayerPrefs/Clear All")]
    public static void ClearAll() {
        PlayerPrefs.DeleteAll();
    }
#endif
}
