using Microsoft.AspNetCore.Mvc;
using Shop_P41.Services;

namespace Shop_P41.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            var images = await _imageService.GetImagesAsync();
            return View(images);
        }
        [HttpGet]
        public IActionResult Insert() => View(new ProductImage { ImageUrl= "Default ImageUrl", ProductId=0 });
        [HttpPost]
        public async Task<IActionResult> Insert(ProductImage productImage)
        {
            if (ModelState.IsValid)
            {
                await _imageService.InsertImageAsync(productImage);
                return RedirectToAction(nameof(Index));
            }
            return View(productImage);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var productImage = await _imageService.GetImageByIdAsync(id);
            return View(productImage);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductImage productImage)
        {
            if (ModelState.IsValid)
            {
                await _imageService.UpdateImageAsync(productImage.Id, productImage);
                return RedirectToAction(nameof(Index));
            }
            return View(productImage);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var productImage = await _imageService.GetImageByIdAsync(id);
            return View(productImage);
        }
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _imageService.DeleteImageAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
