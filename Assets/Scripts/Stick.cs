/**
* @author MiquelValverde
*
* @date - 2019
*/

using UnityEngine;

public class Stick
{
    public float length;
    public Particle a;
    public Particle b;

    public Stick(Particle a, Particle b)
    {
        this.a = a;
        this.b = b;
        length = Vector3.Distance(a.currentPosition, b.currentPosition);
    }

    public Stick(Particle a, Particle b, float length)
    {
        this.a = a;
        this.b = b;
        this.length = length;
    }

    public Particle GetOtherParticle (Particle particle)
    {
        if (a == particle) return b;
        else return a;
    }
}
