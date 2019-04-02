/**
* @author MiquelValverde
*
* @date - 2019
*/

using UnityEngine;
using System.Collections.Generic;

public class VerletParticleSystem
{
    public const int ITERATIONS = 8;

    private const float DRAG = 0.99f;

    public static float Drag { get { return Mathf.Clamp01(DRAG); } }

    public List<Particle> particles;

    public VerletParticleSystem(List<Particle> particles)
    {
        this.particles = particles;
    }

    public void UpdateSimulation(float deltaTime)
    {
        foreach (Particle particle in particles)
        {
            /* Accumulate forces to particles */
            particle.forceApplied = Physics.gravity;

            /* Verlet integration step */
            particle.Verlet(deltaTime);
        }

        /* Solve stick constraints */
        for (int iteration = 0; iteration < ITERATIONS; iteration++)
        {
            foreach (Particle particle in particles)
            {
                SolveConstraints(particle);
            }
        }
    }

    private void SolveConstraints(Particle particle)
    {
        foreach (Stick stick in particle.connections)
        {
            Particle other = stick.GetOtherParticle(particle);
            SolveConstraints(particle, other, stick.length);
        }
    }

    private void SolveConstraints(Particle a, Particle b, float restLength)
    {
        Vector3 delta = b.currentPosition - a.currentPosition;
        float deltaLength = delta.magnitude;
        float difference = (deltaLength - restLength) / deltaLength;
        a.currentPosition += delta * restLength * 0.005f * difference;
        b.currentPosition -= delta * restLength * 0.005f * difference;
    }

    public void DrawGizmos()
    {
        float t = 0;
        float step = 1f / particles.Count;
        foreach (Particle particle in particles)
        {
            /* Paint particle nodes */
            Gizmos.color = Color.Lerp(Color.yellow, Color.red, t);
            Gizmos.DrawSphere(particle.currentPosition, 0.1f);
            t += step;

            /* Paint stick edges */
            foreach(Stick stick in particle.connections)
            {
                Particle other = stick.GetOtherParticle(particle);
                float deltaLength = Vector3.Distance(stick.GetOtherParticle(particle).currentPosition, particle.currentPosition);
                float difference = (deltaLength - stick.length) / deltaLength;
                Gizmos.color = Color.Lerp(Color.gray, Color.red, difference * 10);
                Gizmos.DrawLine(particle.currentPosition, other.currentPosition);
            }
        }
    }
}
