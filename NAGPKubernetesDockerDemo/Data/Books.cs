using System.ComponentModel.DataAnnotations;

namespace NAGPKubernetesDockerDemo.Data
{
    public class books
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Author { get; set; }

        public string Language { get; set; }
    }
}
