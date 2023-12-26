using _146247148111.TVApp.BLC;
using _146247148111.TVApp.Interfaces;

Console.WriteLine("Hello World!");
string libraryName = System.Configuration.ConfigurationManager.AppSettings["DAOLibraryName"];
BLC blc = new BLC(libraryName);

foreach ( IProducer p in blc.GetProducers() )
{
    Console.WriteLine( $"{p.ID}: {p.Name}" );
}

Console.WriteLine("_______________________");

foreach (ITV t in blc.GetTVs())
{
    Console.WriteLine($"{t.ID}: {t.Producer.Name} {t.Name} {t.Screen} {t.ScreenSize}");
}