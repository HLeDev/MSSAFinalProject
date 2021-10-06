using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MSSAProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSAProject.Controllers
{
    //625.  Specify Attribute for the whole class, must log in IoT to access anything in the class, if it's here it will affect everything within the class
    [Authorize]
    public class AccountController : Controller
    {
        //632.  Call user manager here
        private readonly UserManager<AppUser> uManager;
        //652.  Call sign in manager here
        private readonly SignInManager<AppUser> sManager;
        //678.  Call password hasher here
        private readonly IPasswordHasher<AppUser> pwHash;
        public AccountController(UserManager<AppUser> uManager, SignInManager<AppUser> sManager, IPasswordHasher<AppUser> pwHash)
        {
            this.uManager = uManager;
            this.sManager = sManager;
            this.pwHash = pwHash;
        }
        //633.  Go back to Register Post Method

        //626.  Create registrion method to return view  GET /account/register
        [AllowAnonymous]
        public IActionResult Register() => View();
        //627.  Go to User class and add validation attribute
        //628.  Create a Razer View Page call Register, using Create Template and Reference User Class

        //629.  Create a Post method for Register POST /account/register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            //630.  Need to check if ModelState is valid, then create a new user
            if (ModelState.IsValid)
            {
                AppUser aUser = new AppUser
                {
                    UserName = user.UserName,
                    Email = user.Email
                };

                //631.  Create a new user using IdentityUser, add Dependencies for UserManager
                //634.  Call identity result, iresult represent the result of an identity operation
                IdentityResult result = await uManager.CreateAsync(aUser, user.Password);

                //635.  Create an if statement for successful registration or route to errors for specific error using a foreach
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach(IdentityError error in result.Errors )
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                //636.  Go to StartUp.cs to edit passwords models, current restriction is from db
            }
            return View(user);
        }


        //643.  Create Login Method
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            //644.  Need to get ReturnUrl property
            Login login = new Login
            {
                ReturnUrl = returnUrl
            };
            return View(login);
            //645.  Create Razer View Page for Login

        }


        //650.  Create a Post method for login POST /account/login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            //651.  Need to add Dependency for SignInManager at the top of class
            if (ModelState.IsValid)
            {
                //652.  Need to get the user by email for logging in
                AppUser aUser = await uManager.FindByEmailAsync(login.Email);
                //653.  Need to check if the user exist
                if(aUser != null)
                {
                    //654.  Call signinresult to check validation
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await sManager.PasswordSignInAsync(aUser, login.Password, false, false);
                    //655. If succeeded, return to ReturnUrl property or Home Page
                    if (result.Succeeded)
                    {
                        return Redirect(login.ReturnUrl ?? "/");
                    }
                    //656.  If it doesnt exist, call modelerror 
                    ModelState.AddModelError("", "Login failed, please register or check credentials");
                    //657.  Add Authorize Attribute to ProductsController, login and see if it works, then
                    //Inspect browser application for cookies if login is successful
                }
            }
            return View(login);
        }

        //657.  Create a Logout method after Login is successful
        public async Task<IActionResult> Logout()
        {
            //658.  Call the signin manager for the signout method
            await sManager.SignOutAsync();
            //659.  Then redirect back to home page
            return Redirect("/");
            //660.  Go to main _Layout and add logout method

        }

        //668.  Credit an Edit and View Page for User Edit  GET /account/edit
        public async Task<IActionResult> Edit()
        {
            //669.  Get the User
            AppUser aUser = await uManager.FindByNameAsync(User.Identity.Name);
            UserEdit user = new UserEdit(aUser);
            return View(user);

            //670.  Create an Edit Razer View Page using Edit Template and User Model Class, go to Page and make edits
            //671.  Go to main _Layout and add an Edit Link after user have signed in

        }

        //672.  Create a Post Edit method to update the email and password
        //673.  Create a Class call UserEdit in Models, go to class
        //677.  Prior to Posting the edit, add Password hasher dependencies up top
        //679.  Create the method

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEdit uEd)
        {
            //680.  Find user by name first
            AppUser aUser = await uManager.FindByNameAsync(User.Identity.Name);

            //681.  Check if modelstate is valid, get user email, then check if user pw is null, if not update the pw
            if (ModelState.IsValid)
            {
                aUser.Email = uEd.Email;

                if(uEd.Password != null)
                {
                    aUser.PasswordHash = pwHash.HashPassword(aUser, uEd.Password);
                }
                //682.  Call identity result for update method
                IdentityResult result = await uManager.UpdateAsync(aUser);
                //683.  Check if succeeded, if succeed, redirect back to home page, otherwise, return to View()
                if (result.Succeeded)
                {
                    TempData["Success"] = "Account has been updated";
                    //684.  Need to get notification from Admin into main Shared Folder to use TempData and then include it in the main _Layout file
                }
            }
            return View();
            
            //685.  Need to add a user list in admin area, Create a Controller called UserController in Areas > Admin > Controller 
        }

    }
}
