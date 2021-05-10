using System;
using iCollections.Data;
using iCollections.Models;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using iCollections.Data.Abstract;


namespace iCollections.Controllers
{
    // All this class does is retrieves photo(s)
    public class ImageApiController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IPhotoRepository _photoRepo;

        public ImageApiController(UserManager<IdentityUser> userManager, IPhotoRepository photoRepo)
        {
            _userManager = userManager;
            _photoRepo = photoRepo;
        }

        [HttpGet]
        [ActionName("Thumbnail")]
        public IActionResult GetThumbnail(string id)
        {
            var guid = Guid.Parse(id);
            var selectedPhoto = _photoRepo.GetPhoto(guid);
            if (selectedPhoto.Data == null) return new EmptyResult();
            return File(selectedPhoto.Data, "image/base64");
        }

        [HttpPost]
        [ActionName("Thumbnail")]
        public IActionResult ChangeThumbnail(string id, string fileName)
        {
            // do work in here ie change the name of the photo
            _photoRepo.ChangePhotoName(Guid.Parse(id), fileName);
            return Content(fileName);
        }

        // if you change changePhotoName() to asynchronous method, it works but you get this error:
    //     fail: Microsoft.AspNetCore.Server.Kestrel[13]
    //   Connection id "0HM892GUATJ18", Request id "0HM892GUATJ18:0000000A": An unhandled exception was thrown by the application.
    //   System.InvalidOperationException: The connection does not support MultipleActiveResultSets.
    //      at Microsoft.Data.SqlClient.SqlInternalConnectionTds.ValidateConnectionForExecute(SqlCommand command)
    //      at Microsoft.Data.SqlClient.SqlInternalTransaction.Rollback()
    //      at Microsoft.Data.SqlClient.SqlInternalTransaction.Dispose(Boolean disposing)
    //      at Microsoft.Data.SqlClient.SqlInternalTransaction.Dispose()
    //      at Microsoft.Data.SqlClient.SqlTransaction.Dispose(Boolean disposing)
    //      at System.Data.Common.DbTransaction.DisposeAsync()
    //      at Microsoft.EntityFrameworkCore.Storage.RelationalTransaction.DisposeAsync()
    //      at Microsoft.EntityFrameworkCore.Storage.RelationalConnection.DisposeAsync()
    //      at Microsoft.Extensions.DependencyInjection.ServiceLookup.ServiceProviderEngineScope.<DisposeAsync>g__Await|15_0(Int32 i, ValueTask vt, List`1 toDispose)
    //      at Microsoft.Extensions.DependencyInjection.ServiceLookup.ServiceProviderEngineScope.<DisposeAsync>g__Await|15_0(Int32 i, ValueTask vt, List`1 toDispose)
    //      at Microsoft.AspNetCore.Http.Features.RequestServicesFeature.<DisposeAsync>g__Awaited|9_0(RequestServicesFeature servicesFeature, ValueTask vt)
    //      at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpProtocol.<FireOnCompleted>g__ProcessEvents|227_0(HttpProtocol protocol, Stack`1 events)
    }
}