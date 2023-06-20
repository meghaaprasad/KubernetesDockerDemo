using Microsoft.EntityFrameworkCore;

namespace NAGPKubernetesDockerDemo.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize(AppDbContext dbContext)
        {
            //using (var dbContext = serviceProvider.GetRequiredService<AppDbContext>())
            //{
                bool isDatabaseCreated = dbContext.Database.EnsureCreated();

                if (isDatabaseCreated)
                {
                    Console.WriteLine("Creating Database");
                    // Execute raw SQL script to create the table
                    dbContext.Database.ExecuteSqlRaw(@"
                        CREATE DATABASE IF NOT EXISTS bookmanagementdb;
                        USE bookmanagementdb;
                        CREATE TABLE IF NOT EXISTS books (
                            id INT NOT NULL AUTO_INCREMENT,
                            title VARCHAR(255) NOT NULL,
                            author VARCHAR(255) NOT NULL,
                            language VARCHAR(255) DEFAULT NULL,
                            created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
                            created_by VARCHAR(255) DEFAULT NULL,
                            updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
                            updated_by VARCHAR(255) DEFAULT NULL,
                            PRIMARY KEY (id)
                        );
                    ");

                    // Check if the table is empty
                    bool isTableEmpty = !dbContext.books.Any();

                    if (isTableEmpty)
                    {
                        Console.WriteLine("Adding Data");
                        dbContext.Database.ExecuteSqlRaw(@"
                    INSERT INTO books (title, author, language)
                    VALUES
                        ('Book 1', 'Author 1', 'English'),
                        ('Book 2', 'Author 2', 'Hindi'),
                        ('Book 3', 'Author 3', 'French'),
                        ('Book 4', 'Author 4', 'French'),
                        ('Book 5', 'Author 5', 'French'),
                        ('Book 6', 'Author 6', 'French'),
                        ('Book 7', 'Author 7', 'French'),
                        ('Book 8', 'Author 8', 'French'),
                        ('Book 9', 'Author 9', 'French'),
                        ('Book 10', 'Author 10', 'French');
                ");

                        dbContext.SaveChanges();
                    }
                }
           // }
        }



    }
}
