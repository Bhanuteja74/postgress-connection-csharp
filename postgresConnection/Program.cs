using System;
using Npgsql;

class Program
{
    static void Main()
    {
        var connectionString = "Host=localhost;Username=bhanutejakommi;Database=postgres";

        var conn = new NpgsqlConnection(connectionString);
        conn.Open();
        
        var deleteCmd = new NpgsqlCommand("DELETE FROM student WHERE age=21", conn);
        deleteCmd.ExecuteNonQuery();
        
        var insertCmd = new NpgsqlCommand("INSERT INTO student(name, age) VALUES (@name, @age)", conn);
        insertCmd.Parameters.AddWithValue("name", "Teja");
        insertCmd.Parameters.AddWithValue("age", 21);
        insertCmd.ExecuteNonQuery();


        var cmd = new NpgsqlCommand("SELECT name,age FROM student", conn);
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"Age: {reader.GetInt32(1)}, Name: {reader.GetString(0)}");
        }
        
        conn.Close();
    }
}