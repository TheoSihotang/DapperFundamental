using System.Data.SqlClient;
using Dapper;

namespace DapperFundamental;

public class DapperFundamental
{
    public  void Main(string[] args)
    {
        /*
         * Dapper adalah ORM atau bisa disebut micro ORM yang memungkinkan untuk kita melakukan mengakses data lebih cepat
         *
         * keungulan dapper :
         *  -  dapper ringan dan cepat
         *  - dapper mudah diigunakan
         *  - digunakan untuk melakukan mapping SQL menjadi object
         *
         */

        // implementasi dari IDBCOnnection
        // kita tidak perlu untuk membuka connection dan menutupnya kembali karena sudah ditangani oleh using SqlConnection
        var stringConnection = "server=localhost,1433;user=sa;password=your_password;database=your_db";
        using var connection = new SqlConnection(stringConnection);
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        /*
         *
         * method extension dari IDBConnection :
         *  - Query untuk DQL
         *  - Execute untuk DDL dan DML
         *  - ExecuteScalar untuk DML dan DQL
         *  - ExecuteReader untuk DQL
         */
        
        // membuat table
        
        // CreateTable(connection);
        
        // insert data
        // CreateData(connection);
        
        // getAll data
        // GetAllData(connection);
        
        // GetById - Get 1 data

        // GetOneData(connection);
        
        // ExecuteReader(connection);
        
        // ParameterizeInsert();
        
        // GetData Parameterize
        var productPrice = new
        {
            productPrice = 1000000
        };
        var sql = "SELECT * FROM m_product WHERE product_price > @productPrice";
        var products = connection.Query<Product>(sql, productPrice).ToList();
        products.ForEach(Console.WriteLine);
    }
    private static void ParameterizeInsert()
    {
        /*
         * Parameterize dapper -> sql server menggunakan @
         * - anonymous parameters
         * - dynamic Parameters
         */

        var product = new Product
        {
            ProductName = "Laptop",
            ProductPrice = 22000000,
            ProductStock = 9
        };
        var sql = "INSERT INTO m_product(product_name, product_price, product_stock) VALUES (@productName, @productPrice, @productStock)";
        // parameterize menggunakan anonymous object
        // var execute = connection.Execute(sql,new
        // {
        //     productName = product.ProductName,
        //     productPrice= product.ProductPrice,
        //     productStock = product.ProductStock
        // });
        //
        // var message = execute > 0 ? "Successfully add new data" : "Error while add data";
        // Console.WriteLine(message);
        
        // menggunakan dynamic parameter

        // var dynamicObject = new DynamicParameters(product);
        // var execute = connection.Execute(sql, dynamicObject);
        // var message = execute > 0 ? "Successfully add new data" : "Error while add data";
        // Console.WriteLine(message);
    }

    private static void ExecuteReader(SqlConnection connection)
    {
        // menggunakan executeReader

        var sql = "SELECT * FROM m_product";
        var reader = connection.ExecuteReader(sql);
        while (reader.Read())
        {
            Console.WriteLine(new Product
            {
                Id = Convert.ToInt32(reader["id"]),
                ProductName = reader["product_name"].ToString()!,
                ProductPrice = Convert.ToInt64(reader["product_price"]),
                ProductStock = Convert.ToInt32(reader["product_stock"])
            });
        }
    }

    private static void GetOneData(SqlConnection connection)
    {
        /*
         * menampilkan single data dapat menggunakan method :
         * - QuerySingle                => mengembalikan satu data berupa dynamic -> menampilkan exception jika data tidak ada atauoun lebih dari satu
         * - QuerySingle<T>
         * - QuerySingleOrDefault       => mengembalikan satu data berupa dynamic -> menampilkan exception jika data lebih dari satu, jika data tidak ketemu return null
         * - QuerySingleOrDefault<T>
         * - QueryFirst                 => mengembalikan satu data berupa dynamic -> menampilkan exception apabila data tidak ditemukan
         * - QueryFirst<T>
         * - QueryFirstOrDefault        => mengembalikan satu data berupa dynamic -> tidak menampilkan exception apapun
         * - QueryFirstOrDefault<T>
         */
        var sql = "SELECT * FROM m_product WHERE id = 5";
        var product = connection.QueryFirstOrDefault<Product>(sql);
        Console.WriteLine(product);
    }

    private static void GetAllData(SqlConnection connection)
    {
        var sql = "SELECT * FROM m_product";
        // get data and map to object
        
        /*
         * cara kedua kita bisa melakukan config sebelum query untuk melakukan mapping atribut sql ke object
         */

        DefaultTypeMap.MatchNamesWithUnderscores = true;
        var getAll = connection.Query<Product>(sql).ToList();

        // cara pertama
        // var getAll = connection.Query(sql).Select(data => new Product
        // {
        //     Id = data.id,
        //     Name = data.product_name,
        //     Price = data.product_price,
        //     Stock = data.product_stock
        // }).ToList();

        if (getAll.Count > 0)
        {
            foreach (var data in getAll)
            {
                Console.WriteLine(data);
            }
        }
        else
        {
            Console.WriteLine("Data Kosong");
        }
    }

    private static void CreateData(SqlConnection connection)
    {
        var sql = @"INSERT INTO m_product(product_name, product_price, product_stock) VALUES ('Baju Munyuk', 5000, 10)";
        var execute = connection.Execute(sql);
        var message = execute > 0 ? "Successfully save data" : "Error while save data";
        Console.WriteLine(message);
    }

    private static void CreateTable(SqlConnection connection)
    {
        var sql = @"CREATE TABLE m_product
                (
                    id int identity primary key,
                    product_name varchar(100),
                    product_price bigint,
                    product_stock int
                )";

        connection.Execute(sql);
    }
}