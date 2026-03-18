using Microsoft.AspNetCore.SignalR;
using IDFK.Data;
using IDFK.Models;
using Microsoft.AspNetCore.Authorization;

namespace IDFK.Hubs
{
    // [Authorize] מבטיח שרק משתמש מחובר יוכל להשתמש ב-Hub
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string user, string message, string movieId)
        {
            // שליפת המשתמש מהדאטה-בייס כדי למצוא את התמונה שלו
            var dbUser = _context.Users.FirstOrDefault(u => u.Username == user);

            // אם אין תמונה, נשתמש בברירת מחדל
            string profilePic = dbUser?.ProfilePicturePath ?? "/images/default-profile.png";

            // 1. שמירה בדאטה בייס (כמו שעשית קודם)
            var chatMsg = new ChatMessage
            {
                User = user,
                Message = message,
                MovieId = int.Parse(movieId),
                Timestamp = DateTime.Now
            };
            _context.ChatMessages.Add(chatMsg);
            await _context.SaveChangesAsync();

            // 2. שליחה לכולם - הוספנו את profilePic כפרמטר רביעי!
            // הוספנו את chatMsg.Id בסוף
            await Clients.All.SendAsync("ReceiveMessage", user, message, movieId, profilePic, chatMsg.Id);
        }

        [Authorize(Roles = "Admin")] // רק אדמין יכול לקרוא לפונקציה הזו
        public async Task DeleteMessage(int messageId)
        {
            var message = await _context.ChatMessages.FindAsync(messageId);
            if (message != null)
            {
                _context.ChatMessages.Remove(message);
                await _context.SaveChangesAsync();

                // מודיע לכולם למחוק את האלמנט עם ה-ID הספציפי מה-HTML
                await Clients.All.SendAsync("MessageDeleted", messageId);
            }
        }

    }
}
