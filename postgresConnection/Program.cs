using System;
using System.IO;
using Npgsql;
using DotNetEnv;

class Program
{
    static void Main()
    {

        
        var envString = DB_String();

        using var conn = new NpgsqlConnection(envString);
        conn.Open();
        
        var deleteCmd = new NpgsqlCommand("DELETE FROM student WHERE age=16", conn);
        deleteCmd.ExecuteNonQuery();
        
        var insertCmd = new NpgsqlCommand("INSERT INTO student(name, age) VALUES (@name, @age)", conn);
        insertCmd.Parameters.AddWithValue("name", "Hari");
        insertCmd.Parameters.AddWithValue("age", 16);
        insertCmd.ExecuteNonQuery();
        
        var cmd = new NpgsqlCommand("SELECT name,age FROM student", conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"Age: {reader.GetInt32(1)}, Name: {reader.GetString(0)}");
        }
        
        conn.Close();
    }

    private static string? DB_String()
    {
        Env.Load(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".env"));

        var envString = Environment.GetEnvironmentVariable("DB_CONNECTION");
        return envString;
    }
}