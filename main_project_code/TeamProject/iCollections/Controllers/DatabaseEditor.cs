using System;
using System.Collections.Generic;
using System.Linq;
using iCollections.Data;
using iCollections.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace iCollections.Controllers
{
    // This class solely edits database
    public class DatabaseEditor
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICollectionsDbContext _collectionsDbContext;

        public DatabaseEditor(UserManager<IdentityUser> userManager, ICollectionsDbContext collectionsDbContext)
        {
            _userManager = userManager;
            _collectionsDbContext = collectionsDbContext;
        }

        public void ChangePhotoName(Guid id, string newName)
        {
            var selectedPhoto = _collectionsDbContext.Photos.FirstOrDefault(row => row.PhotoGuid == id);
            selectedPhoto.Name = newName;
            _collectionsDbContext.SaveChanges();
        }

    }
}
