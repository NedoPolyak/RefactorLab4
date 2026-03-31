namespace LifeSim;

public class Herbivore : Animal
{
    public Herbivore(World world, Point2 pos, Gender? gender = null)
        : base(world, pos, _characteristics, gender)
    {
    }

    private static readonly AnimalCharacteristics _characteristics = new AnimalCharacteristics
    {
        Vision = 8,
        MoveCost = 2,
        BiteGain = 18,
        ReproduceThreshold = 60,
        InitialEnergy = 30,
        SelfGlyph = 'h',
        Color = ConsoleColor.Yellow
    };

    protected override Organism? FindPrey() => World.FindNearest<Plant>(Pos, Vision);

    protected override Animal MakeChild(Point2 p) => new Herbivore(World, p);
}
