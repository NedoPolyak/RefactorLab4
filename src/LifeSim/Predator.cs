namespace LifeSim;

public class Predator : Animal
{
    public Predator(World world, Point2 pos, Gender? gender = null)
        : base(world, pos, _characteristics, gender)
    {
    }

    private static readonly AnimalCharacteristics _characteristics = new AnimalCharacteristics
    {
        Vision = 12,
        MoveCost = 3,
        BiteGain = 28,
        ReproduceThreshold = 80,
        InitialEnergy = 40,
        SelfGlyph = 'W',
        Color = System.ConsoleColor.Red
    };

    protected override Organism? FindPrey() => World.FindNearest<Herbivore>(Pos, Vision);

    protected override Animal MakeChild(Point2 p) => new Predator(World, p);
}
