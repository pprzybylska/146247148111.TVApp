using _146247148111.TVApp.BLC;
using _146247148111.TVApp.Interfaces;

Console.WriteLine("Hello World!");
string libraryName = System.Configuration.ConfigurationManager.AppSettings["DAOLibraryName"];
BLC blc = new BLC(libraryName);

var prod = blc.CreateNewProducer(15, "Sa234214GF234234D3453mf23423SDFdgsung", "South Korea");
blc.DeleteTVById(1);
blc.DeleteTVById(2);
blc.DeleteTVById(3);

foreach (IProducer p in blc.GetProducers())
{
    Console.WriteLine($"{p.ID}: {p.Name}, {p.Country}");
}

Console.WriteLine("_______________________");


blc.CreateNewTV(1, "afSDFa", 15, _146247148111.TVApp.Core.ScreenType.Plasma, 55);

foreach (ITV t in blc.GetTVs())
{
    Console.WriteLine($"{t.ID}: {t.Name}, {t.Producer.Name}");
}

Console.WriteLine("_______________________");

