using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RioUpesi.CORE.DataContext;
using RioUpesi.CORE.Models.Rooms;
using RioUpesi.CORE.ViewModel.RoomsVm;
using RioUpesi.INFRASTRUCTURE.Iservices.IRoomServices;

namespace RioUpesi.Controllers
{

    [Route("api/[controller]", Name = "Rooms")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly RioContext _context;
        private readonly IRoomServices _roomservices;

        public RoomsController(IHttpContextAccessor contextAccessor,RioContext context, IRoomServices roomservices)
        {
            _contextAccessor = contextAccessor;
            _context = context;
            _roomservices = roomservices;
        }

        [Authorize]
        [Route("AddRoom")]
        [HttpPost]
        public async Task<string> AddRoom(AddRoomsVm vm)
        {
            var res = await _roomservices.AddRoom(vm);
            return res;
        }

        [Authorize]
        [Route("RemoveRoom")]
        [HttpPost]
        public async Task<string> RemoveRoom(string roomID)
        {
            var res= await _roomservices.RemoveRoom(roomID);
            return res; 
        }


        [Authorize]
        [Route("EditRoom")]
        [HttpPost]
        public async   Task<string> EditRoomDetails(AddRoomsVm vm)
        {
            var res= await _roomservices.EditRoomDetails(vm);
            return res; 
        }


        [Authorize]
        [Route("RoomPayment")]
        [HttpPost]
        public async Task<string> RoomPayment(int bookroomID, decimal paymentAmount)
        {
          var res= await _roomservices.RoomPayment(bookroomID, paymentAmount);
            return res;
        }

        [Authorize]
        [Route("GetAllBookings")]
        [HttpGet]
        public async Task<IEnumerable<BookRoom>> GetAllBookings()
        {
            var res= await _roomservices.GetAllBookings();  
            return res;
        }

        [Authorize]
        [Route("GetAllUserBookings")]
        [HttpGet]
        public async Task<IEnumerable<BookRoom>> GetAllUserBookings()
        {
            var res= await _roomservices.GetAllUserBookings();  
            return res;  
        }

        [Authorize]
        [Route("BookingRoom")]
        [HttpPost]
        public async Task<string> BookingRoom(BookRoomvm vm)
        {
            var rss= await _roomservices.BookingRoom(vm);
            return rss;
        }


        [Authorize]
        [Route("AddingRomToStore")]
        [HttpPost]
        public async Task<string> AddRoomToStore(RoomStoreVm vm)
        {
            var rss= await _roomservices.AddRoomToStore(vm);
            return rss;
        }

        [Authorize]
        [Route("RoomCheckout")]
        [HttpPost]
        public async Task<string> RoomCheckout(int roomid)
        {
            return await _roomservices.RoomCheckout(roomid);
        }
    }
}
