namespace DapperFundamental;

public class Lambda
{
    // delegate merupakan sebuah tipe data yang dapat digunakan oleh method lainya yang memiliki kontrak yang sama (memiliki parameter yang sama)
    delegate void HelloWorld(string firstname, string lastname, Action callback);

    public void Main(string[] args)
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
        /*
         * kita dapat membuat callback pada lambda
         * bisa menggunakan delegate ataupun tidak
         */
        HelloWorld helloWorld = (string firstName, string lastName, Action callback) =>
        {
            Console.WriteLine($"nama saya {firstName} {lastName}");
            callback();
        };

        helloWorld("Theo", "Sihotang", () => { Console.WriteLine("Ini Callback"); });
    }
}