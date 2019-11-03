using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TinkerJems.Core.Models;
using TinkerJems.Web2.Services;

namespace TinkerJems.Web2.Pages
{
    public class HomePageModel : PageModel
    {

        public HomePageModel()
        {
            _photoService = new PhotoService();
        }

        private readonly PhotoService _photoService;

        private ApiPhotos photos;

        public ApiPhotos Photos
        {
            get { return photos; }
            set { photos = value; }
        }

       



        public async Task OnGetAsync()
        {
            Photos = await _photoService.GetApiPhotosAsync();
        }
    }
}