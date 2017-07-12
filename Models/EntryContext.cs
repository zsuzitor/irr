using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace irr.Models
{
    public class EntryContext : DbContext
    {
        public DbSet<Entry> Entrys { get; set; }

        /*protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }*/
    }
}