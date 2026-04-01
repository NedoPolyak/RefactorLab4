using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace LifeSim;

public abstract class Animal : Organism
{
    protected Animal(World world, Point2 pos, AnimalCharacteristics characteristics, Gender? gender = null)
        : base(world, pos, gender)
    {
        _characteristics = characteristics;
    }
    private readonly AnimalCharacteristics _characteristics;
    protected int Vision => _characteristics.Vision;
    protected int MoveCost => _characteristics.MoveCost;
    protected int BiteGain => _characteristics.BiteGain;
    protected int ReproduceThreshold => _characteristics.ReproduceThreshold;
    protected int InitialEnergy => _characteristics.InitialEnergy;
    protected char SelfGlyph => _characteristics.SelfGlyph;

    public override char Glyph => SelfGlyph;
    public override ConsoleColor? Color => _characteristics.Color;

    public int Energy { get; set; }

    public int MaxAge { get; set; } = 1000;

    public override void Tick()
    {
        base.Tick();

        if (Age == 1 && Energy == 0)
        {
            Energy = InitialEnergy;
        }

        var prey = FindPrey();
        if (prey != null)
        {
            StepToward(prey.Pos);
            if (Point2.AreNeighborsOrSame(Pos, prey.Pos) && prey.IsAlive)
            {
                World.Remove(prey);
                Energy += BiteGain;
            }
        }
        else
        {
            Wander();
        }

        Energy -= MoveCost;

        if (Energy >= ReproduceThreshold)
        {
            var empty = World.EmptyNeighbors8(Pos).ToList();
            if (empty.Count > 0)
            {
                var child = MakeChild(empty.Pick()!);
                Energy /= 2;
                World.Add(child);
            }
        }

        if (Energy <= 0 || (Age > MaxAge && Rand.Chance(0.02)))
        {
            World.Remove(this);
        }
    }

    protected abstract Organism? FindPrey();

    protected abstract Animal MakeChild(Point2 p);

    protected void StepToward(Point2 target)
    {
        List<Point2>? free = World.GetStepTowardList(Pos, target);
        if (free.Count == 0)
        {
            Wander();
            return;
        }

        World.MoveTo(this, free.Pick()!);
    }

    protected void Wander()
    {
        var options = World.EmptyNeighbors8(Pos).ToList();
        if (options.Count > 0)
        {
            World.MoveTo(this, options.Pick()!);
        }
    }

}
