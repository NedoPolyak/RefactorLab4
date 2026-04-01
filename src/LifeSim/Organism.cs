using System;

namespace LifeSim;

public abstract class Organism
{
    protected Organism(World world, Point2 pos, Gender? gender = null)
    {
        World = world;
        Pos = world.Wrap(pos);
        Gender = gender ?? PickGender();
    }

    public World World { get; }

    public Point2 Pos { get; set; }

    public bool IsAlive { get; set; } = true;

    public int Age { get; private set; }

    public abstract char Glyph { get; }

    public virtual ConsoleColor? Color => null;

    public Gender Gender { get; }

    public virtual void Tick() => Age++;

    private static Gender PickGender() => Rand.Chance(0.5) ? Gender.Female : Gender.Male;

    public Organism? FindNearest<T>(Point2 from, int visionRange)
       where T : Organism
    {
        Organism? best = null;
        var bestDist = int.MaxValue;

        foreach (var o in World.All)
        {
            if (o is T)
            {
                var dx = World.ToroidalDistance(from.X, o.Pos.X, World.Width);
                var dy = World.ToroidalDistance(from.Y, o.Pos.Y, World.Height);
                var distance = dx + dy;
                if (distance <= visionRange && distance < bestDist)
                {
                    best = o;
                    bestDist = distance;
                }
            }
        }

        return best;
    }
}
