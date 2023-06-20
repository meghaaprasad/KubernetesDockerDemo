using FluentMigrator;
using MySql.Data.MySqlClient;

namespace NAGPKubernetesDockerDemo.Data
{
    [Migration(1)]
    public class CreateDatabaseAndBooksTable : Migration
    {
        public override void Up()
        {
            Execute.Sql("CREATE DATABASE IF NOT EXISTS bookmanagementdb;");

            Create.Table("books")
                .InSchema("bookmanagementdb")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Title").AsString(255)
                .WithColumn("Author").AsString(255)
           .WithColumn("Language").AsString(255);

            // Check if the table is empty
            if (!Schema.Table("books").Exists())
                return;

            // Check if records exist in the table
            //if (Execute.WithConnection(CheckIfRecordsExistQuery))
            //    return;

            // Insert records into the table
            Execute.Sql("INSERT INTO bookmanagementdb.books (Title, Author, Language) VALUES ('Book 1', 'Author 1','English')");
           // InsertRecords();
        }

        public override void Down()
        {
            Delete.Table("books").InSchema("bookmanagementdb");

            Execute.Sql("DROP DATABASE IF EXISTS bookmanagementdb;");
        }

        private static bool CheckIfRecordsExistQuery(MySqlConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM BookManagementDb.Books";
                return (long)command.ExecuteScalar() > 0;
            }
        }

        //private static void InsertRecords()
        //{
        //    // Perform your insert operations here
        //    // For example:
        //    Execute.Sql("INSERT INTO BookManagementDb.Books (Title, Author, Language) VALUES ('Book 1', 'Author 1','English')");
        //    // Execute.Sql("INSERT INTO BookManagementDb.Books (Title, Author) VALUES ('Book 2', 'Author 2')");
        //    // Execute.Sql("INSERT INTO BookManagementDb.Books (Title, Author) VALUES ('Book 3', 'Author 3')");
        //}
    }
}
