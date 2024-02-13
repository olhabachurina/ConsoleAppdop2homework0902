using ConsoleAppdop2homework0902.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleAppdop2homework0902
{
    public class LibraryService
    {
        private readonly LibraryContext _context;

        public LibraryService(LibraryContext context)
        {
            _context = context;
        }

        public async Task AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            await LogBookChanges(book.Id, "Added", null, JsonSerializer.Serialize(book));
        }

        public async Task UpdateBook(Book newBook)
        {
            var oldBook = await _context.Books.FindAsync(newBook.Id);
            var oldBookJson = JsonSerializer.Serialize(oldBook);
            var newBookJson = JsonSerializer.Serialize(newBook);
            _context.Entry(oldBook).CurrentValues.SetValues(newBook);
            await _context.SaveChangesAsync();

            
            await LogBookChanges(newBook.Id, "Updated", oldBookJson, newBookJson);
        }

        public async Task DeleteBook(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                await LogBookChanges(book.Id, "Deleted", JsonSerializer.Serialize(book), null);

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Book> GetBookById(int bookId)
        {
            return await _context.Books.FindAsync(bookId);
        }

        private async Task LogBookChanges(int bookId, string action, string oldValue, string newValue)
        {
            var changeLog = new BookChangeLog
            {
                BookId = bookId,
                PropertyName = action,
                OldValue = oldValue ?? "", 
                NewValue = newValue ?? "", 
                ChangeDateTime = DateTime.UtcNow
            };
            _context.BookChangeLogs.Add(changeLog);
            await _context.SaveChangesAsync();
        }
    }
}
