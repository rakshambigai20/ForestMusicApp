using Forest.Services.IService;
using Forest.Services.Models;
using Forest.Services.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Forest.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Forest.Data.Models.Domain;
using Humanizer;


namespace Forest.Controllers
{
    public class CartController : ForestController
    {
        List<CartMusic> cart;
        IOrderService orderService;
        IMusicService musicService;
        IUserService userService;
        public CartController()
        {
            orderService = new OrderService();
            musicService = new MusicService();
            userService = new UserService();
        }
        #region(Private Methods)
        bool IsThereAcart()
        {
            var cartJson = HttpContext.Session.GetString("cart");
            return !string.IsNullOrEmpty(cartJson);
        }
        List<CartMusic> GetCart()
        {
            if (!IsThereAcart())
            {
                // Initialize and return an empty cart if there isn't one
                PutCartInSession(new List<CartMusic>());
            }

            string cartJson = HttpContext.Session.GetString("cart");
            List<CartMusic> cart = JsonConvert.DeserializeObject<List<CartMusic>>(cartJson) ?? new List<CartMusic>();
            return cart;

        }
        void PutCartInSession(List<CartMusic> cart)
        {
            string cartJson = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString("cart", cartJson);

        }
        bool IsItemInCart(int musicId, List<CartMusic> cart)
        {
            return cart.Any(cartMusic => cartMusic.Music.Id == musicId);
        }
        #endregion
        #region(Actions)
        [Authorize(Roles = "User")]
        public ActionResult AddToCart(int id)
        { 
            List<CartMusic> cart = GetCart();
            if (cart != null)
            {
                if (IsItemInCart(id, cart))
                {
                    var data = new Dictionary<string, string>();
                    data.Add("musicId", id.ToString());
                    data.Add("toDo", "+");
                    return UpdateCart(data);
                }
            }
            else
                cart = new List<CartMusic>();
            CartMusic item = new CartMusic()
            {
                Music = musicService.GetMusic(id),
                Quantity = 1

            };
            cart.Add(item);
            PutCartInSession(cart);
            return RedirectToAction("DisplayCart");
        }
        [Authorize(Roles = "User")]
        public ActionResult UpdateCart(Dictionary<string, string> data)
        {
            int musicId = int.Parse(data["musicId"]);
            char toDo = data["toDo"][0];
            switch (toDo)
            {
                case '+':
                    if (IsThereAcart())
                    {
                        cart = GetCart();
                        if (IsItemInCart(musicId, cart))
                            cart.Find(o => o.Music.Id == musicId).Quantity++;
                    }
                    else
                    {
                        cart = new List<CartMusic>();
                        cart.Add(new CartMusic
                        {
                            Music = musicService.GetMusic(musicId),
                            Quantity = 1
                        });
                    }
                    PutCartInSession(cart);
                    return RedirectToAction("DisplayCart");
                    break;
                case '-':
                    if (IsThereAcart())
                    {
                        cart = GetCart();
                        if (IsItemInCart(musicId, cart))
                        {
                            CartMusic item = cart.Find(o => o.Music.Id == musicId);
                            if (item.Quantity > 1)
                                cart.Find(o => o.Music.Id == musicId).Quantity--;
                            else
                                cart.Remove(item);
                            PutCartInSession(cart);
                        }
                    }
                    return RedirectToAction("DisplayCart");
                    break;
                case 'x':
                    if (IsThereAcart())
                    {
                        cart = GetCart();
                        if (IsItemInCart(musicId, cart))
                        {
                            CartMusic item = cart.Find(o => o.Music.Id == musicId);
                            cart.Remove(item);
                            PutCartInSession(cart);
                        }

                    }
                    return RedirectToAction("DisplayCart");
                    break;
                default:
                    break;
            }
            return View();
        }
        [Authorize(Roles = "User")]
        public ActionResult DisplayCart()
        {
            List<CartMusic> cart = GetCart();
            if (cart != null)
                return View(cart);
            return View();
        }
        [Authorize(Roles = "User")]
        public ActionResult CheckOut()
        {
            string userId = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userId))
            {
                // Log the error or handle it as appropriate for your application.
                // For now, we'll return to a login page or an error page.
                return RedirectToAction("Login", "Account"); // Update with the correct controller/action
            }
            User user = userService.GetUser(userId);
            if (user == null)
            {
                // Log the error or handle it as appropriate for your application.
                // The user wasn't found, redirect to an error page or handle accordingly.
                return RedirectToAction("Error", "Home"); // Update with the correct controller/action
            }
            List<CartMusic> cart = GetCart();
            CheckOutUser checkOutUser = new CheckOutUser()
            {
                User = user,
                Cart = cart,
            };
            return View(checkOutUser);

            
        }

        // POST: CartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        #endregion
    }
}
