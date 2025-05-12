// See https://aka.ms/new-console-template for more information
using System.Data.SqlClient;

using (var sqlConn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=test;User ID=testUser;Password=home1974"))
{
    sqlConn.Open();
    using (var command = new SqlCommand($"INSERT INTO dbo.Users (Id, Username, FirstName, LastName, IsEnabled, CreateDateUtc, LastLogedInDateUtc) VALUES ('{Guid.NewGuid().ToString("N")}', 'Username1', 'My first name', 'My last name', 1, '10/12/2025 12:32:10 +01:00', '10/12/2025 12:32:10 +01:00')", sqlConn))
    {
        // Log it
        Console.WriteLine("Adding new user...");

        // Execute INSERT command
        var result = command.ExecuteNonQuery();

        // Log what should be "1 user added"
        Console.WriteLine($"{result} user added");
    }

    using (var command = new SqlCommand("Select * from dbo.Users", sqlConn))
    {
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine($"Username: {reader["Username"]}, First Name: {reader["FirstName"]}, LastName: {reader["LastName"]}, IsEnabled: {reader["IsEnabled"]}");
            }
        }
    }

}

