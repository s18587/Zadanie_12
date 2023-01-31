using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Shared.DTOs
{
    public class MovieDto
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public bool InTheaters { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Poster { get; set; }
    }
}