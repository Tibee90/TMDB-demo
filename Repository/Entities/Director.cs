using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    public class Director
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        //public int TmdbId { get; set; }
        public string Biography { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
