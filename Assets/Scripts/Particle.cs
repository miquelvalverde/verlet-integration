/**
* @author MiquelValverde
*
* @date - 2019
*/

using System.Collections.Generic;
using UnityEngine;

public class Particle
{
    public List<Stick> connections;

    public Vector3 currentPosition;

    private Vector3 previousPosition;

    public Vector3 forceApplied;

    private readonly float mass = 1;

    public Particle(Vector3 initialPosition)
    {
        currentPosition = initialPosition;
        previousPosition = initialPosition;
        connections = new List<Stick>();
    }

    public void Connect(Stick stick)
    {
        connections.Add(stick);
    }

    public void Connect(Particle other)
    {
        if (this == other) return;
        Stick stick = new Stick(this, other);
        Connect(stick);
    }

    public void Connect(Particle other, float length)
    {
        if (this == other) return;
        Stick stick = new Stick(this, other, length);
        Connect(stick);
    }

    public void Verlet(float deltaTime)
    {
        /* 
         *  VERLET INTEGRATION:
         *  NewPos = (2 * CurrentPos) - PreviousPos + (acceleration * dt * dt) 
         *  
         *  VERLET INTEGRATION with drag:
         *  NewPos = (2 * drag * CurrentPos) - drag * PreviousPos + (acceleration * dt * dt) 
         */

        Vector3 tmp = currentPosition;
        //currentPosition = (2 * currentPosition) - previousPosition + (mass * forceApplied * deltaTime * deltaTime);
        currentPosition = ((1 + VerletParticleSystem.Drag) * currentPosition) - VerletParticleSystem.Drag * previousPosition + (mass * forceApplied * deltaTime * deltaTime);
        previousPosition = tmp;
    }
}
