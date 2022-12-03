﻿using LibraryMVC.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryMVC.Models
{
    public class AddBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Translation { get; set; }
        [DisplayName("Number of pages")]
        public int NumberOfPages { get; set; }
        [DisplayName("Date of publication")]
        [DataType(DataType.Date)]
        public DateTime DateOfPublication { get; set; }
        [DisplayName("ID Number")]
        public string IDNumber { get; set; }
        [DisplayName("Image")]
        public string ImageUrl { get; set; }
        [DisplayName("Number of pieces available")]
        public int NumberOfPiecesAvailable { get; set; }
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public IFormFile File { get; set; }
    }
}
