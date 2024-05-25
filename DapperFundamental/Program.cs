using System.Data.SqlClient;
using Dapper;

namespace DapperFundamental;

public class Program
{
    public static void Main(string[] args)
    {
       /*
        * Lambda expression => didefinisikan sebagai anonymus function yang artinya method/ function tanpa nama
        *
        * ada 2 tipe lambda Expression :
        * - lambda expression : dimana bodynya sebagai expression
        * - statement lambda : dimana memiliki block code sebagai bodynya
        */

       // lambda expression
       var square = (int x) => x * x;
       Console.WriteLine(square(10));
       
       // statement lambda
       var HelloWorld = (string firstName, string lastName) =>
       {
           Console.WriteLine($"nama saya {firstName}");
       };
    }
}