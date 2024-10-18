using CateringManagement.CustomControllers;
using CateringManagement.Data;
using CateringManagement.Models;
using CateringManagement.Utilities;
using CateringManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using String = System.String;

namespace CateringManagement.Controllers
{
    public class CustomerController : ElephantController
    {
        private readonly IMyEmailSender _emailSender;
        private readonly CateringContext _context;
        public CustomerController(CateringContext context, IMyEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: Customers
        public async Task<IActionResult> Index(int? page, int? pageSizeID,
            string actionButton, string sortDirection = "asc", string sortField = "Customer")
        {
            //List of sort options.
            //NOTE: make sure this array has matching values to the column headings
            string[] sortOptions = new[] { "Customer", "Company Name", "Phone", "Customer Code" };

            var customers = _context.Customers
                .Include(p => p.CustomerThumbnail)
                .AsNoTracking();

            //Before we sort, see if we have called for a change of filtering or sorting
            if (!String.IsNullOrEmpty(actionButton)) //Form Submitted!
            {
                page = 1;//Reset page to start

                if (sortOptions.Contains(actionButton))//Change of sort is requested
                {
                    if (actionButton == sortField) //Reverse order on same field
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton;//Sort by the button clicked
                }
            }
            //Now we know which field and direction to sort by
            if (sortField == "Company Name")
            {
                if (sortDirection == "asc")
                {
                    customers = customers
                        .OrderBy(p => p.CompanyName);
                }
                else
                {
                    customers = customers
                        .OrderByDescending(p => p.CompanyName);
                }
            }
            else if (sortField == "Phone")
            {
                if (sortDirection == "asc")
                {
                    customers = customers
                        .OrderBy(p => p.Phone);
                }
                else
                {
                    customers = customers
                        .OrderByDescending(p => p.Phone);
                }
            }
            else if (sortField == "Customer Code")
            {
                if (sortDirection == "asc")
                {
                    customers = customers
                        .OrderBy(p => p.CustomerCode);
                }
                else
                {
                    customers = customers
                        .OrderByDescending(p => p.CustomerCode);
                }
            }
            else //Sorting by Customer
            {
                if (sortDirection == "asc")
                {
                    customers = customers
                        .OrderBy(p => p.LastName)
                        .ThenBy(p => p.FirstName);
                }
                else
                {
                    customers = customers
                        .OrderByDescending(p => p.LastName)
                        .ThenByDescending(p => p.FirstName);
                }
            }
            //Set sort for next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            //Handle Paging
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<Customer>.CreateAsync(customers.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: Customers/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Functions)
                .Include(p => p.CustomerPhoto)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        [Authorize(Roles ="User,Staff,Supervisor,Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "User,Staff,Supervisor,Admin")]
        public async Task<IActionResult> Create([Bind("ID,FirstName,MiddleName,LastName,CompanyName," +
            "Phone, Email ,CustomerCode")] Customer customer, IFormFile thePicture)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await AddPicture(customer, thePicture);
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    await _context.SaveChangesAsync();
                    //Send on to add appointments
                    return RedirectToAction("Index", "CustomerFunction", new { CustomerID = customer});
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Customers.CustomerCode"))
                {
                    ModelState.AddModelError("CustomerCode", "Unable to save changes. Remember, you cannot have duplicate Customer Codes.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            //Decide if we need to send the Validaiton Errors directly to the client
            if (!ModelState.IsValid && Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                //Was an AJAX request so build a message with all validation errors
                string errorMessage = "";
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        errorMessage += error.ErrorMessage + "|";
                    }
                }
                //Note: returning a BadRequest results in HTTP Status code 400
                return BadRequest(errorMessage);
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "User,Staff,Supervisor,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(p => p.CustomerPhoto)
                .FirstOrDefaultAsync(p => p.ID == id);
            if (customer == null)
            {
                return NotFound();
            }
            if (User.IsInRole("Staff"))
            {
                if (customer.CreatedBy != User.Identity.Name)
                {
                    ModelState.AddModelError("", "As a staff, you cannot delete this " +
                        "Customer because you did not enter them into the system.");
                    ViewData["NoSubmit"] = "disabled=disabled";
                }
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "User,Staff,Supervisor,Admin")]
        public async Task<IActionResult> Edit(int id, string chkRemoveImage, IFormFile thePicture)
        {
            //Go get the customer to update
            var customerToUpdate = await _context.Customers
                .Include(p => p.CustomerPhoto)
                .FirstOrDefaultAsync(p => p.ID == id);

            //Check that we got the customer or exit with a not found error
            if (customerToUpdate == null)
            {
                return NotFound();
            }

            //Try updating it with the values posted
            if (await TryUpdateModelAsync<Customer>(customerToUpdate, "",
                c => c.FirstName, c => c.MiddleName, c => c.LastName, c => c.CompanyName,
                c => c.Phone, c =>c.Email ,c => c.CustomerCode))
            {
                try
                {
                    //For the image
                    if (chkRemoveImage != null)
                    {
                        //If we are just deleting the two versions of the photo, we need to make sure the Change Tracker knows
                        //about them both so go get the Thumbnail since we did not include it.
                        customerToUpdate.CustomerThumbnail = _context.CustomerThumbnails.Where(p => p.CustomerID == customerToUpdate.ID).FirstOrDefault();
                        //Then, setting them to null will cause them to be deleted from the database.
                        customerToUpdate.CustomerPhoto = null;
                        customerToUpdate.CustomerThumbnail = null;
                    }
                    else
                    {
                        await AddPicture(customerToUpdate, thePicture);
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { customerToUpdate.ID });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customerToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException dex)
                {
                    if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Customers.CustomerCode"))
                    {
                        ModelState.AddModelError("CustomerCode", "Unable to save changes. Remember, you cannot have duplicate Customer Codes.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    }
                }
            }
            return View(customerToUpdate);
        }

        // GET: Customers/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customers == null)
            {
                return Problem("There are no Customers to delete.");
            }
            var customer = await _context.Customers.FindAsync(id);
            try
            {
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                }
                await _context.SaveChangesAsync();
                return Redirect(ViewData["returnURL"].ToString());
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to Delete Customer. Remember, you cannot delete a Customer that has a function in the system.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(customer);

        }
        //For Adding Specialty
        private SelectList SpecialtySelectList(string skip)
        {
            var SpecialtyQuery = _context.Customers
                .AsNoTracking();

            if (!String.IsNullOrEmpty(skip))
            {
                //Convert the string to an array of integers
                //so we can make sure we leave them out of the data we download
                string[] avoidStrings = skip.Split('|');
                int[] skipKeys = Array.ConvertAll(avoidStrings, s => int.Parse(s));
                SpecialtyQuery = SpecialtyQuery
                    .Where(s => !skipKeys.Contains(s.ID));
            }
            return new SelectList(SpecialtyQuery.OrderBy(d => d.FullName), "ID", "SpecialtyName");
        }
        [HttpGet]
        public JsonResult GetSpecialties(string skip)
        {
            return Json(SpecialtySelectList(skip));
        }
        private async Task AddPicture(Customer customer, IFormFile thePicture)
        {
            //Get the picture and save it with the Customer (2 sizes)
            if (thePicture != null)
            {
                string mimeType = thePicture.ContentType;
                long fileLength = thePicture.Length;
                if (!(mimeType == "" || fileLength == 0))//Looks like we have a file!!!
                {
                    if (mimeType.Contains("image"))
                    {
                        using var memoryStream = new MemoryStream();
                        await thePicture.CopyToAsync(memoryStream);
                        var pictureArray = memoryStream.ToArray();//Gives us the Byte[]

                        //Check if we are replacing or creating new
                        if (customer.CustomerPhoto != null)
                        {
                            //We already have pictures so just replace the Byte[]
                            customer.CustomerPhoto.Content = ResizeImage.shrinkImageWebp(pictureArray, 500, 600);

                            //Get the Thumbnail so we can update it.  Remember we didn't include it
                            customer.CustomerThumbnail = _context.CustomerThumbnails.Where(p => p.CustomerID == customer.ID).FirstOrDefault();
                            customer.CustomerThumbnail.Content = ResizeImage.shrinkImageWebp(pictureArray, 75, 90);
                        }
                        else //No pictures saved so start new
                        {
                            customer.CustomerPhoto = new CustomerPhoto
                            {
                                Content = ResizeImage.shrinkImageWebp(pictureArray, 500, 600),
                                MimeType = "image/webp"
                            };
                            customer.CustomerThumbnail = new CustomerThumbnail
                            {
                                Content = ResizeImage.shrinkImageWebp(pictureArray, 75, 90),
                                MimeType = "image/webp"
                            };
                        }
                    }
                }
            }
        }
        // GET/POST: customer/Notification/5
        public async Task<IActionResult> Notification(int? id, string Subject, string emailContent)
        {
            if (id == null)
            {
                return NotFound();
            }
            Customer t = await _context.Customers.FindAsync(id);

            ViewData["id"] = id;
            ViewData["FirstName"] = t.FirstName;

            if (string.IsNullOrEmpty(Subject) || string.IsNullOrEmpty(emailContent))
            {
                ViewData["Message"] = "You must enter both a Subject and some message Content before sending the message.";
            }
            else
            {
                int folksCount = 0;
                try
                {
                    //Send a Notice.
                    List<EmailAddress> folks = (from p in _context.Customers
                                                where p.ID == id
                                                select new EmailAddress
                                                {
                                                    Name = p.FirstName,
                                                    Address = p.Email
                                                }).ToList();
                    folksCount = folks.Count;
                    if (folksCount > 0)
                    {
                        var msg = new EmailMessage()
                        {
                            ToAddresses = folks,
                            Subject = Subject,
                            Content = "<p>" + emailContent + "</p><p>Please access the <strong>Niagara College</strong> web site to review.</p>"

                        };
                        
                        ViewData["Message"] = "Message sent to " + folksCount + "Customer"
                            + ((folksCount == 1) ? "." : "s.");
                    }
                    else
                    {
                        ViewData["Message"] = "Message NOT sent!  No Customer in List.";
                    }
                }
                catch (Exception ex)
                {
                    string errMsg = ex.GetBaseException().Message;
                    ViewData["Message"] = "Error: Could not send email message to the " + folksCount + " Customer"
                        + ((folksCount == 1) ? "" : "s") + " in the List.";
                }
            }
            return View();
        }
        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.ID == id);
        }
    }
}
