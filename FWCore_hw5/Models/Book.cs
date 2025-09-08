using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCore_hw5.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public decimal Price { get; set; }
        public Author? Author { get; set; }
        public Genre? Genre { get; set; }

        public override string ToString() => $"{Title} ({Price}$)";
    }
}
