/**
* @author MiquelValverde
*
* @date - 2019
*/

using System.Collections.Generic;
using UnityEngine;

public class Softbody : MonoBehaviour
{
    private VerletParticleSystem verletParticleSystem;

    private List<Particle> particles;

    private void Awake()
    {
        /* Hardcoded particles */
        Particle p1 = new Particle(new Vector3(0,0,0));
        Particle p2 = new Particle(new Vector3(0,0,2.0f));
        Particle p3 = new Particle(new Vector3(0,0,4.0f));

        Particle p4 = new Particle(new Vector3(-2.0f, 0, 0));
        Particle p5 = new Particle(new Vector3(-2.0f, 0, 2.0f));
        Particle p6 = new Particle(new Vector3(-2.0f, 0, 4.0f));

        Particle p7 = new Particle(new Vector3(-4.0f, 0, 0));
        Particle p8 = new Particle(new Vector3(-4.0f, 0, 2.0f));
        Particle p9 = new Particle(new Vector3(-4.0f, 0, 4.0f));

        /* Hardcoded connections */
        p1.Connect(p4);
        p1.Connect(p2);
        p2.Connect(p3);
        p2.Connect(p5);
        p4.Connect(p5);
        p4.Connect(p7);
        p5.Connect(p6);
        p5.Connect(p8);
        p6.Connect(p3);
        p6.Connect(p9);
        p7.Connect(p8);
        p8.Connect(p9);

        /* Hardcoded extra connections*/
        p1.Connect(p9);
        p1.Connect(p5);
        p7.Connect(p3);
        p7.Connect(p5);
        p9.Connect(p5);

        particles = new List<Particle>();

        particles.Add(p1);
        particles.Add(p2);
        particles.Add(p3);
        particles.Add(p4);
        particles.Add(p5);
        particles.Add(p6);
        particles.Add(p7);
        particles.Add(p8);
        particles.Add(p9);

        verletParticleSystem = new VerletParticleSystem(particles);
    }

    private void Update()
    {
        verletParticleSystem.UpdateSimulation(Time.deltaTime);

        /* Hardcoded fixed constraints */
        verletParticleSystem.particles[0].currentPosition = Vector3.zero;
        verletParticleSystem.particles[1].currentPosition = new Vector3(0, 0, 2.0f);
        verletParticleSystem.particles[2].currentPosition = new Vector3(0, 0, 4.0f);
    }

    private void OnDrawGizmos()
    {
        if (verletParticleSystem == null) return;
        verletParticleSystem.DrawGizmos();
    }
}
