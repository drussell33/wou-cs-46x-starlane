using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using iCollections.Models;
using iCollections.Data;
using System.IO;
using iCollections.Controllers;

namespace iCollections.Models
{

    public partial class CreateCollectionRoute
    {
        [Required]
        public string Route { get; set; }
    }

    public partial class CreateCollectionPublishing
    {
        [RegularExpression(@"^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$")]
        [StringLength(30), MinLength(3)]
        [Required(ErrorMessage = "The Collection Name must only contains letters or numbers and be between 3 and 30 characters")]
        [Display(Name = "iCollection Name")]
        public string CollectionName { get; set; }

        [Required]
        [Display(Name = "Visibility")]
        public string Visibility { get; set; }

        [RegularExpression(@"^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$", ErrorMessage = "The Collection Description must only contains letters or numbers and be between 3 and 30 characters")]
        [StringLength(60)]
        public string Description { get; set; }
    }


    //Class that contains the data being sent to the javascript via DOM.
    public partial class RenderingPhoto
    {
        public string Data { get; set; }
        public string Title { get; set; }
        public int Rank { get; set; }
        public string Description { get; set; }

        public RenderingPhoto(string data, string title, int rank, string description)
        {
            Data = data;
            Title = title;
            Rank = rank;
            Description = description;
        }
    }



}