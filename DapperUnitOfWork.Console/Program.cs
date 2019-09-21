using System;
using System.Threading.Tasks;
using DapperUnitOfWork.Entities;

namespace DapperUnitOfWork.App
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            using (var uow = new UnitOfWork())
            {
                //var cats = uow.CatRepository.GetAll();

                var catsTask = uow.CatRepository.GetAllASync();

                var cats = await catsTask;

                Console.WriteLine("-------------------");
                foreach (var cat in cats)
                {
                    Console.WriteLine($"Nome:   {cat.Name}");
                    Console.WriteLine($"Idade:  {cat.Age}");
                    Console.WriteLine("-------------------");
                }

                //var siamese = new Breed { Name = "Siamese" };
                //uow.BreedRepository.Add(siamese);

                //var foo = new Cat { BreedId = siamese.BreedId, Name = "Foo", Age = 19 };
                //var xing = new Cat { BreedId = siamese.BreedId, Name = "Xing", Age = 6 };
                //var xang = new Cat { BreedId = siamese.BreedId, Name = "Xang", Age = 6 };
                //uow.CatRepository.Add(foo);
                //uow.CatRepository.Add(xing);
                //uow.CatRepository.Add(xang);

                //uow.Commit();
            }

            Console.ReadKey();
        }

        /*
        static void Test2()
        {
            var breed1 = new Breed { Name = "Orange Mackerel" };
            var cat1 = new Cat { Name = "Cheddar", Age = 4 };

            using (var uow = new UnitOfWork("LosGatos"))
            {
                uow.BreedRepository.Insert(breed1);
                cat1.BreedId = breed1.BreedId;
                uow.CatRepository.Insert(cat1);
                uow.SaveChanges();
            }
        }

        static void Test1()
        {
            var breed1 = new Breed { Name = "Egyptian Mau" };
            var breed2 = new Breed { Name = "Arabian Mau" };

            var cat1 = new Cat { Name = "Pharoh", Age = 4 };
            var cat2 = new Cat { Name = "Tut", Age = 2 };
            var cat3 = new Cat { Name = "Anas", Age = 8 };

            using (var uow = new UnitOfWork("LosGatos"))
            {
                uow.BreedRepository.Insert(breed1);
                uow.BreedRepository.Insert(breed2);

                cat1.BreedId = breed1.BreedId;
                cat2.BreedId = breed1.BreedId;
                cat3.BreedId = breed2.BreedId;

                uow.CatRepository.Insert(cat1);
                uow.CatRepository.Insert(cat2);
                uow.CatRepository.Insert(cat3);

                uow.SaveChanges();
            }
        }
        */
    }
}