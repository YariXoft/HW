using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializationExamples
{
    [Serializable]
    public class Fraction
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public Fraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }
    }

    [Serializable]
    public class Journal
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public DateTime PublicationDate { get; set; }
        public int PageCount { get; set; }

        public override string ToString()
        {
            return $"Назва: {Title}\nВидавець: {Publisher}\nДата видання: {PublicationDate}\nКiлькiсть сторiнок: {PageCount}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //1
            Fraction[] fractions = new Fraction[3];

            for (int i = 0; i < fractions.Length; i++)
            {
                Console.WriteLine($"Введiть дрiб {i + 1}:");
                Console.Write("Чисельник: ");
                int numerator = Convert.ToInt32(Console.ReadLine());
                Console.Write("Займенник: ");
                int denominator = Convert.ToInt32(Console.ReadLine());
                fractions[i] = new Fraction(numerator, denominator);
            }

            Serialize(fractions, "fractions.dat");

            Fraction[] loadedFractions = Deserialize<Fraction[]>("fractions.dat");
            Console.WriteLine("Завантаженi дробi:");
            foreach (var fraction in loadedFractions)
            {
                Console.WriteLine(fraction);
            }

            // 2

            Journal journal = new Journal
            {
                Title = "Приклад журнал",
                Publisher = "Приклад видавець",
                PublicationDate = DateTime.Now,
                PageCount = 50
            };

            Serialize(journal, "journal.dat");

            Journal loadedJournal = Deserialize<Journal>("journal.dat");
            Console.WriteLine("Завантажений журнал:");
            Console.WriteLine(loadedJournal);
        }

        static void Serialize<T>(T obj, string fileName)
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, obj);
            }
            Console.WriteLine($"{typeof(T).Name} серiалiзований i засейвлений у {fileName}.");
        }

        static T Deserialize<T>(string fileName)
        {
            T obj;
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                obj = (T)formatter.Deserialize(stream);
            }
            Console.WriteLine($"{typeof(T).Name} десерiалiзований з {fileName}.");
            return obj;
        }
    }
}
