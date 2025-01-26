using System.Data.Entity;
using myProperty.Models;

public class myPropertyDBContext : DbContext
{
    // Constructor specifying the connection string
    public myPropertyDBContext() : base("myPropertyDBContext")
    {
        // Enables lazy loading (optional based on your requirements)
        this.Configuration.LazyLoadingEnabled = true;
    }

    // DbSet properties for each table in the database
    public DbSet<User> Users { get; set; }
    public DbSet<Property> Property { get; set; }
    public DbSet<Auction> Auction { get; set; }
    public DbSet<Bid> Bid { get; set; }
    public DbSet<AuctionReport> AuctionReports { get; set; }

    // Configuring model relationships
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        // Configuring the Property table's relationship with the User table
        modelBuilder.Entity<Property>()
         .HasRequired(p => p.User) // Property requires a User (Seller)
         .WithMany(u => u.Properties) // User can have multiple Properties
         .HasForeignKey(p => p.SellerID) // Foreign Key
         .WillCascadeOnDelete(false); // Prevent cascading delete

        // Configuring the Bid table's relationship with the User table
        modelBuilder.Entity<Bid>()
            .HasRequired(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.BuyerID)
            .WillCascadeOnDelete(false);

        // Configuring the Auction table's relationship with the Property table
        modelBuilder.Entity<Auction>()
            .HasRequired(a => a.Property)
            .WithMany()
            .HasForeignKey(a => a.PropertyID)
            .WillCascadeOnDelete(false);

        // Configuring the AuctionReport table's relationship with the Auction table
        modelBuilder.Entity<AuctionReport>()
            .HasRequired(r => r.Auction)
            .WithMany()
            .HasForeignKey(r => r.AuctionID)
            .WillCascadeOnDelete(false);

        base.OnModelCreating(modelBuilder);
    }
}
