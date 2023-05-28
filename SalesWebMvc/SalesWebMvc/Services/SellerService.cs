using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id) {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            try
            { 
                var obj = _context.Seller.Find(id);
                _context.Seller.Remove(obj);
                _context.SaveChanges();
            }
            catch
            {
                throw new IntegrityException("Can't delete seller because he/she has sales");
            }
        }

        public void Update(Seller obj)
        {
            if(!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id Not Found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            } catch (DbConcurrecyException e) {
                throw new DbConcurrecyException(e.Message);
            }
        }
    }
}
