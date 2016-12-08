using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESecretFunctionCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            int n;
            bool isSecretAdditive = false;
            if (int.TryParse(input, out n))
            {
                int[] primes = GetNPrimes(n);
                isSecretAdditive = CheckIsSecretAdditive(primes);
            }

            Console.WriteLine(isSecretAdditive ? "Secret method is additive" : "Secret method is not additive");
            // this method is used to hold the output screen until the user presses the Enter key.
            Console.ReadLine();
        }

        /// <summary>
        /// Determines if the secret() method is additive 
        /// </summary>
        /// <param name="primeNumbers"></param>
        /// <returns></returns>
        static bool CheckIsSecretAdditive(int[] primeNumbers)
        {
            foreach(int x in primeNumbers)
            {
                foreach(int y in primeNumbers)
                {
                    if(Secret(x+y) != Secret(x) + Secret(y))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// A given method
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static int Secret(int n)
        {
            Func<int,int> f = (int x) => { return x; };

            return f.Invoke(n);
        }

        /// <summary>
        /// All non-prime numbers are divisible by a prime number.
        /// We cross off all numbers divisible by prime number.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static int[] GetNPrimes(int n)
        {
            bool[] primesCheck = new bool[n + 1];
            for (int i = 0; i < n - 1; i++)
            {
                primesCheck[i] = true;
            }

            int prime = 2;

            IList<int> primes = new List<int>();

            while (prime <= Math.Sqrt(n))
            {
                CrossOff(primesCheck, prime);
                prime = GetNextPrime(primesCheck, prime);
                primes.Add(prime);
            }

            return primes.ToArray();
        }

        /// <summary>
        /// Find the next value prime number, which is true.
        /// </summary>
        /// <param name="primes"></param>
        /// <param name="prime"></param>
        /// <returns></returns>
        private static int GetNextPrime(bool[] primes, int prime)
        {
            int next = prime + 1;
            while (next < primes.Length && !primes[next])
            {
                next++;
            }

            return next;
        }

        /// <summary>
        /// Cross off remaining multiples of prime. We can start with (prime*prime),
        /// because if we have a k * prime, where k < prime, this value would have already
        /// been crossed off in a prior iteration.
        /// </summary>
        /// <param name="primes"></param>
        /// <param name="prime"></param>
        private static void CrossOff(bool[] primes, int prime)
        {
            for (int i = prime * prime; i < primes.Length; i += prime)
            {
                primes[i] = false;
            }
        }
    }
}
