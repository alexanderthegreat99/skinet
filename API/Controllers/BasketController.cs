using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
             if (basket.Items.Any(item => item.Quantity < 0))
            {
                return BadRequest("Item quantity cannot be negative.");
            }
            var updatedBasket = await _basketRepository.UpdateBasketAsync(basket);

            return Ok(updatedBasket);
        }
// [HttpPost]
// public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
// {
//     // Retrieve the existing basket to compare item quantities
//     // if (basket.Items.Any(item => item.Quantity < 0))
//     // {
//     //     // Return a BadRequest response indicating that the item quantity is invalid
//     //     return BadRequest("Item quantity cannot be negative.");
//     // }

//     var existingBasket = await _basketRepository.GetBasketAsync(basket.Id);

//     if (existingBasket != null)
//     {
//         // Create a list to hold items that need to be removed
//         var itemsToRemove = new List<BasketItem>();

//         // Update the item quantities based on the existing basket
//         foreach (var item in basket.Items)
//         {
//             // Find the corresponding item in the existing basket
//             var existingItem = existingBasket.Items.FirstOrDefault(i => i.Id == item.Id);

//             if (existingItem != null)
//             {
//                 // Ensure the item quantity doesn't go below zero
//                 item.Quantity = Math.Max(item.Quantity, 0);

//                 // If the item quantity becomes zero, add it to the itemsToRemove list
//                 if (item.Quantity == 0)
//                 {
//                     itemsToRemove.Add(item);
//                 }
//             } else if ((item.Quantity < 0) && (existingItem.Quantity - item.Quantity <0)){
//                  return BadRequest("Item quantity cannot be negative.");
//             }
//         }

//         // Remove the items that need to be removed from the basket
//         foreach (var itemToRemove in itemsToRemove)
//         {
//             basket.Items.Remove(itemToRemove);
//         }
//     }

//     var updatedBasket = await _basketRepository.UpdateBasketAsync(basket);

//     return Ok(updatedBasket);
// }



        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
} 