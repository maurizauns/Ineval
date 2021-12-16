using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.Common.Helpers
{
    public static class MetodosUtils
    {

        public static double GenerateDouble(int limite) {
            bool confirmation = false;
            double number = 0;
            while (!confirmation) {
                Random r = new Random();
                double nextDouble = r.Next(1, limite);

                if (nextDouble >= (limite * 0.65) && nextDouble <= (limite * 0.80))
                {
                    number = nextDouble;
                    break;
                }
            }

            return number;
            
        }
        public static List<double> GetListOfRandomDoubles(int countOfNumbers, double totalSum, int digits, int limite)
        {
            Random r = new Random();

            List<double> randomDoubles = new List<double>();
            double suma = 0;            

            double totalRandomSum = 0;
            randomDoubles = new List<double>();
            suma = 0;
            for (int i = 0; i < countOfNumbers; i++)
            {
                //double nextDouble = r.Next(1, limite);                
                double nextDouble = GenerateDouble(limite);
                randomDoubles.Add(nextDouble);
                totalRandomSum += nextDouble;
            }

            double totalFactor = 1 / totalRandomSum;
            totalFactor = totalFactor * totalSum;

            bool confimacion = true;

            double currentRandomSum = 0;
            randomDoubles.ForEach(x => currentRandomSum += x);
            randomDoubles.Sort();

            for (int i = 0; i < randomDoubles.Count; i++)
            {
                int pp = (int)(totalSum - currentRandomSum);
                int ppp = (int)(pp / countOfNumbers);

                if (randomDoubles[i] != limite)
                {
                    int sumaFinal = (int)(randomDoubles[i] + ppp);
                    if (sumaFinal <= limite)
                    {
                        randomDoubles[i] = sumaFinal;
                    }
                    else
                    {
                        randomDoubles[i] = limite;                        
                    }

                    currentRandomSum -= randomDoubles[i];
                }

                var x = randomDoubles.Sum();
                if (randomDoubles.Sum() >= totalSum)
                {
                    confimacion = false;
                    break;
                }
                else
                {

                }
            }

            if (confimacion)
            {
                for (int i = 0; i < randomDoubles.Count; i++)
                {
                    int pp = (int)(totalSum - currentRandomSum);
                    int ppp = (int)(pp / countOfNumbers);

                    if (randomDoubles[i] != limite)
                    {
                        int sumaFinal = (int)(randomDoubles[i] + ppp);
                        if (sumaFinal <= limite)
                        {
                            randomDoubles[i] = sumaFinal;
                        }
                        else
                        {
                            randomDoubles[i] = limite;                            
                        }

                        currentRandomSum -= randomDoubles[i];                        
                    }

                    var x = randomDoubles.Sum();
                    if (randomDoubles.Sum() >= totalSum)
                    {
                        confimacion = false;
                        break;
                    }
                }
            }



            double currentRandomSum2 = 0;
            randomDoubles.ForEach(x => currentRandomSum2 += x);
            //randomDoubles[0] += totalSum - currentRandomSum2;
            
            return randomDoubles;
        }
    }
}
