using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSim
{
    public class AnimalCharacteristics
    {
        public int Vision { get; init; }
        public int MoveCost { get; init; }
        public int BiteGain { get; init; }
        public int ReproduceThreshold { get; init; }
        public int InitialEnergy { get; init; }
        public char SelfGlyph { get; init; }
        public ConsoleColor? Color { get; init; }
    }
}
