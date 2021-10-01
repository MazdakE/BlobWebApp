using Azure.Storage.Blobs;
using BlobStorageV12MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlobStorageV12MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlobServiceClient _blobServiceClient;

        public HomeController(ILogger<HomeController> logger, BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("kirekhar");
            var blobs = blobContainer.GetBlobsAsync();

            List<string> imageNames = new List<string>();

            await foreach (var item in blobs)
            {
                imageNames.Add(item.Name.ToString());
            }


            return View(imageNames);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
