using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetyczny
{
    class Program
    {
        private static Random rnd { get; set; } = new Random();
        private static int PopSize { get; set; } = 100;
        private static double MutationPossibility { get; set; } = 0.15;

        private static int Xlimit { get; set; } = 2;
        private static int Ylimit { get; set; } = 2;
        static void Main(string[] args)
        {
            var population = populationGeneration();
            for (int i = 0; i < 100; i++)
            {
                populationMutation(population);
                population = PopulationCrossing(population);
                population = NewPopulation(population);
            }
            var results = PopulationToString(population);
            Console.WriteLine(results);
            Console.ReadLine();
        }
        static Candidate[] populationGeneration()
        {
            Candidate[] population = new Candidate[PopSize];
            for (int i = 0; i < PopSize; i++)
            {
                double x = -Xlimit + 2 * Xlimit * rnd.NextDouble();
                double y = -Ylimit + 2 * Ylimit * rnd.NextDouble();

                population[i] = new Candidate{ X = x, Y = y, Solution = function(x, y) };                 
            }
            return population;

        }

        static void populationMutation(Candidate[] population)
        {
            for (int i = 0; i < population.Length; i++)
            {
                if(rnd.NextDouble() < MutationPossibility)
                {
                    if (rnd.NextDouble() < 0.5)
                        population[i].X += rnd.NextDouble();
                    else
                        population[i].X -= rnd.NextDouble();

                    if (rnd.NextDouble() < 0.5)
                        population[i].Y += rnd.NextDouble();
                    else
                        population[i].Y -= rnd.NextDouble();

                    if (population[i].X < -Xlimit || population[i].X > Xlimit || population[i].Y > Ylimit || population[i].Y < Ylimit)
                    {
                        population[i].Solution -= 1000;
                    }
                    Console.WriteLine();
                }
            }
        }
        static Candidate[] NewPopulation(Candidate[] population)
        {
            Candidate[] newPopulation = new Candidate[population.Length];

            for (int i = 0; i < population.Length; i++)
            {
                int? enemyIndex = null;
                do
                {
                    enemyIndex = rnd.Next(population.Length);
                } while (enemyIndex == i);

                if (population[i].Solution <= population[enemyIndex.Value].Solution)
                    newPopulation[i] = population[enemyIndex.Value];
                else
                    newPopulation[i] = population[i];
            }
            return newPopulation;
        }

        static Candidate[] PopulationCrossing(Candidate[] population)
        {
            Candidate[] newPopulation = new Candidate[population.Length];

            for (int i = 0; i < newPopulation.Length; i++)
            {
                int InititalIndex = rnd.Next(population.Length);
                int secondIndex;
                do
                {
                    secondIndex = rnd.Next(population.Length);
                } while (InititalIndex == secondIndex);
                newPopulation[i] = generateChild(population[InititalIndex], population[secondIndex]);
            }
            return newPopulation;
        }
        static double function(double x, double y)
        {
            return -(Math.Pow(x, 2)) - (Math.Pow(y, 2)) + 3;
        }

        static Candidate generateChild(Candidate a, Candidate b)
        {
            double x, y;

            if(Math.Abs(a.X) - Math.Abs(b.X) >=0)
            {
                x = (a.X - b.X) / 2;
            }
            else
            {
                x = (b.X - a.X) / 2;
            }

            if (Math.Abs(a.Y) - Math.Abs(b.Y) >= 0)
            {
                y = (a.Y - b.Y) / 2;
            }
            else
            {
                y = (b.Y - a.Y) / 2;
            }

            return new Candidate { X = x, Y = y, Solution = function(x, y) };
        }
               
        
        static string PopulationToString(Candidate[] population)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < population.Length; i++)
            {
                sb.AppendLine($"{i}: {population[i]}");
            }
            return sb.ToString();
        }
            
            
       
    }
    
}
