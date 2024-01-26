using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using PostOffice.Admin.Services;
using PostOffice.API.Data.Enums;
using PostOffice.API.Data.Models;
using PostOffice.API.DTOs.ParcelOrder;
using PostOffice.API.DTOs.User;

namespace PostOffice.Admin.Areas.Employee.Controllers
{
    [Area("Employee")]        
    public class ParcelOrderManageController : Controller
    {
        private readonly IParcelOrderApiClient _parcelOrderApiClient;
        private readonly IConfiguration _configuration;

        public ParcelOrderManageController(IParcelOrderApiClient parcelOrderApiClient, 
            IConfiguration configuration)
        {
            _parcelOrderApiClient = parcelOrderApiClient;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize(Roles = "employee")]
        public async Task<IActionResult> Index(string keyword = "a", int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetUserPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _parcelOrderApiClient.GetAllParcelOrderPaging(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.ResultObj);
        }



        [HttpGet]
        [Authorize(Roles = "employee")]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _parcelOrderApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var parcelorder = result.ResultObj;
                var updateRequest = new ParcelOrderUpdateDTO()
                {
                    id = parcelorder.id,
                   
                    sender_name = parcelorder.sender_name,
                    sender_pincode = parcelorder.sender_pincode,
                    sender_address = parcelorder.sender_address,
                    sender_phone = parcelorder.sender_phone,
                    sender_email = parcelorder.sender_email,

                    receiver_name = parcelorder.receiver_name,
                    receiver_pincode = parcelorder.receiver_pincode,
                    receiver_address = parcelorder.receiver_address,
                    receiver_phone = parcelorder.receiver_phone,
                    receiver_email = parcelorder.receiver_email,

                    order_status = parcelorder.order_status,
                    description = parcelorder.description,
                    note = parcelorder.note,
                    parcel_length = parcelorder.parcel_length,
                    parcel_height = parcelorder.parcel_height,
                    parcel_width = parcelorder.parcel_width,
                    parcel_weight = parcelorder.parcel_weight,

                    payer = parcelorder.payer,
                    payment_method = parcelorder.payment_method,

                    send_date = parcelorder.send_date,
                    receive_date = parcelorder.receive_date,

                    vpp_value = parcelorder.vpp_value,
                    total_charge = parcelorder.total_charge
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        [Authorize(Roles = "employee")]
        public async Task<IActionResult> Edit(ParcelOrderUpdateDTO request, int id)
        {
            if (!ModelState.IsValid)
                return View();


            var result = await _parcelOrderApiClient.UpdateParcelOrder(id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Update successfully";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }


        [HttpGet]
        [Authorize(Roles = "employee")]
        public async Task<IActionResult> History(int id)
        {
			
		
			return RedirectToAction("Error", "Home");
		}

		}
}
