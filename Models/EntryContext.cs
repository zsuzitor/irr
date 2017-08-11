using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace irr.Models
{
    public class EntryContext : DbContext
    {
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Entry_img> Images { get; set; }
        public DbSet<Entry_info> Info { get; set; }

        /*protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }*/
    }
}