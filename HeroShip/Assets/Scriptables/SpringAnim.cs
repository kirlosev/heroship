using UnityEngine;
using System.Collections;

public class SpringAnim : MonoBehaviour
{
    public static void springFloat(ref float val, ref float vel, float targ,
        float zeta, float omega, float h)
    {
        float f = 1 + 2 * h * zeta * omega;
        float oo = omega * omega;
        float hoo = h * oo;
        float hhoo = h * hoo;
        float detInv = 1f / (f + hhoo);
        float detVal = f * val + h * vel + hhoo * targ;
        float detVel = vel + hoo * (targ - val);
        val = detVal * detInv;
        vel = detVel * detInv;
    }

    static float convertLambdaToZeta(float omega, float lambda)
    {
        return -Mathf.Log(0.5f) / (omega * lambda);
    }

    public static void springFloatByHalfLife(ref float val, ref float vel, float targ,
        float omega, float h, float lambda)
    {
        float zeta = convertLambdaToZeta(omega, lambda);
        springFloat(ref val, ref vel, targ, zeta, omega, h);
    }

    public static void springVector3(ref Vector3 val, ref Vector3 vel, Vector3 targ,
        float zeta, float omega, float h)
    {
        float f = 1 + 2 * h * zeta * omega;
        float oo = omega * omega;
        float hoo = h * oo;
        float hhoo = h * hoo;
        float detInv = 1f / (f + hhoo);
        Vector3 detVal = f * val + h * vel + hhoo * targ;
        Vector3 detVel = vel + hoo * (targ - val);
        val = detVal * detInv;
        vel = detVel * detInv;
    }

    public static void springVector3ByHalfLife(ref Vector3 val, ref Vector3 vel, Vector3 targ,
        float omega, float h, float lambda)
    {
        float zeta = convertLambdaToZeta(omega, lambda);
        springVector3(ref val, ref vel, targ, zeta, omega, h);
    }
}