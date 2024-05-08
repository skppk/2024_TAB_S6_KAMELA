using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Runtime.InteropServices;

/**
 * Klasa odpowiadajaca za operacje na bazie danych programu 
 */
public class DatabaseOperations
{
    private static string connectionParams;
    private MySqlConnection connect;

    public DatabaseOperations()
    {
        connectionParams = "server=localhost; port=3306; user=root; password=root;database=mydb";
        connect = new MySqlConnection(connectionParams);
    }

    public void Activate()
    {
        Console.WriteLine("Connection properties:");
        Console.WriteLine(connectionParams);
        try
        {
            connect.Open();
            Console.WriteLine("Connected successfully to database");
            var command = connect.CreateCommand();
            command.CommandText = @"SELECT*FROM osoby";
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("First row from osoby table");
                Console.WriteLine(reader.GetInt32(0));
                Console.WriteLine(reader.GetString(1));
                Console.WriteLine(reader.GetString(2));
                Console.WriteLine(reader.GetInt32(3));
            }
            reader.Close();
            var command1 = connect.CreateCommand();
            command1.CommandText = @"INSERT INTO osoby VALUES(2,""Jakis"", ""Test"", 2, null)";
            command1.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unable to connect to database");
            Console.WriteLine(ex.ToString());
        }
    }
}
